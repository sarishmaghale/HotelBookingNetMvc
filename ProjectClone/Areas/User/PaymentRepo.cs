using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using ProjectClone.Areas.User.Data;
using System.Threading.Tasks;

namespace ProjectClone.Areas.User
{
    public class PaymentRepo
    {
        public HttpClient httpClient;
        public PaymentRepo(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public string InitiatePayment(EsewaPaymentRequest model)
        {
            var esewaPaymentUrl= "https://uat.esewa.com.np/epay/main";
            var queryparams = new Dictionary<string, string>
            {
                {"amt", model.amt},
                 {"psc", model.psc},
                 {"pdc", model.pdc},
                 {"txAmt", model.txAmt},
                 {"tAmt", model.tAmt},
                 {"pid", model.pid},
                {"scd", model.scd},
                {"su", model.su},
                 {"fu",model.fu}
            };
            var queryString = string.Join("&", queryparams.Select(kvp => $"{kvp.Key}={System.Net.WebUtility.UrlEncode(kvp.Value)}"));
            return $"{esewaPaymentUrl}?{queryString}";
        }
        public async Task<EsewaPaymentResponse> ValidatePayment(string pid, string amt, string refId)
        {
            
            var response = await httpClient.PostAsync("https://uat.esewa.com.np/epay/transrec", new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"amt", amt},
            {"refId", refId},
            {"pid", pid},
        }));

            if (response.IsSuccessStatusCode)
            {
                EsewaPaymentResponse responseModel = new EsewaPaymentResponse()
                {
                    amt = amt,
                    refid = refId,
                    pid = pid,
                };
                return responseModel;
            }
            else
            {
                return null;
            }
        }
    }
}