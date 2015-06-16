using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ONECardPublisher
{
    /// <summary>
    /// The Publisher will send Card data to the Sitrion ONE services
    /// </summary>
    public class Publisher
    {
        private string _companyPassword { get; set; }
        private Guid _companyId { get; set; }
        private string _baseAddress { get; set; }
        public string environmentName { get; set; }

        public Publisher(string companyPassword, Guid companyId)
            : this(companyPassword, companyId, null)
        { }

        public Publisher(string companyPassword, Guid companyId, string environmentName)
        {
            this._companyId = companyId;
            this._companyPassword = companyPassword;
            if (string.IsNullOrEmpty(environmentName))
                environmentName = "https://one.sitrion.com";
            this.environmentName = environmentName;
        }

        public void PublishCard(string messageType, CardActionType action, Dictionary<string, string> data, string uniqueId)
        {
            PublishCard(new CardRouting()
            {
                MessageType = messageType,
                ActionType = action,
                Data = data,
                CardId = uniqueId,
                CardDate = DateTime.UtcNow
            });
        }

        public void PublishCard(CardRouting card)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = UTF8Encoding.UTF8;

            webClient.Headers[HttpRequestHeader.Authorization] =
                "sitauth " + Convert.ToBase64String(Encoding.Default.GetBytes(this._companyPassword ?? "No password provided"));

            webClient.Headers["sitrion-companyId"] = this._companyId.ToString("D", CultureInfo.InvariantCulture);
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");

            var data = card.ToJson();

            string url = string.Format(CultureInfo.InvariantCulture, "{0}/api/v2/cardpublisher/write", environmentName);
            webClient.UploadString(url, data);
        }
    }
}
