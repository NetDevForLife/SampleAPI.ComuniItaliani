namespace WebApi.Comuni;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.ConfigureHttpJsonOptions(options =>
		{
			options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		});

		builder.Services.AddOpenApi();

		builder.Services.AddTransient<ILocationService, EfCoreLocationService>();
		builder.Services.AddDbContextPool<ApplicationDbContext>(optionsBuilder =>
		{
			string connectionString = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
			optionsBuilder.UseSqlite(connectionString, options =>
			{
				// Abilitazione del connection resiliency (Non è supportato dal provider di Sqlite perchè non è soggetto a errori transienti)
				// Per informazioni consultare la pagina: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
				// options.EnableRetryOnFailure(3);
			});
		});

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.MapOpenApi();
		app.UseSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/openapi/v1.json", "v1");
		});

		app.UseHttpsRedirection();
		app.MapEndpoints<ComuniEndpoints>();

		app.Run();
	}
}