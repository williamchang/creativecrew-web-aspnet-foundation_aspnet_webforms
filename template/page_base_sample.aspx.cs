using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ent;

public partial class template_page_base_sample : System.Web.UI.Page {
    
    TableUsers t1 = new TableUsers();

    protected void Page_Load(Object sender, EventArgs e) {
        rptrSampleData.DataSource = t1.getUsers(null);
        rptrSampleData.DataBind();
    }
}
