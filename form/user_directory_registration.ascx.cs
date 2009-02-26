using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class form_user_directory_registration : System.Web.UI.UserControl {
    protected void Page_Load(Object sender, EventArgs e) {
    }
    protected void btnRegistration1Proceed_Click(Object sender, EventArgs e) {
        Response.Redirect("register_user_account.aspx?mode=edit");
    }
}