import { Component, Input, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { CatelogItem } from '@shared';

@Component({
  selector: 'app-card',
  imports: [MatCardModule],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss',
})
export class CardComponent {
  id: string = '';
  sku: string = '';
  name: string = '';
  description: string = '';
  price: number = 0;
  quantity: number = 0;
  tags: string[] = [];
  isDiscounted: boolean = false;
  discountPercentage: number = 0;
  imageUrl: string = '';
  @Input() item: CatelogItem = <CatelogItem>{};

  public displayInfo(): void {
    this.id = this.item.id;
    this.imageUrl = this.item.imageUrl;
    this.name = this.item.name;
    this.price = this.round(this.item.price,2);
    this.tags = this.item.tags;

    console.log(this.name);
  }

  public round(num: number, fractionDigits: number): number {
    return Number(num.toFixed(fractionDigits));
}

  public ngOnInit(): void {
    // console.log(this.item);

    this.displayInfo();
  }
}
