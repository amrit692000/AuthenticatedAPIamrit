using APIauthent.Dbcontext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppdataContext>(
        options => options.UseInMemoryDatabase("db od data")
);

builder.Services.AddDbContext<AppsecurityContext>(
      options => options.UseInMemoryDatabase("db of securtiy")
);
builder.Services.AddAuthorization();
builder.Services.AddIdentiyApiEndpoints<Identityuser>();
      .AddEntityFrameworkStores<AppsecurityContext>();

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapIdentityApi<Identityuser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
