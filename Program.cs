using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace puppet_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            if (isUnix)
            {
                var webHostBuilder = CreateWebHostBuilder(args)
                    .UseLibuv()
                    .UseKestrel(options =>
                    {
                        options.ListenUnixSocket("/tmp/nginx.socket");
                    })
                    .Build();

                var initializedFile = Path.Combine("/tmp/app-initialized");
                if (!File.Exists(initializedFile))
                    File.Create(initializedFile).Close();

                webHostBuilder.Run();
            }
            else
            {
                CreateWebHostBuilder(args)
                    .UseKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 5000);
                    })
                    .Build().Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
