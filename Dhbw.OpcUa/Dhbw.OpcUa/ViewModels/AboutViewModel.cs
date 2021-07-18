using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Dhbw.OpcUa.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
        }

        public ICommand OpenWebCommand { get; } = new Command(async () => await Browser.OpenAsync("https://www.cas.dhbw.de/startseite/"));
    }
}