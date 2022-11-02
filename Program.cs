using asp_net_core.Data;
using asp_net_core.Interfaces;
using asp_net_core.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>((serviceProvider) => {
	var dapperContext = new DapperContext(builder.Configuration);
	var tableAction = args.GetLength(0) > 0 ? args.GetValue(0)!.ToString()!.ToLower() : "nothing";

	if (tableAction == "create") {
		dapperContext.CreateTables().Wait();
	}

	if (tableAction == "recreate") {
		dapperContext.RecreateTables().Wait();
	}

	return dapperContext;
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
