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
                var query = (from c in _dataContext.Inventories
                             join s in _dataContext.InputInfos on c.IdInputInfo equals s.Id
                             join p in _dataContext.OutputInfos on s.Id equals p.Id
                             select new
                             {
                                 Id = s.Id,
                                 IdMedicine = p.IdMedicine,
                                 Count = p.Count - s.Count

                             }).AsQueryable();
                if (request.IdMedicine != 0)
                {
                    query = query.Where(d => d.IdMedicine.Equals(request.IdMedicine));
                }



                var congfig = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                var mapper = congfig.CreateMapper();
                var listcart = query.Select(mapper.Map<InventoryDto>).ToList();

                //var data = await query.ToListAsync();

                //result.Data = data.MapTo<CartListDto>();
                result.Data = listcart.MapTo<InventoryDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
