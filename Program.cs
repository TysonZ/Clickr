using System;
using System.Threading;
using System.Runtime.InteropServices; 
using System.ComponentModel.DataAnnotations;


namespace clickr
{
    public class Program
    {
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        public static void DoMouseClick(int holdTime)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(holdTime);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("#=== Randomised Auto Clicker by James");
            Console.WriteLine("#");
            Console.WriteLine("#--- Generates a random time between min time and max time per click on a right skew bell curve");
            Console.WriteLine("#--- to simulate randomness.");
            Console.WriteLine("#");
            Thread clickerThread = new Thread(Mouseclicker);
            clickerThread.Start();            
        }

        static void Mouseclicker(){
            var rand = new Random();

            Console.WriteLine("# Enter min time to click (s): ");
            Console.Write("# : ");
            int hardBottom = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("# Enter max time to click (s): ");
            Console.Write("# : ");
            int hardMax = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("# Clicking randomly between " + hardBottom + " s and " + hardMax + " s.");
            Console.WriteLine("# Enter 1 for Bell , 2 for Linear (descent) distribution");
            int randomType = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("# Press any key to begin.");
            Console.ReadKey();
            //Calc average click time

            double i = 0;
            int x = 0;
            int rand1 = 0;
            int rand2 = 0;
            int randomSleep = 0;
            double holdTimeRand = 0;
            int holdTime = 0;
            int clickCount = 1;

            //Thread Logic Loop;
            while(true){

                i = rand.NextDouble() * (hardMax - hardBottom) + hardBottom;
                x = Convert.ToInt32(i * 1000);
                rand1 = x;
                i = rand.NextDouble() * (hardMax - hardBottom) + hardBottom;
                x = Convert.ToInt32(i * 1000);
                rand2 = x;
                
                holdTimeRand = rand.NextDouble() * (0.058 - 0.027) + 0.027;

                holdTime = Convert.ToInt32(holdTimeRand * 1000);

                switch(randomType){
                    case 1:
                        //Makes a perfect bell curve.
                        randomSleep = ((rand1 + rand2)/2);

                    break;
                    case 2:
                        //Lowest Random (Linear)
                        if(rand1 > rand2){
                            randomSleep = rand2;
                        } else{
                            randomSleep = rand1;
                        }
                    break;

                }
                
                Console.WriteLine("# Click #" + clickCount + " Sleep: " + randomSleep + " ms | click hold time: " + holdTime + " ms.");

                //Console.WriteLine(randomSleep);
                Thread.Sleep(randomSleep);
                
                //Initiate Mouse click function;
                DoMouseClick(holdTime);
                clickCount++;
                //Console.WriteLine("#---Click!");
            }
        }
    }
}
