var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BestDigiSellerApp_Ocelot>("bestdigisellerapp-ocelot");

builder.AddProject<Projects.BestDigiSellerApp_Discount>("bestdigisellerapp-discount");

builder.AddProject<Projects.BestDigiSellerApp_File>("bestdigisellerapp-file");

builder.AddProject<Projects.BestDigiSellerApp_Product>("bestdigisellerapp-product");

builder.AddProject<Projects.BestDigiSellerApp_Stripe>("bestdigisellerapp-stripe");

builder.AddProject<Projects.BestDigiSellerApp_User>("bestdigisellerapp-user");

builder.AddProject<Projects.BestDigiSellerApp_Discount_Api>("bestdigisellerapp-discount-api");

builder.AddProject<Projects.BestDigiSellerApp_File_Api>("bestdigisellerapp-file-api");

builder.AddProject<Projects.BestDigiSellerApp_Product_Api>("bestdigisellerapp-product-api");

builder.AddProject<Projects.BestDigiSellerApp_Stripe_Api>("bestdigisellerapp-stripe-api");

builder.AddProject<Projects.BestDigiSellerApp_User_Api>("bestdigisellerapp-user-api");

builder.AddProject<Projects.BestDigiSellerApp_Invoice>("bestdigisellerapp-invoice");

builder.AddProject<Projects.BestDigiSellerApp_Invoice_Api>("bestdigisellerapp-invoice-api");

builder.AddProject<Projects.BestDigiSellerApp_Basket_Api>("bestdigisellerapp-basket-api");

builder.Build().Run();
