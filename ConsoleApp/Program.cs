using System;
using AmplitudeSharp;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            AmplitudeService analytics = AmplitudeService.Initialize("6f54da3af189d599fc702debb50c9508");

            // Setup our user properties:
            UserProperties userProps = new UserProperties();
            userProps.UserId = "test_user_1";
            userProps.ExtraProperties.Add("email", "testuser1@example.com");
            // Note that userProps.AppVersion is automatically inferred from the
            // version of the calling assembly, but can be overriden here, e.g:
            // userProps.AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // Setup our device properties:
            DeviceProperties devProps = new DeviceProperties();
            // It's a good idea to set a device ID, that way you can tell how many devices
            // a give user uses. Best way is to generate a device ID on first start and stash it
            // in the settings of the app
            devProps.DeviceId = Guid.NewGuid().ToString();
            analytics.Identify(userProps, devProps);
            //AmplitudeService.Instance.Identify(userProps, devProps);

           for(int i=0; i<20; i++)
            {
                Console.WriteLine("Sending event ..");
                //AmplitudeService.Instance.Track("test_event", new { count = i, category = "count" });
                analytics.Track("test_event", new { count = i, category = "count" });
            }


            Console.WriteLine("Sleeping for 5 minutes so that background thread that send event can finish");
            var stopwatch = Stopwatch.StartNew();
            Thread.Sleep(30000);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
