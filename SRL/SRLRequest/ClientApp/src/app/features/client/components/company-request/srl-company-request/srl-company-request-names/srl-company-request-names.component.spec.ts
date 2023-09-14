/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { SRLCompanyRequestNamesComponent } from './srl-company-request-names.component';


describe('SRLCompanyRequestNamesComponent', () => {
  let component: SRLCompanyRequestNamesComponent;
  let fixture: ComponentFixture<SRLCompanyRequestNamesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SRLCompanyRequestNamesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SRLCompanyRequestNamesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
