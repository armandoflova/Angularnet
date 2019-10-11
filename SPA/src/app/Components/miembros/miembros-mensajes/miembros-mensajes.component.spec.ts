import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MiembrosMensajesComponent } from './miembros-mensajes.component';

describe('MiembrosMensajesComponent', () => {
  let component: MiembrosMensajesComponent;
  let fixture: ComponentFixture<MiembrosMensajesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MiembrosMensajesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MiembrosMensajesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
