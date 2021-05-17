using System;
using System.Collections.Generic;
using System.Text;

namespace KeyExchangeSample.Structs
{
    class ServerAccessInfo
    {
        public PublicKey PublicKey { get; set; }
        public string Token { get; set; }
    }
}
