using MobileDevice;
using MobileDevice.Event;
using System;
using System.IO;
using System.Threading;

using System.Collections.ObjectModel;


namespace Oc34n_OneClickTool
{
    public class iDeviceInformation
    {
 
        static string[] empty = { "", "", "", "", "","" };
        private iOSDeviceManager manager = new iOSDeviceManager();
        private iOSDevice currentiOSDevice;
        public string[] RetrieveInformation()
        {
            string[] DeviceInfo = empty;
            if (currentiOSDevice != null && currentiOSDevice.IsConnected)
            {
                DeviceInfo[0] = currentiOSDevice.UniqueDeviceID;
                DeviceInfo[1] = currentiOSDevice.ProductType;
                DeviceInfo[2] = currentiOSDevice.SerialNumber;
                DeviceInfo[3] = currentiOSDevice.InternationalMobileEquipmentIdentity;
                DeviceInfo[4] = currentiOSDevice.ProductVersion;
                DeviceInfo[5] = currentiOSDevice.DeviceName;
                DeviceInfo[6] = currentiOSDevice.SIMStatus;
            }
            return DeviceInfo;
        }

        public string CheckActivationState()
        {
            string ActivationState = currentiOSDevice.ActivationState;
            return ActivationState;
        }
      
     
    }
    }