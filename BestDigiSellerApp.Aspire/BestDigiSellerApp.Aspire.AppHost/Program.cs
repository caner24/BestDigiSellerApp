using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


var validIssuer = builder.AddParameter("ValidIssuer");
var secretKey = builder.AddParameter("SecretKey", true);
var rabbitUser = builder.AddParameter("RabbitUser");
var rabbitPass = builder.AddParameter("RabbitPassword", true);
var messaging = builder.AddRabbitMQ("messaging", rabbitUser, rabbitPass).WithDataVolume("rabbitmq-volume", false).WithManagementPlugin();
var redis = builder.AddRedis("redis");


var fileApiService = builder.AddProject<Projects.BestDigiSellerApp_File_Api>("bestdigisellerapp-file-api").WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey);

var productApi = builder.AddProject<Projects.BestDigiSellerApp_Product_Api>("bestdigisellerapp-product-api").WithReference(redis).WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey).WithReference(fileApiService);



var walletApiService = builder.AddProject<Projects.BestDigiSellerApp_Wallet_Api>("bestdigisellerapp-wallet-api").WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey).WithReference(messaging);

var stripeApi = builder.AddProject<Projects.BestDigiSellerApp_Stripe_Api>("bestdigisellerapp-stripe-api").WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey).WithReference(messaging).WithReference(productApi).WithReference(walletApiService);
var userApi = builder.AddProject<Projects.BestDigiSellerApp_User_Api>("bestdigisellerapp-user-api").WithExternalHttpEndpoints().WithReference(walletApiService).WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey).WithReference(messaging);

var discountApi = builder.AddProject<Projects.BestDigiSellerApp_Discount_Api>("bestdigisellerapp-discount-api").WithReference(userApi).WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey).WithReference(messaging);


builder.AddProject<Projects.BestDigiSellerApp_Basket_Api>("bestdigisellerapp-basket-api").WithReference(discountApi).WithReference(productApi).WithReference(stripeApi).WithEnvironment("ValidIssuer", validIssuer)
    .WithEnvironment("SecretKey", secretKey);

builder.Build().Run();