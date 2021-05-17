using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KeyExchangeSample
{
    public static class Cryptography
    {
        public static byte[] Decrypt(List<string> strings, RSAParameters keys)
        {
            var bytes = new List<byte>();

            using var service = new RSACryptoServiceProvider();
            service.ImportParameters(keys);
            service.PersistKeyInCsp = false;

            foreach (var str in strings)
            {
                byte[] data = Convert.FromBase64String(str);
                bytes.AddRange(service.Decrypt(data, false));
            }

            return bytes.ToArray();
        }

        public static bool Verify(byte[] data, string signature, RSAParameters? keys = null)
        {
            using var service = new RSACryptoServiceProvider();
            service.ImportParameters(keys ?? Data.ServerPublicKey);
            service.PersistKeyInCsp = false;

            var signatureData = Convert.FromBase64String(signature);

            return service.VerifyData(data, CryptoConfig.MapNameToOID("SHA512"), signatureData);
        }
    }
}
