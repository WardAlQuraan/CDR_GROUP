import { Injectable } from '@angular/core';

export interface MembershipTier {
  name: string;
  price: number;
  features: {
    golfInsurance: boolean;
    clubFacilities: boolean;
    countryClub: boolean;
    weekendSeasonal: boolean;
    premiumCourses: boolean;
    prosNetworking: boolean;
  };
}

export interface TeamMember {
  name: string;
  role: string;
  image: string;
  socialLinks: {
    instagram?: string;
    twitter?: string;
    whatsapp?: string;
  };
}

@Injectable({
  providedIn: 'root',
})
export class MembershipService {
  private tiers: MembershipTier[] = [
    {
      name: 'T1',
      price: 420,
      features: {
        golfInsurance: true,
        clubFacilities: true,
        countryClub: false,
        weekendSeasonal: false,
        premiumCourses: false,
        prosNetworking: false
      }
    },
    {
      name: 'T2',
      price: 640,
      features: {
        golfInsurance: true,
        clubFacilities: true,
        countryClub: true,
        weekendSeasonal: true,
        premiumCourses: false,
        prosNetworking: false
      }
    },
    {
      name: 'T3',
      price: 860,
      features: {
        golfInsurance: true,
        clubFacilities: true,
        countryClub: true,
        weekendSeasonal: true,
        premiumCourses: true,
        prosNetworking: true
      }
    }
  ];

  private teamMembers: TeamMember[] = [
    {
      name: 'Michael',
      role: 'Founder',
      image: 'assets/images/members/portrait-young-handsome-businessman-wearing-white-shirt-standing-with-crossed-arms-near-window-modern-office.jpg',
      socialLinks: {
        instagram: '#',
        twitter: '#',
        whatsapp: '#'
      }
    },
    {
      name: 'Sandy',
      role: 'Co-Founder',
      image: 'assets/images/members/successful-asian-lady-boss-businesswoman-suit-walking-with-laptop-cross-street-crosswalk.jpg',
      socialLinks: {
        instagram: '#',
        twitter: '#',
        whatsapp: '#'
      }
    }
  ];

  getTiers(): MembershipTier[] {
    return this.tiers;
  }

  getTeamMembers(): TeamMember[] {
    return this.teamMembers;
  }
}
