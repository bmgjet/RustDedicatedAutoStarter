using System;
using System.Diagnostics;
using System.Threading;

namespace RustAutoStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Process[] pname = Process.GetProcessesByName("RustDedicated");
                if (pname.Length == 0)
                {
                    DateTime now = DateTime.Now;
                    Console.WriteLine("Rust server offline " + now);
                    Console.WriteLine("Restarting");
                    Process proc = null;
                    try
                    {
                        proc = new Process();
                        proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                        proc.StartInfo.FileName = "CustomMap.bat";
                        proc.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace.ToString());
                    }
                }
                Thread.Sleep(10000);
            }
        }
    }
}
