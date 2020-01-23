using System;

namespace ShopStore.Common
{
    public class OperationResult
    {
        public bool Successed { get; set; }
        public int ErrorCode { get; set; }
        public string Description { get; set; }
    }
}
