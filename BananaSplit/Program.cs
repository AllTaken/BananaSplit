using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace BananaSplit
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());            
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => services                        
                        .AddSingleton<Settings>()
                        .AddTransient<Scanner>()
                        .AddTransient<Processor>()
                        .AddTransient<Renamer>()
                        .AddSingleton<MainForm>()
                        .AddSingleton<LogForm>()
                        .AddSingleton<SettingsForm>()
                        .AddSingleton<QueueManager>()
                        .AddSingleton<StatusBarManager>()
                        .AddAutoMapper(typeof(MainForm).Assembly));
        }
    }
}
