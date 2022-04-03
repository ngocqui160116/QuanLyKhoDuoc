using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.InventoryTags;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IInventoryTagsProxy
    {
        Task<BaseResponse<InventoryTagsDto>> GetAllInventoryTags(InventoryTagsRequest request);
    }

    public class InventoryTagsProxy : BaseProxy, IInventoryTagsProxy
    {
        public async Task<BaseResponse<InventoryTagsDto>> GetAllInventoryTags(InventoryTagsRequest request)
        {
            try
            {
                var api = RestService.For<IInventoryTagsApi>(GetHttpClient());

                return await api.GetAllInventoryTags(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IInventoryTagsApi
        {
            [Post("/InventoryTags/GetAllInventoryTags")]
            Task<BaseResponse<InventoryTagsDto>> GetAllInventoryTags([Body] InventoryTagsRequest request);

        }
    }
}
