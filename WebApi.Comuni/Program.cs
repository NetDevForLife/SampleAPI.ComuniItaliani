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

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.Comuni", Version = "v1" });
			options.OperationFilter<MissingSchemasOperationFilter>();
		});

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

		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Comuni v1"));

		app.UseHttpsRedirection();
		app.MapEndpoints<ComuniEndpoints>();

		app.Run();
	}
}