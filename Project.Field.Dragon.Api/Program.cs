using Project.Field.Dragon.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. 加入 CORS 服務設定
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

// 2. 啟用 CORS (必須在 UseAuthorization 之前)
app.UseCors();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();