using Garage_Finder_Backend.Services.AuthService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Repositories;
using Garage_Finder_Backend;
using Twilio;
using Services.PhoneVerifyService;
using Microsoft.OpenApi.Models;
using Services;
using Services.WebSocket;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAnyOrigins",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});
builder.Services.AddControllers();
builder.Services.ConfigRepository();
builder.Services.ConfigServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });

    c.AddSignalRSwaggerGen();
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
JwtBearerOptions  jwtBearerOptions = new JwtBearerOptions()
{
    RequireHttpsMetadata = false,
    TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ClockSkew = TimeSpan.Zero
    }
};
builder.Services.AddSingleton<JwtBearerOptions>(jwtBearerOptions);
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = jwtBearerOptions.RequireHttpsMetadata;
        options.TokenValidationParameters = jwtBearerOptions.TokenValidationParameters;
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/UserGF")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var accountSid = builder.Configuration["Twilio:AccountSID"];
var authToken = builder.Configuration["Twilio:AuthToken"];
TwilioClient.Init(accountSid, authToken);
builder.Services.Configure<TwilioVerifySettings>(builder.Configuration.GetSection("Twilio"));

builder.Services.AddTransient<IPhoneVerifyService, TwilioService>();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
//app.UseWebSockets();
//var webSocketOptions = new WebSocketOptions
//{
//    KeepAliveInterval = TimeSpan.FromMinutes(2)
//};

//app.UseWebSockets(webSocketOptions);
app.MapHub<UserGFHub>("/UserGF");

app.UseCors("AllowAnyOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
