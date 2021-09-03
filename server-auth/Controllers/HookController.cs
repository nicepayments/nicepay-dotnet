using System;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace nicepay
{
    [RoutePrefix("hook")]
    public class HookController : ApiController
    {
        [HttpPost]
        [Route("/")]
        public string HookPost([FromBody] Object body)
        {
            JObject jObject = JObject.Parse(body.ToString());
            String resultCode = (string)jObject["resultCode"];

            if (resultCode.Equals("0000"))
            {
                //비즈니스 로직 처리
                return "ok";
            }

            return "fail";
        }
    }
}