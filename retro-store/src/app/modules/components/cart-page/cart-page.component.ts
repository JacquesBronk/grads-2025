import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-cart-page',
  imports: [MatTableModule, MatIconModule, MatButtonModule],
  templateUrl: './cart-page.component.html',
  styleUrl: './cart-page.component.scss',
})
export class CartPageComponent {
  // Mock items in the cart
  cartItems = [
    { name: 'Laptop', price: 999 },
    { name: 'Headphones', price: 199 },
    { name: 'Keyboard', price: 49 },
    { name: 'Mouse', price: 29 },
  ];

  displayedColumns: string[] = ['item', 'price', 'action'];

  removeItem(item: any) {
    this.cartItems = this.cartItems.filter((cartItem) => cartItem !== item);
  }
  getTotal() {
    return this.cartItems.reduce((total, item) => total + item.price, 0);
  }
}
