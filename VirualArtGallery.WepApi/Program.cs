using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VirtualArtGallery.Bussiness.DataProtection;
using VirtualArtGallery.Bussiness.Operations.Artwork;
using VirtualArtGallery.Bussiness.Operations.Exhibition;
using VirtualArtGallery.Bussiness.Operations.User;
using VirtualArtGallery.Bussiness.Setting;
using VirtualArtGallery.WepApi.Middlewares;
using VirtualArtGalleryProject.Data.Context;
using VirtualArtGalleryProject.Data.Repository;
using VirtualArtGalleryProject.Data.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options=>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Jwt Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put**_ONLY_** your JWT Bearer Token on Texbox below!",


        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>() },
    });

});
builder.Services.AddScoped<IDataProdection, DataProtection>();
var keysDicretory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));

builder.Services.AddDataProtection()
    .SetApplicationName("VirtualArtGallery")
    .PersistKeysToFileSystem(keysDicretory);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],



            ValidateLifetime = true,


            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };



    });


var cn = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<VirtualArtGalleryProjectDbContext>(options=>options.UseSqlServer(cn));


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));//Generic olduðu için typof ile çevirisi yapýlýr  
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IArtworkService,ArtworkManager>();
builder.Services.AddScoped<IExhibitionService,ExhibitionManager>();
builder.Services.AddScoped<ISettingService, SettingManager>();




var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddlewareMode();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
