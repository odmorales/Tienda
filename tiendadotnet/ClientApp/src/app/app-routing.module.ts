import { ProductoRegistroComponent } from './Tienda/producto-registro/producto-registro.component';
import { ProductoConsultaComponent } from './tienda/producto-consulta/producto-consulta.component';
import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'productoRegistro',
    component: ProductoRegistroComponent
  },
  {
    path: 'productoConsulta',
    component: ProductoConsultaComponent
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }
