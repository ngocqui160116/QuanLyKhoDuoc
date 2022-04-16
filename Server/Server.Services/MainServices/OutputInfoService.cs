using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;

using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IOutputInfoService
    {
        Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request);
        Task<CrudResult> CreateOutputInventory(OutputInfoRequest request);
        Task<BaseResponse<OutputInfoDto>> CreateOutputInfo(OutputInfoRequest request);
       // Task<BaseResponse<OutputInfoDto>> Complete(int Id, OutputInfoRequest request);
        ///
        Task<BaseResponse<OutputInfoDto>> GetAllOutputInfoById(int Id, OutputInfoRequest request);
        OutputInfo GetLatestOutputInfo();
    }
    public class OutputInfoService : IOutputInfoService
    {
        private readonly DataContext _dataContext;
        public OutputInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request)
        {
            var result = new BaseResponse<OutputInfoDto>();
            try
            {

                // setup query
                var query = _dataContext.OutputInfos.AsQueryable();

                // filter
               
                //query = query.Where(d => d.Count.Equals(Soluong));

                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<OutputInfoDto>();
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

        #region CreateOutput
        public async Task<BaseResponse<OutputInfoDto>> CreateOutputInfo(OutputInfoRequest request)
        {
            var result = new BaseResponse<OutputInfoDto>();
            var medicineItems = _dataContext.MedicineItems.ToList();
            try
            {
                Output outputs = new Output
                {
                    IdStaff = request.IdStaff,
                    DateOutput = request.DateOutput,
                    IdReason = request.IdReason,
                    Status = "Đã hoàn thành"
            };

                _dataContext.Outputs.Add(outputs);
                await _dataContext.SaveChangesAsync();

                var Latest = GetLatestOutput();

                OutputInfo outputinfos = new OutputInfo();
                foreach (var item in medicineItems)
                {
                    outputinfos.IdOutput = Latest.Id;
                    outputinfos.IdMedicine = item.Medicine_Id;
                    outputinfos.Count = item.Amount;
                    if (request.IdReason == 3 || request.IdReason == 4)
                    {
                        outputinfos.OutputPrice = 0;
                    }
                    else
                    {
                        outputinfos.OutputPrice = item.InputPrice;
                    }
                    outputinfos.Total = outputinfos.Count * outputinfos.OutputPrice;
                    outputinfos.Inventory_Id = item.Inventory_Id;

                    _dataContext.OutputInfos.Add(outputinfos);
                    await _dataContext.SaveChangesAsync();
                }
                result.Success = true;

                //lấy danh sách các chi tiết hóa đơn nhập
                var list = _dataContext.OutputInfos.Where(p => p.IdOutput.Equals(outputs.Id));
                var data = await list.ToListAsync();
                //thêm chi tiết hóa đơn nhập vào kho
                foreach (var item in data)
                {
                    //Thuốc và lô trong kho trùng với hóa đơn nhập
                    var inventories = _dataContext.Inventories.ToList()
                                        .FindAll(d => d.IdMedicine == item.IdMedicine && d.LotNumber == item.Inventory.LotNumber);
                    //đã có thuốc trong kho
                    if (inventories.Count != 0)
                    {
                        var inventory = inventories.FirstOrDefault();
                        //cập nhật lại số lượng tồn trong kho
                       
                        inventory.Count = inventory.Count - item.Count;
                        
                        inventory.IdInputInfo = item.Inventory.IdInputInfo;
                        await _dataContext.SaveChangesAsync();

                        //thêm chi tiết hóa đơn nhập vào thẻ kho
                        InventoryTags inventoryTags = new InventoryTags();
                        inventoryTags.DocumentId = "PX00" + item.Id;
                        inventoryTags.DocumentDate = DateTime.Now;
                        inventoryTags.DocumentType = request.IdReason;
                        inventoryTags.MedicineId = item.IdMedicine;
                        inventoryTags.LotNumber = (int)item.Inventory.LotNumber;
                        inventoryTags.ExpiredDate = DateTime.Now;
                        inventoryTags.Qty_Before = 0;
                        inventoryTags.Qty = item.Count;
                        inventoryTags.Qty_After = inventory.Count;

                        if(request.IdReason == 2)
                        {
                            inventoryTags.UnitPrice = item.OutputPrice;
                        }    
                        else
                        {
                            inventoryTags.UnitPrice = 0;
                        }    
                      
                        inventoryTags.TotalPrice = item.Total;

                        _dataContext.InventoryTags.Add(inventoryTags);
                        await _dataContext.SaveChangesAsync();
                    }
                    //chưa có thuốc trong kho
                    
                }

                result.Data = data.MapTo<OutputInfoDto>();
                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        public async Task<CrudResult> CreateOutputInventory(OutputInfoRequest request)
        {
            var Output = new Output();

            Output.IdStaff = request.IdStaff;
            Output.DateOutput = request.DateOutput;
            Output.IdReason = request.IdReason;
            Output.Status = request.Status;

            _dataContext.Outputs.Add(Output);
            await _dataContext.SaveChangesAsync();

            var OutputInfo = new OutputInfo();

            OutputInfo.IdOutput = Output.Id;
            OutputInfo.IdMedicine = request.IdMedicine;
            OutputInfo.Count = request.Count;
            OutputInfo.Total = request.Total;

            _dataContext.OutputInfos.Add(OutputInfo);
            await _dataContext.SaveChangesAsync();

            var inventories = _dataContext.Inventories.ToList()
                                        .FindAll(d => d.IdMedicine == request.IdMedicine && d.LotNumber == request.IdBatch);

           var inventory = inventories.FirstOrDefault();

            inventory.IdMedicine = request.IdMedicine;
            inventory.Count = inventory.Count - request.Count;
            inventory.LotNumber = inventory.LotNumber;
            //inventory.IdInputInfo = null;

            await _dataContext.SaveChangesAsync();

            var InventoryTags = new InventoryTags();
            InventoryTags.DocumentId = "PX00" + OutputInfo.Id;
            InventoryTags.ExpiredDate = DateTime.Now;
            InventoryTags.DocumentDate = DateTime.Now;
            InventoryTags.LotNumber = (int)inventory.LotNumber;
            InventoryTags.UnitPrice = 2;
            InventoryTags.TotalPrice = 2;
            //InventoryTags.SupplierId = 32;

            InventoryTags.DocumentType = request.IdReason;
            InventoryTags.MedicineId = request.IdMedicine;

            InventoryTags.Qty_After = inventory.Count;
            InventoryTags.Qty = request.Count;
            InventoryTags.Qty_Before = 0;


            _dataContext.InventoryTags.Add(InventoryTags);
            await _dataContext.SaveChangesAsync();

            return new CrudResult() { IsOk = true };
        }


        ///web
        public async Task<BaseResponse<OutputInfoDto>> GetAllOutputInfoById(int Id, OutputInfoRequest request)
        {
            var result = new BaseResponse<OutputInfoDto>();
            try
            {
                var query = _dataContext.InputInfos.AsQueryable();

                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);
                //var get = GetInputInfoById(Id);
                var list = _dataContext.OutputInfos.Where(p => p.IdOutput.Equals(Id));

                var data = await list.ToListAsync();
                result.Data = data.MapTo<OutputInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public OutputInfo GetLatestOutputInfo()
        {
            var query = _dataContext.OutputInfos.AsQueryable();

            query = query.OrderByDescending(d => d.Id);
            var da = query.FirstOrDefault();
            return da;
        }
    }
}
