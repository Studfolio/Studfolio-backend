using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Studfolio.FileService.Database;
using MassTransit;
using Studfolio.FileService.Broker.Consumers;
using Studfolio.FileService.Business.Interfaces;
using Studfolio.FileService.Business;
using Studfolio.FileService.Data.Interfaces;
using Studfolio.FileService.Data;
using Studfolio.FileService.Mappers.Interfaces;
using Studfolio.FileService.Database.Entities;
using Studfolio.FileService.Models.Dto;
using Studfolio.FileService.Mappers;
using FluentValidation;
using Studfolio.FileService.Validation;
using LT.DigitalOffice.Kernel;

namespace Studfolio.FileService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddDbContext<FileServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });
            services.AddControllers();

            ConfigureCommands(services);
            ConfigureMappers(services);
            ConfigureRepositories(services);
            ConfigureValidators(services);
            ConfigureMassTransit(services);
        }

        private void ConfigureMassTransit(IServiceCollection services)
        {
            const string serviceSection = "RabbitMQ";
            string serviceName = Configuration.GetSection(serviceSection)["Username"];
            string servicePassword = Configuration.GetSection(serviceSection)["Password"];

            services.AddMassTransit(x =>
            {
                x.AddConsumer<GetFileConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username($"{serviceName}_{servicePassword}");
                        host.Password(servicePassword);
                    });

                    cfg.ReceiveEndpoint(serviceName, ep =>
                    {
                        ep.ConfigureConsumer<GetFileConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<IAddNewFileCommand, AddNewFileCommand>();
            services.AddTransient<IGetFileByIdCommand, GetFileByIdCommand>();
            services.AddTransient<IDisableFileByIdCommand, DisableFileByIdCommand>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IFileRepository, FileRepository>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<DbFile, File>, FileMapper>();
            services.AddTransient<IMapper<FileCreateRequest, DbFile>, FileMapper>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<FileCreateRequest>, AddNewFileValidator>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/healthcheck");

            app.UseExceptionHandler(tempApp => tempApp.Run(CustomExceptionHandler.HandleCustomException));

            UpdateDatabase(app);

#if RELEASE
            app.UseHttpsRedirection();
#endif

            app.UseRouting();

            string corsUrl = Configuration.GetSection("Settings")["CorsUrl"];

            app.UseCors(builder =>
                builder
                    .WithOrigins(corsUrl)
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<FileServiceDbContext>();
            context.Database.Migrate();
        }
    }
}