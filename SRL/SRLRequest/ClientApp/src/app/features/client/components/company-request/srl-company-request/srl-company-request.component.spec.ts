/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SRLCompanyRequestComponent } from './srl-company-request.component';

describe('SRLCompanyRequestComponent', () => {
  let component: SRLCompanyRequestComponent;
  let fixture: ComponentFixture<SRLCompanyRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SRLCompanyRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SRLCompanyRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
