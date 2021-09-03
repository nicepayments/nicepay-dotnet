﻿using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Web.UI;
using System.Diagnostics;

public partial class response : Page
{
    protected const String clientId = "클라이언트 키";
    protected const String secretKey = "시크릿 키";

    protected String resultMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
        var payload = new JsonObject
        {
            ["amount"] = Request["amount"]
        };

        var client = new RestClient();
        client.Authenticator = new HttpBasicAuthenticator(clientId, secretKey);

        var request = new RestRequest("https://api.nicepay.co.kr/v1/payments/" + Request["tid"]);
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

