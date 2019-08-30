import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MiembrosListaComponent } from './miembros-lista.component';

describe('MiembrosListaComponent', () => {
  let component: MiembrosListaComponent;
  let fixture: ComponentFixture<MiembrosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MiembrosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MiembrosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
