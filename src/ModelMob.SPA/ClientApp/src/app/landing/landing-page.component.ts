import { Component } from "@angular/core";
import { Subject } from "rxjs/Subject";

@Component({
  templateUrl: "./landing-page.component.html",
  styleUrls: ["./landing-page.component.css"],
  selector: "app-landing-page"
})
export class LandingPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
