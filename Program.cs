var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) => {
	Console.WriteLine("First middleware (before)");
	await next();
	Console.WriteLine("First middleware (after)");
});

app.Use(async (context, next) => {
	Console.WriteLine("Second middleware (before)");
	await next();
	Console.WriteLine("Second middleware (after)");
});

app.Map("/api/Auth/foo", (builder) => {
	builder.Use(async (context, next) => {
		Console.WriteLine("Foo middleware (before)");
		await next();
		Console.WriteLine("Foo middleware (after)");
	});
});

app.UseWhen(
	(context) => {
		return context.Request.Path.StartsWithSegments("/api/Auth/bar");
	},
	(builder) => {
		builder.Use(async (context, next) => {
			Console.WriteLine("Bar middleware (before)");
			await next();
			Console.WriteLine("Bar middleware (after)");
		});
	}
);

app.Run(async (context) => {
	Console.WriteLine("Final middleware");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
