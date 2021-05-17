using System;
using System.Collections.Generic;
using System.Text;

namespace KeyExchangeSample.Structs
{
    public class ServerKeyResponse
    {
        public string EncryptedInfo { get; set; }
        public string Signature { get; set; }
    }
}
