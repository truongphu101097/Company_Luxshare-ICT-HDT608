using MerryDllFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MerryDllFramework_Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            MerryDll dll = new MerryDll();
            Console.WriteLine(dll.Start(new List<string>(),IntPtr.Zero));
            new List<string>()
            {
                "GetDeviceName",
            /*    "headsetAnt1Ch1",*/
                //"RightLED",
                //"DongleLED",

            }.ForEach(item =>
            {
                Console.WriteLine(item + ":" + dll.Run(item));
            });
            Console.ReadKey();

        }
    }
}
