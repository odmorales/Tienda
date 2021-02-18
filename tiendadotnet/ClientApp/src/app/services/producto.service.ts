import { Inject, Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Producto } from '../tienda/models/producto';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleHttpErrorService } from '../@base/handle-http-error.service';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  baseUrl: string;
  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private handleErrorService: HandleHttpErrorService) {
    this.baseUrl = baseUrl;
  }
  get(): Observable<Producto[]> {
    return this.http.get<Producto[]>(this.baseUrl + 'api/Producto')
      .pipe(
        tap(_ => this.handleErrorService.log('datos enviados')),
        catchError(this.handleErrorService.handleError<Producto[]>('Consulta Producto', null))
      );
  }

  getIndivdual(identificacion:string): Observable<Producto> {
    const url = `${this.baseUrl}api/Producto/${identificacion}`; 
    return this.http.get<Producto>(url).pipe(
    tap(_ => this.handleErrorService.log(`fetched hero id=${identificacion}`)),
    catchError(this.handleErrorService.handleError<Producto>(`getHero id=${identificacion}`))
  );
  }

  post(producto: Producto): Observable<Producto> {
    return this.http.post<Producto>(this.baseUrl + 'api/Producto', producto)
      .pipe(
        tap(_ => this.handleErrorService.log('datos enviados')),
        catchError(this.handleErrorService.handleError<Producto>('Registrar Producto', null))
      );
  }
}
