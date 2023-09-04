﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(configuration =>
            {
                //Controller üzerinden Send edilen nesneye ait Command, Handler, Response ' u otomatik olarak bulması için.
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            return services;
        }
        //bu işlem için bu extension methodunu Program.cs içerisinde geçtim.
    }
}
