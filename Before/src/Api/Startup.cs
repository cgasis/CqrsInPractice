using Api.Utils;
using Logic.Dtos;
using Logic.Students.Commands;
using Logic.Students.Query;
using Logic.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Api
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
            services.AddMvc();

            services.AddSingleton(new SessionFactory(Configuration["ConnectionString"]));
            services.AddTransient<UnitOfWork>();
            services.AddTransient<ICommandHandler<EditPersonalInfoCommand>, EditPersonalInfoCommandHandler>();
            services.AddTransient<IQueryHandler<ListStudentsQuery, List<StudentDto>>, ListStudentQueryHandler>();
            services.AddSingleton<Messages>();
            //services.AddScoped<ICommand, EditPersonalInfoCommand>();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
            });
        }
    }
}
