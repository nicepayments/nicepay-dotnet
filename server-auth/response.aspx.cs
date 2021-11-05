using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Web.UI;
using System.Diagnostics;

public partial class response : Page
{
    protected const String clientId = "S2_af4543a0be4d49a98122e01ec2059a56";
    protected const String secretKey = "9eb85607103646da9f9c02b128f2e5ee";

    protected String resultMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
        var payload = new JsonObject
        {
            ["amount"] = Request["amount"]
        };

        var client = new RestClient();
        client.Authenticator = new HttpBasicAuthenticator(clientId, secretKey);

        var request = new RestRequest("https://sandbox-api.nicepay.co.kr/v1/payments/" + Request["tid"]);
        request.Method = Method.POST;
        request.AddHeader("Accept", "application/json");
        request.AddParameter("application/json; charset=utf-8", payload.ToString(), ParameterType.RequestBody);

        try
        {
            var response = client.Execute(request);
            Debug.WriteLine(response.Content);

            JObject jObject = JObject.Parse(response.Content);
            resultMsg = (string)jObject["resultMsg"];

            if((string)jObject["resultCode"] == "0000")
            {
                Debug.WriteLine("success");
            }
            else
            {
                Debug.WriteLine("fail");
            }
        }
        catch(Exception error)
        {
            Debug.WriteLine(error);
        }
    }
}

