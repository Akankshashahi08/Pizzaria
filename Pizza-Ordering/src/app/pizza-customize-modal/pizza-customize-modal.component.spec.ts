import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PizzaCustomizeModalComponent } from './pizza-customize-modal.component';

describe('PizzaCustomizeModalComponent', () => {
  let component: PizzaCustomizeModalComponent;
  let fixture: ComponentFixture<PizzaCustomizeModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PizzaCustomizeModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PizzaCustomizeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
