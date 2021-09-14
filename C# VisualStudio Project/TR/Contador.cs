using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;

namespace TR
{
    public partial class MainWindow
    {
        public void CuentaAtras(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                AD.Content = $"24:00:00";
            });
            Thread.Sleep(1000);
            Horas();
        }
        private void Horas()
        {
            for (int i = _Horas; i >= 0; i--)
            {
                _Minutos = 59;
                _Horas = i;
                Minutos();
            }
        }
        private void Minutos()
        {
            for (int i = _Minutos; i >= 0; i--)
            {
                _Minutos = i;
                _Segundos = 59;
                Segundos();
            }
        }
        private void Segundos()
        {
            for (int i = _Segundos; i >= 0; i--)
            {
                _Segundos = i;
                if (Regex.IsMatch(_Segundos.ToString(), @"^\d$") && Regex.IsMatch(_Minutos.ToString(), @"^\d$"))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        AD.Content = $"{_Horas}:0{_Minutos}:0{_Segundos}";
                    });
                }
                else if (Regex.IsMatch(_Segundos.ToString(), @"^\d$"))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        AD.Content = $"{_Horas}:{_Minutos}:0{_Segundos}";
                    });
                }
                else if (Regex.IsMatch(_Minutos.ToString(), @"^\d$"))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        AD.Content = $"{_Horas}:0{_Minutos}:{_Segundos}";
                    });
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        AD.Content = $"{_Horas}:{_Minutos}:{_Segundos}";

                    });
                }
                Thread.Sleep(1000);

            }
        }
        private int _Horas = 23;
        private int _Minutos;
        private int _Segundos;
    }
}