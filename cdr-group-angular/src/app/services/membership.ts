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
      name: 'Majd A. Abbasi',
      role: 'General Manager',
      image: 'https://media.licdn.com/dms/image/v2/D4E03AQFxUvDvwMSrfQ/profile-displayphoto-shrink_800_800/profile-displayphoto-shrink_800_800/0/1730276966523?e=1772064000&v=beta&t=uMkXsx7Ojn5uZSqyiyqLB_qbSxMDt2wGpSGxnSH60ew',
      socialLinks: {}
    },
    {
      name: 'Zain Alrefaai',
      role: 'Administrative Assistant',
      image: 'https://media.licdn.com/dms/image/v2/D4D03AQEdeeCedhRqag/profile-displayphoto-crop_800_800/B4DZvxHp2mJoAI-/0/1769276875294?e=1772064000&v=beta&t=olSipT5PlC9GwCvbXAJ9BKBoMcamhTnMRnJIYoi32I0',
      socialLinks: {}
    }
  ];

  getTiers(): MembershipTier[] {
    return this.tiers;
  }

  getTeamMembers(): TeamMember[] {
    return this.teamMembers;
  }
}
