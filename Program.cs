using KeyExchangeSample.Structs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KeyExchangeSample
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            // Tạo key RSA cho session hiện tại.
            #region KeyGeneration
            using var cryptoService = new RSACryptoServiceProvider(1024);
            cryptoService.PersistKeyInCsp = false;

            Console.WriteLine("Generated RSA key:");
            var rsaParams = cryptoService.ExportParameters(true);

            var members = rsaParams.GetType().GetFields();
            foreach (var fieldInfo in members)
            {
                Console.WriteLine($"{fieldInfo.Name}: {Convert.ToBase64String((byte[])fieldInfo.GetValue(rsaParams))}");
            }

            var publicKey = new PublicKey
            {
                Modulus = Convert.ToBase64String(rsaParams.Modulus),
                Exponent = Convert.ToBase64String(rsaParams.Exponent)
            };
            #endregion

            // Gửi server
            #region SendRequest
            var collection = new NameValueCollection();
            collection.Add("key", JsonConvert.SerializeObject(publicKey));
            var responseString = await Server.SendRequestAsync(HttpMethod.Get, "key", "getKey", collection);
            #endregion

            #region Decode
            var response = JsonConvert.DeserializeObject<ServerKeyResponse>(responseString);
            var encryptedChunks = JsonConvert.DeserializeObject<List<string>>(response.EncryptedInfo);

            byte[] data = Cryptography.Decrypt(encryptedChunks, cryptoService.ExportParameters(true));

            // Xem key có phải là key thật không, hay là do Man-in-the-middle.
            Console.WriteLine($"Key's integrity: {Cryptography.Verify(data, response.Signature)}");

            var encoding = new UTF8Encoding();
            var serverPublicKeyJson = encoding.GetString(data);
            var serverPublicKey = JsonConvert.DeserializeObject<ServerAccessInfo>(serverPublicKeyJson);
            #endregion

            Console.WriteLine("Server's public key:");
            Console.WriteLine(serverPublicKey.PublicKey.Modulus);
            Console.WriteLine(serverPublicKey.PublicKey.Exponent);

            Console.WriteLine("Access Token:");
            Console.WriteLine(serverPublicKey.Token);

            Console.WriteLine("Hello World!");

            return 0;
        }
    }
}
