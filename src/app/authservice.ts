import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Authservice {
  

  HttpApi = "http://localhost:5206/api/Investor"

  constructor(private http:HttpClient){
  }

  getData():Observable<any>{
    return this.http.get(`${this.HttpApi}`);
  }
  
  addInvestor(user:{name:string,email:string}):Observable<any>{
    return this.http.post(`${this.HttpApi}`,user);
  }

  updateuser(id:number ,user:{name:string,email:string}): Observable<any>{

    return this.http.put(`${this.HttpApi}/${id}`,user)
  }
}
