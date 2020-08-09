using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.Windows.Forms;
using MetroFramework.Forms;
using MobileDevice;
using MobileDevice.Event;
using RestSharp;
using Renci.SshNet;
using System.Runtime.CompilerServices;
using Renci.SshNet.Security;
using System.Windows.Input;
using System.Net;
using System.Net.NetworkInformation;

namespace Oc34n_OneClickTool
{
    public partial class Form1 : MetroForm
    {
        private bool UntWild = false;
        bool debug = Activation.debug;
        long usedMemory = GC.GetTotalMemory(true);
        private iOSDeviceManager manager = new iOSDeviceManager();
        private iOSDevice currentiOSDevice;
        public bool headless;
        public Form1()
        {
            InitializeComponent();
        }
        public string udid;
        private void findTheKey()
        {
            bool isenabled = false;
            RegistryKey wildkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Oc34n");
            if (wildkey != null)
            {
                try { isenabled = Convert.ToBoolean(wildkey.GetValue("isWildCheckboxEnabled")); }
                catch (FormatException)
                {
                    MessageBox.Show("You are almost there!");
                }
                this.Invoke((MethodInvoker)(() => WildCardBox.Enabled = isenabled));
                UntWild = isenabled;
                wildkey.Close();
            }
            else
            {
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Oc34n");
                findTheKey();
            }
        }
        private void Form1_Shown(Object sender, EventArgs e)
        {
            findTheKey();
            if (File.Exists(@"%USERPROFILE%\.ssh\known_hosts"))
            {

                File.Delete(@"%USERPROFILE%\.ssh\known_hosts");
            }
            foreach (var process in Process.GetProcessesByName("iproxy"))
            {
                process.Kill();
            }
            this.Invoke((MethodInvoker)(() => metroCheckBox2.Checked = Properties.Settings.Default.isDarkMode));
            this.Invoke((MethodInvoker)(() => headless = Properties.Settings.Default.isNonInteractive));
            SetButtonText();
            this.Invoke((MethodInvoker)(() => waitingText.Visible = false));
            try
            {
                if (currentiOSDevice == null)
                {
                    SetData(false);
                }
            } catch 
            {
                SetData(false);

            }
            //try {if (headless == true && currentiOSDevice != null) { DoActivate(); } } catch{}

        }
        private void Form1_FormClosed(object sender, FormClosingEventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("iproxy"))
            {
                process.Kill();
            }
        }

        /* public void CheckDevice()
       {
           Activation.StartIproxy();
           SshClient sshclient = new SshClient("127.0.0.1", "root", "alpine");
           string devicestate = "notconnected";
           string devicelaststate = "notconnected";
           int loop = 0;
           while (loop == 0)
           {
               try
               {
                   sshclient.Connect();
                   sshclient.Disconnect();
                   devicestate = "connected";

               }
               catch
               {
                   devicestate = "notconnected";
               } finally
               {
                   if (devicestate != devicelaststate) 
                       {
                       GetDeviceInfo();
                       devicelaststate = devicestate;
                       } 
               }
           }
       }
       */
        public void Button1_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                TriggerHeadlessMode();
                }
            else
            {
                DoActivate();
            }
        }

        public void TriggerHeadlessMode()
        {
            if (headless == false)
            {
                headless = true;
            }
            else
            {
                headless = false;
            }
            Properties.Settings.Default.isNonInteractive = headless;
            Properties.Settings.Default.Save();
            SetButtonText();
        }
        public void SetButtonText()
        {
            if (RFSCheckBox.Checked && !SubstrateBox.Checked)
            {
                this.Invoke((MethodInvoker)(() => button1.Text = "Deactivate"));
            }
            else if (RFSCheckBox.Checked && SubstrateBox.Checked)
            {
                this.Invoke((MethodInvoker)(() => button1.Text = "Erase"));
            }
            else if (headless == false)
            {
                this.Invoke((MethodInvoker)(() => button1.Text = "Activate"));
            }
            if (headless == true) { this.Invoke((MethodInvoker)(() => button1.Text = "Waiting..")); }
        }
        public void DoActivate()
        {

            this.Invoke((MethodInvoker)(() => button1.Enabled = false));
            //metroCheckBox1.Enabled = false;
            this.Invoke((MethodInvoker)(() => SkipSetupBox.Enabled = false));
            this.Invoke((MethodInvoker)(() => DisableBBBox.Enabled = false));
            this.Invoke((MethodInvoker)(() => NoOTABox.Enabled = false));
            this.Invoke((MethodInvoker)(() => RebootBox.Enabled = false));
            this.Invoke((MethodInvoker)(() => SubstrateBox.Enabled = false));
            this.Invoke((MethodInvoker)(() => RFSCheckBox.Enabled = false));

            if (RFSCheckBox.Checked == false)
            {
                try
                {
                    if (currentiOSDevice.SIMStatus == "kCTSIMSupportSIMStatusReady" ^ currentiOSDevice.SIMStatus == "kCTSIMSupportSIMStatusPINLocked")
                    {
                        BoxShow("Sim Card detected, its recommended to eject it", "Sim Card on the phone", 7000);
                     }
                }
                catch { }
                this.Invoke((MethodInvoker)(() => button1.Text = "Activating"));
                this.Invoke((MethodInvoker)(() => button1.Enabled = false));
                ActivationWorker.RunWorkerAsync();
            }
            else
            {
                this.Invoke((MethodInvoker)(() => button1.Text = "Deactivating"));
                Activation Deactivate = new Activation();
                Deactivate.RootFSRestore();
                this.Invoke((MethodInvoker)(() => progressBar1.Value = 0));
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        public void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            manager.CommonConnectEvent += CommonConnectDevice;
            manager.ListenErrorEvent += ListenError;
            manager.StartListen();
        }

        private void CommonConnectDevice(object sender, DeviceCommonConnectEventArgs args)
        {
            if (args.Message == MobileDevice.Enumerates.ConnectNotificationMessage.Connected)
            {
                currentiOSDevice = args.Device;
                SetData(true);
                if (headless == true) { DoActivate(); }

            }
            if (args.Message == MobileDevice.Enumerates.ConnectNotificationMessage.Disconnected)
            {
                SetData(false);
            }
        }
        
                private void ListenError(object sender, ListenErrorEventHandlerEventArgs args)
                {
            if (args.ErrorType == MobileDevice.Enumerates.ListenErrorEventType.StartListen)
            {
                throw new Exception(args.ErrorMessage);
            }
                }


        public void SetProgress(int progress)
        {

            progressBar1.Value = progress;
        }


        public void ProgressBar1_Click(object sender, EventArgs e)
        {

        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            //  RefreshRate.Enabled = false;
            //  GetDeviceInfo();
            //  RefreshRate.Enabled = true;
        }


        private void RefreshBox_CheckedChanged(object sender, EventArgs e)
        {
            //if (RefreshBox_Checked = false)
            {
                //  Timer1.Enabled = false;
            }

        }
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (RFSCheckBox.Checked)
            {
                RebootBox.Checked = true;
                SubstrateBox.Checked = false;
                button1.Text = "Deactivate";
                if (debug == false)
                {
                    SubstrateBox.Enabled = false;
                }
                else
                {
                    SubstrateBox.Text = "Wipe Data";
                }
                DisableBBBox.Checked = false;
                DisableBBBox.Enabled = false;
                SkipSetupBox.Checked = false;
                SkipSetupBox.Enabled = false;
                NoOTABox.Checked = false;
                NoOTABox.Enabled = false;
                RebootBox.Enabled = false;
            }
            else
            {
                button1.Text = "Activate";
                if (debug == false)
                {
                    SubstrateBox.Enabled = true;
                }
                else
                {
                    SubstrateBox.Text = "Don't push substrate";
                }
                DisableBBBox.Enabled = true;
                NoOTABox.Enabled = true;
                SkipSetupBox.Enabled = true;
                RebootBox.Checked = false;
                RebootBox.Enabled = true;
                this.Invoke((MethodInvoker)(() => button1.BackColor = Color.FromArgb(0, 170, 173)));
            }
        }
        string path = Directory.GetCurrentDirectory();
        private void ActivationWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Activation Activate = new Activation();
            if (CheckNetwork())
            {
                if (WildCardBox.Checked == false) { Activate.FactoryActivation(currentiOSDevice); }
                else { Activate.WildCardActivation(currentiOSDevice); }
            } else
            {
                if (File.Exists(@path + "\\Data\\" + currentiOSDevice.SerialNumber + "\\activation_record.plist"))
                {
                    BoxShow("Unable to receive response from server, Trying offline mode", "Network Unavailable", 5000);
                    if (WildCardBox.Checked == false) { Activate.OfflineFactory(currentiOSDevice); }
                    //else { Activate.WildCardActivation(currentiOSDevice); }
                }
                else
                {
                    BoxShow("Unable to receive response from server, no records detected, cancelling operation...", "Network Unavailable", 5000);
                }

            }

            SetButtonText();
            //this.Invoke((MethodInvoker)(() => button1.Text = "Activate"));
            this.Invoke((MethodInvoker)(() => button1.Enabled = true));
            this.Invoke((MethodInvoker)(() => progressBar1.Value = 0));
            this.Invoke((MethodInvoker)(() => SkipSetupBox.Enabled = true));
            if (WildCardBox.Checked == false) { this.Invoke((MethodInvoker)(() => DisableBBBox.Enabled = true));}
            this.Invoke((MethodInvoker)(() => NoOTABox.Enabled = true));
            this.Invoke((MethodInvoker)(() => RebootBox.Enabled = true));
            if (WildCardBox.Checked == false) { this.Invoke((MethodInvoker)(() => SubstrateBox.Enabled = true)); }
            if (WildCardBox.Checked == false) { this.Invoke((MethodInvoker)(() => RFSCheckBox.Enabled = true)); }
        }

        private void DisableBBBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DisableBBBox.Checked)
            {
                Properties.Settings.Default.doesDisableBaseband = true;
            }
            else
            {
                Properties.Settings.Default.doesDisableBaseband = false;
            }
            Properties.Settings.Default.Save();
        }

        public void BoxShow(string text, string caution, int timeout)
        {
            if (headless == false) {
                MessageBox.Show(text, caution, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            {
            AutoClosingMessageBox.Show(
             text, 
             caution,
             timeout,
             MessageBoxButtons.OK
             );
            }
        }
        private void SetData(bool connected)
        {
               
            if (connected== true)
            {
                this.Invoke((MethodInvoker)(() => udid = currentiOSDevice.UniqueDeviceID.ToUpper().Trim()));
                this.Invoke((MethodInvoker)(() => UDID_TEXT.Text = udid));
                this.Invoke((MethodInvoker)(() => MODEL_TEXT.Text = currentiOSDevice.ProductType));
                this.Invoke((MethodInvoker)(() => SN_TEXT.Text = currentiOSDevice.SerialNumber));
                this.Invoke((MethodInvoker)(() => IMEI_TEXT.Text = currentiOSDevice.InternationalMobileEquipmentIdentity));
                this.Invoke((MethodInvoker)(() => IOS_TEXT.Text = currentiOSDevice.ProductVersion));

                this.Invoke((MethodInvoker)(() => waitingText.Visible = false));
                this.Invoke((MethodInvoker)(() => metroProgressSpinner1.Visible = false));
                this.Invoke((MethodInvoker)(() => metroLabel1.Visible = true));
                this.Invoke((MethodInvoker)(() => metroLabel2.Visible = true));
                this.Invoke((MethodInvoker)(() => metroLabel3.Visible = true));
                this.Invoke((MethodInvoker)(() => metroLabel4.Visible = true));
                this.Invoke((MethodInvoker)(() => metroLabel5.Visible = true));
                this.Invoke((MethodInvoker)(() => MODEL_TEXT.Visible = true));
                this.Invoke((MethodInvoker)(() => IOS_TEXT.Visible = true));
                this.Invoke((MethodInvoker)(() => SN_TEXT.Visible = true));
                this.Invoke((MethodInvoker)(() => UDID_TEXT.Visible = true));
                this.Invoke((MethodInvoker)(() => IMEI_TEXT.Visible = true));
            }
            else
            {
                this.Invoke((MethodInvoker)(() => metroLabel1.Visible = false));
                this.Invoke((MethodInvoker)(() => metroLabel2.Visible = false));
                this.Invoke((MethodInvoker)(() => metroLabel3.Visible = false));
                this.Invoke((MethodInvoker)(() => metroLabel4.Visible = false));
                this.Invoke((MethodInvoker)(() => metroLabel5.Visible = false));
                this.Invoke((MethodInvoker)(() => MODEL_TEXT.Visible = false));
                this.Invoke((MethodInvoker)(() => IOS_TEXT.Visible = false));
                this.Invoke((MethodInvoker)(() => SN_TEXT.Visible = false));
                this.Invoke((MethodInvoker)(() => UDID_TEXT.Visible = false));
                this.Invoke((MethodInvoker)(() => IMEI_TEXT.Visible = false));
                this.Invoke((MethodInvoker)(() => waitingText.Visible = true));
                this.Invoke((MethodInvoker)(() => metroProgressSpinner1.Visible = true));
            }
        }
        private void metroLabel6_Click(object sender, EventArgs e)
        {

        }

        private void metroProgressSpinner1_Click(object sender, EventArgs e)
        {

        }

        private void metroCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox2.Checked)
            {
                this.Invoke((MethodInvoker)(() => metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark));
                Properties.Settings.Default.isDarkMode = true;
            }
            else
            {
                this.Invoke((MethodInvoker)(() => metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light));
                Properties.Settings.Default.isDarkMode = false;
            }
            Properties.Settings.Default.Save();
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (WildCardBox.Checked)
            {
                UntWild = true;
                this.Invoke((MethodInvoker)(() => metroStyleManager1.Style = MetroFramework.MetroColorStyle.Magenta));
                this.Invoke((MethodInvoker)(() => button1.BackColor = Color.FromArgb(255, 0, 148)));
                //MessageBox.Show(this, "Somehow, you've enabled this! Congratulations, enjoy your baseband ;)", "SECRET MODE UNCOVERED!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Invoke((MethodInvoker)(() => WildCardBox.Enabled = false));
            }
            else
            {
                UntWild = false;
                this.Invoke((MethodInvoker)(() => metroStyleManager1.Style = MetroFramework.MetroColorStyle.Teal));
                this.Invoke((MethodInvoker)(() => button1.BackColor = Color.FromArgb(0, 170, 173)));
            }
        }

        private void SkipSetupBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipSetupBox.Checked)
            {
                Properties.Settings.Default.doesSkipSetup = true;
            }
            else
            {
                Properties.Settings.Default.doesSkipSetup = false;
            }
            Properties.Settings.Default.Save();
        }

        private void NoOTABox_CheckedChanged(object sender, EventArgs e)
        {
            if (NoOTABox.Checked)
            {
                Properties.Settings.Default.doesDisableOTA = true;
            }
            else
            {
                Properties.Settings.Default.doesDisableOTA = false;
            }
            Properties.Settings.Default.Save();
        }

        private void RebootBox_CheckedChanged(object sender, EventArgs e)
        {
        }

      public static bool CheckNetwork()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("https://oc34n.pw/"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            /*
            try
            {
                using (var pingSender = new Ping())
                {
                    string[] url = { "oc34n.website", "oc34n.pw" };
                    PingReply reply = pingSender.Send(url[0]);
                    if (reply.Status == IPStatus.Success)
                    {
                        return true;
                    } else{ return false;}
                }
            }
            catch
            {
                return false;
            }
            */
        }
        /*
           void CheckInternetConnectivity(object state)
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                        webClient.Proxy = null;
                        webClient.OpenReadCompleted += webClient_OpenReadCompleted;
                        webClient.OpenReadAsync(new Uri("http://oc34n.website/"));
                    }
                }
            }
        bool internetAvailable;
        void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                internetAvailable = true;
                //Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                //{
                    // UI changes made here
              //  }));
            } else
            {
                internetAvailable = false;
            }
        }
        */
        private void SubstrateBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RFSCheckBox.Checked == true)
            {
                if (SubstrateBox.Checked == true)
                {
                    button1.Text = "Erase";
                    this.Invoke((MethodInvoker)(() => button1.BackColor = Color.FromArgb(190, 0, 40)));
                }
                else
                {
                    button1.Text = "Deactivate";
                    this.Invoke((MethodInvoker)(() => button1.BackColor = Color.FromArgb(0, 170, 173)));
                }


            } 
        }

 }
}
