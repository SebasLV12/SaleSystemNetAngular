import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalDetailSaleComponent } from './modal-detail-sale.component';

describe('ModalDetailSaleComponent', () => {
  let component: ModalDetailSaleComponent;
  let fixture: ComponentFixture<ModalDetailSaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalDetailSaleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalDetailSaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
