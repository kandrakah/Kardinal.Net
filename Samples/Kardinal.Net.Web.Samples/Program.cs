using Kardinal.Net.Web.Auth;
using Kardinal.Net.Web.Data;
using Kardinal.Net.Web.Samples.Data;
using Kardinal.Net.Web.Samples.Endpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kardinal.Net.Web.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder()
                .UseKardinal("Sample App", KardinalVersion.Parse(1, 0), args);

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());                    
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.None;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kardinal.Net.Web.Samples", Version = "v1" });
            });

            builder.Services.AddAuthServer();

            //builder.Services.AddDataProtection<SampleDbContext>(builder.Configuration);

            builder.Services.AddDbContext<SystemDbContext>(x => x.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Kardinal;Integrated Security=true;"))
                .AddUnitOfWork<SystemDbContext>()
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
