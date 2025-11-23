using Project.Field.Dragon.Data;
using Microsoft.EntityFrameworkCore;
using Project.Field.Dragon.Api.Security; // 引用我們剛剛建立的安全資料夾
using Microsoft.AspNetCore.Authentication.JwtBearer; // 引用 JWT 套件
using Microsoft.AspNetCore.Authorization; // 引用授權套件

var builder = WebApplication.CreateBuilder(args);

// 1. 讀取 Auth0 設定
string authority = builder.Configuration["Auth0:Authority"] ?? throw new ArgumentNullException("Auth0:Authority");
string audience = builder.Configuration["Auth0:Audience"] ?? throw new ArgumentNullException("Auth0:Audience");

// 2. 加入 CORS 服務設定
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// 3. 加入 Authentication (身分驗證) 服務
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = authority;
    options.Audience = audience;
});

// 4. 加入 Authorization (權限授權) 服務
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("delete:catalog", policy =>
        policy.Requirements.Add(new HasScopeRequirement("delete:catalog", authority)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite("Data Source=../Registrar.sqlite",
    b => b.MigrationsAssembly("Project.Field.Dragon.Api")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
       "Project.Field.Dragon.Api v1"));
}

app.UseHttpsRedirection();

app.UseCors();

// 5. 啟用驗證與授權 (順序很重要！必須在 MapControllers 之前)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();