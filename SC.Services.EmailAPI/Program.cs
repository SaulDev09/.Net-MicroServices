using Microsoft.EntityFrameworkCore;
using SC.Services.EmailAPI.Data;
using SC.Services.EmailAPI.Extension;
using SC.Services.EmailAPI.Messaging;
using SC.Services.EmailAPI.Services;

var builder = WebApplication.CreateBuilder(args);

#region [Setting up EF - INIT]
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region [Service Bus]
var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(new EmailService(optionBuilder.Options));
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
#endregion

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // https://localhost:7299/swagger/index.html
    if (!app.Environment.IsDevelopment())
    {
        // https://localhost:7299/index.html
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmailAPI");
        c.RoutePrefix = string.Empty;
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
ApplyMigration(); // Added
app.UseAzureServiceBusConsumer();
app.Run();

#region [EF Migration]
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
#endregion