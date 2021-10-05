using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CloudMVC
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // use this to allow command line parameters in the config
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var hostUrl = configuration["hostUrl"];
            if (string.IsNullOrEmpty(hostUrl))
                hostUrl = "http://0.0.0.0:6000";
            
            CreateHostBuilder(args).Build().Run();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(hostUrl)   // <!-- this 
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .Build();

            host.Run(); 
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // webBuilder.UseKestrel(options =>
                    // {
                    //     options.Listen(IPAddress.Any, 80);
                    //     // options.Listen(IPAddress.Loopback, port: 8080);
                    //     // options.ListenAnyIP(9090);
                    //     // options.ListenLocalhost(5005, opts => opts.UseHttps());
                    // });
                    webBuilder.UseStartup<Startup>();
                });
    }
}