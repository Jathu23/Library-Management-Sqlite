using Library_Api_Sqlite.Data;
using Library_Api_Sqlite.FileService;
using Library_Api_Sqlite.Repository;
using Library_Api_Sqlite.Services;
using System.Diagnostics;


//var builder = WebApplication.CreateBuilder(args);

////// Set up connection string

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//// Add services to the container.
//builder.Services.AddSingleton(connectionString);

//// Add CORS policy
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        builder => builder
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader());
//});

//// Add services to the container


//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<BookService>(); 
//builder.Services.AddScoped<UserServices>();
//builder.Services.AddScoped<LentService>();
//builder.Services.AddScoped<ReturnService>();

//builder.Services.AddScoped<BookRepo>(); 
//builder.Services.AddScoped<UserRepo>();
//builder.Services.AddScoped<LentRepo>();
//builder.Services.AddScoped<ReturnRepo>();



//builder.Services.AddScoped<RootOprations>();

//var app = builder.Build();
//app.UseStaticFiles();

//// Initialize the database
////var dbInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
////dbInitializer.Initialize();

//var dbInitializer = new DatabaseInitializer(connectionString);
//dbInitializer.Initialize();

//// Configure the HTTP request pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//// Enable CORS
//app.UseCors("AllowAllOrigins");

//app.UseHttpsRedirection();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();


var builder = WebApplication.CreateBuilder(args);

// Set up connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container
builder.Services.AddSingleton(connectionString);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services and repositories
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<LentService>();
builder.Services.AddScoped<ReturnService>();

builder.Services.AddScoped<BookRepo>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<LentRepo>();
builder.Services.AddScoped<ReturnRepo>();
builder.Services.AddScoped<RootOprations>();

var app = builder.Build();

// Configure the HTTP and HTTPS ports
app.Urls.Add("https://localhost:7182");  // Configure HTTPS port
app.Urls.Add("http://localhost:5000");   // Optional: HTTP fallback port

// Serve static files
app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new List<string> { "welcompage/index.html" } // Set welcome.html as the default file
});
app.UseStaticFiles();

// Initialize database
var dbInitializer = new DatabaseInitializer(connectionString);
dbInitializer.Initialize();

// Enable Swagger for both Development and Production environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

});


var url = "https://localhost:7182/welcompage/index.html"; // URL to welcome.html file
try
{
    Process.Start(new ProcessStartInfo
    {
        FileName = url,
        UseShellExecute = true
    });
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to open browser: {ex.Message}");
}



// Enable CORS
app.UseCors("AllowAllOrigins");

// Optional: Enable HTTPS Redirection
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();





//https://localhost:7182/swagger/index.html