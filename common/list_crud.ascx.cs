using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using ent;

public partial class common_list_crud : System.Web.UI.UserControl {
    public String TITLE = "Unknown";
    public String URL = ApplicationCommon.getPageThis();
    
    public String TABLE1_NAME = String.Empty;
    public String TABLE1_PK = String.Empty;
    public String TABLE1_DELETED = String.Empty;
    public String TABLE1_C1 = String.Empty;
    public String TABLE1_C2 = String.Empty;

    public String TABLE1_DELETED_LABEL = "Deleted";
    public String TABLE1_C1_LABEL = "ID";
    public String TABLE1_C2_LABEL = "Name";

    private String _qsMode = String.Empty;
    private int _qsId = 0;

    private DatabaseCommon d1 = new DatabaseCommon();

#region Properties

    public String prop_TITLE {
        get {return TITLE;}
        set {TITLE = value;}
    }
    public String prop_TABLE1_NAME {
        get {return TABLE1_NAME;}
        set {TABLE1_NAME = value;}
    }
    public String prop_TABLE1_PK {
        get {return TABLE1_PK;}
        set {TABLE1_PK = value;}
    }
    public String prop_TABLE1_DELETED {
        get {return TABLE1_DELETED;}
        set {TABLE1_DELETED = value;}
    }
    public String prop_TABLE1_C1 {
        get {return TABLE1_C1;}
        set {TABLE1_C1 = value;}
    }
    public String prop_TABLE1_C2 {
        get {return TABLE1_C2;}
        set {TABLE1_C2 = value;}
    }
    public String prop_TABLE1_C1_LABEL {
        get {return TABLE1_C1_LABEL;}
        set {TABLE1_C1_LABEL = value;}
    }
    public String prop_TABLE1_C2_LABEL {
        get {return TABLE1_C2_LABEL;}
        set {TABLE1_C2_LABEL = value;}
    }
    public PlaceHolder prop_plhClientScript {
        get { return plhClientScript; }
        set { plhClientScript = value; }
    }
    public PlaceHolder prop_plhJavaScriptDataTableCrud {
        get { return plhJavaScriptDataTableCrud; }
        set { plhJavaScriptDataTableCrud = value; }
    }
    public PlaceHolder prop_plhJavaScriptDataTableEdit {
        get { return plhJavaScriptDataTableEdit; }
        set { plhJavaScriptDataTableEdit = value; }
    }

#endregion

    protected void Page_Load(Object sender, EventArgs e) {
        _qsMode = Request.QueryString["mode"];
        _qsId = System.Convert.ToInt32(Request.QueryString["id"]);
        getRequestJavaScript();

        // Mode.
        if(String.Equals(_qsMode, "add")) {
            viewAdd();
        } else if(String.Equals(_qsMode, "edit") && _qsId > 0) {
            viewEdit();
        } else if(String.Equals(_qsMode, "trash")) {
            viewTrash();
        } else {
            viewCrud();
        }
    }

#region Methods

    /// <summary>Get HTTP request from JavaScript postback.</summary>
    protected void getRequestJavaScript() {
        if(!Page.IsPostBack) {return;}

        String eventName = Request.Form["__EVENTTARGET"];
        String eventArgument = Request.Form["__EVENTARGUMENT"];

        if(String.Equals(eventName, "onMode")) {
            // Get multiple values from serialized string.
            System.Collections.Specialized.NameValueCollection aryEventArguments = System.Web.HttpUtility.ParseQueryString(eventArgument);
            // Set mode by query string.
            if(String.Equals(aryEventArguments["action"], "add")) {
                Hashtable qs1 = new Hashtable();
                qs1.Add("mode", "add");
                Response.Redirect(ApplicationCommon.setQueryString(qs1, URL));
            } else if(String.Equals(aryEventArguments["action"], "edit")) {
                if(!ApplicationCommon.isEmpty(aryEventArguments["id"])) {
                    Hashtable qs1 = new Hashtable();
                    qs1.Add("mode", "edit");
                    qs1.Add("id", System.Convert.ToInt32(aryEventArguments["id"]));
                    Response.Redirect(ApplicationCommon.setQueryString(qs1, URL));
                }
            } else if(String.Equals(aryEventArguments["action"], "trash")) {
                Hashtable qs1 = new Hashtable();
                qs1.Add("mode", "trash");
                Response.Redirect(ApplicationCommon.setQueryString(qs1, URL));
            } else {
                Response.Redirect(URL);
            }
        }
    }
    protected void viewCrud() {
        plhCrud.Visible = true;
        plhClientScript.Visible = true;
        plhJavaScriptDataTableCrud.Visible = true;
    }
    protected void viewAdd() {
        plhAdd.Visible = true;
        plhForm.Visible = true;
    }
    protected void viewEdit() {
        plhEdit.Visible = true;
        plhForm.Visible = true;
        if(!Page.IsPostBack) {
            DataRow dr1 = d1.dynamicSqlSelect(null, TABLE1_NAME, TABLE1_PK + " = " + _qsId.ToString()).Rows[0];
            txtName.Text = (String)dr1[TABLE1_C2];
        }
    }
    protected void viewTrash() {
        plhTrash.Visible = true;
        plhClientScript.Visible = true;
        plhJavaScriptDataTableEdit.Visible = true;
    }

#endregion

#region Events

    protected void btnFormOkay_Click(Object sender, EventArgs e) {
        if(plhAdd.Visible) {
            Hashtable p1 = new Hashtable();
            p1.Add(TABLE1_C2, txtName.Text);
            d1.dynamicSqlInsert(p1, TABLE1_NAME);
            Response.Redirect(URL);
        } else if(plhEdit.Visible) {
            Hashtable p1 = new Hashtable();
            p1.Add(TABLE1_C2, txtName.Text);
            d1.dynamicSqlUpdate(p1, TABLE1_NAME, TABLE1_PK + " = " + _qsId.ToString());
            Response.Redirect(URL);
        }
    }
    protected void btnFormCancel_Click(Object sender, EventArgs e) {
        Response.Redirect(URL);
    }

#endregion

}