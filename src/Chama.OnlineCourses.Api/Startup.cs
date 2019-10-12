﻿using AutoMapper;
using Chama.OnlineCourses.Api.Infrastructure;
using Chama.OnlineCourses.Api.V1.Commands;
using Chama.OnlineCourses.Api.V1.Validators.Commands;
using Chama.OnlineCourses.Infrastructure;
using Chama.OnlineCourses.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chama.OnlineCourses.Api
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
            services.Configure<CosmosDbSettings>("CosmosDb", Configuration);

            services.AddApplicationInsightsTelemetry();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation();

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddAutoMapper(typeof(Startup).Assembly);

            ConfigureValidators(services);
            ConfigureRepositories(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Chama Online Courses API", Version = "v1" });
            });
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<RegisterStudentCommand>, RegisterStudentCommandValidator>();
        }
        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICosmosDbContext, CosmosDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chama Online Courses API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}