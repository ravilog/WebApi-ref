using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using MetalSouk.Provider;
using MetalSouk.App_Start;
using Microsoft.AspNet.SignalR;
using StructureMap;
using MetalSouk.DependencyResolution;
using System.Diagnostics;
using Domain.Contracts;

[assembly: OwinStartup(typeof(MetalSouk.App_Start.StartUp))]
namespace MetalSouk.App_Start 
{
    public class StartUp
    {
      
       
        public void Configuration(IAppBuilder app)
        {
            IContainer container = IoC.Initialize();
            container.AssertConfigurationIsValid();
            Debug.WriteLine(container.WhatDoIHave());                 

            HttpConfiguration config = new HttpConfiguration();
            config.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvide()
            };

            // Token Generation
         
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
        }
    }
}