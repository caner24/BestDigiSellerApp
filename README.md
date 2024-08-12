# BestDigiSellerApp

Digital Marketplace Microservices Application
This project is a digital marketplace application developed using microservices architecture, created as part of the Papara .NET Core Bootcamp. The application leverages a wide range of technologies to provide a robust and scalable system for handling various marketplace operations.

Key Features:
<ul>
<li>Microservices Architecture</li>:
    <br>
  Independent and loosely coupled services designed for scalability and maintainability.
<br>
<li>Payment System</li>: 
    <br>
  Integrated with Stripe for secure and efficient payment processing.
<br>
<li>User Management</li>: 
    <br>
  Includes features for creating users and promoting them to admin roles.
<br>
<li>Order Processing</li>:
    <br>
  Upon successful payment, the system triggers processes such as receipt generation and stock reduction via consumers.
<br>
Cashback System:
    <br>
  Rewards users with cashback on successful payments.
<br>
</ul>
Technologies Used:
<ul>
<li>.NET Aspire: Core framework</li>
    <br>
  For developing the microservices.
  <br>
<li>CQRS & Mediatr</li>: 
    <br>
  For clear separation of commands and queries, ensuring scalable and maintainable code.
  <br>
<li>FluentValidation</li>: 
    <br>
  To enforce validation rules across services.
  <br>
<li>FluentResult</li>: 
    <br>
  For handling operation results in a structured way.
  <br>
<li>RabbitMQ & MassTransit</li>:   
  <br>
For message brokering and handling asynchronous communication between services.
  <br>
<li>Stripe</li>: Integrated payment gateway for handling all transactions.
</ul>
