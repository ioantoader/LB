/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { CompanyAssociateComponent } from './company-associate.component';


describe('CompanyAssociateComponent', () => {
  let component: CompanyAssociateComponent;
  let fixture: ComponentFixture<CompanyAssociateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyAssociateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyAssociateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
