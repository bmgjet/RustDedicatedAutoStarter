using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
namespace RustAutoStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            string startupfile = "CustomMap.bat"; //Sets default bat to run.
            if (args.Length == 1) //Checks if args have been passed
                startupfile = args[0]; //Sets passed args as the startup file to run.

            //Check that starup bat exsists
            Console.WriteLine("Searching for startup bat " + Environment.CurrentDirectory + @"\" + startupfile);
            if (!File.Exists(Environment.CurrentDirectory + @"\" + startupfile))
            {
                Console.WriteLine("Startup bat not found, If no args passed then default will be CustomMap.Bat");
                Console.ReadKey();
                Environment.Exit(0);
            }
            //Sets title of window to bat it will run.
            Console.Title = Environment.CurrentDirectory  + @"\" + startupfile;
            while (true) //Makes it a constant loop
            {
                Process[] pname = Process.GetProcessesByName("RustDedicated"); //Gets all processes called RustDedicated
                //Loops though incase hosting multipal servers.
                bool found = false;
                foreach (Process p in pname)
                {
                    if (Environment.CurrentDirectory == p.MainModule.FileName.Replace("\\RustDedicated.exe",""))
                    {
                        //Found so break for loop.
                        found = true;
                        break;
                    }
                }
                //If not found then start up.
                if (!found)
                {
                    DateTime now = DateTime.Now;
                    Console.WriteLine("Rust server offline " + now);
                    Console.WriteLine("Restarting");
                    Process proc = null;
                    try
                    {
                        proc = new Process();
                        proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                        proc.StartInfo.FileName = startupfile;
                        proc.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace.ToString());
                    }
                }
                Thread.Sleep(10000); //Sleep for 10 secs
            }
        }
    }
}
