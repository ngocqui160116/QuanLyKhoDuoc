using Phoenix.Mobile.Core.Models.InventoryTags;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.InventoryTags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IInventoryTagsService
    {
        Task<List<InventoryTagsModel>> GetAllInventoryTags(InventoryTagsRequest request);
    }

    public class InventoryTagsService : IInventoryTagsService
    {
        private readonly IInventoryTagsProxy _InventoryTagsProxy;
        public InventoryTagsService(IInventoryTagsProxy InventoryTagsProxy)
        {
            _InventoryTagsProxy = InventoryTagsProxy;
        }
        public async Task<List<InventoryTagsModel>> GetAllInventoryTags(InventoryTagsRequest request)
        {
            var InventoryTags = await _InventoryTagsProxy.GetAllInventoryTags(request);
            return InventoryTags.Data.MapTo<InventoryTagsModel>();
        }

       
    }
}
