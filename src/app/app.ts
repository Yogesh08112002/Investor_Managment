import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Authservice } from './authservice';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [FormsModule, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('InvestorFramework');
  name:string='';
  email:string='';
  usredata:any[]=[];
  constructor(private auth:Authservice){}

  getdata(): void {
    this.auth.getData().subscribe({
      next:(res:any)=>{
         this.usredata=res;
         console.log(res)
      },
      error:(e)=>{
        console.log("ERROR",e.error);
      },
    })
  }
addUsre(){
  this.auth.addInvestor({name:this.name,email:this.email}).subscribe({
    next:()=>{
      alert("Successfully added")
    },
    error:(e)=>{
      alert("ERROR"+e.error)
    }
  })
}

edituser(){
    this.auth.updateuser(1,{name:this.name,email:this.email}).subscribe({next:()=>{
      alert('update Sucessfull')
    },

    error:(e)=>{
      alert(e.error)
    }
  })
}


}
