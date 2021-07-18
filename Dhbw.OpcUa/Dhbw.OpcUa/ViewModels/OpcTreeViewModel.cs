using Dhbw.OpcUa.Opc;
using Dhbw.OpcUa.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dhbw.OpcUa.ViewModels
{
    public class OpcTreeViewModel : BaseViewModel
    {
        public OpcTreeViewModel(OpcMachineClient client)
        {
            Client = client;
            Client.ConnectionChanged += Client_ConnectionChanged;
            DetailsCommand = new Command<OpcNodeViewModel>(ShowDetails);
        }

        private OpcNodeViewModel _selectedItem;
        private bool _isLoading;
        private OpcMachineClient Client { get; }
        public ObservableCollection<OpcNodeViewModel> Items { get; } = new ObservableCollection<OpcNodeViewModel>();

        public Command<OpcNodeViewModel> DetailsCommand { get; }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }


        public OpcNodeViewModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private async void ShowDetails(OpcNodeViewModel value)
        {
            SelectedItem = value;
            await Shell.Current.GoToAsync(nameof(ItemDetailPage));
        }

        private async void Client_ConnectionChanged(object sender, UnifiedAutomation.UaClient.ServerConnectionStatus e)
        {
            if (e == UnifiedAutomation.UaClient.ServerConnectionStatus.Connected)
            {
                await LoadAsync();
            }
            else
            {
                Items.Clear();
            }
        }

        private async Task LoadAsync()
        {
            try
            {
                var items = await Task.Run(() =>
                {
                    return Client.BrowseNodes();
                });

                foreach (var item in items.Select(k => new OpcNodeViewModel(k)))
                {
                    Items.Add(item);
                }

            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }
    }
}
