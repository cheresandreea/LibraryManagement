Instructions
```bash
Clone the repository
```bash
git clone
cd WebApplication2
```
Install dependencies
```bash
dotnet restore
```
Build the project
```bash
dotnet build
```
Apply database migrations
```bash
dotnet ef database update
```
Run the application
```bash
dotnet run
```
Open your web browser and navigate to `http://localhost:7004` or `https://localhost:5114` to access the application.

Functionality Overview
Core Features
Book Management:
Add, update, delete, and search for books.
Endpoints: /api/books, /api/books/search.

Loan Management:
Loan books to borrowers and return them.
Endpoints: /api/loans/loan, /api/loans/return.

Overdue Reminders:
Automatically send email reminders for overdue books.
Endpoint: /api/loans/send-overdue-reminders.

Functionality from Point 7
Overdue Reminder Emails:
The application checks for overdue loans (loans older than 10 seconds for testing purposes).
Sends an email to the borrower with a list of overdue books.
Uses the SmtpEmailService to send emails via the configured SMTP server.

Testing the Application
Swagger UI - it will be available at `http://localhost:7004/swagger` or `https://localhost:5114/swagger`.