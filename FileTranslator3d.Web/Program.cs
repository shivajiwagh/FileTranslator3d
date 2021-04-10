using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FileTranslator3d.Web
{
    /// <summary>
    /// Program - class
    /// </summary>
    public class Program
    {
        #region Member Functions

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the host builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        #endregion
    }
}