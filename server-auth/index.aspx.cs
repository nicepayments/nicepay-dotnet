using System;
using System.Web.UI;
public partial class index : Page{
    protected String orderId;

    protected void Page_Load(object sender, EventArgs e){
        Guid uuid = Guid.NewGuid();
        orderId = uuid.ToString();
    }
}
