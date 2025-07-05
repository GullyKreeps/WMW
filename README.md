 WillMyWay Backend
A secured, production-ready ASP.NET Core 8 Web API backend for the WillMyWay Hypnotherapy Platform.
Supports user authentication, admin audio library management, purchase validation via payment provider webhooks, and Cal.com virtual session bookings.

🌟 Features
User authentication (JWT-based, hashed passwords)

Role-based admin/user authorization

Audio library CRUD (admin only)

Purchase tracking (user + admin)

Payment provider webhook validation:

✅ Stripe

✅ PayPal

✅ NowPayments

Cal.com booking integration:

Bookings persisted in database

Webhook endpoint for updates

Global error handling & validation

HTTPS redirection & CORS locked to frontend domain

SQL Server with EF Core migrations

🖥️ Tech Stack
ASP.NET Core 8

Entity Framework Core (SQL Server)

JWT Authentication

Swagger (OpenAPI)

Cal.com API

Stripe / PayPal / NowPayments webhooks

🚀 How to Run Locally
🧰 Prerequisites
✅ .NET 8 SDK: Download
✅ SQL Server (local or cloud)
✅ Git

📂 Clone the repo
bash
Copy
git clone https://github.com/GullyKreeps/WMW.git
cd WMW
⚙️ Update appsettings.json
Set your SQL Server connection string and your own JWT secret & Cal.com API key:

json
Copy
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=HypnoAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
},
"Jwt": {
  "Key": "your_super_secret_jwt_key",
  "Issuer": "WillMyWayAPI"
},
"Cal": {
  "ApiKey": "your_cal_com_api_key"
}
🗄️ Run EF Core migrations
bash
Copy
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
▶️ Run the API
bash
Copy
dotnet run
Visit:
🌐 https://localhost:5001/swagger for API docs & testing.

📝 API Endpoints Overview
✅ /api/auth/register
✅ /api/auth/login
✅ /api/audio — view & admin CRUD
✅ /api/purchase — purchase & history
✅ /api/booking — Cal.com proxy
✅ /api/webhook/calcom — Cal.com webhook
✅ /api/paymentwebhook/{provider} — Stripe, PayPal, NowPayments

👤 Admin
Set Role to Admin in Users table manually or seed

Admin-only endpoints protected with [Authorize(Roles="Admin")]

📜 License
MIT © GullyKreeps