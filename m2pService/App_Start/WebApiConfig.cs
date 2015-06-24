using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using m2pService.DataObjects;
using m2pService.Models;

namespace m2pService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));
            config.SetIsHosted(true);
            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new m2pInitializer());
        }
    }

    public class m2pInitializer : ClearDatabaseSchemaIfModelChanges<m2pContext>
    {
        protected override void Seed(m2pContext context)
        {
			List<Activity> activities = new List<Activity>
            {
                new Activity { Id = Guid.NewGuid().ToString(), Name = "Basket" },
                new Activity { Id = Guid.NewGuid().ToString(), Name = "Bieganie" },
                new Activity { Id = Guid.NewGuid().ToString(), Name = "Rower" },
            };

            foreach (var activity in activities)
            {
				context.Set<Activity>().Add(activity);
            }

            base.Seed(context);
        }
    }
}

