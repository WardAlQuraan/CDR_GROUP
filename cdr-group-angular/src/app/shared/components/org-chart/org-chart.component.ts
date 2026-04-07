import {
  Component,
  OnInit,
  OnDestroy,
  ElementRef,
  ViewChild,
  AfterViewInit,
  ChangeDetectorRef,
  ViewEncapsulation,
  Input,
  OnChanges,
  SimpleChanges,
  HostListener,
  effect
} from '@angular/core';
import * as d3 from 'd3';
import { TranslationService } from '../../../services/translation.service';
import { EmployeesService } from '../../../services/employees.service';
import { EmployeeTreeNodeDto } from '../../../models/employee.model';

interface OrgNode {
  id: string;
  name: string;
  title: string;
  company: string;
  initials: string;
  filePath?: string;
  children?: OrgNode[];
  isVirtualRoot?: boolean;
}

@Component({
  selector: 'app-org-chart',
  standalone: false,
  templateUrl: './org-chart.component.html',
  styleUrl: './org-chart.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class OrgChartComponent implements OnInit, AfterViewInit, OnDestroy, OnChanges {
  @ViewChild('chartContainer', { static: false }) chartContainer!: ElementRef;
  @Input() companyId?: string;
  @Input() companyNameEn?: string;
  @Input() companyNameAr?: string;

  loading = false;
  error = false;
  hasData = false;

  // Pan state
  isDragging = false;
  canPanLeft = false;
  canPanRight = false;
  canPanUp = false;
  canPanDown = false;

  // viewBox origin (what SVG coordinate is at top-left of viewport)
  private vbX = 0;
  private vbY = 0;
  // viewport size in pixels
  private vpW = 0;
  private vpH = 0;
  // total content bounds
  private contentW = 0;
  private contentH = 0;
  // drag helpers
  private dragStartX = 0;
  private dragStartY = 0;
  private startVbX = 0;
  private startVbY = 0;
  private panStep = 300;
  private get isMobile(): boolean { return window.innerWidth < 768; }

  private data: OrgNode | null = null;
  private svg: d3.Selection<SVGSVGElement, unknown, null, undefined> | null = null;
  private resizeObserver: ResizeObserver | null = null;
  private currentLanguage: string;

  constructor(
    private translationService: TranslationService,
    private employeesService: EmployeesService,
    private cdr: ChangeDetectorRef
  ) {
    this.currentLanguage = this.translationService.language();

    effect(() => {
      const newLanguage = this.translationService.language();
      if (this.currentLanguage !== newLanguage) {
        this.currentLanguage = newLanguage;
        this.loadData();
      }
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyId'] && !changes['companyId'].firstChange) {
      this.loadData();
    }
  }

  ngAfterViewInit(): void {
    this.setupResizeObserver();
  }

  ngOnDestroy(): void {
    if (this.resizeObserver) {
      this.resizeObserver.disconnect();
    }
    d3.select('body').selectAll('.org-chart-body-tooltip').remove();
  }

  @HostListener('window:resize')
  onResize(): void {
    if (this.hasData) {
      this.renderChart();
    }
  }

  // Arrow navigation — shift viewBox
  panLeft(): void {
    this.vbX = Math.max(0, this.vbX - this.panStep);
    this.applyViewBox();
  }

  panRight(): void {
    this.vbX = Math.min(Math.max(0, this.contentW - this.vpW), this.vbX + this.panStep);
    this.applyViewBox();
  }

  panUp(): void {
    this.vbY = Math.max(0, this.vbY - this.panStep);
    this.applyViewBox();
  }

  panDown(): void {
    this.vbY = Math.min(Math.max(0, this.contentH - this.vpH), this.vbY + this.panStep);
    this.applyViewBox();
  }

  // Mouse drag (dragging right → viewBox moves left → shows content to the left)
  onMouseDown(event: MouseEvent): void {
    this.isDragging = true;
    this.dragStartX = event.clientX;
    this.dragStartY = event.clientY;
    this.startVbX = this.vbX;
    this.startVbY = this.vbY;
    event.preventDefault();
  }

  onMouseMove(event: MouseEvent): void {
    if (!this.isDragging) return;
    const dx = event.clientX - this.dragStartX;
    const dy = event.clientY - this.dragStartY;
    this.vbX = this.clampX(this.startVbX - dx);
    this.vbY = this.clampY(this.startVbY - dy);
    this.applyViewBox(true);
  }

  onMouseUp(): void {
    this.isDragging = false;
  }

  // Touch drag
  onTouchStart(event: TouchEvent): void {
    this.isDragging = true;
    this.dragStartX = event.touches[0].clientX;
    this.dragStartY = event.touches[0].clientY;
    this.startVbX = this.vbX;
    this.startVbY = this.vbY;
  }

  onTouchMove(event: TouchEvent): void {
    if (!this.isDragging) return;
    const dx = event.touches[0].clientX - this.dragStartX;
    const dy = event.touches[0].clientY - this.dragStartY;
    this.vbX = this.clampX(this.startVbX - dx);
    this.vbY = this.clampY(this.startVbY - dy);
    this.applyViewBox(true);
  }

  onTouchEnd(): void {
    this.isDragging = false;
  }

  private clampX(x: number): number {
    return Math.max(0, Math.min(x, Math.max(0, this.contentW - this.vpW)));
  }

  private clampY(y: number): number {
    return Math.max(0, Math.min(y, Math.max(0, this.contentH - this.vpH)));
  }

  private applyViewBox(instant = false): void {
    if (!this.svg) return;
    const svgEl = this.svg.node();
    if (!svgEl) return;

    if (instant) {
      svgEl.style.transition = 'none';
    } else {
      svgEl.style.transition = 'none'; // viewBox doesn't animate via CSS, we keep it instant
    }

    svgEl.setAttribute('viewBox', `${this.vbX} ${this.vbY} ${this.vpW} ${this.vpH}`);
    this.updatePanBounds();
  }

  private updatePanBounds(): void {
    this.canPanLeft = this.vbX > 0;
    this.canPanRight = this.vbX < this.contentW - this.vpW;
    this.canPanUp = this.vbY > 0;
    this.canPanDown = this.vbY < this.contentH - this.vpH;
    this.cdr.markForCheck();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get companyDisplayName(): string {
    return (this.isArabic ? this.companyNameAr : this.companyNameEn) || '';
  }

  loadData(): void {
    this.loading = true;
    this.error = false;
    this.employeesService.getTree(this.companyId).subscribe({
      next: (response) => {
        if (response.success && response.data && response.data.length > 0) {
          if (response.data.length === 1) {
            this.data = this.mapToOrgNode(response.data[0]);
          } else {
            this.data = {
              id: 'virtual-root',
              name: '',
              title: '',
              company: '',
              initials: '',
              isVirtualRoot: true,
              children: response.data.map(node => this.mapToOrgNode(node))
            };
          }
          this.hasData = true;
        } else {
          this.hasData = false;
        }
        this.loading = false;
        this.cdr.markForCheck();
        if (this.hasData) {
          setTimeout(() => this.renderChart(), 50);
        }
      },
      error: () => {
        this.loading = false;
        this.error = true;
        this.cdr.markForCheck();
      }
    });
  }

  private mapToOrgNode(node: EmployeeTreeNodeDto): OrgNode {
    const name = this.isArabic ? node.fullNameAr : node.fullNameEn;
    return {
      id: node.id,
      name: name || '',
      title: (this.isArabic ? node.positionNameAr : node.positionNameEn) || '',
      company: (this.isArabic ? node.companyNameAr : node.companyNameEn) || '',
      initials: this.getInitials(name),
      filePath: node.filePath,
      children: node.children?.map(child => this.mapToOrgNode(child))
    };
  }

  private getInitials(name: string): string {
    if (!name) return '?';
    return name
      .split(' ')
      .map(n => n[0])
      .join('')
      .substring(0, 2)
      .toUpperCase();
  }

  private setupResizeObserver(): void {
    if (this.chartContainer?.nativeElement) {
      this.resizeObserver = new ResizeObserver(() => {
        if (this.hasData) {
          this.renderChart();
        }
      });
      this.resizeObserver.observe(this.chartContainer.nativeElement);
    }
  }

  private renderChart(): void {
    if (!this.data || !this.chartContainer?.nativeElement) return;

    const container = this.chartContainer.nativeElement;
    this.vpW = container.clientWidth || 800;
    this.vpH = container.clientHeight || 600;

    // Clear existing chart
    d3.select(container).selectAll('*').remove();

    // Card dimensions — smaller on mobile
    const mobile = this.isMobile;
    const cardWidth = mobile ? 100 : 140;
    const cardHeight = mobile ? 95 : 130;
    const avatarRadius = mobile ? 20 : 30;
    const avatarCenterY = mobile ? 28 : 38;
    const horizontalSpacing = mobile ? 10 : 20;
    const verticalSpacing = mobile ? 25 : 40;
    this.panStep = mobile ? 150 : 300;

    // Create hierarchy & layout
    const root = d3.hierarchy(this.data);
    const treeLayout = d3.tree<OrgNode>()
      .nodeSize([cardWidth + horizontalSpacing, cardHeight + verticalSpacing]);
    const treeData = treeLayout(root);

    // Flip x-coordinates for RTL
    if (this.isArabic) {
      treeData.each(d => { d.x = -d.x; });
    }

    // Calculate actual bounds
    let minX = Infinity, maxX = -Infinity, maxY = 0;
    treeData.each(d => {
      if (d.x < minX) minX = d.x;
      if (d.x > maxX) maxX = d.x;
      if (d.y > maxY) maxY = d.y;
    });

    // Content dimensions with padding
    const padding = 50;
    this.contentW = maxX - minX + cardWidth + padding * 2;
    this.contentH = maxY + cardHeight + padding * 2;

    // Group offset: position nodes so minX node starts at (padding + cardWidth/2)
    const offsetX = padding - minX + cardWidth / 2;
    const offsetY = padding;

    // Find the first visible node (skip virtual root)
    const firstVisible = treeData.data.isVirtualRoot && treeData.children?.length
      ? treeData.children[0]
      : treeData;
    const focusX = offsetX + firstVisible.x;
    const focusY = offsetY + firstVisible.y;

    // Initial viewBox: center on the first visible node
    if (this.contentW <= this.vpW) {
      this.vbX = 0;
    } else {
      this.vbX = this.clampX(focusX - this.vpW / 2);
    }
    this.vbY = this.clampY(focusY - this.vpH / 3);

    // Create SVG filling the viewport, viewBox controls visible area
    this.svg = d3.select(container)
      .append('svg')
      .attr('width', '100%')
      .attr('height', '100%')
      .attr('viewBox', `${this.vbX} ${this.vbY} ${this.vpW} ${this.vpH}`)
      .attr('class', 'org-chart-svg')
      .style('display', 'block');

    // Defs
    const defs = this.svg.append('defs');

    const avatarGradient = defs.append('linearGradient')
      .attr('id', 'avatarGradient')
      .attr('x1', '0%').attr('y1', '0%')
      .attr('x2', '100%').attr('y2', '100%');
    avatarGradient.append('stop').attr('offset', '0%').attr('stop-color', '#D9A93E');
    avatarGradient.append('stop').attr('offset', '100%').attr('stop-color', '#C4962E');

    const linkGradient = defs.append('linearGradient')
      .attr('id', 'linkGradient')
      .attr('x1', '0%').attr('y1', '0%')
      .attr('x2', '0%').attr('y2', '100%');
    linkGradient.append('stop').attr('offset', '0%').attr('stop-color', '#D9A93E');
    linkGradient.append('stop').attr('offset', '100%').attr('stop-color', '#d1d5db');

    const filter = defs.append('filter')
      .attr('id', 'cardShadow')
      .attr('x', '-20%').attr('y', '-20%')
      .attr('width', '140%').attr('height', '140%');
    filter.append('feDropShadow')
      .attr('dx', '0').attr('dy', '4')
      .attr('stdDeviation', '8')
      .attr('flood-color', 'rgba(0, 0, 0, 0.1)');

    defs.append('clipPath')
      .attr('id', 'avatarClip')
      .append('circle')
      .attr('cx', 0).attr('cy', 0).attr('r', avatarRadius);

    // Main group — positions all nodes within the content coordinate space
    const g = this.svg.append('g')
      .attr('transform', `translate(${offsetX}, ${offsetY})`);

    // Links
    g.selectAll('.link')
      .data(treeData.links().filter(d => !d.source.data.isVirtualRoot))
      .enter()
      .append('path')
      .attr('class', 'org-chart-link')
      .attr('d', d => {
        const sourceX = d.source.x;
        const sourceY = d.source.y + cardHeight;
        const targetX = d.target.x;
        const targetY = d.target.y;
        const midY = (sourceY + targetY) / 2;
        return `M ${sourceX} ${sourceY} C ${sourceX} ${midY}, ${targetX} ${midY}, ${targetX} ${targetY}`;
      })
      .style('fill', 'none')
      .style('stroke', '#D9A93E')
      .style('stroke-width', '2.5px')
      .style('stroke-linecap', 'round');

    // Tooltip
    d3.select('body').selectAll('.org-chart-body-tooltip').remove();
    const tooltip = d3.select('body')
      .append('div')
      .attr('class', 'org-chart-body-tooltip')
      .style('position', 'fixed')
      .style('visibility', 'hidden')
      .style('background', 'rgba(30, 41, 59, 0.95)')
      .style('color', 'white')
      .style('padding', '10px 14px')
      .style('border-radius', '6px')
      .style('font-size', '16px')
      .style('box-shadow', '0 4px 12px rgba(0, 0, 0, 0.3)')
      .style('pointer-events', 'none')
      .style('z-index', '10000')
      .style('max-width', '250px')
      .style('line-height', '1.4');

    // Nodes
    const self = this;
    const nodes = g.selectAll('.node')
      .data(treeData.descendants().filter(d => !d.data.isVirtualRoot))
      .enter()
      .append('g')
      .attr('class', 'org-chart-node')
      .attr('transform', d => `translate(${d.x - cardWidth / 2}, ${d.y})`)
      .style('cursor', 'pointer')
      .on('mouseenter', (_event, d) => {
        const titleLabel = self.isArabic ? '\u0627\u0644\u0645\u0633\u0645\u0649 \u0627\u0644\u0648\u0638\u064A\u0641\u064A' : 'Position';
        const companyLabel = self.isArabic ? '\u0627\u0644\u0634\u0631\u0643\u0629' : 'Company';
        let html = '';
        if (d.data.filePath) {
          html += `<div style="text-align: center; margin-bottom: 8px;">
            <img src="${d.data.filePath}" alt="${d.data.name}" style="width: 60px; height: 60px; border-radius: 50%; object-fit: cover; border: 2px solid #D9A93E;">
          </div>`;
        }
        html += `<div style="font-weight: 600; color: #D9A93E; margin-bottom: 4px; text-align: center;">${d.data.name}</div>`;
        if (d.data.title) html += `<div><span style="color: #94a3b8;">${titleLabel}:</span> ${d.data.title}</div>`;
        if (d.data.company) html += `<div><span style="color: #94a3b8;">${companyLabel}:</span> ${d.data.company}</div>`;
        tooltip
          .html(html)
          .style('direction', self.isArabic ? 'rtl' : 'ltr')
          .style('text-align', self.isArabic ? 'right' : 'left')
          .style('visibility', 'visible');
      })
      .on('mousemove', (event) => {
        tooltip
          .style('left', (event.clientX + 10) + 'px')
          .style('top', (event.clientY + 10) + 'px');
      })
      .on('mouseleave', () => {
        tooltip.style('visibility', 'hidden');
      });

    // Card backgrounds
    nodes.append('rect')
      .attr('class', 'node-card-bg')
      .attr('width', cardWidth).attr('height', cardHeight)
      .attr('rx', 12).attr('ry', 12)
      .attr('filter', 'url(#cardShadow)')
      .style('fill', 'rgba(255, 255, 255, 0.98)');

    nodes.append('rect')
      .attr('class', 'node-card-border')
      .attr('width', cardWidth).attr('height', cardHeight)
      .attr('rx', 12).attr('ry', 12)
      .style('fill', 'none')
      .style('stroke', '#e2e8f0')
      .style('stroke-width', '1.5px');

    const centerX = cardWidth / 2;

    // Avatar ring
    nodes.append('circle')
      .attr('class', 'node-avatar-ring')
      .attr('cx', centerX).attr('cy', avatarCenterY)
      .attr('r', avatarRadius + 3)
      .style('fill', 'none')
      .style('stroke', 'url(#avatarGradient)')
      .style('stroke-width', '2.5px');

    // Avatar circle (no image)
    nodes.filter(d => !d.data.filePath)
      .append('circle')
      .attr('class', 'node-avatar-circle')
      .attr('cx', centerX).attr('cy', avatarCenterY)
      .attr('r', avatarRadius)
      .style('fill', 'url(#avatarGradient)');

    // Avatar initials (no image)
    nodes.filter(d => !d.data.filePath)
      .append('text')
      .attr('class', 'node-avatar-text')
      .attr('x', centerX).attr('y', avatarCenterY)
      .attr('text-anchor', 'middle')
      .attr('dominant-baseline', 'central')
      .style('fill', 'white')
      .style('font-size', mobile ? '10px' : '13px')
      .style('font-weight', '700')
      .style('letter-spacing', '1px')
      .text(d => d.data.initials);

    // Avatar image bg
    nodes.filter(d => !!d.data.filePath)
      .append('circle')
      .attr('cx', centerX).attr('cy', avatarCenterY)
      .attr('r', avatarRadius)
      .style('fill', '#e2e8f0');

    // Avatar image
    nodes.filter(d => !!d.data.filePath)
      .append('image')
      .attr('class', 'node-avatar-image')
      .attr('x', centerX - avatarRadius)
      .attr('y', avatarCenterY - avatarRadius)
      .attr('width', avatarRadius * 2)
      .attr('height', avatarRadius * 2)
      .attr('clip-path', `circle(${avatarRadius}px at ${avatarRadius}px ${avatarRadius}px)`)
      .attr('preserveAspectRatio', 'xMidYMid slice')
      .attr('href', d => d.data.filePath || '');

    // Name — use foreignObject for reliable Arabic text on mobile
    if (this.isArabic) {
      nodes.append('foreignObject')
        .attr('x', 0).attr('y', avatarCenterY + avatarRadius + (mobile ? 4 : 6))
        .attr('width', cardWidth).attr('height', mobile ? 14 : 18)
        .append('xhtml:div')
        .style('width', '100%')
        .style('text-align', 'center')
        .style('direction', 'rtl')
        .style('font-size', mobile ? '8px' : '11px')
        .style('font-weight', '700')
        .style('color', '#1e293b')
        .style('line-height', '1')
        .style('overflow', 'hidden')
        .style('text-overflow', 'ellipsis')
        .style('white-space', 'nowrap')
        .text(d => this.truncateText(d.data.name, mobile ? 12 : 16));

      nodes.append('foreignObject')
        .attr('x', 0).attr('y', avatarCenterY + avatarRadius + (mobile ? 16 : 22))
        .attr('width', cardWidth).attr('height', mobile ? 12 : 14)
        .append('xhtml:div')
        .style('width', '100%')
        .style('text-align', 'center')
        .style('direction', 'rtl')
        .style('font-size', mobile ? '7px' : '9px')
        .style('font-weight', '600')
        .style('color', '#D9A93E')
        .style('line-height', '1')
        .style('overflow', 'hidden')
        .style('text-overflow', 'ellipsis')
        .style('white-space', 'nowrap')
        .text(d => this.truncateText(d.data.title, mobile ? 14 : 20));

      nodes.append('foreignObject')
        .attr('x', 0).attr('y', avatarCenterY + avatarRadius + (mobile ? 26 : 34))
        .attr('width', cardWidth).attr('height', mobile ? 10 : 12)
        .append('xhtml:div')
        .style('width', '100%')
        .style('text-align', 'center')
        .style('direction', 'rtl')
        .style('font-size', mobile ? '6px' : '8px')
        .style('font-weight', '500')
        .style('color', '#64748b')
        .style('line-height', '1')
        .style('overflow', 'hidden')
        .style('text-overflow', 'ellipsis')
        .style('white-space', 'nowrap')
        .text(d => this.truncateText(d.data.company, mobile ? 18 : 28));
    } else {
      // Name
      nodes.append('text')
        .attr('class', 'node-name-text')
        .attr('x', centerX)
        .attr('y', avatarCenterY + avatarRadius + (mobile ? 12 : 16))
        .attr('text-anchor', 'middle')
        .style('fill', '#1e293b')
        .style('font-size', mobile ? '8px' : '11px')
        .style('font-weight', '700')
        .text(d => this.truncateText(d.data.name, mobile ? 12 : 16));

      // Title
      nodes.append('text')
        .attr('class', 'node-title-text')
        .attr('x', centerX)
        .attr('y', avatarCenterY + avatarRadius + (mobile ? 22 : 30))
        .attr('text-anchor', 'middle')
        .style('fill', '#D9A93E')
        .style('font-size', mobile ? '7px' : '9px')
        .style('font-weight', '600')
        .text(d => this.truncateText(d.data.title, mobile ? 14 : 20));

      // Company
      nodes.append('text')
        .attr('class', 'node-company-text')
        .attr('x', centerX)
        .attr('y', avatarCenterY + avatarRadius + (mobile ? 31 : 42))
        .attr('text-anchor', 'middle')
        .style('fill', '#64748b')
        .style('font-size', mobile ? '6px' : '8px')
        .style('font-weight', '500')
        .text(d => this.truncateText(d.data.company, mobile ? 18 : 28));
    }


    // Bottom connector
    nodes.filter(d => !!(d.children && d.children.length > 0))
      .append('circle')
      .attr('class', 'node-connector')
      .attr('cx', centerX).attr('cy', cardHeight)
      .attr('r', 4)
      .style('fill', '#D9A93E');

    // Top connector
    nodes.filter(d => d.parent !== null && !d.parent.data.isVirtualRoot)
      .append('circle')
      .attr('class', 'node-connector-top')
      .attr('cx', centerX).attr('cy', 0)
      .attr('r', 3)
      .style('fill', '#e2e8f0');

    this.updatePanBounds();
  }

  private truncateText(text: string, maxLength: number): string {
    if (!text) return '';
    return text.length > maxLength ? text.substring(0, maxLength) + '...' : text;
  }
}
