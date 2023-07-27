import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import BudgetItem from './BudgetItem';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  public data: BudgetItem[] = [
    { id: 10000, categoryName: 'Test Item', amount: 200.0 },
  ];

  httpHeaders = new HttpHeaders()
    .set('Access-Control-Allow-Origin', '*')
    .set('Content-Type', 'application/json');

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    let url = `${environment.baseUrl}/api/BudgetItems`;

    this.http
      .get<BudgetItem[]>(url, { headers: this.httpHeaders })
      .subscribe((result) => {
        this.data = [...this.data, ...result];
      });
  }

  addData() {
    let url = `${environment.baseUrl}/api/BudgetItems`;

    const categoryName = document.getElementById(
      'addBudgetItemCategoryName'
    ) as HTMLInputElement;

    const amount = document.getElementById(
      'addBudgetItemAmount'
    ) as HTMLInputElement;

    let data: BudgetItem = {
      id: 0,
      categoryName: categoryName.value,
      amount: parseInt(amount.value),
    };

    this.http
      .post<BudgetItem>(url, data, { headers: this.httpHeaders })
      .subscribe((result) => {
        this.data = [...this.data, result];
      });
  }

  updateData() {
    const id = document.getElementById(
      'updateBudgetItemID'
    ) as HTMLInputElement;

    const categoryName = document.getElementById(
      'updateBudgetItemCategoryName'
    ) as HTMLInputElement;

    const amount = document.getElementById(
      'updateBudgetItemAmount'
    ) as HTMLInputElement;

    let url = `${environment.baseUrl}/api/BudgetItems/${id.value}`;

    let data: BudgetItem = {
      id: parseInt(id.value),
      categoryName: categoryName.value,
      amount: parseInt(amount.value),
    };

    this.http
      .put<any>(url, data, { headers: this.httpHeaders })
      .subscribe((result) => {
        this.updateItem(parseInt(id.value), data);
      });
  }

  updateItem(id: number, newData: BudgetItem) {
    let index = this.data.findIndex((el) => el.id == id);

    if (index != null) {
      this.data[index] = newData;
    }
  }

  deleteData() {
    const id = document.getElementById(
      'deleteBudgetItemID'
    ) as HTMLInputElement;

    let url = `${environment.baseUrl}/api/BudgetItems/${id.value}`;

    this.http
      .delete<any>(url, { headers: this.httpHeaders })
      .subscribe((result) => {
        this.deleteItem(parseInt(id.value));
      });
  }

  deleteItem(id: number) {
    let index = this.data.findIndex((el) => el.id == id);

    if (index != null) {
      delete this.data[index];
    }
  }
}
