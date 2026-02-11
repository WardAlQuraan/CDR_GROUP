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
  department: string;
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
  @Input() departmentId?: string;
  @Input() companyCode?: string;

  loading = false;
  error = false;
  hasData = false;

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

    // Watch for language changes and reload data
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
    if ((changes['departmentId'] && !changes['departmentId'].firstChange) ||
        (changes['companyCode'] && !changes['companyCode'].firstChange)) {
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
    // Clean up tooltip from body
    d3.select('body').selectAll('.org-chart-body-tooltip').remove();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  loadData(): void {
    this.loading = true;
    this.error = false;
    this.employeesService.getTree(this.departmentId, this.companyCode).subscribe({
      next: (response) => {
        if (response.success && response.data && response.data.length > 0) {
          // Handle multiple root nodes by creating a virtual root
          if (response.data.length === 1) {
            this.data = this.mapToOrgNode(response.data[0]);
          } else {
            // Create virtual root to contain all root nodes
            this.data = {
              id: 'virtual-root',
              name: '',
              title: '',
              department: '',
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
          setTimeout(() => this.renderChart(), 0);
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
      department: (this.isArabic ? node.departmentNameAr : node.departmentNameEn) || '',
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
    const containerPadding = 60; // 30px padding on each side
    const containerWidth = (container.clientWidth || 800) - containerPadding;
    const containerHeight = container.clientHeight || 600;

    // Clear existing chart
    d3.select(container).selectAll('*').remove();

    // Card dimensions - vertical centered layout
    const cardWidth = 200;
    const cardHeight = 160;
    const avatarRadius = 32;
    const avatarCenterY = 40;
    const horizontalSpacing = 40;
    const verticalSpacing = 60;

    // Create hierarchy
    const root = d3.hierarchy(this.data);

    // Calculate tree height based on nodes
    const treeHeight = Math.max(containerHeight - 100, ((root.height + 1) * (cardHeight + verticalSpacing)));

    // Create tree layout
    const treeLayout = d3.tree<OrgNode>()
      .nodeSize([cardWidth + horizontalSpacing, cardHeight + verticalSpacing]);

    // Apply layout
    const treeData = treeLayout(root);

    // Calculate bounds
    let minX = Infinity, maxX = -Infinity;
    treeData.each(d => {
      if (d.x < minX) minX = d.x;
      if (d.x > maxX) maxX = d.x;
    });

    const treeWidth = maxX - minX + cardWidth + 100;
    const svgHeight = treeHeight + 120;

    // Calculate scale to fit within container width
    const scale = treeWidth > containerWidth ? containerWidth / treeWidth : 1;
    const scaledHeight = svgHeight * scale;

    // Create SVG with viewBox for proper scaling
    this.svg = d3.select(container)
      .append('svg')
      .attr('width', '100%')
      .attr('height', scaledHeight)
      .attr('viewBox', `0 0 ${treeWidth} ${svgHeight}`)
      .attr('preserveAspectRatio', 'xMidYMin meet')
      .attr('class', 'org-chart-svg');

    // Add SVG definitions for gradients
    const defs = this.svg.append('defs');

    // Avatar gradient
    const avatarGradient = defs.append('linearGradient')
      .attr('id', 'avatarGradient')
      .attr('x1', '0%')
      .attr('y1', '0%')
      .attr('x2', '100%')
      .attr('y2', '100%');

    avatarGradient.append('stop')
      .attr('offset', '0%')
      .attr('stop-color', '#D9A93E');

    avatarGradient.append('stop')
      .attr('offset', '100%')
      .attr('stop-color', '#C4962E');

    // Link gradient
    const linkGradient = defs.append('linearGradient')
      .attr('id', 'linkGradient')
      .attr('x1', '0%')
      .attr('y1', '0%')
      .attr('x2', '0%')
      .attr('y2', '100%');

    linkGradient.append('stop')
      .attr('offset', '0%')
      .attr('stop-color', '#D9A93E');

    linkGradient.append('stop')
      .attr('offset', '100%')
      .attr('stop-color', '#d1d5db');

    // Card shadow filter
    const filter = defs.append('filter')
      .attr('id', 'cardShadow')
      .attr('x', '-20%')
      .attr('y', '-20%')
      .attr('width', '140%')
      .attr('height', '140%');

    filter.append('feDropShadow')
      .attr('dx', '0')
      .attr('dy', '4')
      .attr('stdDeviation', '8')
      .attr('flood-color', 'rgba(0, 0, 0, 0.1)');

    // Clip path for circular avatar images
    defs.append('clipPath')
      .attr('id', 'avatarClip')
      .append('circle')
      .attr('cx', 0)
      .attr('cy', 0)
      .attr('r', avatarRadius);

    // Create main group with translation (centered in viewBox)
    const g = this.svg.append('g')
      .attr('transform', `translate(${treeWidth / 2}, 60)`)
      .append('g')
      .attr('transform', `translate(${-(maxX + minX) / 2}, 0)`);

    // Draw curved links (filter out links from virtual root)
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

        // Bezier curve for smooth connection
        const midY = (sourceY + targetY) / 2;

        return `M ${sourceX} ${sourceY}
                C ${sourceX} ${midY},
                  ${targetX} ${midY},
                  ${targetX} ${targetY}`;
      })
      .style('fill', 'none')
      .style('stroke', '#D9A93E')
      .style('stroke-width', '2.5px')
      .style('stroke-linecap', 'round');

    // Create tooltip appended to body for proper positioning
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
      .style('font-size', '13px')
      .style('box-shadow', '0 4px 12px rgba(0, 0, 0, 0.3)')
      .style('pointer-events', 'none')
      .style('z-index', '10000')
      .style('max-width', '250px')
      .style('line-height', '1.4');

    // Draw nodes (filter out virtual root)
    const self = this;
    const nodes = g.selectAll('.node')
      .data(treeData.descendants().filter(d => !d.data.isVirtualRoot))
      .enter()
      .append('g')
      .attr('class', 'org-chart-node')
      .attr('transform', d => `translate(${d.x - cardWidth / 2}, ${d.y})`)
      .style('cursor', 'pointer')
      .on('mouseenter', (_event, d) => {
        const titleLabel = self.isArabic ? 'المسمى الوظيفي' : 'Position';
        const deptLabel = self.isArabic ? 'القسم' : 'Department';

        let html = '';
        if (d.data.filePath) {
          html += `<div style="text-align: center; margin-bottom: 8px;">
            <img src="${d.data.filePath}" alt="${d.data.name}" style="width: 60px; height: 60px; border-radius: 50%; object-fit: cover; border: 2px solid #D9A93E;">
          </div>`;
        }
        html += `<div style="font-weight: 600; color: #D9A93E; margin-bottom: 4px; text-align: center;">${d.data.name}</div>`;
        if (d.data.title) html += `<div><span style="color: #94a3b8;">${titleLabel}:</span> ${d.data.title}</div>`;
        if (d.data.department) html += `<div><span style="color: #94a3b8;">${deptLabel}:</span> ${d.data.department}</div>`;

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

    // Node card background with shadow
    nodes.append('rect')
      .attr('class', 'node-card-bg')
      .attr('width', cardWidth)
      .attr('height', cardHeight)
      .attr('rx', 12)
      .attr('ry', 12)
      .attr('filter', 'url(#cardShadow)')
      .style('fill', 'rgba(255, 255, 255, 0.98)');

    // Node card border
    nodes.append('rect')
      .attr('class', 'node-card-border')
      .attr('width', cardWidth)
      .attr('height', cardHeight)
      .attr('rx', 12)
      .attr('ry', 12)
      .style('fill', 'none')
      .style('stroke', '#e2e8f0')
      .style('stroke-width', '1.5px');

    // Centered vertical layout - works for both RTL and LTR
    const centerX = cardWidth / 2;

    // Avatar border ring - for all nodes
    nodes.append('circle')
      .attr('class', 'node-avatar-ring')
      .attr('cx', centerX)
      .attr('cy', avatarCenterY)
      .attr('r', avatarRadius + 3)
      .style('fill', 'none')
      .style('stroke', 'url(#avatarGradient)')
      .style('stroke-width', '2.5px');

    // Avatar circle with gradient - for nodes WITHOUT image
    nodes.filter(d => !d.data.filePath)
      .append('circle')
      .attr('class', 'node-avatar-circle')
      .attr('cx', centerX)
      .attr('cy', avatarCenterY)
      .attr('r', avatarRadius)
      .style('fill', 'url(#avatarGradient)');

    // Avatar initials - for nodes WITHOUT image
    nodes.filter(d => !d.data.filePath)
      .append('text')
      .attr('class', 'node-avatar-text')
      .attr('x', centerX)
      .attr('y', avatarCenterY)
      .attr('text-anchor', 'middle')
      .attr('dominant-baseline', 'central')
      .style('fill', 'white')
      .style('font-size', '15px')
      .style('font-weight', '700')
      .style('letter-spacing', '0.5px')
      .text(d => d.data.initials);

    // Avatar image background - for nodes WITH image
    nodes.filter(d => !!d.data.filePath)
      .append('circle')
      .attr('cx', centerX)
      .attr('cy', avatarCenterY)
      .attr('r', avatarRadius)
      .style('fill', '#e2e8f0');

    // Avatar image - for nodes WITH image
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

    // Name - centered below avatar
    nodes.append('text')
      .attr('class', 'node-name-text')
      .attr('x', centerX)
      .attr('y', avatarCenterY + avatarRadius + 22)
      .attr('text-anchor', 'middle')
      .style('fill', '#1e293b')
      .style('font-size', '13px')
      .style('font-weight', '700')
      .text(d => this.truncateText(d.data.name, 18));

    // Title - centered below name
    nodes.append('text')
      .attr('class', 'node-title-text')
      .attr('x', centerX)
      .attr('y', avatarCenterY + avatarRadius + 40)
      .attr('text-anchor', 'middle')
      .style('fill', '#D9A93E')
      .style('font-size', '11px')
      .style('font-weight', '600')
      .text(d => this.truncateText(d.data.title, 22));

    // Department - centered at bottom
    nodes.append('text')
      .attr('class', 'node-department-text')
      .attr('x', centerX)
      .attr('y', avatarCenterY + avatarRadius + 56)
      .attr('text-anchor', 'middle')
      .style('fill', '#64748b')
      .style('font-size', '10px')
      .style('font-weight', '500')
      .text(d => this.truncateText(d.data.department, 24));

    // Bottom connector dot for nodes with children
    nodes.filter(d => !!(d.children && d.children.length > 0))
      .append('circle')
      .attr('class', 'node-connector')
      .attr('cx', centerX)
      .attr('cy', cardHeight)
      .attr('r', 4)
      .style('fill', '#D9A93E');

    // Top connector dot for non-root nodes (exclude children of virtual root)
    nodes.filter(d => d.parent !== null && !d.parent.data.isVirtualRoot)
      .append('circle')
      .attr('class', 'node-connector-top')
      .attr('cx', centerX)
      .attr('cy', 0)
      .attr('r', 3)
      .style('fill', '#e2e8f0');
  }

  private truncateText(text: string, maxLength: number): string {
    if (!text) return '';
    return text.length > maxLength ? text.substring(0, maxLength) + '...' : text;
  }
}
