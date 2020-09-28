import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {

  constructor(
    private fb: FormBuilder,
    private http: HttpClient) { }

  readonly BaseURI = 'http://localhost:58701/api';

  search(formData: any, pageIndex: number, pageSize: number) {
    return this.http.get(this.BaseURI + `/Paycor/products?name=${formData.Name}&sellStartDate=${formData.SellStartDate}&description=${formData.Description}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

  searchOrders(formData: any, pageIndex: number, pageSize: number) {
    return this.http.get(this.BaseURI + `/Paycor/orders?dateFrom=${formData.DateFrom}&dateTo=${formData.DateTo}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }
}
