using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Web.UI;
using System.Diagnostics;

public partial class cancelResponse : Page
{
    protected const String clientId = "S1_6eaa0db1afdc41f3becb770878d67d25";
    protected const String secretKey = "e80d068e400649a6ada66777fa350d40";

    protected String resultMsg;
    protected Guid uuid = Guid.NewGuid();

    protected void Page_Load(object sender, EventArgs e)
    {
        var payload = new JsonObject
        {
            ["amount"] = Request["amount"],
            ["reason"] = "test",
            ["orderId"] = uuid
        };

        var client = new RestClient();
        client.Authenticator = new HttpBasicAuthenticator(clientId, secretKey);

        var request = new RestRequest("https://sandbox-api.nicepay.co.kr/v1/payments/" + Request["tid"] + "/cancel");
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

