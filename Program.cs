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
            CreateWebHostBuilder(args)
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5000);

                    var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

                    if (isUnix)
                        options.ListenUnixSocket(Path.Combine(Environment.GetEnvironmentVariable("TMP"), "nginx.socket"));
                })
                .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
