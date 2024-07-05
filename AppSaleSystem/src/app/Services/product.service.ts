import { Injectable } from '@angular/core';

import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from 'src/environments/environment';
import {ResponseApi} from '../Interfaces/response-api';

import {Product} from '../Interfaces/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private urlApi:string=environment.endpoint+"Product/";

  constructor(private http:HttpClient) {}

  list():Observable<ResponseApi>{
    return this.http.get<ResponseApi>(`${this.urlApi}List`)
  }

  create(request: Product):Observable<ResponseApi>{
    return this.http.post<ResponseApi>(`${this.urlApi}Create`,request); 
  }
  
  update(request: Product):Observable<ResponseApi>{
    return this.http.put<ResponseApi>(`${this.urlApi}Update`,request); 
  }

  delete(id: number):Observable<ResponseApi>{
    return this.http.delete<ResponseApi>(`${this.urlApi}Delete/${id}`); 
  }

}
