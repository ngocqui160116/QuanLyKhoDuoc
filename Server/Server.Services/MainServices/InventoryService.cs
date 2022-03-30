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
                var person = (from d in _dataContext.InputInfos
                              join e in _dataContext.OutputInfos
                              on d.IdMedicine equals e.IdMedicine into j1

                              from r in j1.DefaultIfEmpty()

                              select new
                              {
                                  Id = d.Id,
                                  IdMedicine = d.IdMedicine,
                                  Count = d.Count - r.Count
                                  
                              })
                    .ToList();
                //if (request.IdMedicine != 0)
                //{
                //    person = person.Where(d => d.IdMedicine.Equals(request.IdMedicine));
                //}



               // var congfig = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                //var mapper = congfig.CreateMapper();
                //var listcart = query.Select(mapper.Map<InventoryDto>).ToList();

                //var data = await query.ToListAsync();

                //result.Data = data.MapTo<CartListDto>();
                result.Data = person.MapTo<InventoryDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
