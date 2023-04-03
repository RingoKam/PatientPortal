# Patient Portal 💊

## Project Description
Please create a responsive web application that will have an upload view that accepts CSV files with
patient records as input (see CSV sample file attached), and a view that displays the patients already
uploaded to the application with basic ordering and filtering by patient name. Once uploaded, each
patient record should be editable.

## Requirements
- Backend Web API written in .NET 6 C#.
- Frontend written in Angular 12 or higher.
- Data uploaded can be stored in SQL and should not be volatile (survive a restart).
- An online Git based repository accessible to clone and review the code.
---
### Project Structure
The solution is consist of a number of prorjects:
1. PatientPortal: It serves as our start up project and host our API and Angular FE. There are very little to no application logic here. 
2. PartientPortalApplication: Consist of all our business logic and application flow. 
3. PatientPortalDomain: hold our domain models. Consume by other projects and has no dependency on others.
4. PatientRepository: Project that deal with our DB

### Backend
* ASPNET CORE: BE Framework  
* EF CORE 6: ORM to interact w/ SQL server
* nswag: Swagger gen + Typescript definition gen

### Frontend
* Angular 14: FE framework
* Angular Material 14: Component Library
* ngx-dropzone: component to handle file upload

#### Landing Page
![image](https://user-images.githubusercontent.com/10913199/229395971-e88eff93-820a-411d-adb7-b3f6848efbd4.png)
#### Upload CSV
![image](https://user-images.githubusercontent.com/10913199/229396051-273705cd-5fc6-408c-aebf-4da76b700aee.png)
#### View Data
![image](https://user-images.githubusercontent.com/10913199/229396097-f569e4c8-ddc1-4149-b061-31e6dc496b13.png)
