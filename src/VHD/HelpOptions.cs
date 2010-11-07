using System;
using System.IO;
using HubKey.Management.HyperV;
using Mono.Options;
using System.Collections.Generic;

namespace HubKey
{
    public class HelpOptions : OptionsBase
    {
        public override string OptionsName { get { return "Help Options"; } }

        public HelpOptions()
        {
            Options = new OptionSet() {
                { "h|?|help",  "show this message and exit.", v => Help = true }
            };
        }

        protected override bool Execute()
        {
            return true;
        }

        public override void WriteUsage()
        {
            Console.WriteLine("VHD.exe [OPTIONS]");
            base.WriteUsage();
            Parent.ForEach(o =>
            {
                if (o != this)
                    o.WriteUsage();
            });
        }
    }

}
