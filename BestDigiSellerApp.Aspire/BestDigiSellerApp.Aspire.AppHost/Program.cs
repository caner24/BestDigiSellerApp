using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


var validIssuer = builder.AddParameter("ValidIssuer");
var secretKey = builder.AddParameter("SecretKey", true);
var rabbitUser = builder.AddParameter("RabbitUser");
var rabbitPass = builder.AddParameter("RabbitPassword", true);
var messaging = builder.AddRabbitMQ("messaging", rabbitUser, rabbitPass).WithDataVolume("rabbitmq-volume", false).WithManagementPlugin();
var redis = builder.AddRedis("redis");

builder.AddProject<Projects.BestDigiSellerApp_Ocelot>("bestdigisellerapp-ocelot");
builder.AddProject<Projects.BestDigiSellerApp_Discount_Api>("bestdigisellerapp-discount-api");
builder.AddProject<Projects.BestDigiSellerApp_File_Api>("bestdigisellerapp-file-api");
builder.AddProject<Projects.BestDigiSellerApp_Product_Api>("bestdigisellerapp-product-api").WithReference(redis).WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey);
builder.AddProject<Projects.BestDigiSellerApp_Stripe_Api>("bestdigisellerapp-stripe-api").WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey);

builder.AddProject<Projects.BestDigiSellerApp_Invoice_Api>("bestdigisellerapp-invoice-api");
builder.AddProject<Projects.BestDigiSellerApp_Basket_Api>("bestdigisellerapp-basket-api");

var walletApiService = builder.AddProject<Projects.BestDigiSellerApp_Wallet_Api>("bestdigisellerapp-wallet-api").WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey).WithReference(messaging);

builder.AddProject<Projects.BestDigiSellerApp_User_Api>("bestdigisellerapp-user-api").WithExternalHttpEndpoints().WithReference(walletApiService).WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey).WithReference(messaging);

builder.Build().Run();
