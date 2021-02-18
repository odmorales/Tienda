import { Component, OnInit } from '@angular/core';
import { AngularFireStorage } from '@angular/fire/storage';
import { finalize } from 'rxjs/operators';
import { Observable } from 'rxjs/internal/Observable';
import { Producto } from '../models/producto';
import { ProductoService } from '../../services/producto.service';
@Component({
  selector: 'app-producto-registro',
  templateUrl: './producto-registro.component.html',
  styleUrls: ['./producto-registro.component.css']
})
export class ProductoRegistroComponent implements OnInit {

  producto: Producto;
  constructor(private productoService: ProductoService, private storage: AngularFireStorage) { }

  uploadPercent: Observable<number>;
  urlImage: Observable<string>;

  ngOnInit() {
    this.producto = new Producto;
  }

  add() {
    console.log(this.producto.descripcion);
    this.productoService.post(this.producto).subscribe(p => {
      if (p != null) {
        alert('Producto creada!');
        this.producto = p;
      }
    });
  }

  onPhotoSelected(event){
    
    const file = event.target.files[0];
    let nombre = new Date().getTime().toString();
    console.log(nombre);
    const ruta = `Productos/${nombre}`;
    const refs = this.storage.ref(ruta);
    const task = refs.put(file);
    task.percentageChanges().subscribe(porcentaje =>{
      console.log(porcentaje);
    });

    task.then(() => {
      refs.getDownloadURL().subscribe(imageUrl => {
        console.log(this.producto.imageUrl = imageUrl);
      })
    })
  }
}
