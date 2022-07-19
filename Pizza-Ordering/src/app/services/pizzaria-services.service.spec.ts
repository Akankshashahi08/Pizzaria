import { TestBed } from '@angular/core/testing';

import { PizzariaServicesService } from './pizzaria-services.service';

describe('PizzariaServicesService', () => {
  let service: PizzariaServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PizzariaServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
