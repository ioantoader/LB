/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { CompanyRequestComponent } from './company-request.component';


describe('CompanyRequestComponent', () => {
  let component: CompanyRequestComponent;
  let fixture: ComponentFixture<CompanyRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
