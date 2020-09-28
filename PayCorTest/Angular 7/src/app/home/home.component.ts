import { HttpClientService } from './../shared/httpclient.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  formModel = {
    Name: '',
    SellStartDate: '',
    Description: ''
  }

  formModelOrder = {
    DateFrom: '',
    DateTo: ''
  }

  currentPage = 1;
  currentPageOrder = 1;

  totalPages = 1;
  totalOrderPages = 1;

  pageSize = 8;

  displayDialog: boolean;
  showDeleteButton: boolean = true;

  errorMsg: string = null;
  textCalc: string = null;

  product: any = {};
  selectedProduct: any;
  newProduct: boolean;
  allProducts: any[];
  products: any[];
  cols: any[];
  cols2: any[];

  productRecords: any[];
  orderDetails: any[];

  constructor(
    private service: HttpClientService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.cols = [
      { field: 'name', header: 'Name' },
      { field: 'productModel', header: 'Product Model' },
      { field: 'cultureID', header: 'Culture ID' },
      { field: 'description', header: 'Description' },
      { field: 'sellStartDate', header: 'Sell Start Date' },
    ];

    this.cols2 = [
      { field: 'day', header: 'Day' },
      { field: 'lineTotal', header: 'Sum Of Traffic' },
      { field: 'orderQty', header: 'Product Units Sold' },
      { field: 'totalSum', header: 'Total Sum' }
    ];
  }

  showDialogToAdd() {
    this.newProduct = true;
    this.product = {};
    this.displayDialog = true;
    this.showDeleteButton = false;
  }

  onRowSelect(event) {
    this.newProduct = false;
    this.product = this.cloneProduct(event.data);
    this.displayDialog = true;
    this.showDeleteButton = true;
  }

  cloneProduct(productDto: any): any {
    let product = {};
    for (let prop in productDto) {
      product[prop] = productDto[prop];
    }
    return product;
  }

  isNumber(value: string | number): boolean {
    return ((value != null) &&
      (value !== '') &&
      !isNaN(Number(value.toString())));
  }

  onSubmit(form: NgForm, nextOrPrevious: number = 0) {
    let searchParams = form.value;
    if(searchParams.SellStartDate){
      searchParams.SellStartDate = new Date(searchParams.SellStartDate).toLocaleString();
    }
    else{
      searchParams.SellStartDate = '';
    }

    let pageIndex = nextOrPrevious == 0 ? 1 : nextOrPrevious == 1 ? this.currentPage + 1 : nextOrPrevious == 2 ? this.currentPage - 1 : 1;

    this.service.search(searchParams, pageIndex, this.pageSize).subscribe(
      (res: any) => {
        this.productRecords = res.products;
        this.currentPage = pageIndex;
        this.totalPages = res.totalPages;
      },
      error => {
        error.error.forEach(error => {
          error.error.forEach(error => {
            this.toastr.error(error.Value, 'ERROR!');
          });
        });
      }
    );
  }

  onSubmitOrder(form: NgForm, nextOrPrevious: number = 0) {
    let searchParams = form.value;
    searchParams.DateFrom = new Date(searchParams.DateFrom).toLocaleString();
    searchParams.DateTo = new Date(searchParams.DateTo).toLocaleString();

    let pageIndex = nextOrPrevious == 0 ? 1 : nextOrPrevious == 1 ? this.currentPageOrder + 1 : nextOrPrevious == 2 ? this.currentPageOrder - 1 : 1;

    this.service.searchOrders(searchParams, pageIndex, this.pageSize).subscribe(
      (res: any) => {
        this.orderDetails = res.orders;
        this.currentPageOrder = pageIndex;
        this.totalOrderPages = res.totalPages;
      },
      error => {
        error.error.forEach(error => {
          error.error.forEach(error => {
            this.toastr.error(error.Value, 'ERROR!');
          });
        });
      }
    );
  }

  nextPage(form: NgForm){
    if(this.totalPages > this.currentPage){
      this.onSubmit(form, 1);
    }
  }

  prevPage(form: NgForm){
    if(this.currentPage > 1){
      this.onSubmit(form, 2);
    }
  }

  nextPageOrder(form: NgForm){
    if(this.totalOrderPages > this.currentPageOrder){
      this.onSubmitOrder(form, 1);
    }
  }

  prevPageOrder(form: NgForm){
    if(this.currentPageOrder > 1){
      this.onSubmit(form, 2);
    }
  }
}
