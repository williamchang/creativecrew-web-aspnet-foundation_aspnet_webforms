using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class form_user_roles : System.Web.UI.UserControl {

#region Properties

    public Label prop_lblRoles {
        get {return lblRoles;}
        set {lblRoles = value;}
    }
    public CheckBoxList prop_cblRoles {
        get {return cblRoles;}
        set {cblRoles = value;}
    }
    public DropDownList prop_ddlRoles {
        get {return ddlRoles;}
        set {ddlRoles = value;}
    }

#endregion

    protected void Page_Init(Object sender, EventArgs e) {}
}