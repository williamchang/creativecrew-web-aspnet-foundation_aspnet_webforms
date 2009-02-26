using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using ent;

public partial class main_account_login : System.Web.UI.Page {
    public String URL = ApplicationCommon.getPath();
    public String URL_USER_ACCOUNT = "~/main/register_user_account.aspx";

    private String _qsMode = String.Empty;
    private String _qsReturn = String.Empty;
    private String _qsNotice = String.Empty;

    protected void Page_Load(Object sender, EventArgs e) {
        _qsMode = Request.QueryString["mode"];
        _qsReturn = Request.QueryString["return"];
        _qsNotice = Request.QueryString["n"];

        if(!Page.IsPostBack) {
            Session.Clear(); // Clear values from session.
            Session.Abandon(); // End current session.
        }

        // Mode.
        if(String.Equals(_qsMode, "lost")) {
            plhLogin.Visible = false;
            plhForgot.Visible = true;

            txtForgotEmail.Focus();
            txtForgotEmail.Attributes["onkeypress"] = "return clickButton(event,'" + btnForgotSubmit.ClientID + "');";
        } else {
            plhLogin.Visible = true;
            plhForgot.Visible = false;

            if(ApplicationCommon.isEmpty(_qsReturn)) {
                _qsReturn = HttpUtility.UrlEncode("~/main/default.aspx");
            }
            txtLoginUser.Focus();
            txtLoginPassword.Attributes["onkeypress"] = "return clickButton(event,'" + btnLoginSubmit.ClientID + "');";
        }
        // Notice.
        if(!ApplicationCommon.isEmpty(_qsNotice)) {
            lblLoginError.Text = _qsNotice;
            lblForgotError.Text = _qsNotice;
        }
    }
    protected void Page_PreRender(Object sender, EventArgs e) {
        this.Page.Header.Controls.Add(JavaScript());
    }
    protected void lbLoginForgot_Click(Object sender, EventArgs e) {
        Hashtable qs1 = new Hashtable();
        qs1.Add("return", _qsReturn);
        qs1.Add("mode", "lost");
        Response.Redirect(ApplicationCommon.setQueryString(qs1, URL));
    }
    protected void btnLoginSubmit_Click(Object sender, EventArgs e) {
        if(ApplicationCommon.isValidLogin(txtLoginUser.Text, txtLoginPassword.Text)) {
            DateTime dtn = DateTime.Now;
            TableUsers t1 = new TableUsers();
            DataRow dr1 = t1.getUser(txtLoginUser.Text);
            int id = (int)dr1[TableUsers.TBL__users__user_id];

            Session.Add("user_id", id);
            Session.Add("user_alias", (String)dr1[TableUsers.TBL__users__user_alias]);
            Session.Add("user_name_first", (String)dr1[TableUsers.TBL__users__user_name_first]);
            Session.Add("user_name_last", (String)dr1[TableUsers.TBL__users__user_name_last]);
            Session.Add("user_email", (String)dr1[TableUsers.TBL__users__user_email]);
            Session.Add("user_date_login", dtn);
            Session.Add(TableUsers.TBL__user_settings, t1.getUserSettings(id));
            t1.setUserDateLogin(id, dtn);
            Response.Redirect(HttpUtility.UrlDecode(_qsReturn));
        } else {
            lblLoginError.Text = ResourceCommon.msgError_LoginInvalid;
        }
    }
    protected void lbForgotLogin_Click(Object sender, EventArgs e) {
        Hashtable qs1 = new Hashtable();
        qs1.Add("return", _qsReturn);
        Response.Redirect(ApplicationCommon.setQueryString(qs1, URL));
    }
    protected void btnForgotSubmit_Click(Object sender, EventArgs e) {
        ApplicationEmail ae1 = new ApplicationEmail();
        TableUsers d1 = new TableUsers();
        if(d1.hasEmail(txtForgotEmail.Text)) {
            DataRow dr1 = d1.dynamicSqlSelect(null, TableUsers.TBL__users, TableUsers.TBL__users__user_email + " = " + DatabaseCommon.sanitize(txtForgotEmail.Text)).Rows[0];
            if(ae1.sendEmailAccountReset((String)dr1[TableUsers.TBL__users__user_alias], d1.getActivationKey((String)dr1[TableUsers.TBL__users__user_email]), (String)dr1[TableUsers.TBL__users__user_email])) {
                lblForgotError.Text = String.Empty;
                txtForgotEmail.Text = String.Empty;

                Hashtable qs1 = new Hashtable();
                qs1.Add("return", _qsReturn);
                Response.Redirect(ApplicationCommon.setQueryString(qs1, URL));
            } else {
                lblForgotError.Text = ResourceCommon.emlError_Failure;
            }
        } else {
            lblForgotError.Text = ResourceCommon.msgError_LoginForgetEmail;
        }
    }

#region JavaScript Embedded

    protected LiteralControl JavaScript() {
        LiteralControl collection = new LiteralControl();
        String wrapper = String.Empty;
        String clientScript = String.Empty;

        wrapper = "\n" + "\t" + "<!-- BEGIN: JavaScript -->" + "\n" + "\t";
        // BEGIN: Script Wrap.
        clientScript =
        "<script type=\"text/javascript\">" + "\n" + "\t" +
        "//<![CDATA[" + "\n" + "\t" +
        "";
        // Embed JavaScript Code.
        clientScript = clientScript + @"
            function clickButton(e, id) {
                var evt = e ? e : window.event;
                var btn = document.getElementById(id);
                if(btn) {
                    if(evt.keyCode == 13) {btn.click();return false;}
                }
            }
        ";
        // END: Script Wrap.
        clientScript = clientScript + "\n" + "\t" +
        "//]]>" + "\n" + "\t" +
        "</script>" +
        "";
        // Collection.
        wrapper += clientScript;
        wrapper += "\n" + "\t" + "<!-- END: JavaScript -->" + "\n" + "\t";
        collection.Text = wrapper;
        return collection;
    }

#endregion

}
