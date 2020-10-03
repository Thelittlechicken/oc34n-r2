using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using System.IO;
using System.Diagnostics;
using MetroFramework;
using MetroFramework.Forms;
using System.ComponentModel;
using System.Drawing;
using Renci.SshNet;
using RestSharp;
using MobileDevice;
using Newtonsoft.Json;

namespace Oc34n_OneClickTool
{
    public class Activation : commands
    {
        private byte[] untethered = Convert.FromBase64String(B64.GetFile("untethered"));
        private byte[] untetheredplist = Convert.FromBase64String(B64.GetFile("untetheredplist"));
        private byte[] iuntethered = Convert.FromBase64String(B64.GetFile("iuntethered"));
        private byte[] iuntetheredplist = Convert.FromBase64String(B64.GetFile("iuntetheredplist"));
        private byte[] purplebuddy = Convert.FromBase64String(B64.GetFile("purplebuddy"));
        private byte[] uikit = Convert.FromBase64String(B64.GetFile("uikit"));
        private byte[] raptorbinary = Convert.FromBase64String(B64.GetFile("RaptorBinary"));
        private byte[] lzma = Convert.FromBase64String(B64.GetFile("lzma"));
        private byte[] plutil = Convert.FromBase64String(B64.GetFile("plutil"));
        //private byte[] mina = Convert.FromBase64String(B64.GetFile("mina"));
        private byte[] mobileactivationdplist = Convert.FromBase64String(B64.GetFile("mobileactivationdplist"));
        private byte[] emptyplist = Convert.FromBase64String(B64.GetFile("emptyplist"));
        private byte[] commcenterDS = Convert.FromBase64String(B64.GetFile("commcenterDS"));
        private byte[] restore = Convert.FromBase64String(B64.GetFile("restore"));
        private byte[] aservice = Convert.FromBase64String(_2b64.GetFile("aservice"));
        private byte[] substrate = Convert.FromBase64String(_2b64.GetFile("simple"));
        private byte[] ActivateMe = Convert.FromBase64String(B64.GetFile("ActivateMe"));
        private byte[] ActivateMePlist = Convert.FromBase64String(B64.GetFile("ActivateMePlist"));
        private iOSDeviceManager manager = new iOSDeviceManager();
        // private iOSDevice currentiOSDevice;
        ScpClient scpClient = new ScpClient("127.0.0.1", "root", "alpine");
        internal static bool debug = false;
        private bool PhoneHasMeid;
        public bool FactoryActivation(iOSDevice currentiOSDevice)
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            bool headless = frm1.headless;
            PhoneHasMeid = true;
            StartIproxy();
            try {
                string guid = GetSystemPath("");
                if (CheckMEID() == false && frm1.DisableBBBox.Checked == true) {
                    frm1.BoxShow("GSM Device detected!, disabling baseband on these devices is experimental and can cause some side effects like Battery draining", "Warning", 3500);
                    PhoneHasMeid = false;
                }
                while (iDevicePair("pair") == false)
                {
                    frm1.BoxShow("Unlock Phone and press Trust", "Device not paired", 5000);
                }
                iDevicePair("validate");
                SSHExecute("mount -o rw,union,update /");
                //ScpClient scpClient = new ScpClient(host, user, pass);
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 4));
                try
                {
                    scpClient.Connect();
                } catch
                {
                    return false;
                }
                //pushing certificate
                try
                {
                    scpClient.Upload(new MemoryStream(raptorbinary), "/System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem");

                } catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Raptor binary not found");
                    return false;
                }
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 8));

                //renaming snapshot
                SSHExecute("snappy -f / -r `snappy -f / -l | sed -n 2p` -t orig-fs");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 15));
                //Installing Substrate
                if (frm1.SubstrateBox.Checked == false)
                {
                    Substrate("Install");
                }
                //Setting Wallpaper
                Module();
                //Installing UIKit
                scpClient.Upload(new MemoryStream(uikit), "/uikit.tar");
                SSHExecute("cd / && tar -xvf uikit.tar && rm uikit.tar");
                ///SSHExecute("chmod 755 /usr/bin/*");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 22));
                //Installing plutil
                scpClient.Upload(new MemoryStream(plutil), "/usr/bin/plutil");
                SSHExecute("chmod 755 /usr/bin/*");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 31));
                //Enabling Baseband..
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.CommCenterRootHelper.plist");
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.CommCenterMobileHelper.plist");
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.CommCenter.plist");
                Mv("/usr/local/standalone/firmware/Baseband2", "/usr/local/standalone/firmware/Baseband");
                // SSHExecute("chflags -R nouchg /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 41));
                //Unflagging settings
                SSHExecute("chflags -R nouchg /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                //Installing Dylibs
                SSHExecute("launchctl unload /System/Library/LaunchDaemons/*");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 49));
                try
                {
                    scpClient.Upload(new MemoryStream(untethered), "/Library/MobileSubstrate/DynamicLibraries/untethered.dylib");
                    scpClient.Upload(new MemoryStream(untetheredplist), "/Library/MobileSubstrate/DynamicLibraries/untethered.plist");
                    SSHExecute("chmod 755 /Library/MobileSubstrate/DynamicLibraries/untethered.dylib");
                    frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 52));

                    //Sending Settings
                    if (frm1.SkipSetupBox.Checked == true)
                    {
                        scpClient.Upload(new MemoryStream(purplebuddy), "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                        SSHExecute("chmod 600 /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                        SSHExecute("uicache --all && chflags uchg /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                    }
                    SSHExecute("launchctl load /System/Library/LaunchDaemons/*");
                    frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 58));

                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to send dylibs:" + e.Message);
                    return false;
                }
                //iDevicepair();
                Activate();
                if (currentiOSDevice.ActivationState == "Unactivated") { Ldrestart(); }
                if (currentiOSDevice.ActivationState == "Unactivated") { throw new ApplicationException("The device didnt activate"); }
                SSHExecute("ls");SSHExecute("ls");SSHExecute("ls");
                BackupRecords(currentiOSDevice, guid);
                SSHExecute("rm /Library/MobileSubstrate/DynamicLibraries/untethered.dylib");
                SSHExecute("rm /Library/MobileSubstrate/DynamicLibraries/untethered.plist");
                //ScpClient scpClient = new ScpClient(host, user, pass);
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 65));
                try
                {
                    scpClient.Upload(new MemoryStream(iuntethered), "/Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                    scpClient.Upload(new MemoryStream(iuntetheredplist), "/Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
                    SSHExecute("chmod 755 /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to send dylibs:" + e.Message);
                    return false;
                }
                ldrestart();
                SSHExecute("ls");
                SSHExecute("ls");
                SSHExecute("ls");
                SSHExecute("rm /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                SSHExecute("rm /Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
                //frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 74));

                //Remove Substrate washere
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 100));

                scpClient.Disconnect();
                iDeviceInformation mobile = new iDeviceInformation();
                string state;
                // bool except = false;
                iDevicePair("pair");
                try
                {
                    state = currentiOSDevice.ActivationState;
                } catch
                {
                    state = "Unknown";
                }
                if (state != "Unactivated")
                {
                    PostActivation();
                    Substrate("RemoveGif");
                    ReplaceString("/System/Library/PrivateFrameworks/SystemStatusServer.framework", "SystemStatusServer.strings", "LOCKED_SIM", "OC34N");
                    frm1.BoxShow("Done", "Successfully Activated!", 6000);
                } else
                {
                    Substrate("RemoveGif");
                    MessageBox.Show("Error, Device is Unactivated");
                }

                if (frm1.RebootBox.Checked == true)
                {
                    SSHExecute("launchctl reboot userspace");
                    //Reboot();
                }
                KillIproxy();
                return true;
            } catch(Exception e)
            {
                frm1.BoxShow("Failed to activate device reason:" + e.Message, "Error", 5000);
                return false;
            }
        }
        public bool OfflineFactory(iOSDevice currentiOSDevice)
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            StartIproxy();
            try
            {
                string SN = currentiOSDevice.SerialNumber;
                string guid = GetSystemPath("");
                Mount();
                if (!frm1.SubstrateBox.Checked) { Substrate("Install"); }
                if (!scpClient.IsConnected) { scpClient.Connect(); }
                //UikitInstall
                scpClient.Upload(new MemoryStream(uikit), "/uikit.tar");
                SSHExecute("cd / && tar -xvf uikit.tar && rm uikit.tar");
                scpClient.Upload(new MemoryStream(plutil), "/usr/bin/plutil");
                Chmod(755, "/usr/bin/*");
                pi(40);
                //Activation
                mkdir("/private/var/containers/Data/System/" + guid + "/Library/activation_records/");
                //Fxing path
                string Source = path + "\\Data\\" + SN + "\\activation_record.plist";
                SendFile(Source, "/private/var/containers/Data/System/" + guid + "/Library/activation_records/activation_record.plist");
                //
                if (!scpClient.IsConnected) { scpClient.Connect(); }
                scpClient.Upload(new MemoryStream(iuntethered), "/Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                scpClient.Upload(new MemoryStream(iuntetheredplist), "/Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
                scpClient.Disconnect();
                pi(40);
                SkipSetup();
                Ldrestart();
                Rm("/Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                Rm("/Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
                if (!frm1.SubstrateBox.Checked) { Substrate("remove"); }
                pi(20);
                while (!iDevicePair("pair"))
                {
                    frm1.BoxShow("Unlock and press Trust on the phone", "Important", 4000);
                };
                if (currentiOSDevice.ActivationState != "Unactivated")
                {
                    PostActivation();
                    frm1.BoxShow("Done", "Activation completed", 5000);
                } else
                {
                    frm1.BoxShow("Failed to Activate Device", "Error",5000);
                }
                return true;
            } catch (Exception e)
            {
                frm1.BoxShow("Failed to Activate Device:"+ e.Message, "Error", 8000);
                return false;
            }
        }
        public bool AfterFactory()
        {

            return true;
        }
        public bool WildCardActivation(iOSDevice currentiOSDevice)
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            bool headless = frm1.headless;
            try
            {
                //Pairing
                while (iDevicePair("pair") == false)
                {
                    if (headless != true) { MessageBox.Show("Unlock phone and press Trust if asked", "Important", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else
                    {
                        AutoClosingMessageBox.Show(
                     "Unlock phone and press Trust if asked", "Important", 7000, MessageBoxButtons.OK
                     );
                    }
                }
                iDevicePair("validate");
                string guid = GetSystemPath("");
                StartIproxy();
                Mount();
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 4));
                try
                {
                    scpClient.Connect();
                }
                catch
                {
                    return false;
                }
                //pushing certificate
                try
                {
                    scpClient.Upload(new MemoryStream(raptorbinary), "/System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem");

                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Raptor binary not found");
                    return false;
                }
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 8));
                SnappyRename();
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 15));
                Substrate("Install");
                //Installing UIKut
                scpClient.Upload(new MemoryStream(uikit), "/uikit.tar");
                SSHExecute("cd / && tar -xvf uikit.tar && rm uikit.tar");
                ///SSHExecute("chmod 755 /usr/bin/*");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 22));
                //Installing plutil
                scpClient.Upload(new MemoryStream(plutil), "/usr/bin/plutil");
                SSHExecute("chmod 755 /usr/bin/*");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 31));
                //Deactivating Device
                UnflagActivation(guid);
                DeleteActivation(guid);
                //Enabling Baseband..
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.CommCenterRootHelper.plist");
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.CommCenterMobileHelper.plist");
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.CommCenter.plist");
                SSHExecute("chflags -R nouchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 41));
                //Unflagging settings
                SSHExecute("chflags -R nouchg /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                //Installing Dylibs
                SSHExecute("launchctl unload /System/Library/LaunchDaemons/*");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 49));
                try
                {
                    scpClient.Upload(new MemoryStream(untethered), "/Library/MobileSubstrate/DynamicLibraries/untethered.dylib");
                    scpClient.Upload(new MemoryStream(untetheredplist), "/Library/MobileSubstrate/DynamicLibraries/untethered.plist");
                    SSHExecute("chmod 755 /Library/MobileSubstrate/DynamicLibraries/untethered.dylib");
                    frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 52));

                    //Sending Settings
                    if (frm1.SkipSetupBox.Checked == true)
                    {
                        scpClient.Upload(new MemoryStream(purplebuddy), "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                        SSHExecute("chmod 600 /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                        SSHExecute("uicache --all && chflags uchg /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                    }
                    SSHExecute("launchctl load /System/Library/LaunchDaemons/*");
                    frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 58));

                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to send dylibs:" + e.Message);
                    return false;
                }
                BBActivate();
                if (currentiOSDevice.ActivationState == "Unactivated") { Ldrestart(); }
                if (currentiOSDevice.ActivationState == "Unactivated") { throw new ApplicationException("The device didnt activate"); }
                SSHExecute("ls"); SSHExecute("ls"); SSHExecute("ls");
                SSHExecute("rm /Library/MobileSubstrate/DynamicLibraries/untethered.dylib");
                SSHExecute("rm /Library/MobileSubstrate/DynamicLibraries/untethered.plist");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 65));
                try
                {
                    scpClient.Upload(new MemoryStream(iuntethered), "/Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                    scpClient.Upload(new MemoryStream(iuntetheredplist), "/Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
                    SSHExecute("chmod 755 /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to send dylibs:" + e.Message);
                    return false;
                }
                ldrestart();SSHExecute("ls");SSHExecute("ls");SSHExecute("ls");
                SSHExecute("rm /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                SSHExecute("rm /Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
                frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 75));



                scpClient.Disconnect();
                string state = currentiOSDevice.ActivationState;
                switch (state) {

                    case "FactoryActivated":
                        UntWild(currentiOSDevice);
                        PostActivation();
                        MessageBox.Show("Done");
                        break;
                    case "Activated":
                        UntWild(currentiOSDevice);
                        PostActivation();
                        MessageBox.Show("Done");
                        break;
                    case "Unactivated":
                        MessageBox.Show("Error: Device is unactivated");
                        break;
                    default:
                        UntWild(currentiOSDevice);
                        PostActivation();
                        MessageBox.Show("Activation State of device is unknown");
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to Activate Device. Reason:" + e.Message);
                return false;
            }
        }

        public bool UntWild(iOSDevice currentiOSDevice)
            {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            iDevicePair("pair");
            MessageBox.Show("Trust connection if asked to, Setup iCloud, iMessage and FaceTime, then press OK", "Important", MessageBoxButtons.OK, MessageBoxIcon.Information);
            iDevicePair("validate");
            frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 85));
            string ActivationTicket = Get(currentiOSDevice);
            string guid = GetSystemPath("");
            string imei = currentiOSDevice.InternationalMobileEquipmentIdentity;
            FixActivationRecords(guid);
            FixDataArk(guid);
            FixCCDS(guid, imei, ActivationTicket);
            frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Value = 100));
            return true;
            }
        
      
        public void pi(int number)
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            frm1.Invoke((MethodInvoker)(() => frm1.progressBar1.Increment(number)));
        }
        

        public bool PostActivation()
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            if (frm1.DisableBBBox.Checked == true)
            {
               try
                  {
                    /*if (PhoneHasMeid)
                    {
                        string RandomString = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4KPCFET0NUWVBFIHBsaXN0IFBVQkxJQyAiLS8vQXBwbGUvL0RURCBQTElTVCAxLjAvL0VOIiAiaHR0cDovL3d3dy5hcHBsZS5jb20vRFREcy9Qcm9wZXJ0eUxpc3QtMS4wLmR0ZCI+CjxwbGlzdCB2ZXJzaW9uPSIxLjAiPgo8ZGljdD4KCTxrZXk+QWN0aXZhdGlvblJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkFjdGl2YXRpb25SYW5kb21uZXNzPC9rZXk+CgkJPHN0cmluZz4zMGI2MGZkMC02Njc0LTQ3NzgtYmIxNC1mNGZhOTQ0MWQ0Yzg8L3N0cmluZz4KCQk8a2V5PkFjdGl2YXRpb25TdGF0ZTwva2V5PgoJCTxzdHJpbmc+VW5hY3RpdmF0ZWQ8L3N0cmluZz4KCQk8a2V5PkZNaVBBY2NvdW50RXhpc3RzPC9rZXk+CgkJPHRydWUvPgoJPC9kaWN0PgoJPGtleT5CYXNlYmFuZFJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkFjdGl2YXRpb25SZXF1aXJlc0FjdGl2YXRpb25UaWNrZXQ8L2tleT4KCQk8dHJ1ZS8+CgkJPGtleT5CYXNlYmFuZEFjdGl2YXRpb25UaWNrZXRWZXJzaW9uPC9rZXk+CgkJPHN0cmluZz5WMjwvc3RyaW5nPgoJCTxrZXk+QmFzZWJhbmRDaGlwSUQ8L2tleT4KCQk8aW50ZWdlcj4xMjM0NTY3PC9pbnRlZ2VyPgoJCTxrZXk+QmFzZWJhbmRNYXN0ZXJLZXlIYXNoPC9rZXk+CgkJPHN0cmluZz44Q0IxMDcwRDk1QjlDRUU0QzgwMDAwNUUyMTk5QkI4RkIxODNCMDI3MTNBNTJEQjVFNzVDQTJBNjE1NTM2MTgyPC9zdHJpbmc+CgkJPGtleT5CYXNlYmFuZFNlcmlhbE51bWJlcjwva2V5PgoJCTxkYXRhPgoJCUVnaGRDdz09CgkJPC9kYXRhPgoJCTxrZXk+SW50ZXJuYXRpb25hbE1vYmlsZUVxdWlwbWVudElkZW50aXR5PC9rZXk+CgkJPHN0cmluZz4xMjM0NTY3ODkxMjM0NTY8L3N0cmluZz4KCQk8a2V5Pk1vYmlsZUVxdWlwbWVudElkZW50aWZpZXI8L2tleT4KCQk8c3RyaW5nPjEyMzQ1Njc4OTEyMzQ1PC9zdHJpbmc+CgkJPGtleT5TSU1TdGF0dXM8L2tleT4KCQk8c3RyaW5nPmtDVFNJTVN1cHBvcnRTSU1TdGF0dXNOb3RJbnNlcnRlZDwvc3RyaW5nPgoJCTxrZXk+U3VwcG9ydHNQb3N0cG9uZW1lbnQ8L2tleT4KCQk8dHJ1ZS8+CgkJPGtleT5rQ1RQb3N0cG9uZW1lbnRJbmZvUFJMTmFtZTwva2V5PgoJCTxpbnRlZ2VyPjA8L2ludGVnZXI+CgkJPGtleT5rQ1RQb3N0cG9uZW1lbnRJbmZvU2VydmljZVByb3Zpc2lvbmluZ1N0YXRlPC9rZXk+CgkJPGZhbHNlLz4KCTwvZGljdD4KCTxrZXk+RGV2aWNlQ2VydFJlcXVlc3Q8L2tleT4KCTxkYXRhPgoJTFMwdExTMUNSVWRKVGlCRFJWSlVTVVpKUTBGVVJTQlNSVkZWUlZOVUxTMHRMUzBLVFVsSlFuaEVRME5CVXpCRFFWRkIKCWQyZFpUWGhNVkVGeVFtZE9Wa0pCVFZSS1JVa3pUbXRSTUU1RlJUVk1WVmt6VGpCUmRFNUZVVEJOYVRBMFVWVktRZzBLCglURlJyZUZKcVdrVlNSRWw1VWtWS1IwNXFSVXhOUVd0SFFURlZSVUpvVFVOV1ZrMTRRM3BCU2tKblRsWkNRV2RVUVd0TwoJYWpaeFNVbHRUbmxXU21WMU5sTTJVak40UVcxT1RXNWFjREpHTDNoRVNIRjViVmxVT1ZoT1JFdzBjRlJaYjFnMmF6QmsKCVFrMVNTWGRGUVZsRVZsRlJTQTBLUlhkc1JHUllRbXhqYmxKd1ltMDRlRVY2UVZKQ1owNVdRa0Z2VkVOclJuZGpSM2hzCglTVVZzZFZsNU5IaEVla0ZPUW1kT1ZrSkJjMVJDYld4UllVYzVkUTBLV2xSRFFtNTZRVTVDWjJ0eGFHdHBSemwzTUVKQgoJUVUxQk1FZERVM0ZIVTBsaU0wUlJSVUpDVVZWQlFUUkhRa0ZETDJ4eWJHVlJUamR3UVEwS00yaEhWVlkwU0ZsU1lXdHYKCWFrazRPV3d4YUZKdmRqQlROREJPTUhBeU1UaHJUV295YkRGT2EzUXdWWEJxV2s5RU5WVldlVGRDT0VsT1FrSm1RMmxNCglNZzBLWnk4dkx5dHpaVVZoVjFjMGFEWXdUM0pOZG5KbFFWQTBNR0psVTJaUFlucE1WR3hYUzJGV2NXRnJNV1JGVGpSSgoJTkd4TVRYaHBlVFVyYjNwSVpqWmlWdzBLVGl0bldFSlVNMjl4WkhWRFF6RldWelZKV25aMlpFUlNWRWx3YUZoNmEyRUsKCVVVVkdRVUZQUW1wUlFYZG5XV3REWjFsRlFYSlVhMVpFZDBGV01IbHRZazVWUm14ME0yeExjMHRCWkEwS2JuYzBTRlpPCglaMEZ1UkhoaWRRMEtRVUpXV1VSMlNGaEJNREZNV0ZOS1F5dHRkamd5VFZSSWQySk5ORVF2V2xJclJFaFpRV1kyWXlzNQoJYVc1TlJtUk9PR2xaV0hSSWFFOXdjV3MwYVd4TlR3MEtZMnRuWWtsNlMwb3lOWFJPWTFKVWMwOXdWVU5CZDBWQlFXRkIKCUxTMHRMUzFGVGtRZ1EwVlNWRWxHU1VOQlZFVWdVa1ZSVlVWVFZDMHRMUzB0Cgk8L2RhdGE+Cgk8a2V5PkRldmljZUlEPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PlNlcmlhbE51bWJlcjwva2V5PgoJCTxzdHJpbmc+RlIxUDJHSDhKOFhIPC9zdHJpbmc+CgkJPGtleT5VbmlxdWVEZXZpY2VJRDwva2V5PgoJCTxzdHJpbmc+ZDk4OTIwOTZjZjM0MTFlYTg3ZDAwMjQyYWMxMzAwMDNmMzQxMWU0Mjwvc3RyaW5nPgoJPC9kaWN0PgoJPGtleT5EZXZpY2VJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkJ1aWxkVmVyc2lvbjwva2V5PgoJCTxzdHJpbmc+MThGMDA8L3N0cmluZz4KCQk8a2V5PkRldmljZUNsYXNzPC9rZXk+CgkJPHN0cmluZz5pUGhvbmU8L3N0cmluZz4KCQk8a2V5PkRldmljZVZhcmlhbnQ8L2tleT4KCQk8c3RyaW5nPkI8L3N0cmluZz4KCQk8a2V5Pk1vZGVsTnVtYmVyPC9rZXk+CgkJPHN0cmluZz5NTExOMjwvc3RyaW5nPgoJCTxrZXk+T1NUeXBlPC9rZXk+CgkJPHN0cmluZz5pUGhvbmUgT1M8L3N0cmluZz4KCQk8a2V5PlByb2R1Y3RUeXBlPC9rZXk+CgkJPHN0cmluZz5pUGhvbmUwLDA8L3N0cmluZz4KCQk8a2V5PlByb2R1Y3RWZXJzaW9uPC9rZXk+CgkJPHN0cmluZz4xNC4wLjA8L3N0cmluZz4KCQk8a2V5PlJlZ2lvbkNvZGU8L2tleT4KCQk8c3RyaW5nPkxMPC9zdHJpbmc+CgkJPGtleT5SZWdpb25JbmZvPC9rZXk+CgkJPHN0cmluZz5MTC9BPC9zdHJpbmc+CgkJPGtleT5SZWd1bGF0b3J5TW9kZWxOdW1iZXI8L2tleT4KCQk8c3RyaW5nPkExMjM0PC9zdHJpbmc+CgkJPGtleT5TaWduaW5nRnVzZTwva2V5PgoJCTx0cnVlLz4KCQk8a2V5PlVuaXF1ZUNoaXBJRDwva2V5PgoJCTxpbnRlZ2VyPjEyMzQ1Njc4OTEyMzQ8L2ludGVnZXI+Cgk8L2RpY3Q+Cgk8a2V5PlJlZ3VsYXRvcnlJbWFnZXM8L2tleT4KCTxkaWN0PgoJCTxrZXk+RGV2aWNlVmFyaWFudDwva2V5PgoJCTxzdHJpbmc+Qjwvc3RyaW5nPgoJPC9kaWN0PgoJPGtleT5Tb2Z0d2FyZVVwZGF0ZVJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkVuYWJsZWQ8L2tleT4KCQk8dHJ1ZS8+Cgk8L2RpY3Q+Cgk8a2V5PlVJS0NlcnRpZmljYXRpb248L2tleT4KCTxkaWN0PgoJCTxrZXk+Qmx1ZXRvb3RoQWRkcmVzczwva2V5PgoJCTxzdHJpbmc+ZmY6ZmY6ZmY6ZmY6ZmY6ZmY8L3N0cmluZz4KCQk8a2V5PkJvYXJkSWQ8L2tleT4KCQk8aW50ZWdlcj4yPC9pbnRlZ2VyPgoJCTxrZXk+Q2hpcElEPC9rZXk+CgkJPGludGVnZXI+MzI3Njg8L2ludGVnZXI+CgkJPGtleT5FdGhlcm5ldE1hY0FkZHJlc3M8L2tleT4KCQk8c3RyaW5nPmZmOmZmOmZmOmZmOmZmOmZmPC9zdHJpbmc+CgkJPGtleT5VSUtDZXJ0aWZpY2F0aW9uPC9rZXk+CgkJPGRhdGE+CgkJTUlJRDB3SUJBakNDQTh3RUlQNEMzc3FRdFAxUzJod0JaekNvSGNzb0gyeE51NWMrYTRRNDVvSjFNS0YzCgkJQkVFRTJlOTNlb1ZPeHVmMGVLUFVxTkVnNnpNbEJzTnEranIrUnFNQXhTaFZBL2NUNW9ua3IwdCtFMEhLCgkJblNkdkhNMi9GZXRyT3FpT0k0RHZIUElEVzBEMnVBUVEzaW9iUHdhQWxGbFhIUFdyOE1KLyt3UVFHVGxuCgkJRVhPMTZOdDJrVUUrdy8vQmxHd1Q4V3hSZXkvSU41SW1NbGtZelpsSnpack83dVl0bHBlZ3k2K3hJaWwyCgkJQjJYbHk0aUd4UlppUld5NXNLcFFvMll6b0pFbW1XU25manUwY1UyL3JiOUZCdnVWaS9rV1NGbkFrdDR5CgkJcVF3NGswaWJ0cDVXK1lVQ0NvZm8zeWVuak0yVWMwbit5SExyU20wRTlPUDNwdExUN3ZHcnJma3IzWFJpCgkJdHdEcGRCT3NzK1h6SEFRWEt1cG85WGkxUW1ObGp1VGoxakpZbzZNc1kyOURYOUVacFdEdmpJc0l5THd4CgkJQjRjbUlTVWY4Qm5yUlFHOURBM01lYzZiaFRkUEJjdUtXZHBCbm5DMlY4V3BmTXBwVUQ2U2RndW5pejZ6CgkJTEcwNmNGR3dvUXZuWXhRa1Vra2pkWWR6NG85eXM5L3ZxQ2JxZnBuNHRjZEkyMWM5Z29Nd0xoRHNoYms1CgkJUENaQnNoNUY0U1JSaWdBV3JBU0NBejk4MkI3bzhwQ0NaL2pZK3laQ3pBb3J6SG5zR2Z2d0tpSlBBTWppCgkJZTA0RzRqSk04cEpRUU5uWmFhUCt0RmVsZGhER1FubzA0dmZKRFkzOEZGTSthZUN3elJyQy9DUGJrZVpRCgkJNXR5NTdMSXNzMUhyUmUzSTFjK0ZMNXBuZmwvaEsxQjF1QTRHRDRWbFkxU0xMMXk1ajRHdUZUM1hTeHpiCgkJWlIvZmJEa1V5VHNUM3I2eGdoWnRNNEJYSW9hNjJaREMzSVBtT2J4S2JobGFLQTRtSzJzM1FCNFZjNlMvCgkJbTZ1YTZQakwvQjE1QzBjTGpyMUNNb0x0Lzc4TFVRV21GRXV5SkhkdnRTNnhIbWtMRG9FZW1tMHlDcGJqCgkJMmhrRmt2d3dISlg2SDFiUm1KWS9HUmY1UXVIWDVKdlk3ZGhOY2YzNENmaVExRHdwZ2VKUkw5eTN2SG0vCgkJZkFSV0JxWDNkWjV1VUpXcUNzMklvMFdIRGdqMTh3cW5vUEw2QnRHcjVhWEJFeGF3WkpGT1ZOcVZjV2lPCgkJOE9LMzhuSDFKaGcxVk44UURBelhmTEpjQ2w0UEN6Mm5sVlpSMDl1WnF0NlpPaXFjVUNyZ3hZbTdIQktaCgkJOS9BRmIyVmxLUFRZTTNueXBDeGh5MmNMQnowK3RCK0V6V0hTbjlzU3FMelN1eFBOdGIxY21FMno5OFNoCgkJMk1UVzJaWk42NWdvYkxrSU5wbzdUb1RBMm50cHY1ZjBqdlhpVnZIV1V1dmhUSVlLZG4vKzA0czNJQ0VLCgkJQVlJQ0NPNjgvakxucDVQUERuRmVsQ3Z1d0dFRTFkb0lMNzZ6UllNOWlrWTJHRVB5NW5XdW1ydXp4U2RCCgkJMURBNnNOeUxQanN2QnBnYUVnWmI0OUpXSjlERU5vYWZKeGQ4dlBoRnpORHZEL0NRKzU4VGtCYmYwWEVLCgkJa2xIRzdzOFY0SkRsYS9jMTBjSDcyWS8wL0lOUi9kUVk1V3FSaHNiSEVFalBVekdDTGNVPQoJCTwvZGF0YT4KCQk8a2V5PldpZmlBZGRyZXNzPC9rZXk+CgkJPHN0cmluZz5mZjpmZjpmZjpmZjpmZjpmZjwvc3RyaW5nPgoJPC9kaWN0Pgo8L2RpY3Q+CjwvcGxpc3Q+";
                        SSHExecute("launchctl unload /System/Library/LaunchDaemons/com.apple.CommCenterRootHelper.plist");
                        SSHExecute("launchctl unload /System/Library/LaunchDaemons/com.apple.CommCenterMobileHelper.plist");
                        SSHExecute("launchctl unload /System/Library/LaunchDaemons/com.apple.CommCenter.plist");
                        SSHExecute("chflags nouchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("plutil -backup /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("rm -rf /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("plutil -create /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("plutil -key kPostponementTicket -remove /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("plutil -key kPostponementTicket -dict /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("plutil -key kPostponementTicket -key ActivationTicket -string " + RandomString + " /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("plutil -key kPostponementTicket -key ActivityURL -string https://albert.apple.com/deviceservices/activity /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("plutil -key kPostponementTicket -key PhoneNumberNotificationURL -string https://albert.apple.com/deviceservices/phoneHome /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("plutil -key kPostponementTicket -key ActivationState -string FactoryActivated /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("chflags uchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                        SSHExecute("launchctl load /System/Library/LaunchDaemons/com.apple.CommCenterRootHelper.plist");
                        SSHExecute("launchctl load /System/Library/LaunchDaemons/com.apple.CommCenterMobileHelper.plist");
                        SSHExecute("launchctl load /System/Library/LaunchDaemons/com.apple.CommCenter.plist");
                        ldrestart(); SSHExecute("ls"); SSHExecute("ls");
                    } else
                    {
                    */
                        Mv("/usr/local/standalone/firmware/Baseband", "/usr/local/standalone/firmware/Baseband2");
                        AirplaneMode(true, true);
                        Launchctl(false, "com.apple.CommCenter", false, false);
                        Launchctl(true, "com.apple.CommCenter", false, false);
                        Sbreload();
                    //}
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to Disable Baseband:" + e.Message);
            }

            }
            try
            {
                if (frm1.NoOTABox.Checked == true)
                {
                    Launchctl(false, "com.apple.softwareupdateservicesd", true, true);
                    Launchctl(false, "com.apple.softwareupdateservicesd", true, true);
                    Launchctl(false, "com.apple.mobile.softwareupdated", true, true);
                    Launchctl(false, "com.apple.OTATaskingAgent", true, true);
                    Launchctl(false, "com.apple.mobile.obliteration", true, true);
                }
            } catch (Exception e) { MessageBox.Show("Failed to Disable OTA: " + e.Message); }
                    Rm("/usr/bin/plutil");
                    if (frm1.SubstrateBox.Checked == false)
                    {
                        Substrate("remove");
                    }
            return true;
        }

        public bool BackupRecords(iOSDevice iDevice, string guid)
        {
            try
            {
                string SN = iDevice.SerialNumber;
                if (!scpClient.IsConnected) { scpClient.Connect(); }
                Random Rand = new Random();
                int RandomInt = Rand.Next(100000);
                //Creating Data Folder
                if (!Directory.Exists(path + "\\Data"))
                {
                    DirectoryInfo d1 = Directory.CreateDirectory(path + "\\Data");
                }
                //Creating Device specific folder
                if (!Directory.Exists(path + "\\Data\\" + SN))
                {
                    DirectoryInfo d2 = Directory.CreateDirectory(path + "\\Data\\" + SN);
                }
                //Renaming old records
                if (File.Exists(path + "\\Data\\" + SN + "\\activation_record.plist"))
                {
                    try
                    {
                        File.Move(path + "\\Data\\" + SN + "\\activation_record.plist", path + "\\Data" + SN + "\\activation_record.plist"+RandomInt);
                        //File.Move(path + "\\Data\\" + SN + "\\guid", path + "\\Data" + SN + "\\guid" + RandomInt);
                    }
                    catch{ }
                }
                //Getting new records
                ExportFile("/private/var/containers/Data/System/" + guid + "/Library/activation_records/activation_record.plist", path + "\\Data\\" + SN +"\\activation_record.plist");
                return true;
            } catch(Exception e)
            {
                Log("Failed to Backup Records, Reason:"+e.Message);
                return false;
            }
        }
        public bool RootFSRestore()
        {
            StartIproxy();
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            SSHExecute("snappy -f / -r orig-fs -x");
            try
            {
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.CommCenterRootHelper.plist");
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.CommCenterMobileHelper.plist");
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.CommCenter.plist");
            } catch { }
            frm1.progressBar1.Value = 50;
            if (frm1.SubstrateBox.Checked == false)
            {
                Reboot();
                frm1.progressBar1.Value = 100;
                MessageBox.Show("RootFS Restore complete");
            } else
            {
                SSHExecute("launchctl load -w /System/Library/LaunchDaemons/com.apple.mobile.obliteration.plist");
                WipeData();
                frm1.progressBar1.Value = 100;
                MessageBox.Show("Erase phone completed!");
            }


            frm1.Invoke((MethodInvoker)(() => frm1.button1.Text = "Deactivate"));
            frm1.Invoke((MethodInvoker)(() => frm1.button1.Enabled = true));
            //frm1.Invoke((MethodInvoker)(() => frm1.metroCheckBox1.Enabled = true));
            frm1.Invoke((MethodInvoker)(() => frm1.SkipSetupBox.Enabled = true));
            frm1.Invoke((MethodInvoker)(() => frm1.DisableBBBox.Enabled = true));
            frm1.Invoke((MethodInvoker)(() => frm1.NoOTABox.Enabled = true));
            frm1.Invoke((MethodInvoker)(() => frm1.RebootBox.Enabled = true));
            frm1.Invoke((MethodInvoker)(() => frm1.SubstrateBox.Enabled = true));
            frm1.Invoke((MethodInvoker)(() => frm1.RFSCheckBox.Enabled = true));
            return true;
        }

        private Process proc;
        string path = Directory.GetCurrentDirectory();
        string host = "127.0.0.1";
        string user = "root";
        string pass = "alpine";
        public void StartIproxy()
        {
            try
            {

                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path + @"\Runtime\iproxy.exe",
                        Arguments = "22 44",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
            } catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("iproxy not found");
            }
        }
       public void KillIproxy()
        {
            foreach (var process in Process.GetProcessesByName("iproxy"))
            {
                process.Kill();
            }
        }
        public void Reboot()
        {
            try
            {

                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path + @"\Runtime\idevicediagnostics.exe",
                        Arguments = "restart ",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("idevicediagnostics not found");
            } catch (Exception e)
            {
                MessageBox.Show("An exception has occured:" + e.Message);
            }
        }

        public bool Activate()
        {
            try
            {
                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path + @"\Runtime\ideviceactivation.exe",
                        Arguments = "activate -s http://oc34n.website/pentest/activ8.php",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();
                proc.WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("ideviceactivation not found");
                return false;
            }
            return true;
        }

        public bool BBActivate()
        { 
            try
            {

                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path + @"\Runtime\ideviceactivation.exe",
                        Arguments = "activate -s http://oc34n.website/wildtest/wildactiv8.php",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }

                };

                proc.Start();
                StreamReader reader = proc.StandardOutput;
                string data = reader.ReadToEnd();
                File.WriteAllText("output.txt", data);
                proc.WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("ideviceactivation not found");
                return false;
            }
            return true;
        }
        public bool Deactivate()
        {
            try
            {

                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path + @"\Runtime\ideviceactivation.exe",
                        Arguments = "deactivate",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }

                };

                proc.Start();
                proc.WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("ideviceactivation not found");
                return false;
            }
            return true;
        }

        public string Get(iOSDevice currentiOSDevice)
        {
            //GET Device DATA
            string DeviceName = currentiOSDevice.DeviceName;
            string SerialNumber = currentiOSDevice.SerialNumber;
            string ticket;
            RestClient restClient = new RestClient("http://oc34n.website/APIM8.php");
            restClient.Timeout = -1;
            RestRequest restRequest = new RestRequest((Method)1);
            restRequest.AlwaysMultipartFormData = true;
            restRequest.AddParameter("untethered-0x3", (object)"ab7b81f10be5e2dcecd9");
            restRequest.AddParameter("untether-id", (object)((SerialNumber ?? "") ?? ""));
            restRequest.AddParameter("untether-type", (object)((DeviceName ?? "") ?? ""));
            ticket = restClient.Execute((IRestRequest)restRequest).Content;
            int length = ticket.Length;
            if (length < 10)
            {
                MessageBox.Show("Failed to get Activation Ticket. Make sure Device is Registered.");
                //throw new FailedtoFetchTicketException;
                return null;
            }
            return ticket;
        }

        public bool iDevicePair(string argument)
        {
            try
            {
                if (argument == "pair")
                {
                    proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = path + @"\Runtime\idevicepair.exe",
                            Arguments = "pair",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                } else
                {
                    proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = path + @"\Runtime\idevicepair.exe",
                            Arguments = "validate",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                }
                try
                {
                    proc.Start();
                    /*
                    using (StreamReader reader = proc.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                        }
                        */
                    StreamReader reader = proc.StandardOutput;
                    string result = reader.ReadToEnd();

                    Thread.Sleep(2000);
                    try { proc.Kill(); } catch { }
                    if (result.Contains("SUCCESS"))
                    {
                        reader.Dispose();
                        return true;
                    }
                    else { return false; }
                }
                catch { }
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("idevicepair not found");
                return false;
            }
            return false;
        }

        public bool CheckMEID()
        {
            try
            {
                    proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = path + @"\Runtime\ideviceinfo.exe",
                            //Arguments = "pair",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
            };
                proc.Start();
                StreamReader reader = proc.StandardOutput;
                string result = reader.ReadToEnd();

                Thread.Sleep(2000);
                try { proc.Kill(); } catch { }
                if (result.Contains("MobileEquipmentIdentifier"))
                {
                    reader.Dispose();
                    return true;
                }
                else { return false; }

            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("ideviceinfo not found");
                return false;
            }
        }

        public bool SkipSetup(bool action = true)
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            if (!scpClient.IsConnected) { scpClient.Connect(); }
            Chflags("-R nouchg", "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
            if (action && frm1.SkipSetupBox.Checked)
            {
                //if (frm1.SkipSetupBox.Checked)
                //{
                    scpClient.Upload(new MemoryStream(purplebuddy), "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                    Chmod(600, "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                    uicache("all");
                    Chflags("uchg", "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                //}
            } //else {  Chflags("nouchg", "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");}
            scpClient.Disconnect();
            return true;
        }

        public string SSHExecute(string command)
        {
            SshClient sshclient = new SshClient(host, user, pass);
            try
            {
                sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
                sshclient.Connect();
                SshCommand execute = sshclient.CreateCommand(@command);
                var asynch = execute.BeginExecute();
                while (!asynch.IsCompleted)
                {
                    Thread.Sleep(1000);
                }
                var result = execute.EndExecute(asynch);
                //result = killall.EndExecute(asynch);
                sshclient.Disconnect();
                Log(result);
                return result;

            }
            catch
            {
                switch (command)
                {
                    case "ls":
                        break;
                    case "uicache --all":
                        break;
                    default:
                       if (debug == true) { Log("SSH Error caused by:" + command); }
                        else { Thread.Sleep(2000); }
                        StartIproxy();
                        SSHExecute(command);
                        break;
                }
                return "exception";
            }
        }
        public void ldrestart()
        {
            SshClient sshclient = new SshClient(host, user, pass);
            try
            {
                sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(5);
                sshclient.Connect();
                sshclient.CreateCommand("ldrestart").Execute();
                Thread.Sleep(5000);
                sshclient.Disconnect();
                foreach (var process in Process.GetProcessesByName("iproxy"))
                {
                    process.Kill();
                    StartIproxy();
                }
                // StartIproxy();
                if (File.Exists(@"%USERPROFILE%\.ssh\known_hosts"))
                {

                    File.Delete(@"%USERPROFILE%\.ssh\known_hosts");
                }
                Thread.Sleep(5000);
            }

            catch {
                foreach (var process in Process.GetProcessesByName("iproxy"))
                {
                    process.Kill();
                    StartIproxy();
                }
                StartIproxy();
                if (File.Exists(@"%USERPROFILE%\.ssh\known_hosts"))
                {

                    File.Delete(@"%USERPROFILE%\.ssh\known_hosts");
                }
            }
        }
        public void RestartIproxy()
        {
            SshClient sshclient = new SshClient(host, user, pass);
            try
            {

                sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(5);
                sshclient.Connect();
                sshclient.CreateCommand("ls").Execute();
                Thread.Sleep(5000);
                sshclient.Disconnect();
                foreach (var process in Process.GetProcessesByName("iproxy"))
                {
                    process.Kill();
                    StartIproxy();
                }
                StartIproxy();
                if (File.Exists(@"%USERPROFILE%\.ssh\known_hosts"))
                {

                    File.Delete(@"%USERPROFILE%\.ssh\known_hosts");
                }
                Thread.Sleep(5000);
                sshclient.Connect();
            }

            catch
            {
                foreach (var process in Process.GetProcessesByName("iproxy"))
                {
                    process.Kill();
                    StartIproxy();
                }
                //StartIproxy();
                if (File.Exists(@"%USERPROFILE%\.ssh\known_hosts"))
                {

                    File.Delete(@"%USERPROFILE%\.ssh\known_hosts");
                }
            }
        }
        public void Substrate(string action)
        {
            //ScpClient scpClient = new ScpClient(host, user, pass);
            if (!scpClient.IsConnected) { scpClient.Connect(); }
            Rm("/Library/MobileSubstrate/DynamicLibraries/simple.dylib");
            Rm("/Library/MobileSubstrate/DynamicLibraries/simple.plist");
            if (action == "Install")
            {
                scpClient.Upload(new MemoryStream(lzma), "/sbin/lzma");
                scpClient.Upload(new MemoryStream(substrate), "/boot.tar.lzma");
                Chmod(777, "/sbin/lzma");
                SSHExecute("cd / && lzma -d -f boot.tar.lzma && tar -xvf boot.tar && rm boot.tar && cd /usr/libexec/ && ./substrate");
                SSHExecute("rm /Library/MobileSubstrate/DynamicLibraries/*");
                if (File.Exists(path+"\\Animation.tar"))
                {
                    scpClient.Upload(new FileInfo(path + "\\animation.tar"), "/animation.tar");
                    SSHExecute("cd / && tar -xvf animation.tar && chmod 755 /usr/bin/");
                }
            }
            else if (action == "remove")
            {
                SSHExecute("rm -rf /Library/MobileSubstrate/DynamicLibraries");
                SSHExecute("rm -rf /Library/MobileSubstrate");
                SSHExecute("rm /usr/libexec/substrate");
                SSHExecute("rm /usr/libexec/substrated");
                SSHExecute("rm -rf /Library/Frameworks/CydiaSubstrate.framework/*");
                SSHExecute("rm -rf /usr/lib/substrate");
                SSHExecute("rm -rf /usr/lib/cycript0.9");
                SSHExecute("rm /usr/lib/libsubstrate.dylib");
            }
            else if (action == "wildcard")
            {

                scpClient.Upload(new MemoryStream(lzma), "/sbin/lzma");
                scpClient.Upload(new MemoryStream(substrate), "/boot.tar.lzma");
                SSHExecute("chmod 777 /sbin/lzma && cd / && lzma -d -v boot.tar.lzma");
                SSHExecute("cd / && tar -xvf boot.tar && rm boot.tar && cd /usr/libexec/ && ./substrate");
            }
            else if (action == "wildcard2")
            {
                scpClient.Upload(new MemoryStream(lzma), "/sbin/lzma");
                scpClient.Upload(new MemoryStream(aservice), "/boot.tar.lzma");
                SSHExecute("chmod 777 /sbin/lzma && cd / && lzma -d -v boot.tar.lzma");
                SSHExecute("cd / && tar -xvf boot.tar && rm boot.tar && cd /usr/libexec/ && ./substrate");
            }
            else if (action == "RemoveGif")
            {
                if (File.Exists(path + "\\Animation.tar"))
                {
                    Rm("/System/Library/LaunchDaemons/com.rpetrich.rocketbootstrapd.plist");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/AppList.dylib");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/AppList.plist");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/gif2ani2.dylib");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/gif2ani2.plist");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/libCSPreferencesHooks.dylib");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/libCSPreferencesHooks.plist");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/PreferenceLoader.dylib");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/PreferenceLoader.plist");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/RocketBootstrap.dylib");
                    Rm("/Library/MobileSubstrate/DynamicLibraries/RocketBootstrap.plist");
                    Rm("/Library/PreferenceBundles/", true);
                    Rm("/Library/PreferenceLoader/", true);
                    Rm("/System/Library/PreferenceBundles/AppList.bundle/", true);
                    Rm("/private/var/mobile/Library/Preferences/com.midnightchips.gif2aniprefs.plist");
                    Rm("/private/var/mobile/Library/Preferences/Respring.gif");
                    Rm("/usr/include/AppList/", true);
                    Rm("/usr/include/libprefs/", true);
                    Rm("/usr/include/rocketbootstrap.h");
                    Rm("/usr/include/rocketbootstrap_dynamic.h");
                    Rm("/usr/lib/libapplist.dylib");
                    Rm("/usr/lib/libcolorpicker.dylib");
                    Rm("/usr/lib/libCSColorPicker.dylib");
                    Rm("/usr/lib/libCSPreferences.dylib");
                    Rm("/usr/lib/libCSPreferencesProvider.dylib");
                    Rm("/usr/lib/libCSPUtilities.dylib");
                    Rm("/usr/lib/libprefs.dylib");
                    Rm("/usr/lib/librocketbootstrap.dylib");
                    Rm("/usr/libexec/rocketd");
                    Rm("/usr/libexec/_rocketd_reenable");
                }
            }
            //scpClient.Disconnect();
        }
        public void Module()
        {
            if (File.Exists(path + "\\wallpaper.tar"))
            {
                ScpClient scpClient = new ScpClient(host, user ,pass);
                scpClient.Connect();
                scpClient.Upload(new FileInfo(path + "\\wallpaper.tar"), "/wallpaper.tar");
                SSHExecute("cd / && tar -xvf wallpaper.tar && chmod 755 /usr/bin/wallpaper");
                SSHExecute("wallpaper -n /private/var/mobile/Media/lockscreen.png -l");
                SSHExecute("wallpaper -n /private/var/mobile/Media/home.png -h");
                Rm("/private/var/mobile/Media/lockscreen.png");
                Rm("/private/var/mobile/Media/home.png");
                Rm("/usr/bin/wallpaper");
                scpClient.Disconnect();
            }
        }
        public string GetSystemPath(string file)
        {
            SshClient sshClient = new SshClient(host, user, pass);
            if (!sshClient.IsConnected) { sshClient.Connect(); }
            SshCommand sshCommand;
            sshCommand = sshClient.CreateCommand("find /private/var/containers/Data/System -iname \"internal\"");
            IAsyncResult asyncResult = sshCommand.BeginExecute();
            while (!asyncResult.IsCompleted)
            {
                Thread.Sleep(500);
            }
            sshCommand.EndExecute(asyncResult);
            string result = sshCommand.Result;
            if (file == "data_ark")
            {
                result = (result + "/data_ark.plist");
            } else if (file == "activation_records")
            {
                result = result.Replace("/Library/Internal/", "/Library/activation_records/activation_record.plist");
                // result = (result + ""); 
            } else
            {
               result = result.Replace("/private/var/containers/Data/System/", "");
               result = result.Replace("/Library/internal", "");
               result = result.Replace("\n", "");
            }
            sshClient.Disconnect();
            return result; 
        }

        public void WipeData()
        {
            ScpClient scpClient = new ScpClient(host, user, pass);
            scpClient.Connect();
            scpClient.Upload(new MemoryStream(restore), "/usr/bin/restore");
            SshClient sshclient = new SshClient(host, user, pass);
            try
            {
                sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
                sshclient.Connect();
                sshclient.CreateCommand("chmod 755 /usr/bin/restore").Execute();
                sshclient.CreateCommand("restore -command da7e6b6d2c20eb316c093 & rm /usr/bin/restore").Execute();
                sshclient.Disconnect();
            }
            catch
            {
            }
        }

        }
    }
    
