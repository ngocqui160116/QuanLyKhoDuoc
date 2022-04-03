using Phoenix.Mobile.Core.Models.Inventory;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IInventoryService
    {
        Task<List<InventoryModel>> GetAllInventory(InventoryRequest request);
    }
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryProxy _InventoryProxy;
        public InventoryService(IInventoryProxy InventoryProxy)
        {
            _InventoryProxy = InventoryProxy;
        }
        public async Task<List<InventoryModel>> GetAllInventory(InventoryRequest request)
        {
            var Inventory = await _InventoryProxy.GetAllInventory(request);
            return Inventory.Data.MapTo<InventoryModel>();
        }
    }
}
