using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Common
{
    public class CustomException : ApplicationException
    {
        public CustomException()
        {}

        public CustomException(string errorMessage)
            : base(errorMessage) 
        { }
    }
}
