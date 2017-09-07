using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    /*
     * in order of importance
     * use switch web when prompted
     * 
     * - Ensure response does get written into file from web response without error
     * - Convert  System.IO.File.WriteAllText into own class 
     * - Write unit test for IWebNotification
     * - Improve WebNotification code structure
     * - Remove unused code
     * Use log section
     * - Rewrite log section to better practices and print only one line with message
     * Important you must do
     * - Finish off by submitting PR(pull request)
    */

    class Program
    {
        static void Main(string[] args)
        {
            var container = CreateAutofacContainer();

            var logger = container.Resolve<ILogger>();
            logger.LogSystemMessage("Application has started.. enter run, exit");

            var jobTime = Stopwatch.StartNew();

            do
            {
                ShortPause();

                Console.WriteLine();
                logger.LogSystemMessage("enter a command and hit enter");

                var command = Console.ReadLine().ToLowerInvariant().Trim();

                if (command.StartsWith("run"))
                {
                    logger.LogSystemMessage("run command executed");
                }

                if (command.StartsWith("log"))
                {
                    var allPrinted = false;
                    do
                    {
                        var ActorsName = "Mr Dolittle";
                        var message = "Really I had that general reaction ";
                        message += "(unexpectedly charmed and amused) ";
                        logger.LogSystemMessage(message);
                        message = "to most of the movie. " + ActorsName + "I went in with very low expectations, |";
                        logger.LogSystemMessage(message);
                        allPrinted = false;
                    } while (allPrinted);
                }

                if (command.StartsWith("web"))
                {
                    new WebNotification(logger, new Settings()).Notify(".net core");
                }

                if (command.StartsWith("exit"))
                {
                    jobTime.Stop();

                    Console.WriteLine("Job complete in {0}ms ", jobTime.ElapsedMilliseconds);
                    logger.LogSystemMessage("system shutdown. Press any key to exit...");
                    Console.ReadKey();

                    Environment.Exit(1);
                }

            } while (true);
        }

        private static Autofac.IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            
            return builder.Build();
        }

        public static void ShortPause()
        {
            System.Threading.Thread.Sleep(1000);
        }
    }
}
