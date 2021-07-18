using Dhbw.OpcUa.Models;
using Dhbw.OpcUa.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Dhbw.OpcUa.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<OpcSettings> DataStore => DependencyService.Get<IDataStore<OpcSettings>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        protected async void Message(string message)
        {
            try
            {
                await Application.Current?.MainPage.DisplayAlert("Error", message, "Ok");
            }
            catch (NullReferenceException)
            {
                //No main page is initialized - so the error can't be displayed.
            }
        }

        protected void Message(Exception ex)
            => Message(ex.Message);
    }
}
