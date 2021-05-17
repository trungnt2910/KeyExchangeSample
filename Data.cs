using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KeyExchangeSample
{
    static class Data
    {
        // Trusted public key of the server:
        const string Modulus = "xxzv536dYVPq7Odgl5aAGj+nEo928Vk7GGB9eRat3JdPFpzA78SiWUeO1ESLQk1hLjXSa67SiZ6mO7k7bHJy97y+Kh88Rtmcbxsoo/p/59v3C1VMS9QG/rgEIFrveBA3GlB2IEGLcjmbpnYtHhsFUFV3Kx5jVmG8ROXX/7N0ulddBHJG9Py+hg5KjP3cxXrE2Aujr4he1C2SjhyD1RJuG1IwLV0y0s4kGqwiMAHFhn8vxtts1Lgb1qIYX9pcF67SyI5PXknMt1o+4Md8xRKJsICa9hGiWyjtP1CDqZbuePj9FNGIAj6I0UONcVY9yqC7hCdepPesbbSLwUCNsYGiwQ==";
        const string Exponent = "AQAB";
        public static readonly RSAParameters ServerPublicKey = new RSAParameters
        {
            Modulus = Convert.FromBase64String(Modulus),
            Exponent = Convert.FromBase64String(Exponent)
        };
    }
}
