using Dhbw.OpcUa.Models;
using Dhbw.OpcUa.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dhbw.OpcUa.ViewModels
{
    public class OpcSettingViewModel : BaseViewModel
    {
        public OpcSettings Model { get; }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public string Address
        {
            get => Model.Address;
            set
            {
                Model.Address = value;
                OnPropertyChanged();
            }
        }

        public int Port
        {
            get => Model.Port;
            set
            {
                Model.Port = value;
                OnPropertyChanged();
            }
        }

        public OpcSettingViewModel(OpcSettings model)
        {
            Model = model;

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            Title = "Opc Ua Connection Settings";
        }

        private async void OnCancel()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ItemsPage));
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }

        private async void OnSave()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ItemsPage));
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }

    }
}
