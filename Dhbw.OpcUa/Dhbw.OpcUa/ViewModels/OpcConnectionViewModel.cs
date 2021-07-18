using Dhbw.OpcUa.Opc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dhbw.OpcUa.ViewModels
{
    public class OpcConnectionViewModel : BaseViewModel
    {



        private bool _isConnected;
        public OpcSettingViewModel Settings { get; }

        public OpcMachineClient Model { get; }

        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }

        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }


        public OpcConnectionViewModel(OpcSettingViewModel settings, OpcMachineClient client)
        {
            Settings = settings;
            Model = client;
            Model.ConnectionChanged += Model_ConnectionChanged;
            ConnectCommand = new Command(ConnectAsync);
            DisconnectCommand = new Command(DisconnectAsync);
        }

        private void Model_ConnectionChanged(object sender, UnifiedAutomation.UaClient.ServerConnectionStatus e)
        {
            IsConnected = e == UnifiedAutomation.UaClient.ServerConnectionStatus.Connected;
        }


        private async void ConnectAsync()
        {
            try
            {
                await Task.Run(() => Model.Connect(Settings.Address, Settings.Port));
            }
            catch(Exception ex)
            {
                Message(ex);
            }
        }

        private async void DisconnectAsync()
        {
            try
            {
                await Task.Run(() => Model.Disconnect());
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }

    }
}
