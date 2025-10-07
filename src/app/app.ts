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
export class App implements OnInit{
  protected readonly title = signal('InvestorFramework');
  name:string='';
  email:string='';
  uname:string='';
  uemail:string='';
  usredata:any[]=[];

  ngOnInit(): void {
  }
  closeForm() {
  this.name = '';
  this.email = '';
  this.selectedUserId = null;
}

refreshData() {
  this.getdata();      // Fetch latest data
  this.name = '';      // Clear input fields
  this.email = '';
  this.selectedUserId = null; // Deselect any user
}



  constructor(private auth:Authservice){

  }

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
      this.getdata()
    },
    error:(e)=>{
      alert("ERROR"+e.error)
    }
  })
}

selectedUserId: number | null = null; // Track which user to edit

selectUser(u: any) {
  this.selectedUserId = u.id;
  this.name = u.name;
  this.email = u.email;
}
editUser() {
  if (this.selectedUserId == null) {
    alert("Please select a user to edit");
    return;
  }

  const user = this.usredata.find(u => u.id === this.selectedUserId);
  if (!user) return;

  const payload = {
    ...user,             // keep all existing fields
    name: this.name,     // update name
    email: this.email    // update email
  };

  this.auth.updateuser(this.selectedUserId, payload).subscribe({
    next: () => {
      alert("Updated successfully");
      this.getdata();
      this.name = '';
      this.email = '';
      this.selectedUserId = null;
    },
    error: (e) => {
      console.error("ERROR", e);
      alert("Update failed");
    }
  });
}
 deleteUser(id: number) {
    if (confirm('Are you sure you want to delete this user?')) {
      this.auth.deleteUser(id).subscribe({
        next: () => {
          alert('User deleted successfully');
          this.getdata();
        },
        error: (err) => {
          console.log('ERROR deleting user: ' + err.error);
        }
      });
    }
  }


}
