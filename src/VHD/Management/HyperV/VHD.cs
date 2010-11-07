//see http://msdn.microsoft.com/en-us/library/cc136845%28v=VS.85%29.aspx

using System;
using System.Management;

namespace HubKey.Management.HyperV
{
    public static class VHD
    {

        public static void Mount(string path)
        {
            ManagementScope scope = new ManagementScope(@"root\virtualization", null);
            ManagementObject imageService = Utility.GetServiceObject(scope, "Msvm_ImageManagementService");

            ManagementBaseObject inParams = imageService.GetMethodParameters("Mount");
            inParams["Path"] = path;
            ManagementBaseObject outParams = imageService.InvokeMethod("Mount", inParams, null);
            if ((UInt32)outParams["ReturnValue"] == ReturnCode.Started)
            {
                if (Utility.JobCompleted(outParams, scope))
                {
                    Console.WriteLine("{0} was mounted successfully.", inParams["Path"]);
                }
                else
                {
                    Console.WriteLine("Unable to mount {0}", inParams["Path"]);
                }
            }
            inParams.Dispose();
            outParams.Dispose();
            imageService.Dispose();
        }

        public static void Unmount(string path)
        {
            ManagementScope scope = new ManagementScope(@"root\virtualization", null);
            ManagementObject imageService = Utility.GetServiceObject(scope, "Msvm_ImageManagementService");

            ManagementBaseObject inParams = imageService.GetMethodParameters("Unmount");
            inParams["Path"] = path;
            ManagementBaseObject outParams = imageService.InvokeMethod("Unmount", inParams, null);
            if ((UInt32)outParams["ReturnValue"] == ReturnCode.Completed)
            {
                Console.WriteLine("{0} was unmounted successfully.", inParams["Path"]);
            }
            else
            {
                Console.WriteLine("Unmount operation for {0} failed with error {1}.", path, outParams["ReturnValue"]);
            }

            inParams.Dispose();
            outParams.Dispose();
            imageService.Dispose();
        }

    }
}
