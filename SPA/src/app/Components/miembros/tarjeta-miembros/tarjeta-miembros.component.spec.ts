import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TarjetaMiembrosComponent } from './tarjeta-miembros.component';

describe('TarjetaMiembrosComponent', () => {
  let component: TarjetaMiembrosComponent;
  let fixture: ComponentFixture<TarjetaMiembrosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TarjetaMiembrosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TarjetaMiembrosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
