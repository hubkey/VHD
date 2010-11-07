using System;
using System.IO;
using HubKey.Management.HyperV;
using Mono.Options;
using System.Collections.Generic;

namespace HubKey
{
    public abstract class OptionsBase : IExecutable
    {
        public OptionSet Options { get; set; }
        public bool Help { get; set; }
        public abstract string OptionsName { get; }
        public List<IExecutable> Parent { get; set; }

        public OptionsBase()
        {
        }

        protected virtual bool Execute()
        {
            return true;
        }


        public bool Execute(string[] args)
        {
            try
            {
                if (Options.Parse(args).Count > 0)
                    throw new ArgumentOutOfRangeException();
            }
            catch(ArgumentOutOfRangeException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            if (Help)
            {
                WriteUsage();
                return true;
            }

            return Execute();
        }

        public virtual void WriteUsage()
        {
            Console.WriteLine("{0}:", OptionsName);
            Options.WriteOptionDescriptions(Console.Out);
        }
    }

    public interface IExecutable
    {
        void WriteUsage();
        bool Execute(string[] args);
    }

}
