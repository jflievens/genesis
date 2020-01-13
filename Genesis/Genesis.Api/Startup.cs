using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Genesis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            var storageMode = Configuration.GetValue<string>("StorageMode", "InMemory");
            if (storageMode == "EntityFramework")
            {
                services.AddDbContext<Data.EntityFramework.GenesisContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GenesisDatabase")));
                services.AddTransient<Data.Core.ICompanyRepository, Data.EntityFramework.CompanyRepository>();
                services.AddTransient<Data.Core.IContactRepository, Data.EntityFramework.ContactRepository>();
            }
            else
            {
                services.AddSingleton(new Data.Memory.Storage());
                services.AddTransient<Data.Core.ICompanyRepository, Data.Memory.CompanyRepository>();
                services.AddTransient<Data.Core.IContactRepository, Data.Memory.ContactRepository>();

            }


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Genesis API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Genesis Api");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
