using Dhbw.OpcUa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dhbw.OpcUa.Services
{
    public class MockDataStore : IDataStore<OpcSettings>
    {
        private OpcSettings Settings { get; set; } = new OpcSettings();

        public Task<OpcSettings> GetItemAsync()
            => Task.FromResult(Settings);

        public Task<bool> UpdateItemAsync(OpcSettings item)
        {
            Settings = item;
            return Task.FromResult(true);
        }
    }
}