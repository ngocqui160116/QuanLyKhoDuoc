using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Medicine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IMedicine_ImageService
    {
        Task<BaseResponse<Medicine_ImageDto>> GetAllMedicine_Image(Medicine_ImageRequest request);
    }
    public class Medicine_ImageService : IMedicine_ImageService
    {
        private readonly DataContext _dataContext;
        public Medicine_ImageService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách thuốc
        public async Task<BaseResponse<Medicine_ImageDto>> GetAllMedicine_Image(Medicine_ImageRequest request)
        {
            var result = new BaseResponse<Medicine_ImageDto>();
            try
            {
                //setup query
                var query = _dataContext.Medicine_Images.AsQueryable();
                //filter
                if (!string.IsNullOrEmpty(request.FileName))
                {
                    query = query.Where(d => d.FileName.Contains(request.FileName));
                }
                if (!string.IsNullOrEmpty(request.RelativePath))
                {
                    query = query.Where(d => d.RelativePath.Contains(request.RelativePath));
                }


                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<Medicine_ImageDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
