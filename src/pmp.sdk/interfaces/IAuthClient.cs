using RestSharp;
using pmp.sdk.model;

namespace pmp.sdk.interfaces
{
    /// <summary>
    /// The methods needed to authenticate and sign requests
    /// </summary>
    public interface IAuthClient
    {
        Token GetToken();
        void RemoveToken();
        void SignRequestWithToken(IRestRequest request);
    }
}
