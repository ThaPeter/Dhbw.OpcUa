using System;
using System.Collections.Generic;
using System.Text;

namespace Dhbw.OpcUa.ViewModels
{
    public class NodeValueViewModel : BaseViewModel
    {
        public NodeValueViewModel(object value, DateTime dateTime)
        {
            Value = value;
            DateTime = dateTime;
        }

        public object Value { get;  }

        public DateTime DateTime { get; }
        public string DateTimeString => DateTime.ToString("T");

    }
}
