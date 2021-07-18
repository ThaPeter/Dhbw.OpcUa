using Dhbw.OpcUa.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dhbw.OpcUa.TemplateSelector
{
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HierachieTemplate { get; set; }

        public DataTemplate ItemTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is OpcNodeViewModel value)) return null;
            return value.HasItems ? HierachieTemplate : ItemTemplate;
        }
    }
}
