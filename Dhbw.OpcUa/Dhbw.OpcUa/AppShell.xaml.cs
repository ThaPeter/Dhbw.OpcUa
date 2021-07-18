using Dhbw.OpcUa.ViewModels;
using Dhbw.OpcUa.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dhbw.OpcUa
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(ConnectionSettingsPage), typeof(ConnectionSettingsPage));
        }
    }
}
