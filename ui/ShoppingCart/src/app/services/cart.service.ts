import { Injectable } from '@angular/core';
import { Product } from '../interfaces/product.interface';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
cart:boolean=false;
  private cartProducts:Product[]=[]
  private _products:BehaviorSubject<Product[]>=new BehaviorSubject<Product[]>([])

  constructor() { }
get products(){
  return this._products.asObservable();
}
  addNewProduct(product:Product){
this.cartProducts.push(product);
this._products.next(this.cartProducts)
  }
  open_closeCart(){
    this.cart= !this.cart
  }
  get qtyProducts(){
    return this.cartProducts.length
  }

  deleteProduct(index:number){
this.cartProducts.splice(index,1)
this._products.next(this.cartProducts)
  }
}
