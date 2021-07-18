using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dhbw.OpcUa.Services
{
    public interface IDataStore<T>
    {
        Task<bool> UpdateItemAsync(T item);
        Task<T> GetItemAsync();
    }
}
