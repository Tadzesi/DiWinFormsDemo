using DiWinFormsDemo.Custom;
using DiWinFormsDemo.Interfaces;
using DiWinFormsDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiWinFormsDemo
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }


        public static IServiceProvider? ServiceProvider { get; private set; }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IBgCounterService, BgWorkerCounterService>();
                    services.AddSingleton<ITaskCounterService, TaskCounterService>();
                    services.AddSingleton<Form1>();
                    services.AddSingleton<BgCounterDisplay>();
                    services.AddSingleton<TaskCounterDisplay>();
                });
        }
    }
}
