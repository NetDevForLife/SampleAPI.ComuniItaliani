namespace WebApi.Comuni;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.Comuni", Version = "v1" });
		});

		builder.Services.AddDbContextPool<ApplicationDbContext>(optionsBuilder =>
		{
			string connectionString = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
			optionsBuilder.UseSqlite(connectionString, options =>
			{
				// Abilitazione del connection resiliency (Non è supportato dal provider di Sqlite perchè non è soggetto a errori transienti)
				// Per informazioni consultare la pagina: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
				// options.EnableRetryOnFailure(3);
			});
		})
			.AddTransient<ILocationService, EfCoreLocationService>()
			.Configure<RouteOptions>(options => options.LowercaseUrls = true);

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage()
				.UseSwagger()
				.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Comuni v1"));
		}

		app.UseHttpsRedirection();
		app.UseRouting();

		app.MapControllers();
		app.Run();
	}
}