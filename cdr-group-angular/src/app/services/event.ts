import { Injectable } from '@angular/core';

export interface Event {
  id: number;
  title: string;
  description: string;
  date: string;
  month: string;
  location: string;
  ticketPrice: number;
  image: string;
  featured?: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class EventService {
  private events: Event[] = [
    {
      id: 1,
      title: "CDR Group Tournaments",
      description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis finibus mi id elit gravida, quis tincidunt purus fringilla.",
      date: "18",
      month: "Mar",
      location: "CDR Group",
      ticketPrice: 150,
      image: "assets/images/professional-golf-player.jpg",
      featured: true
    },
    {
      id: 2,
      title: "Weekend Golf Games",
      description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis finibus mi id elit gravida, quis tincidunt purus fringilla.",
      date: "24",
      month: "Mar",
      location: "CDR Group",
      ticketPrice: 200,
      image: "assets/images/girl-taking-selfie-with-friends-golf-field.jpg",
      featured: true
    },
    {
      id: 3,
      title: "Spring Championship",
      description: "Join us for our annual spring championship event. Experience world-class golf in a beautiful setting.",
      date: "05",
      month: "Apr",
      location: "CDR Group",
      ticketPrice: 180,
      image: "assets/images/anna-rosar-ew-olGvgCCs-unsplash.jpg"
    },
    {
      id: 4,
      title: "Members Only Tournament",
      description: "Exclusive tournament for our valued members. Compete for exciting prizes and recognition.",
      date: "12",
      month: "Apr",
      location: "CDR Group",
      ticketPrice: 100,
      image: "assets/images/professional-golf-player.jpg"
    }
  ];

  getEvents(): Event[] {
    return this.events;
  }

  getFeaturedEvents(): Event[] {
    return this.events.filter(e => e.featured);
  }

  getEventById(id: number): Event | undefined {
    return this.events.find(e => e.id === id);
  }
}
