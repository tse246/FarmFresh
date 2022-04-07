import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private http: HttpClient, private router: Router, @Inject('BASE_URL') private baseUrl: string) {

  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onSearch(searchText) {
    const body = { Username: 'admin', Password: 'admin' };
    this.http.post<any>(this.baseUrl + 'api/users/Auth', body, { responseType: 'text' as 'json' }).subscribe(result => {
      sessionStorage.setItem("token", result);
      this.router.navigate(["/product", searchText]);
    }, error => console.error(error));
  }
}
