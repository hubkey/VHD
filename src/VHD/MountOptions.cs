using System;
using System.IO;
using HubKey.Management.HyperV;
using Mono.Options;

namespace HubKey
{
    public class MountOptions : OptionsBase
    {
        public OperationType Operation { get; set; }
        
        public override string OptionsName
        {
            get
            {
                return "Mount Options";
            }
        }

        public string Path { get; set; }

        public MountOptions()
        {
            Options = new OptionSet() {
                { "o=|operation=", "the operation (mount|unmount) to perform.", v => Operation = (OperationType)Enum.Parse(typeof(OperationType), v, true) },
                { "p=|path=", "the path to the vhd file.",  v => Path = v },
            };
        }

        protected override bool Execute()
        {
            switch (Operation)
            {
                case OperationType.Mount:
                    VHD.Mount(Path);
                    break;
                case OperationType.Unmount:
                    VHD.Unmount(Path);
                    break;
                default:
                    WriteUsage();
                    break;
            }
            return true;
        }

        public override void WriteUsage()
        {
            base.WriteUsage();
            Console.WriteLine("  Example:");
            Console.WriteLine(@"  VHD -o=mount -p=""C:\Virtual Hard Disks\example.vhd""");
        }
    }

    public enum OperationType
    {
        Mount,
        Unmount
    }
}
