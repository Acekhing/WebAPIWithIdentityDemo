using WebAPIWithIdentityDemo.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext();

builder.Services.AddAuthentication();

builder.Services.AddIdentityCore(builder);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors();

//app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapControllers();

app.Run();
