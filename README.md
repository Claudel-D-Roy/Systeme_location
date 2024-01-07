# Car Rental System

My first major web project, completed during the fourth session of my software development program.

The project was a team effort.

## 1. Description

The goal of this project is to develop a simple web application by a team of three to four students. The objectives include applying good application development practices, project planning, teamwork, and web design. The estimated duration is between 25 and 50 hours.

The project will account for 60% of the final course grade, divided into three assignments (TP01, TP02, and TP03), each with two evaluations. The first evaluation will involve a presentation to the teacher, simulating a client demonstration. The second evaluation will occur during code submission, where the teacher will assess the technical quality of your work. Cumulative points have been adjusted for each assignment, as indicated in the summary table in the latest project statement.

**Details:**
- Duration: 25 to 50 hours
- Team: 3 to 4 students

## 2. Context

This web application aims to provide a management solution for car rental businesses in Quebec. It will allow employees, including administrators, managers, and clerks, to manage branches, car reservations, cars, drivers (customers), and users. The management system will be easy to use and provide a comprehensive overview of all rental operations.

Reservations will be made when a customer physically visits a branch. The clerk will then process the reservation in the system, using a user-friendly interface. Employees will have the ability to annotate reservations and cars for traceability reasons.

To ensure the security and confidentiality of information, all employees must authenticate to access the system. However, no payments will be made directly through the application, simplifying its use further.

In summary, this application will be an essential tool for car rental companies in Quebec, offering efficient and reliable management of rental operations. You must produce a web application according to the requirements mentioned below. Aim to specify all integrity constraints that can be deduced from the use cases. If you introduce derived elements, specify the corresponding integrity constraints.

**Actors:**
- Administrator: Responsible for managing users and branch management. An administrator user account will have been created during system installation.
- Manager: Responsible for car management.
- Clerk: Responsible for data entry, including the management of rentals and drivers.
- User: Represents a system user.
- System: Represents the system.

**Domain:**
Within the system, several relationships between entities can be observed.

*Note:*
- The structure of domain objects presented here is a suggestion of how information can be organized in the system. This structure does not necessarily correspond to how data is stored in the database or presented in views.
- Entities in the "Identity" subdomain are presented here to provide a comprehensive overview of the domain, but if you use a user and role management library like ASP.NET Core Identity, you may not need to implement them in your system.
- In this system, the relationship between "Driver" and "Rental" entities is 1:1, meaning a driver can never have more than one rental. If a driver wants to have multiple rentals, there will be multiple copies of the entity in the system.

**Navigation:**
The car rental system includes several pages that must be created. You are free to choose the style of pages and navigation, and you can use synchronous or asynchronous web pages, as well as on-page forms or pop-ups. However, the following pages must be implemented:
- Authentication page: Allows users to log in (default page for non-connected users).
- Branch management pages: Allows managers to manage branches (the page displaying the list of branches should be the default page for administrators).
- Car management pages: Allows managers to manage cars in a specific branch and allows employees to view available cars (the page displaying the list of cars in a selected branch should be the default page for clerks).
- User management pages: Allows administrators to manage users.
- Rental management pages: Allows employees to manage rentals in a specific branch or globally (the page displaying rentals should be the default page for managers).
- Driver management pages: Allows employees to view drivers.
