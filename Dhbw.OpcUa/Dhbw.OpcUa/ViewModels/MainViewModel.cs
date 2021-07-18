using Dhbw.OpcUa.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dhbw.OpcUa.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public OpcSettingViewModel Settings { get; } = new OpcSettingViewModel(new Models.OpcSettings());

        public OpcConnectionViewModel Connection { get; }

        public OpcTreeViewModel Tree { get; }
        public Command NavigateSettingsCommand { get; }

        public MainViewModel()
        {
            var client = new Opc.OpcMachineClient();
            Connection = new OpcConnectionViewModel(Settings, client);
            Tree = new OpcTreeViewModel(client);
            Initialize();
            NavigateSettingsCommand = new Command(NavigateSettingsAsync);
        }


        private async void NavigateSettingsAsync()
            => await Shell.Current.GoToAsync(nameof(ConnectionSettingsPage));

        private void Initialize()
        {
            try
            {
                throw new NotSupportedException("This project doesn't contain a license.");
                //If there is no license file in this project - sorry
                //Ususally this file will be embedded as a ressource - so it is not public in the deliverable
                //Connection.Model.Initialize();
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }

    }
}
