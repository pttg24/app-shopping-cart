import { Component } from '@angular/core';
import { Product } from 'src/app/interfaces/product.interface';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
products:Product[]=[]
  constructor(private cartService:CartService){
this.cartService.products.subscribe(data=>{
this.products=data
})
  }

  onClickDelete(indice: any){
    this.cartService.deleteProduct(indice)

  }

}
