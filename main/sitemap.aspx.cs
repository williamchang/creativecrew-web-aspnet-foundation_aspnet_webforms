using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using ent;

public partial class main_sitemap : System.Web.UI.Page {
    public String URL = ApplicationCommon.getPath();
    public String URL_CONTENT = "~/main/content.aspx";

    //private bool _permitManagement = false;

    private TableUsers t1 = new TableUsers();

    protected void Page_Load(Object sender, EventArgs e) {
        // Authenticate.
        authenticateUser();
    }

#region Methods

    /// <summary>Authenticate user.</summary>
    protected void authenticateUser() {
        if(!ApplicationCommon.isValidSession(Session)) {
            //_permitManagement = false;
        } else {
            //_permitManagement = true;
        }
    }

#endregion

#region Events

    protected void rptrSitemap_ItemDataBound(Object sender, RepeaterItemEventArgs e) {}
    protected void rptrSitemap_ItemCommand(Object sender, RepeaterCommandEventArgs e) {}

#endregion

}
