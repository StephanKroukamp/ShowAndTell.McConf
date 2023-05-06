using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShowAndTell.McConf.Application.Common.Interfaces;
using ShowAndTell.McConf.Application.Prompts.GenerateVideo;
using ShowAndTell.McConf.Domain.Common.Interfaces;
using ShowAndTell.McConf.Domain.Repositories;
using ShowAndTell.McConf.Infrastructure.Persistence;
using ShowAndTell.McConf.Infrastructure.Repositories;
using ShowAndTell.McConf.Infrastructure.Services;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace ShowAndTell.McConf.Infrastructure
{
    public static class DependencyInjection
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseInMemoryDatabase("DefaultConnection");
                options.UseLazyLoadingProxies();
            });
            services.AddScoped<IUnitOfWork>(provider => provider.GetService<ApplicationDbContext>());
            services.AddTransient<IPromptRepository, PromptRepository>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddScoped<IOpenAiClient, OpenAIClient>();
            services.AddScoped<IDALLEApiClient, DALLEApiClient>();

            return services;
        }
    }
}