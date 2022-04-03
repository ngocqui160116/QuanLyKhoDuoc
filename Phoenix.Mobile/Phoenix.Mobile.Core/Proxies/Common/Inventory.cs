using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Inventory;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IInventoryProxy
    {
        Task<BaseResponse<InventoryDto>> GetAllInventory(InventoryRequest request);
    }

    public class InventoryProxy : BaseProxy, IInventoryProxy
    {
        public async Task<BaseResponse<InventoryDto>> GetAllInventory(InventoryRequest request)
        {
            try
            {
                var api = RestService.For<IInventoryApi>(GetHttpClient());

                return await api.GetAllInventory(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IInventoryApi
        {
            [Post("/Inventory/GetAllInventory")]
            Task<BaseResponse<InventoryDto>> GetAllInventory([Body] InventoryRequest request);

        }
    }
}
