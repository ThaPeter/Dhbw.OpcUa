using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dhbw.OpcUa.CustomControl
{
    public class ContentControl : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(ContentControl), null, propertyChanged: OnItemTemplateChanged);

        private static void OnItemTemplateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var cp = (ContentControl)bindable;

            var template = cp.ItemTemplate;
            if (template != null)
            {
                var content = (View)template.CreateContent();
                cp.Content = content;
            }
            else
            {
                cp.Content = null;
            }
        }

        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set
            {
                SetValue(ItemTemplateProperty, value);
            }
        }
    }
}
