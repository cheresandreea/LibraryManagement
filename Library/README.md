## Instructions (Visual Studio 2022)

1. Clone the repository.
2. Open the solution in Visual Studio 2022.
3. Run the application.

Open your web browser and navigate to:
- `http://localhost:7004` or
- `https://localhost:5114`

---

## Functionality Overview

### 📚 Book Management
- Add, update, delete, and search for books.
- **Endpoints**: `/api/Book`, `/api/Book/{id},`, `/api/Book/search`

### 📖 Loan Management
- Loan books to borrowers and return them.
- **Endpoints**: `/api/Loan/loan`, `/api/Loan/return`, `/api/Loan/search`

### ⏰ Overdue Reminders
- Automatically sends email reminders for overdue books.
- **Endpoint**: `/api/loans/send-overdue-reminders`

> The application checks for overdue loans (set to 10 seconds for testing).
> Uses `SmtpEmailService` to send emails via a configured SMTP server.
> Make sure to set up the SMTP server in `appsettings.json`:

``` appsettings.json
  "AllowedHosts": "*",
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUsername": "cheresandreea@gmail.com",
    "SmtpPassword": "**** **** **** ****", //the password from the app password
    "FromEmail": "cheresandreea@gmail.com"
  }
```

## 🧪 Testing the Application

- Visit Swagger UI at:
    - `http://localhost:7004/swagger`
    - `https://localhost:5114/swagger`
