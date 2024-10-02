import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/interfaces/product.interface';
import { CartService } from 'src/app/services/cart.service';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {

  constructor(private productsService:ProductsService, private cartService:CartService){

  }
  ngOnInit(): void {
    this.productsService.getAll().subscribe(data=>{
      this.products=data
      console.log(this.products)
    })
  }

  products:Product[]=[]

  addProduct(item: Product){
    this.cartService.addNewProduct(item)

  }

}
