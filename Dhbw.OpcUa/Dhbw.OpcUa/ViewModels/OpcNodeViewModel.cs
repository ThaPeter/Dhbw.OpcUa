using Dhbw.OpcUa.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UnifiedAutomation.UaBase;
using Xamarin.Forms;

namespace Dhbw.OpcUa.ViewModels
{
    public class OpcNodeViewModel : BaseViewModel
    {
        public OpcNode Model { get; }

        public ObservableCollection<NodeValueViewModel> LastChanges { get; } = new ObservableCollection<NodeValueViewModel>();

        public ObservableCollection<OpcNodeViewModel> Items { get; } = new ObservableCollection<OpcNodeViewModel>();

        public string Name => Model.Name;

        public string NodeId => Model.NodeId;

        public BuiltInType DataType => Model.DataType;

        public object Value => Model.Value;

        public bool HasItems => Items.Any();

        public OpcNodeViewModel(OpcNode model)
        {
            Model = model;
            foreach(var item in model.Items)
            {
                Items.Add(new OpcNodeViewModel(model));
            }

            Model.ValueChanged += Model_ValueChanged;
        }

        private void Model_ValueChanged(object sender, object e)
        {
            OnPropertyChanged(nameof(Value));
            if(LastChanges.Count > 100)
            {
                while(LastChanges.Count > 90)
                {
                    LastChanges.Remove(LastChanges.First());
                }
            }

            LastChanges.Add(new NodeValueViewModel(e, DateTime.Now));
        }


        
    }

}
