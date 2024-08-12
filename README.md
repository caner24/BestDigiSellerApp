# BestDigiSellerApp

Digital Marketplace Microservices Application
This project is a digital marketplace application developed using microservices architecture, created as part of the Papara .NET Core Bootcamp. The application leverages a wide range of technologies to provide a robust and scalable system for handling various marketplace operations.

Key Features
<ul>
<li>Microservices Architecture</li>
  Independent and loosely coupled services designed for scalability and maintainability.
<br>
<li>Payment System</li>
  Integrated with Stripe for secure and efficient payment processing.
<br>
<li>User Management</li
  Includes features for creating users and promoting them to admin roles.
<br>
<li>Order Processing</li>
  Upon successful payment, the system triggers processes such as receipt generation and stock reduction via consumers.
<br>
Cashback System
  Rewards users with cashback on successful payments.
<br>
</ul>
Technologies Used
<ul>
<li>.NET Aspire: Core framework</li>
  For developing the microservices.
<li>CQRS & Mediatr</li>
  For clear separation of commands and queries, ensuring scalable and maintainable code.
<li>FluentValidation</li>
  To enforce validation rules across services.
<li>FluentResult</li>
  For handling operation results in a structured way.
<li>RabbitMQ & MassTransit</li> 
For message brokering and handling asynchronous communication between services.
<li>Stripe</li>
    Integrated payment gateway for handling all transactions.
</ul>
