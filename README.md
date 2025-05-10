# Building Ecommerce web application with C#

## 🔧 Core Functional Requirements.

### 1. Product Management.
 
<li>Add, update, delete products.

<li>Store product details: name, description, price, SKU, images, category, etc.

<li>Display products for customers. 

### 2. Inventory Management.

<li>Track inventory per product.

<li>Add stock (e.g., when restocking).

<li>Subtract stock when orders are placed (and paid).

<li>Optional: Alert for low inventory levels.

### 3. Customer Management.

<li>Register and authenticate customers.

<li>Store profile: name, email, phone, shipping address, etc.

<li>Allow customers to update their info.

### 4. Order Management

<li>Store order details: products, quantities, price, customer info, shipping status.

<li>Allow order history viewing for users.

<li>Admins can manage or update order statuses (e.g., Processing, Shipped, Delivered).

### 5. Shopping Cart & Checkout

<li>Add/remove items to/from cart.

<li>Show total cost, taxes, and shipping.

<li>Checkout page with payment and shipping details.

### 6. Payment Processing

<li>Integrate a payment gateway (e.g., Stripe, PayPal, Square).

<li>Securely handle payment details (PCI compliance).

<li>Confirm successful/failed transactions and update order status accordingly.

## 📂 Optional (but valuable) Features

<li>User authentication with JWT/cookies.

<li>Product search and filtering (by category, price, etc.).

<li>Promo codes or discount system.

<li>Email confirmation and notifications.

<li>Admin panel for managing products, customers, and orders.

<li>Reviews/ratings system.

## 🧱 Technology Stack Suggestion (C#/.NET Core)

<li>Backend: ASP.NET Core Web API

<li>Frontend: Razor Pages, Blazor, or a JS framework (React, Angular) with API

<li>Database: SQL Server or PostgreSQL via Entity Framework Core

<li>Authentication: ASP.NET Identity or JWT

<li>Payments: Stripe API (easiest to integrate/test)

## 🗂️ Project Structure

Assuming you're building an ASP.NET Core Web API backend, your folder structure might look like this:
```ruby

EcommerceApp/
│
├── Controllers/           → API endpoints (ProductsController, OrdersController, etc.)
├── Models/                → Data models (Product, Customer, Order, etc.)
├── Data/                  → DbContext and seed data
├── DTOs/                  → Data Transfer Objects for requests/responses
├── Services/              → Business logic (e.g., OrderService, PaymentService)
├── Repositories/          → Data access layer (optional abstraction)
├── Migrations/            → EF Core migrations
├── wwwroot/               → Static files (if applicable)
├── Program.cs             → App entry point
└── appsettings.json       → Configuration (connection strings, etc.)
```
### 🧩 1. Database Schema
### 📦 2. Order
### 📄 3. OrderItem
### 👤 4. Customer
### 💳 5. Payment (Optional if using external gateway only)
### 🔗 6. AppDbContext.cs
### 🧾 7. ProductsController.cs

# 🧪 Example Test with curl or Postman.

### GET all products
```ruby
GET http://localhost:5000/api/products
```
### POST new product

```ruby
POST http://localhost:5000/api/products
Content-Type: application/json

{
    "name": "Laptop",
    "description": "High-performance laptop",
    "price": 1200.00,
    "stock": 15,
    "sku": "LAP123",
    "imageUrl": "https://example.com/laptop.jpg"
}
```
### 🧩 8. OrderService.cs

# ✅ How to Use OrderService

Example us

```ruby
[HttpPost("create")]
public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
{
    var result = await _orderService.CreateOrderAsync(request.CustomerId, request.Items);

    if (!result.Success)
        return BadRequest(result.Message);

    return Ok(result.Message);
}
```
### DTO

```ruby
public class CreateOrderRequest
{
    public int CustomerId { get; set; }
    public List<OrderItemRequest> Items { get; set; }
}

public class OrderItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
```
# 💳 Step-by-Step: Stripe Integration for

### ✅ 1. Set Up Stripe in Your

Install the Stri

```ruby
dotnet add package Stripe.net
```

In `appsettings.json`, add you
```ruby
"Stripe": {
  "SecretKey": "sk_test_YourSecretKey",
  "PublishableKey": "pk_test_YourPublicKey"
}
```
### Load Stripe configuration in Program.cs or Startup.cs:

```ruby
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
```
### 🧾 2. Create a PaymentService

This will handle creating a Stripe charge (or PaymentIntent for newer APIs):
  
### 🎯 3. Payment Controller Endpoint

### 🧪 4. Testing

You can now POST to /api/payments/create-checkout-session/{orderId} and redirect the user to the returned url

# 📡 Stripe Webhook Integration (Payment Confirmation)

### ✅ 1. Why Use Webhooks?

Stripe Checkout redirects users after payment, but the most secure way to confirm payment is by listening to Stripe’s server-side webhooks.

### 🛠️ 2. Add Webhook Endpoint to Your App

>📌 Note: When creating the Checkout Session earlier, you can add Metadata = new Dictionary<string, string> { { "order_id", order.Id.ToString() } } to the SessionCreateOptions.
Stripe Checkout redirects users after payment, but the most secure way to confirm payment is by listening to Stripe’s server-side webhooks.

### 🧪 3. Expose Localhost for Stripe Testing

# ✅ Result

1.Now your app will:

2.Create Stripe Checkout sessions.

3.Send users to Stripe to pay.

4.Listen for successful payments via webhook.

5.Automatically mark the order as “Paid” in your database.

# 🖼️  Basic UI Components

### 📄 Pages/Products.razor

### 🛒 Services/CartService.cs

# 💳 Checkout Page

### 📄 Pages/Checkout.razor

### 🧪 Test Flow

1.Run your backend API and Blazor app

2.Visit /products

3.Add items to cart

4. /checkout

5.Click "Proceed to Payment" → redirects to Stripe

6.After successful payment, Stripe webhook marks order as "Paid"

# 🚀 Azure Deployment:

✅ Backend (ASP.NET Core Web API)

✅ Frontend

✅ SQL Databas

✅ Stripe Webhooks (via Stripe CLI or Azure F)

# 🚀 GitHub Ac

🧭 Y

✅ Automatic build and test on each push

✅ Publish to Azure App Servi (API and Blazor)

### 🔧 Step 1: Prep Your Azure App Services

### 🔑 Step 2: Create Azure Deployment Credentials

### 📁 Step 3: Create GitHub Workflow Files

### 🔐 Step 4: Add Secrets to GitHub

### 🧪 Step 5: Test CI/CD

# ✅ Result

Every time you push changes:

<li>API and frontend are built

<li>Deployed automatically to Azure App Services

<li>No manual steps needed

### Perfect — you're now fully set up with:

✅ A C# e-commerce backend (API)
✅ A Blazor Server frontend
✅ Stripe payment + webhook handling
✅ Azure SQL integration
✅ Fully automated CI/CD via GitHub Actions

This stack is solid for portfolio use, production trials, or job interviews.


