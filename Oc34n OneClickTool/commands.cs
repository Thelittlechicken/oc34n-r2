using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;

using Renci.SshNet;

namespace Oc34n_OneClickTool
{
    public class commands
    {
        SshClient sshclient = new SshClient("127.0.0.1", "root", "alpine");
        ScpClient SCPClient = new ScpClient("127.0.0.1","root", "alpine");
        int attemptlimit = 5;
        string path = Directory.GetCurrentDirectory();
        Exception AttemptsExceded = new Exception();
        //UiKit
        internal void Sbreload()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
            if (!sshclient.IsConnected) { sshclient.Connect(); }
            sshclient.CreateCommand("sbreload").Execute();
            sshclient.Disconnect();
        }

        internal void Ldrestart()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
            if (!sshclient.IsConnected) { sshclient.Connect(); }
            try
            {
                sshclient.CreateCommand("ldrestart && ldrestart").Execute();
                Thread.Sleep(10000);
                sshclient.CreateCommand("ldrestart");
                Thread.Sleep(3000);
            }
            catch { }
            sshclient.Disconnect();
        }

        internal void uicache(string argument)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            sshclient.Connect();
            try
            {
                if (argument == "all") { sshclient.CreateCommand("uicache --all").Execute(); }
                else { sshclient.CreateCommand("uicache -" + argument).Execute(); }

            }
            catch { }
            sshclient.Disconnect();
        }


        //Launchctl
        internal void ReloadDaemons()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            try
            {
                sshclient.Connect();
                sshclient.CreateCommand("launchctl unload -F /System/Library/LaunchDaemons/*").Execute();

            }
            catch { }
            try
            {
                sshclient.CreateCommand("launchctl load -F /System/Library/LaunchDaemons/*").Execute();
                sshclient.Disconnect();
            }
            catch
            {
                sshclient.Disconnect();
            }
           
        }
        internal void UnloadDaemons()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            try
            {
                if (!sshclient.IsConnected) { sshclient.Connect(); }
                sshclient.CreateCommand("launchctl unload -F /System/Library/LaunchDaemons/*").Execute();
                sshclient.Disconnect();

            }
            catch { }

        }
        internal void LoadDaemons()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            try
            {
                if (!sshclient.IsConnected) { sshclient.Connect(); }
                sshclient.CreateCommand("launchctl load -F /System/Library/LaunchDaemons/*").Execute();
                sshclient.Disconnect();

            }
            catch { }

        }
        internal void Launchctl(bool load ,string daemon, bool Force = false, bool W = false)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            try
            {
                if (!sshclient.IsConnected) { sshclient.Connect(); }
                string argument1 = ""; 
                string argument2 = "";
                string order;
                if (Force == true) { argument1 = "-F"; }
                if (W == true) { argument2 = "-w"; }
                if (load == false ) { order = "unload"; } else { order = "load"; }
                sshclient.CreateCommand("launchctl " + order +" "+ argument1 +" " + argument2 + " /System/Library/LaunchDaemons/"+daemon+".plist").Execute();
                sshclient.Disconnect();

            }
            catch { }

        }
        internal void ReloadActivation()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("launchctl unload /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist").Execute();
                    sshclient.CreateCommand("launchctl load /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist").Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to reaload activation. attempt " + attemptCounter + "/" + attemptlimit + ". Reason: " + e.Message);
                    attemptCounter++;
                    if (attemptCounter == attemptlimit) { throw AttemptsExceded; }
                    reattempt = true;

                }
            } while (reattempt == true);

        }

        //chflags
        internal void Chflags(string argument, string dir)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            sshclient.Connect();
            sshclient.CreateCommand("chflags " + argument + " " + dir).Execute();
            sshclient.Disconnect();
        }
        internal void UnflagActivation(string guid)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("chflags nouchg /private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist").Execute();
                    sshclient.CreateCommand("chflags nouchg /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("chflags nouchg /var/root/Library/Lockdown/data_ark.plist").Execute();
                    sshclient.CreateCommand("chflags nouchg /private/var/containers/Data/System/" + guid + "/Library/activation_records/activation_record.plist").Execute();
                    sshclient.CreateCommand("chflags nouchg /private/var/containers/Data/System/" + guid + "/Library/internal/data_ark.plist").Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch
                {
                    reattempt = true;
                }
            } while (reattempt == true);
        }
        internal void DeleteActivation(string guid)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("rm /private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist").Execute();
                    sshclient.CreateCommand("rm /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("rm /var/root/Library/Lockdown/data_ark.plist").Execute();
                    sshclient.CreateCommand("rm /private/var/containers/Data/System/" + guid + "/Library/activation_records/activation_record.plist").Execute();
                    sshclient.CreateCommand("rm /private/var/containers/Data/System/" + guid + "/Library/internal/data_ark.plist").Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch
                {
                    reattempt = true;
                }
            } while (reattempt == true);
        }
        internal void FlagActivation(string guid)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);

            bool reattempt = false;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("chflags uchg /private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist").Execute();
                    sshclient.CreateCommand("chflags uchg /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("chflags uchg /var/root/Library/Lockdown/data_ark.plist").Execute();
                    sshclient.CreateCommand("chflags uchg /private/var/containers/Data/System/" + guid + "/Library/activation_records/activation_record.plist").Execute();
                    sshclient.CreateCommand("chflags uchg /private/var/containers/Data/System/" + guid + "/Library/internal/data_ark.plist").Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch
                {
                    reattempt = true;
                }
            } while (reattempt == true);
        }


        //Special commands
        internal void FixActivationRecords(string guid)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("mkdir /private/var/containers/Data/System/" + guid + "/Library/activation_records/").Execute();
                    sshclient.CreateCommand("plutil -key UniqueDeviceCertificate -remove /private/var/containers/Data/System/" + guid + "/Library/activation_records/activation_record.plist").Execute();
                    sshclient.CreateCommand("plutil -key LDActivationVersion -remove /private/var/containers/Data/System/" + guid + "/Library/activation_records/activation_record.plist").Execute();
                    sshclient.CreateCommand("plutil -key DeviceConfigurationFlags -string 0 /private/var/containers/Data/System/" + guid + "/Library/activation_records/activation_record.plist").Execute();
                    sshclient.CreateCommand("chflags uchg /private/var/containers/Data/System/" + guid + "/Library/activation_records/activation_record.plist").Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to fix records. attempt " + attemptCounter + "/" + attemptlimit + ". Reason: " + e.Message);
                    attemptCounter++;
                    if (attemptCounter == attemptlimit) { throw AttemptsExceded; }
                    reattempt = true;

                }
            } while (reattempt == true);

        }
        internal void MKdirActivation(string guid)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);

            bool reattempt = false;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("mkdir -p /var/root/Library/Lockdown/").Execute();
                    sshclient.CreateCommand("mkdir -p /private/var/containers/Data/System/" + guid + "/Library/activation_records/").Execute();
                    sshclient.CreateCommand("mkdir -p /private/var/containers/Data/System/" + guid + "/Library/internal/").Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch
                {
                    reattempt = true;
                }
            } while (reattempt == true);
        }
        internal void FixDataArk(string guid)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("plutil -key ActivationState -remove /private/var/containers/Data/System/" + guid + "/Library/internal/data_ark.plist").Execute();
                    sshclient.CreateCommand("plutil -key -ActivationState -remove /private/var/containers/Data/System/" + guid + "/Library/internal/data_ark.plist").Execute();
                    sshclient.CreateCommand("plutil -key -UCRTOOBForbidden -remove /private/var/containers/Data/System/" + guid + "/Library/internal/data_ark.plist").Execute();
                    sshclient.CreateCommand("plutil -key ActivationState -string Activated /private/var/containers/Data/System/" + guid + "/Library/internal/data_ark.plist").Execute();
                    sshclient.CreateCommand("chflags uchg /private/var/containers/Data/System/" + guid + "/Library/internal/data_ark.plist").Execute();
                    sshclient.CreateCommand("cp /private/var/containers/Data/System/" + guid + "/Library/internal/data_ark.plist /private/var/root/Library/Lockdown/data_ark.plist").Execute();
                    sshclient.CreateCommand("chflags uchg /private/var/root/Library/Lockdown/data_ark.plist");
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to fix data_ark. attempt " + attemptCounter + "/" + attemptlimit + ". Reason: " + e.Message);
                    attemptCounter++;
                    if (attemptCounter == attemptlimit) { throw AttemptsExceded; }
                    reattempt = true;

                }
            } while (reattempt == true);

        }
        internal void FixCCDS(string guid,string imei, string ActivationTicket)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("chflags nouchg /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("plutil -key imei -remove /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("plutil -key imei /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("plutil -key imei -string " + imei + " /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("plutil -key kPostponementTicket -remove /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("plutil -key kPostponementTicket -dict /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("plutil -key kPostponementTicket -key ActivationTicket -string " + ActivationTicket + " /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("plutil -key kPostponementTicket -key ActivityURL -string https://albert.apple.com/deviceservices/activity /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("plutil -key kPostponementTicket -key PhoneNumberNotificationURL -string https://albert.apple.com/deviceservices/phoneHome /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("plutil -key kPostponementTicket -key ActivationState -string Activated /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.CreateCommand("chflags uchg /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to generate device specific plist. attempt " + attemptCounter + "/" + attemptlimit + ". Reason: " + e.Message);
                    attemptCounter++;
                    if (attemptCounter == attemptlimit) { throw AttemptsExceded; }
                    reattempt = true;

                }
            } while (reattempt == true);

        }

        internal void ReplaceString(string path,  string file, string key, string value,string suffix = ".lproj")
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
            sshclient.Connect();
            string[] lang = { "ar", "ca", "cs", "da", "de", "el", "en", "en_AU", "en_GB", "es", "es_419", "fi", "fr", "fr_CA", "fi", "fr", "fr_CA", "he", "hi", "hr", "hu", "id", "it", "ja", "ko", "ms", "nl", "no", "pl", "pt", "pt_PT", "ro", "ru", "sk", "sv", "th", "tr", "uk", "vi", "zh_CN", "zh_HK", "zh_TW" };
            int i=0;
            do {
                sshclient.CreateCommand("plutil -key " + key + " -remove " + path + "/" + lang[i] + suffix + "/" + file).Execute();
                sshclient.CreateCommand("plutil -key " + key + " "  + path + "/" + lang[i] + suffix + "/" + file).Execute();
                sshclient.CreateCommand("plutil -key " + key + " -string " + value + " " + path + "/" + lang[i] + suffix + "/" + file).Execute();
                i++;
            } while (i<42);
            sshclient.Disconnect();
        }

        //Common Commands
        internal void Respring()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    if (!sshclient.IsConnected) { sshclient.Connect(); }
                    sshclient.CreateCommand("killall backboardd").Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to respring. attempt " + attemptCounter + "/" + attemptlimit + ". Reason: " + e.Message);
                    attemptCounter++;
                    if (attemptCounter == attemptlimit) { throw AttemptsExceded; }
                    reattempt = true;

                }
            } while (reattempt == true);

        }

        internal void Chmod(int argument, string dir)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("chmod " + argument +" "+ dir).Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to fix permission. attempt " + attemptCounter + "/" + attemptlimit + ". Reason: " + e.Message);
                    attemptCounter++;
                    if (attemptCounter == attemptlimit) { throw AttemptsExceded; }
                    reattempt = true;

                }
            } while (reattempt == true);
        }

        internal void Chown(string argument, string dir)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("chown " + argument + " " + dir).Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to change file owner. attempt " + attemptCounter + "/" + attemptlimit + ". Reason: " + e.Message);
                    attemptCounter++;
                    if (attemptCounter == attemptlimit) { throw AttemptsExceded; }
                    reattempt = true;

                }
            } while (reattempt == true);
        }
        internal void Cp(string argument)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    if (!sshclient.IsConnected) { sshclient.Connect(); }
                    sshclient.CreateCommand("cp " + argument).Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to copy file. attempt " + attemptCounter + "/" + attemptlimit + ". Reason: " + e.Message);
                    attemptCounter++;
                    if (attemptCounter == attemptlimit) { throw AttemptsExceded; }
                    reattempt = true;

                }
            } while (reattempt == true);
        }

        internal void Mv(string source, string destination)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    if (!sshclient.IsConnected) { sshclient.Connect(); }
                    sshclient.CreateCommand("mv " + source + " " + destination).Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to move file. attempt " + attemptCounter + "/" + attemptlimit + ". Reason: " + e.Message);
                    attemptCounter++;
                    if (attemptCounter == attemptlimit) { throw AttemptsExceded; }
                    reattempt = true;

                }
            } while (reattempt == true);
        }
        internal void Rm(string dir, bool rf = false)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
            bool reattempt = false;
            int attemptCounter = 1;
            do
            {
                try
                {
                    if (!sshclient.IsConnected) { sshclient.Connect(); }
                    if (rf == false)
                    {
                        sshclient.CreateCommand("rm " + dir).Execute();
                    } else { sshclient.CreateCommand("rm -rf " + dir).Execute(); }
                    //sshclient.Disconnect();
                    reattempt = false;
                }
                catch (Exception e)
                {
                    Log("failed to delete file. attempt " + attemptCounter + "/" + (attemptlimit+5) + ". Reason: " + e.Message  + "\n");
                    attemptCounter++;
                    if (attemptCounter == attemptlimit+5) { throw AttemptsExceded; }
                    reattempt = true;
                    Thread.Sleep(500);
                }
            } while (reattempt == true);
            sshclient.Disconnect();
        }
        internal void mkdir (string dir)
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
            sshclient.Connect();
            sshclient.CreateCommand("mkdir "+dir).Execute();
            sshclient.Disconnect();
        }
        internal void Mount()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
            sshclient.Connect();
            sshclient.CreateCommand("mount -o rw,union,update /").Execute();
            sshclient.Disconnect();
        }
        internal void SnappyRename()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
            try
            {
                sshclient.Connect();
                sshclient.CreateCommand("snappy -f / -r `snappy -f / -l | sed -n 2p` -t orig-fs").Execute();
                sshclient.Disconnect();
            }
            catch { }
        }
        internal void ReplaceRaptor()
        {
            sshclient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
            bool reattempt;
            do
            {
                try
                {
                    sshclient.Connect();
                    sshclient.CreateCommand("cp /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/FactoryActivation.pem /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem").Execute();
                    sshclient.Disconnect();
                    reattempt = false;
                } catch
                {
                    reattempt = true;
                }
            } while (reattempt == true);
        }
        internal void ExportFile(string source, string destination)
        {
            SCPClient.Connect();
            SCPClient.Download(source, new FileInfo(destination));
            SCPClient.Disconnect();
        }
        internal void SendFile(string source, string destination)
        {
            SCPClient.Connect();
            SCPClient.Upload(new FileInfo(source), destination);
            SCPClient.Disconnect();
        }

       internal void AirplaneMode(bool state, bool Lock = false)
        {
            if (!sshclient.IsConnected) { sshclient.Connect(); }
            sshclient.CreateCommand("plutil -key AirplaneMode -"+state+" /Library/Preferences/SystemConfiguration/com.apple.radios.plist").Execute();
            if (Lock)
            {
                Chflags("uchg", "/Library/Preferences/SystemConfiguration/com.apple.radios.plist");
            } else
            {
                Chflags("nouchg", "/Library/Preferences/SystemConfiguration/com.apple.radios.plist");
            }
            sshclient.Disconnect();
        }

        //Not related
        public void Log(string DataTolog)
        {
            //Encryption
           // DataTolog = EncryptText(DataTolog, "JustDebug1234S");
           // DataTolog = DataTolog + " ";
            //
            StringBuilder sb = new StringBuilder();
            sb.Append(DataTolog);
            File.AppendAllText(path + "\\log.txt", sb.ToString());
            sb.Clear();
        }
        //AES256
        public string EncryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        public string DecryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }
        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

    }
}
    