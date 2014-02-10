using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using pmp.sdk.interfaces;

namespace pmp.sdk.model
{
    public class CollectionDoc
    {
        public string Version { get; set; }
        public string Href { get; set; }

        public Dictionary<string, object> Attributes { get; set; }
        public Dictionary<string, List<Link>> Links { get; set; }
        

        private readonly string _uri;
        private readonly IAuthClient _authClient;

        public CollectionDoc(string uri, IAuthClient authClient)
        {
            _uri = uri;
            _authClient = authClient;
        }

        public CollectionDoc GetDocument()
        {
            var client = new RestClient(_uri);
            var request = new RestRequest();
            _authClient.SignRequestWithToken(request);
            var response = client.Execute(request);
            return Deserialize(response.Content);
        }

        public static CollectionDoc Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<CollectionDoc>(json);
        }

        public IEnumerable<string> GetQueryRelTypes()
        {
            return Links["query"].SelectMany(x => x.Rels);
        }

        /// <summary>
        /// Gets a default "query" relation link that has the given URN
        /// </summary>
        /// <param name="urn"></param>
        /// <returns></returns>
        public Link Query(string urn)
        {
            if (Links == null) return null;
            if (Links["query"] == null) return null;

            return Links["query"].FirstOrDefault(x => x.Rels.Contains(urn));
        }
    }

    public class Link
    {
        [JsonProperty("href-template")]
        public string HrefTemplate { get; set; }
        public string Href { get; set; }
        public string Title { get; set; }
        public string[] Rels { get; set; }
    }
}
