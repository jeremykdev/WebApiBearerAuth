using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApiBearerAuth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.Audience = TokenService.Audience;
    opt.RequireHttpsMetadata = false; // off for development
    //opt.TokenValidationParameters.ValidIssuer = TokenService.Issuer;
    //opt.TokenValidationParameters.IssuerSigningKey;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = TokenService.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = TokenService.GetSecurityKey(),
        ValidateAudience = true,
        ValidAudience = TokenService.Audience,
        ValidateLifetime = true,
    };
});


builder.Services.AddSingleton<IEchoService, EchoService>();
builder.Services.AddSingleton<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
