using Dhbw.OpcUa.Opc;
using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;

namespace Dhbw.OpcUa.Models
{
    public class OpcNode
    {
        private OpcMachineClient Client { get; }

        private Subscription Subscription { get; set; }

        public OpcNode(OpcMachineClient client, ExpandedNodeId nodeId, List<OpcNode> items)
        {
            Client = client;
            ExpandedNodeId = nodeId;
            Items = items;
            Initialize();
        }

        public List<OpcNode> Items { get; } = new List<OpcNode>();

        public ExpandedNodeId ExpandedNodeId { get; }

        public string NodeId => ExpandedNodeId.Identifier.ToString();

        public string Name { get; set; }

        public BuiltInType DataType { get; set; }

        public object Value { get; set; }

        public event EventHandler<object> ValueChanged;

        public void Subscribe()
        {
            Subscription = Client.Subscribe(new List<string> { NodeId });
            Subscription.DataChanged += Subscription_DataChanged;
        }

        public void Unsubscribe()
        {
            if (Subscription == null) return;
            Subscription.DataChanged -= Subscription_DataChanged;
            Subscription.Delete();
            Subscription = null;
        }

        private void Subscription_DataChanged(Subscription subscription, DataChangedEventArgs e)
        {
            if (e.DataChanges == null) return;
            foreach(var change in e.DataChanges)
            {
                Value = change.Value.Value;
                ValueChanged.Invoke(this, Value);
            }
        }

        private void Initialize()
        {
            var value = Client.Read(NodeId);
            Value = value.Value;
            DataType = value.WrappedValue.DataType;
            Name = value.WrappedValue.ToQualifiedName().Name;
        }

    }
}
