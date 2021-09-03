using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Web.UI;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

public partial class response : Page
{
    protected const String clientId = "클라이언트 키";
    protected const String secretKey = "시크릿 키";

    protected Guid uuid = Guid.NewGuid();
    protected String resultMsg;
    protected String bid;

    protected void Page_Load(object sender, EventArgs e)
    {
        var plainText = "cardNo=" + Request["cardNo"] +
                        "&expYear=" + Request["expYear"] +
                        "&expMonth=" + Request["expMonth"] +
                        "&idNo=" + Request["idNo"] +
                        "&cardPw=" + Request["cardPw"];

        var payload = new JsonObject
        {
            ["encData"] = Encrypt(plainText, secretKey.Substring(0, 32), secretKey.Substring(0, 16)),
            ["orderId"] = uuid,
            ["encMode"] = "A2"
        };

        var client = new RestClient();
        client.Authenticator = new HttpBasicAuthenticator(clientId, secretKey);

        var request = new RestRequest("https://api.nicepay.co.kr/v1/subscribe/regist");
        request.Method = Method.POST;
        request.AddHeader("Accept", "application/json");
        request.AddParameter("application/json; charset=utf-8", payload.ToString(), ParameterType.RequestBody);

        try
        {
            var response = client.Execute(request);
            Debug.WriteLine(response.Content);

            JObject jObject = JObject.Parse(response.Content);
            resultMsg = (string)jObject["resultMsg"];
            bid = (string)jObject["bid"];

            if ((string)jObject["resultCode"] == "0000")
            {
                Debug.WriteLine("success");
            }
            else
            {
                Debug.WriteLine("fail");
            }
        }
        catch (Exception error)
        {
            Debug.WriteLine(error);
        }
    }

    public string Billing(string bid)
    {
        Guid uuid = Guid.NewGuid();
        JsonObject jsonObject = new JsonObject
        {
            ["orderId"] = uuid,
            ["amount"] = 1004,
            ["goodsName"] = "test",
            ["cardQuota"] = 0,
            ["useShopInterest"] = false
        };

        var payload = jsonObject;
        string responseContent = "";

        var client = new RestClient();
        client.Authenticator = new HttpBasicAuthenticator(clientId, secretKey);

        var request = new RestRequest("https://api.nicepay.co.kr/v1/subscribe/" + bid + "/payments");
        request.Method = Method.POST;
        request.AddHeader("Accept", "application/json");
        request.AddParameter("application/json; charset=utf-8", payload.ToString(), ParameterType.RequestBody);

        try
        {
            var response = client.Execute(request);
            responseContent = response.Content;
            Debug.WriteLine(responseContent);

            JObject jObject = JObject.Parse(response.Content);
             
            if ((string)jObject["resultCode"] == "0000")
            {
                Debug.WriteLine("success");
            }
            else
            {
                Debug.WriteLine("fail");
            }
        }
        catch (Exception error)
        {
            Debug.WriteLine(error);
            return "fail";
        }

        return responseContent;
    }

    public string Expire(string bid)
    {
        Guid uuid = Guid.NewGuid();
        JsonObject jsonObject = new JsonObject
        {
            ["orderId"] = uuid,
        };

        var payload = jsonObject;
        string responseContent = "";

        var client = new RestClient();
        client.Authenticator = new HttpBasicAuthenticator(clientId, secretKey);

        var request = new RestRequest("https://api.nicepay.co.kr/v1/subscribe/" + bid + "/expire");
        request.Method = Method.POST;
        request.AddHeader("Accept", "application/json");
        request.AddParameter("application/json; charset=utf-8", payload.ToString(), ParameterType.RequestBody);

        try
        {
            var response = client.Execute(request);
            responseContent = response.Content;
            Debug.WriteLine(responseContent);

            JObject jObject = JObject.Parse(response.Content);

            if ((string)jObject["resultCode"] == "0000")
            {
                Debug.WriteLine("success");
            }
            else
            {
                Debug.WriteLine("fail");
            }
        }
        catch (Exception error)
        {
            Debug.WriteLine(error);
            return "fail";
        }

        return responseContent;
    }

    public static string Encrypt(string text, string key, string iv)
    {
        AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        UTF8Encoding utf8 = new UTF8Encoding();

        byte[] byteKey = utf8.GetBytes(key);
        byte[] byteIv = utf8.GetBytes(iv);

        aes.BlockSize = 128;
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        byte[] src = Encoding.UTF8.GetBytes(text);
        using (ICryptoTransform encrypt = aes.CreateEncryptor(byteKey, byteIv))
        {
            byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
            encrypt.Dispose();
            return BitConverter.ToString(dest).Replace("-", "");
        }
    }

}