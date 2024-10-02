import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../interfaces/product.interface';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(
    private http:HttpClient
  ) { }

  getAll(){
    return this.http.get<Product[]>('https://localhost:7070/api/products')
  }
}
