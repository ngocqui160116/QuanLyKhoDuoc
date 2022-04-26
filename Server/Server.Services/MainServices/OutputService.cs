using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Output;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IOutputService
    {
        Task<BaseResponse<OutputDto>> GetAllOutput(OutputRequest request);
        Task<CrudResult> UpdateOutput(string Id, OutputRequest request);
        Task<CrudResult> DeleteOutput(string Id);
        Output GetLatestOutput();
        ///
        Task<BaseResponse<OutputDto>> GetAll(OutputRequest request);
        Task<BaseResponse<OutputDto>> Create(OutputRequest request);
    }
    public class OutputService : IOutputService
    {
        private readonly DataContext _dataContext;
        private readonly IInventoryService _inventoryService;
        private readonly IOutputInfoService _outputinfoService;
        public OutputService(DataContext dataContext, IInventoryService inventoryService, IOutputInfoService outputinfoService)
        {
            _dataContext = dataContext;
            _inventoryService = inventoryService;
            _outputinfoService = outputinfoService;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<OutputDto>> GetAllOutput(OutputRequest request)
        {
            //setup query
            var result = new BaseResponse<OutputDto>();
            try
            {
                // setup query
                var query = _dataContext.Outputs.AsQueryable();

                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<OutputDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        #region GetLatestOutput
        public Output GetLatestOutput()
        {
            var query = _dataContext.Outputs.AsQueryable();

            query = query.OrderByDescending(d => d.Id);
            var da = query.FirstOrDefault();
            return da;
        }

        #endregion

        public async Task<CrudResult> UpdateOutput(string Id, OutputRequest request)
        {
            var Output = _dataContext.Outputs.Find(Id);
            Output.IdStaff = request.IdStaff;
            Output.DateOutput = request.DateOutput;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> DeleteOutput(string Id)
        {
            var Output = _dataContext.Outputs.Find(Id);
            if (Output == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.Outputs.Remove(Output);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        //////
        public async Task<BaseResponse<OutputDto>> GetAll(OutputRequest request)
        {
            //setup query
            var result = new BaseResponse<OutputDto>();
            try
            {
                // setup query
                var query = _dataContext.Outputs.AsQueryable();

                /*if (!int.TryParse(request.Id))
                {
                    query = query.Where(d => d.Id.Contains(request.Id));
                }*/

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<OutputDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public async Task<BaseResponse<OutputDto>> Create(OutputRequest request)
        {
            var result = new BaseResponse<OutputDto>();
            try
            {
                //thêm hóa đơn xuất
                Output outputs = new Output
                {
                    IdStaff = request.IdStaff,
                    IdReason = request.IdReason,
                    DateOutput = request.DateOutput,
                    Status = "Đã hoàn thành"
                };

                _dataContext.Outputs.Add(outputs);
                await _dataContext.SaveChangesAsync();

                var LatestOutput = GetLatestOutput();
                //thêm chi tiết hóa đơn xuất
                OutputInfo outputinfos = new OutputInfo();
                foreach (var item in request.List)
                {
                    outputinfos.IdOutput = LatestOutput.Id;
                    outputinfos.IdMedicine = item.medicineId;
                    outputinfos.Count = item.Count;
                    if(request.IdReason == 2)
                    {
                        outputinfos.OutputPrice = item.OutputPrice;
                        outputinfos.Total = item.Total;
                    }
                    else
                    {
                        outputinfos.OutputPrice = 0;
                        outputinfos.Total = 0;
                    }

                    _dataContext.OutputInfos.Add(outputinfos);
                    await _dataContext.SaveChangesAsync();
                    //cập nhật số lượng xuất vào kho
                    var inventories = _inventoryService.GetListInventory();
                    foreach (var i in inventories)
                    {
                        if(i.IdMedicine == item.medicineId && i.LotNumber == item.LotNumber)
                        {
                            i.Count = i.Count - item.Count;
                            await _dataContext.SaveChangesAsync();
                        }
                    }

                    //thêm thẻ kho
                    var LatestOutputInfo = _outputinfoService.GetLatestOutputInfo();
                    InventoryTags inventoryTags = new InventoryTags();
                    inventoryTags.DocumentId = "PX00" + LatestOutputInfo.Id;
                    inventoryTags.DocumentDate = DateTime.Now;
                    inventoryTags.DocumentType = request.IdReason;
                    inventoryTags.MedicineId = item.medicineId;
                    inventoryTags.LotNumber = item.LotNumber;
                    inventoryTags.Qty = item.Count;
                    _dataContext.InventoryTags.Add(inventoryTags);
                    await _dataContext.SaveChangesAsync();

                }
                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
