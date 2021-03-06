using Phoenix.Mobile.Core.Models.OutputInfo;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.OutputInfo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IOutputInfoService
    {
        Task<List<OutputInfoModel>> GetAllOutputInfo(OutputInfoRequest request);
        Task<List<OutputInfoModel>> CreateOutputInfo(OutputInfoRequest request);
    }

    public class OutputInfoService : IOutputInfoService
    {
        private readonly IOutputInfoProxy _OutputInfoProxy;
        public OutputInfoService(IOutputInfoProxy OutputInfoProxy)
        {
            _OutputInfoProxy = OutputInfoProxy;
        }
        public async Task<List<OutputInfoModel>> GetAllOutputInfo(OutputInfoRequest request)
        {
            var outputinfo = await _OutputInfoProxy.GetAllOutputInfo(request);
            return outputinfo.Data.MapTo<OutputInfoModel>();
        }

        public async Task<List<OutputInfoModel>> CreateOutputInfo( OutputInfoRequest request)
        {
            var outputinfo = await _OutputInfoProxy.CreateOutputInfo( request);
            return outputinfo.Data.MapTo<OutputInfoModel>();
        }
    }
}
