using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebAPI.Migrations;
using WebAPI.Models;

namespace WebAPI {
    public class Program {
        public static void Main (string[] args) {
            // BuildWebHost(args).Run();
            var host = BuildWebHost (args);

            host.Run ();
        }

        public static IWebHost BuildWebHost (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ()
            .UseSetting("https_port","8080")
            .Build ();
    }
}