using System;
using System.Web.UI;
using System.Diagnostics;

public partial class response : Page
{
    protected String resultMsg;
    protected void Page_Load(object sender, EventArgs e)
    {
        resultMsg = Request["resultMsg"];
        var resultCode = Request["resultCode"];
        var tid = Request["tid"];

        Debug.WriteLine(tid);

        try
        {
            if (resultCode == "0000")
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
}