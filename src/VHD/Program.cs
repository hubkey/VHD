using System;
using Mono.Options;
using System.Collections.Generic;
using System.Linq;

namespace HubKey
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
#if DEBUG
                //args = new string[] { "-o=unmount", @"-p=C:\Virtual Hard Disks\example.vhd" };
#endif
                
                var helpOptions = new HelpOptions(); 

                var options = new List<IExecutable>() {
                    { helpOptions },
                    { new MountOptions() }
                };

                helpOptions.Parent = options;

                options.SingleOrDefault(option => option.Execute(args));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }

    }
}


