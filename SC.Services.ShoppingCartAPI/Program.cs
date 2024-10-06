using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SC.MessageBus;
using SC.Services.ShoppingCartAPI;
using SC.Services.ShoppingCartAPI.Data;
using SC.Services.ShoppingCartAPI.Extensions;
using SC.Services.ShoppingCartAPI.Service;
using SC.Services.ShoppingCartAPI.Service.IService;
using SC.Services.ShoppingCartAPI.Utility;

var builder = WebApplication.CreateBuilder(args);

#region [Setting up EF - INIT]
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region [AutoMapper] 
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region [Sharing Token]
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
#endregion

// Add services to the container.
#region [ProductService]
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpClient("Product", x => x.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:ProductAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
#endregion

#region [CouponService]
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddHttpClient("Coupon", x => x.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:CouponAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
#endregion

#region [MessageBus]
builder.Services.AddScoped<IMessageBus, MessageBus>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region [SwaggerUI PadLock Added]
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            }, new string [] { }
        }
    });
});
#endregion

#region [Authentication]
builder.AddAppAuthentication();
builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // https://localhost:7003/swagger/index.html
    if (!app.Environment.IsDevelopment())
    {
        // https://localhost:7003/index.html
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CartAPI");
        c.RoutePrefix = string.Empty;
    }
});

app.UseHttpsRedirection();
app.UseAuthentication(); // [Authentication]
app.UseAuthorization();
// app.UseStaticFiles();
app.MapControllers();
ApplyMigration(); // [EF Migration]
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