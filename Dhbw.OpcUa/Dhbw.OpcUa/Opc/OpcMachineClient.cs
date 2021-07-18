using Dhbw.OpcUa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;

namespace Dhbw.OpcUa.Opc
{
    public class OpcMachineClient : IDisposable
    {
        private const int OperationTimeout = 1000;
        private const int Lifecycle = 10000;

        private Session Session { get; set; }

        public event EventHandler<ServerConnectionStatus> ConnectionChanged;

        public bool IsConnected { get; private set; }

        public void Initialize()
        {
            if (!OpcLicense.IsInitialized) OpcLicense.Initialize();
            if (!OpcLicense.IsInitialized) throw new LicenseException(typeof(OpcLicense));
        }

        public void Connect(string address, int port = 4840, string protocol = "opc.tcp")
        {
            if (true.Equals(Session?.ConnectionStatus == ServerConnectionStatus.Connected)) Disconnect();

            Session = new Session();
            Session.ConnectionStatusUpdate += Session_ConnectionStatusUpdate;
            Session.Connect($"{protocol}://{address}:{port}", SecuritySelection.None);
        }


        public IEnumerable<OpcNode> BrowseNodes()
            => Session.Browse(ObjectIds.ObjectsFolder, out var point).Select(k => new OpcNode(this, k.NodeId, new List<OpcNode>()));

        public void Write(string nodeName, object value)
        {
            var dataValue = new DataValue
            {
                Value = value
            };

            var nodeId = NodeId.Parse(nodeName);
            var nodesToWrite = new List<WriteValue>
            {
                new WriteValue
                {
                    NodeId = nodeId,
                    AttributeId = Attributes.Value,
                    Value = dataValue
                }
            };

            var writeRes = Session.Write(
                nodesToWrite,
                new RequestSettings
                {
                    OperationTimeout = OperationTimeout
                }
            );

            if (!StatusCode.IsGood(writeRes.Single()))
                throw new InvalidOperationException("Bad Status Code");
        }


        public DataValue Read(string nodeId)
            => Session.Read(new List<ReadValueId>() { new ReadValueId
                {
                    NodeId = NodeId.Parse(nodeId),
                    AttributeId = Attributes.Value
                }}).Single();

        public Subscription Subscribe(IEnumerable<string> nodes)
        {
            var subscription = new Subscription(Session)
            {
                PublishingInterval = 0,
                MaxKeepAliveTime = OperationTimeout,
                Lifetime = Lifecycle,
                MaxNotificationsPerPublish = 0,
                Priority = 0,
                PublishingEnabled = true
            };

            subscription.Create(new RequestSettings()
            {
                OperationTimeout = OperationTimeout
            });



            var monitoredItems = nodes.Select(nodeId => new DataMonitoredItem(NodeId.Parse(nodeId), Attributes.Value)
            {
                MonitoringMode = MonitoringMode.Reporting,
                DataChangeTrigger = DataChangeTrigger.StatusValue,
                SamplingInterval = 0
            })
                .Cast<MonitoredItem>().ToList();

            subscription.CreateMonitoredItems(monitoredItems,
                new RequestSettings { OperationTimeout = OperationTimeout });
            return subscription;
        }

      
        public void Disconnect()
        {
            try
            {
                if (Session == null) return;
                Session.ConnectionStatusUpdate -= Session_ConnectionStatusUpdate;
            }
            finally
            {
                Session?.Disconnect();
                Session?.Dispose();
                Session = null;
            }
        }

        public void Dispose()
            => Disconnect();

        private void Session_ConnectionStatusUpdate(Session sender, ServerConnectionStatusUpdateEventArgs e)
        {
            IsConnected = e.Status == ServerConnectionStatus.Connected;
            ConnectionChanged?.Invoke(this, e.Status);
        }
    }

    public static class OpcLicense
    {
        public static bool IsInitialized { get; private set; }

        public static void Initialize()
        {
            if (IsInitialized) return;
            ApplicationLicenseManager.AddProcessLicenses(Assembly.GetAssembly(typeof(OpcLicense)), "License.lic");
            ApplicationInstance.Default.Start();
            IsInitialized = true;
        }

    }
}
