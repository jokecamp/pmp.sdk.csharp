using System;

namespace pmp.sdk.model
{
    /// <summary>
    /// example json response 
    ///     {"access_token":"a2596eff41bc985803678d7d","token_type":"Bearer","token_issue_date":"2014-02-07T18:47:18+00:00","token_expires_in":1380000}
    /// </summary>
    public class Token
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public DateTime TokenIssueDate { get; set; }

        /// <summary>
        /// what unit is this? Milliseconds?
        /// </summary>
        public long TokenExpiresIn { get; set; }

        public bool IsExpired()
        {
            return TokenIssueDate.AddSeconds(TokenExpiresIn) <= DateTime.Now;
        }
    }
}