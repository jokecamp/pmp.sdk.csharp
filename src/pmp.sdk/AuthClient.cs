using System;
using System.Net;
using System.Text;
using RestSharp;
using pmp.sdk.Intefaces;
using pmp.sdk.interfaces;
using pmp.sdk.model;

namespace pmp.sdk
{
    public class AuthClient : IAuthClient
    {
        public static void Log(string message)
        {
            // poor mans logger
            Console.WriteLine(message);
        }

        // Inject?
        public string AuthEndpoint = "/auth/access_token";

        public string Host { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        private Token _currentToken;

        public AuthClient(string host, string clientId, string clientSecret)
        {
            Host = host;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        private string GetBasicHash()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", ClientId, ClientSecret)));
        }

        public void RemoveToken()
        {
            
        }

        public Token GetToken()
        {
            if (_currentToken != null && !_currentToken.IsExpired())
            {
                Log("Token retrieved from cache");
                return _currentToken;
            }

            var client = new RestClient(Host);
            var request = new RestRequest(AuthEndpoint) {Method = Method.POST};
            request.AddHeader("Authorization", "Basic " + GetBasicHash());
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("grant_type", "client_credentials");

            var response = client.Execute<Token>(request);

            if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrWhiteSpace(response.Content))
                throw new Exception("Got non-HTTP-200 and/or empty response from the authentication server");

            _currentToken = response.Data;
            return _currentToken;
        }

        public void SignRequestWithToken(IRestRequest request)
        {
            request.AddHeader("Authorization", "Bearer " + GetToken().AccessToken);
            request.AddHeader("Content-Type", "application/vnd.pmp.collection.doc+json");
        }
    }
}

