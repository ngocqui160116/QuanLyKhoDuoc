using Phoenix.Shared.InputInfo;
using System.Collections.Generic;

namespace Phoenix.Shared.Common
{
    public class BaseResponse<T>
    {

        public List<T> Data { get; set; }
        public T Record { get; set; }
        public int DataCount { get; set; }
        public bool success { get; set; }
    }
}
