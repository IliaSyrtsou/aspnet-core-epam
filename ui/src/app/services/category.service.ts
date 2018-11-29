import { Http, RequestOptionsArgs } from '@angular/http';
import { Injectable } from '@angular/core';
import { ApiEndpoints } from '../constants/api-endpoints';
import { map } from 'rxjs/operators';
import { Category } from '../models/category';
import { Observable } from 'rxjs';

@Injectable()
export class CategoryService {
  constructor(private readonly http: Http) {

  }

  public getPagedCategories(pageNumber: number, pageSize: number): Observable<Category[]> {
    const options: RequestOptionsArgs = {
      params: {
        pageNumber: pageNumber,
        pageSize: pageSize
      }
    };
    return this.http
      .get(ApiEndpoints.Category.GetPaged, options)
      .pipe(map(response => response.json()));
  }
}
