using Falcon.Web.Core.Helpers;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.InventoryTags;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInventoryTagsService
    {
        Task<BaseResponse<InventoryTagsDto>> GetAllInventoryTags(InventoryTagsRequest request);

        ///
        Task<BaseResponse<InventoryTagsDto>> GetAll(InventoryTagsRequest request);
       // Task<BaseResponse<InventoryTagsDto>> GetAllInventoryTagsById(int Medicine,int LotNumber, InventoryTagsRequest request);
    }
    public class InventoryTagsService : IInventoryTagsService
    {
        private readonly DataContext _dataContext;
        public InventoryTagsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BaseResponse<InventoryTagsDto>> GetAllInventoryTags(InventoryTagsRequest request)
        {
            var result = new BaseResponse<InventoryTagsDto>();

            try
            {
                // setup query
                var query = _dataContext.InventoryTags.AsQueryable();

                // filter

                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<InventoryTagsDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /////
        public async Task<BaseResponse<InventoryTagsDto>> GetAll(InventoryTagsRequest request)
        {
            var result = new BaseResponse<InventoryTagsDto>();

            try
            {
                // setup query
                var query = _dataContext.InventoryTags.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.DocumentId))
                {
                    query = query.Where(d => d.DocumentId.Contains(request.DocumentId));
                }

                query = query.OrderByDescending(d => d.Id);

                //var data = await query.ToListAsync();
                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<InventoryTagsDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }
        /*public async Task<BaseResponse<InventoryTagsDto>> GetAllInventoryTagsById(int MedicineId,int LotNumber, InventoryTagsRequest request)
        {
            var result = new BaseResponse<InventoryTagsDto>();
            try
            {
                var query = _dataContext.InventoryTags.AsQueryable();

                query = query.OrderByDescending(d => d.Id);
                //query = query.OrderByDescending(d => d.IdBatch);
                //var get = GetInputInfoById(Id);
                //var list = _dataContext.InventoryTags.Where(d => d.MedicineId == MedicineId && d => d.LotNumber == LotNumber);
                //if (query. == MedicineId && item.LotNumber == LotNumber)
                var data = await query.ToListAsync();
                List<InventoryTagsDto> List = new List<InventoryTagsDto>();
                foreach (var item in data)
                {
                    if(item.MedicineId == MedicineId && item.LotNumber == LotNumber)
                    {
                        List.Id = item.Id;
                        List.DocumentId = item.DocumentId;
                        List.DocumentDate = item.DocumentDate;
                        List.DocumentType = item.DocumentType;
                        List.MedicineId = item.MedicineId;
                        List.LotNumber = item.LotNumber;
                        List.ExpiredDate = item.ExpiredDate;
                        List.Qty_Before = item.Qty_Before;
                        List.Qty = item.Qty;
                        List.Qty_After = item.Qty_After;
                        List.UnitPrice = item.UnitPrice;
                        List.TotalPrice = item.TotalPrice;
                    }
                }
                result.Data = data.MapTo<InventoryTagsDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }*/
    }
}
