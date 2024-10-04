using Library_Api_Sqlite.Data;
using Library_Api_Sqlite.FileSaver;
using Library_Api_Sqlite.Repository;
using Library_Api_Sqlite.Services;


var builder = WebApplication.CreateBuilder(args);

//// Set up connection string

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
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

// Add services to the container


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<BookService>(); // Register your book service
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<BookRepo>(); // Register your book repository
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<SaveToRoot>();

var app = builder.Build();

// Initialize the database
//var dbInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
//dbInitializer.Initialize();
var dbInitializer = new DatabaseInitializer(connectionString);
dbInitializer.Initialize();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();




//using Library_Api_Sqlite.Data;

//var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//// Add services to the container.
//builder.Services.AddSingleton(connectionString);

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();
//app.UseStaticFiles();
//// Initialize the database
//var dbInitializer = new DatabaseInitializer(connectionString);
//dbInitializer.Initialize();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


