using Core.Business;
using Core.Common;
using Core.Data;
using Core.TimeJob;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public static class QREntry
    {
        public static void UseQR(this IServiceCollection services)
        {
            Singleton<DataManager>.Create();
            Singleton<BusinessManager>.Create();
            //Singleton<TimeJobManager>.Create();

            Singleton<DataManager>.Instance.ServiceCollection(services);
            Singleton<BusinessManager>.Instance.ServiceCollection(services);
            //Singleton<TimeJobManager>.Instance.ServiceCollection(services);
        }

        public static void UseQR(this IApplicationBuilder app, IHostingEnvironment env, IServiceProvider context)
        {
            Singleton<DataManager>.Instance.ApplicationBuilder(app, env, context);
            Singleton<BusinessManager>.Instance.ApplicationBuilder(app, env, context);
            //Singleton<TimeJobManager>.Instance.ApplicationBuilder(app, env, context);
        }
    }
}
