using ABCIgnite.datab;
using ABCIgnite.Repository;
using ABCIgnite.Repository.Interface;
using ABCIgnite.Service;
using ABCIgnite.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ABCIgnite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Initialize DB Context
           services.AddDbContext<AbcclientDatabaseContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(MappingProfile));

            // Initialize Repositories
          services.AddScoped<IABCRepository, ABCRepository>();


            // Initilize Services
           services.AddScoped<IABCService, ABCService>();
           
            // Add controllers first
            services.AddControllers();

        
          
            // Swagger Configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ABCIgnite.API", Version = "v1" });

                            
            });


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ABCIgnite.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            // Enable Authentication Middleware
           // app.UseAuthentication();
           // app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
