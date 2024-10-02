import { Component } from '@angular/core';
import { map } from 'rxjs';
import { CartService } from 'src/app/services/cart.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-total',
  templateUrl: './total.component.html',
  styleUrls: ['./total.component.css'],
})
export class TotalComponent {
  total: number = 0;
  products: any[] = [];
  constructor(private cartService: CartService, private http: HttpClient) {
    this.cartService.products
      .pipe(
        map((products) => {
          this.products = products;
          return products.reduce((prev, curr) => prev + curr.amount, 0);
        }) 
      )
      .subscribe((data) => {
        this.total = data;
      });
  }

  onSubmitOrder() {
    const order = {
      total: this.total, 
      products: this.products,
    };

    console.log(order);

    this.http.post('https://localhost:44348/api/orders', order)
      .subscribe({
        next: (response) => {
          console.log('Order submitted successfully', response);
          alert('Order submitted successfully');
        },
        error: (error) => {
          console.error('Error submitting order', error);
          alert('There was an error submitting your order');
        }
      });
  }
}
