using Kardinal.Net.Web.Samples.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Kardinal.Net.Web.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder()
                .UseKardinal("Sample App", KardinalVersion.Parse(1, 0), args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kardinal.Net.Web.Samples", Version = "v1" });
            });

            builder.Services.AddDataProtection(builder.Configuration);

            builder.Services.AddDbContext<SampleDbContext>(x => x.UseInMemoryDatabase("TestDB"))
                .AddUnitOfWork<SampleDbContext>()
                .AddUnitOfWork()
                .AddRepositories();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kardinal.Net.Web.Samples v1"));
            }


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplyMigrations<SampleDbContext>();
            app.RunAsync().Wait();
            //Initialize<Startup>(KardinalVersion.Parse(1, 0), "Sample App", args);
        }
    }
}
