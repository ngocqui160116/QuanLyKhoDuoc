using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Inventory;
using System.Collections.Generic;
using System.Linq;
using Phoenix.Shared.Common;
using System.Threading.Tasks;
using System;
using System.Data.Entity;
using AutoMapper;
using System.Collections.ObjectModel;

namespace Phoenix.Server.Services.MainServices
{
   
    public interface IInventoryService
    {

        Task<BaseResponse<InventoryDto>> GetAllInventory(InventoryRequest request);
    }
    public class InventoryService : IInventoryService
    {
        private readonly DataContext _dataContext;
        public InventoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BaseResponse<InventoryDto>> GetAllInventory(InventoryRequest request)
        {
            var result = new BaseResponse<InventoryDto>();

            try
            {
                // setup query
                var query = _dataContext.Inventories.AsQueryable();

                // filter
                
                query = query.OrderByDescending(d => d.IdMedicine);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<InventoryDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
