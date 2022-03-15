using Phoenix.Mobile.Core.Models.Output;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IOutputService
    {
        Task<List<OutputModel>> GetAllOutput(OutputRequest request);
    }

    public class OutputService : IOutputService
    {
        private readonly IOutputProxy _OutputProxy;
        public OutputService(IOutputProxy OutputProxy)
        {
            _OutputProxy = OutputProxy;
        }
        public async Task<List<OutputModel>> GetAllOutput(OutputRequest request)
        {
            var output = await _OutputProxy.GetAllOutput(request);
            return output.Data.MapTo<OutputModel>();
        }
    }
}
