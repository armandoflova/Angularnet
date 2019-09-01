import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MiembrosDetalleComponent } from './miembros-detalle.component';

describe('MiembrosDetalleComponent', () => {
  let component: MiembrosDetalleComponent;
  let fixture: ComponentFixture<MiembrosDetalleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MiembrosDetalleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MiembrosDetalleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
