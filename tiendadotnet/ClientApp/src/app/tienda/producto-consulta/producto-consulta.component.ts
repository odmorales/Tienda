import { Component, OnInit } from '@angular/core';
import { Producto } from '../models/producto';
import { ProductoService } from '../../services/producto.service';


@Component({
  selector: 'app-producto-consulta',
  templateUrl: './producto-consulta.component.html',
  styleUrls: ['./producto-consulta.component.css']
})
export class ProductoConsultaComponent implements OnInit {

  productos: Producto[];
  constructor(private productoService: ProductoService) { }

  ngOnInit() {
    this.productoService.get().subscribe(result => {
      this.productos = result;
    })
  }

}
