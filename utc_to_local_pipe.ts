To handle UTC date conversion within an array using a custom Angular Pipe, you can create a pipe to transform the UTC dates to local browser time and format them as needed. Here's how you can do it:

1. Create a Custom Pipe
Pipe Code
typescript
Copy code
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'utcToLocal',
})
export class UtcToLocalPipe implements PipeTransform {
  transform(utcDate: string | null): Date | null {
    if (!utcDate) return null; // Handle null or undefined
    return new Date(utcDate); // Converts UTC to local time
  }
}
2. Register the Pipe
Add the pipe to your module's declarations array.

Example: app.module.ts
typescript
Copy code
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { UtcToLocalPipe } from './utc-to-local.pipe'; // Import your custom pipe

@NgModule({
  declarations: [
    AppComponent,
    UtcToLocalPipe // Add the custom pipe here
  ],
  imports: [BrowserModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
3. Use the Pipe in the Template
You can now use the utcToLocal pipe to transform an array of UTC dates.

Example Template
html
Copy code
<ul>
  <li *ngFor="let date of dates">
    {{ date | utcToLocal | date: 'medium' }}
  </li>
</ul>
Example Component
typescript
Copy code
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  dates: string[] = [
    '2025-01-02T10:00:00Z',
    '2025-01-03T15:30:00Z',
    '2025-01-04T08:45:00Z',
  ]; // Example array of UTC dates
}
4. Output
The utcToLocal pipe converts each UTC date to the local browser time and formats it using Angular's built-in DatePipe.

Result (Example Output)
yaml
Copy code
Jan 2, 2025, 2:00:00 PM
Jan 3, 2025, 8:30:00 PM
Jan 4, 2025, 12:45:00 PM
