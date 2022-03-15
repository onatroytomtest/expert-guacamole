using System;
using System.Collections.Generic;
using System.Text;

namespace Faircode.Platform.Remoting
{
    public class ClientSettings
    {
        public int RetryCount { get; set; }

        public override string ToString()
        {
            return $"{nameof(RetryCount)} : {RetryCount} {Environment.NewLine}";
        }
    }
}
