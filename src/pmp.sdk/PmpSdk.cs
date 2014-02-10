using System;
using Newtonsoft.Json;
using RestSharp;
using pmp.sdk.Intefaces;
using pmp.sdk.interfaces;
using pmp.sdk.model;

namespace pmp.sdk
{
    public class PmpSdk
    {
        private readonly string _uri;
        private readonly IAuthClient _authClient;

        public PmpSdk(string uri, IAuthClient authClient)
        {
            _uri = uri;
            _authClient = authClient;
        }

        public void GetDocument()
        {
            var client = new RestClient(_uri);

            var request = new RestRequest();
            _authClient.SignRequestWithToken(request);

            var response = client.Execute(request);
            var json = response.Content;

            Console.WriteLine(json);
            
            // testing differnt types of serialization
            var obj = Deserialize(json);
        }

        public static CollectionDoc Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<CollectionDoc>(json);
        }
    }
}
