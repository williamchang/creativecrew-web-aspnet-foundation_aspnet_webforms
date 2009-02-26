using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using ent;

public partial class controlpanel_users_crud : System.Web.UI.Page {
    public String URL = ApplicationCommon.getPageThis();
    public String URL_USER_ACCOUNT = "~/main/register_user_account.aspx";

    private String _qsMode = String.Empty;
    private int _qsId = 0;

    private TableUsers t1 = new TableUsers();
    private TableLists t2 = new TableLists();
    private TableLocations t3 = new TableLocations();

    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);
        this.Page.Header.DataBind();
    }
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
            } else if(String.Equals(aryEventArguments["action"], "impersonate")) {
                if(!ApplicationCommon.isEmpty(aryEventArguments["id"])) {
                    int id = System.Convert.ToInt32(aryEventArguments["id"]);
                    DataRow dr1 = t1.dynamicSqlSelect(null, TableUsers.TBL__users, TableUsers.TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(id)).Rows[0];
                    if(!dr1.IsNull(TableUsers.TBL__users__user_date_login)) {
                        Session.Clear();
                        Session.Add("user_id", id);
                        Session.Add("user_alias", (String)dr1[TableUsers.TBL__users__user_alias]);
                        Session.Add("user_name_first", (String)dr1[TableUsers.TBL__users__user_name_first]);
                        Session.Add("user_name_last", (String)dr1[TableUsers.TBL__users__user_name_last]);
                        Session.Add("user_email", (String)dr1[TableUsers.TBL__users__user_email]);
                        Session.Add("user_date_login", (DateTime)dr1[TableUsers.TBL__users__user_date_login]);
                        Session.Add(TableUsers.TBL__user_settings, t1.getUserSettings(id));
                        Response.Redirect(URL_USER_ACCOUNT);
                    } else {
                        lblError.Text = ResourceCommon.msgError_UserImpersonateFailed;
                    }
                }
            } else {
                Response.Redirect(URL);
            }
        }
    }
    /// <summary>Init form input values.</summary>
    protected void initFormInputValues() {
        // First load data for manipulation before postback.
        if(!Page.IsPostBack) {
            // Get list from database.
            ucUserRoles.prop_cblRoles.Visible = false;
            ucUserRoles.prop_ddlRoles.Visible = true;
            ucUserRoles.prop_ddlRoles.DataSource = ApplicationCommon.getListRoles();
            ucUserRoles.prop_ddlRoles.DataTextField = "Key";
            ucUserRoles.prop_ddlRoles.DataValueField = "Value";
            ucUserRoles.prop_ddlRoles.DataBind();
        }
    }
    /// <summary>View in CRUD mode.</summary>
    protected void viewCrud() {
        plhCrud.Visible = true;
        plhClientScript.Visible = true;
        plhJavaScriptDataTableCrud.Visible = true;
    }
    /// <summary>View in add mode.</summary>
    protected void viewAdd() {
        plhAdd.Visible = true;
        plhForm.Visible = true;
        // Init form input values.
        initFormInputValues();
        // First load data for manipulation before postback.
        if(!Page.IsPostBack) {
            // List type controls goes here.
            ucUserRoles.prop_ddlRoles.Items.FindByValue(((int)ApplicationCommon.enumRoles.Subscriber).ToString()).Selected = true;
        }
    }
    /// <summary>View in edit mode.</summary>
    protected void viewEdit() {
        plhEdit.Visible = true;
        plhForm.Visible = true;
        // Set form.
        ucUserAccount.prop_plhTypical.Visible = false;
        ucUserAccount.prop_plhEdit.Visible = true;
        ucUserAccount.prop_rowEditCurrentPassword.Visible = false;
        // Init form input values.
        initFormInputValues();
        // First load data for manipulation before postback.
        if(!Page.IsPostBack) {
            DataRow dr1 = t1.dynamicSqlSelect(null, TableUsers.TBL__users, TableUsers.TBL__users___PK__user_id + " = " + _qsId).Rows[0];
            ucUserAccount.prop_lblEditCurrentEmailValue.Text = (String)dr1[TableUsers.TBL__users__user_email];
            ucUserAccount.prop_txtEditUserAlias.Text = (String)dr1[TableUsers.TBL__users__user_alias];
            ucUserAccount.prop_txtEditNameFirst.Text = (String)dr1[TableUsers.TBL__users__user_name_first];
            ucUserAccount.prop_txtEditNameLast.Text = (String)dr1[TableUsers.TBL__users__user_name_last];
            ucUserRoles.prop_ddlRoles.Items.FindByValue(t1.getUserRole(_qsId).ToString()).Selected = true;
            // Get user profile from database.
            if(t1.hasUserProfile(_qsId)) {
                ucUserAccount.prop_rowEditNameFirst.Visible = false;
                ucUserAccount.prop_rowEditNameLast.Visible = false;
                ucUserProfile.Visible = true;

                Hashtable p1 = new Hashtable();
                p1.Add(TableUsers.TBL__user_profiles___FK__user_id, _qsId);
                DataRow dr2 = t1.getUserProfile(p1).Rows[0];
                ucUserProfile.prop_ddlNameSalutation.ClearSelection();
                ucUserProfile.prop_ddlNameSalutation.Items.FindByValue((String)dr2[TableUsers.TBL__user_profiles__user_name_middle]).Selected = true;
                ucUserProfile.prop_txtNameFirst.Text = (String)dr1[TableUsers.TBL__users__user_name_first];
                ucUserProfile.prop_txtNameMiddle.Text = (String)dr2[TableUsers.TBL__user_profiles__user_name_middle];
                ucUserProfile.prop_txtNameLast.Text = (String)dr1[TableUsers.TBL__users__user_name_last];
                ucUserProfile.prop_txtOccupation.Text = (String)dr2[TableUsers.TBL__user_profiles__user_occupation];
                ucUserProfile.prop_txtPhone.Text = (String)dr2[TableUsers.TBL__user_profiles__user_phone];
                ucUserProfile.prop_txtOrganizationName.Text = (String)dr2[TableLocations.TBL__locations__location_name];
                ucUserProfile.prop_txtOrganizationAddress1.Text = (String)dr2[TableLocations.TBL__locations__location_address1];
                ucUserProfile.prop_txtOrganizationAddress2.Text = (String)dr2[TableLocations.TBL__locations__location_address2];
                ucUserProfile.prop_txtOrganizationCity.Text = (String)dr2[TableLocations.TBL__locations__location_city];
                ucUserProfile.prop_ddlOrganizationState.ClearSelection();
                ucUserProfile.prop_ddlOrganizationState.Items.FindByValue((String)dr2[TableLocations.TBL__locations__location_state]).Selected = true;
                ucUserProfile.prop_txtOrganizationZip.Text = (String)dr2[TableLocations.TBL__locations__location_zip];
                ucUserProfile.prop_ddlOrganizationCountry.ClearSelection();
                ucUserProfile.prop_ddlOrganizationCountry.Items.FindByValue((String)dr2[TableLocations.TBL__locations__location_country]).Selected = true;
            }
        }
    }
    /// <summary>View in trash mode.</summary>
    protected void viewTrash() {
        plhTrash.Visible = true;
        plhClientScript.Visible = true;
        plhJavaScriptDataTableEdit.Visible = true;
    }

#endregion

#region Events

    protected void btnCrudTrash_Click(Object sender, EventArgs e) {
        Response.Redirect(URL + "?mode=trash");
    }
    protected void btnFormOkay_Click(Object sender, EventArgs e) {
        if(plhAdd.Visible) {
            bool isFieldInvalid = false;
            Hashtable p1 = new Hashtable();
            if(t1.hasUsernameOrEmail(ucUserAccount.prop_txtUserAlias.Text, ucUserAccount.prop_txtEmail.Text)) {
                lblError.Text = ResourceCommon.msgError_AccountDuplicate;
                isFieldInvalid = true;
            } else {
                p1.Add(TableUsers.TBL__users__user_alias, ucUserAccount.prop_txtUserAlias.Text);
                p1.Add(TableUsers.TBL__users__user_name_first, ucUserAccount.prop_txtNameFirst.Text);
                p1.Add(TableUsers.TBL__users__user_name_last, ucUserAccount.prop_txtNameLast.Text);
                p1.Add(TableUsers.TBL__users__user_password, ApplicationCommon.getGeneratedHash(ucUserAccount.prop_txtPassword.Text));
                p1.Add(TableUsers.TBL__users__user_email, ucUserAccount.prop_txtEmail.Text);
                p1.Add(TableUsers.TBL__users__user_activation_key, String.Empty);
                p1.Add(TableUsers.TBL__users__user_activated, 1);
                p1.Add(TableUsers.TBL__users__user_session_key, String.Empty);
                p1.Add(TableUsers.TBL__users__user_date_login, DBNull.Value);
                p1.Add(TableUsers.TBL__users__user_date_activation_created, DateTime.Now);
                p1.Add(TableUsers.TBL__users__user_date_session_created, DBNull.Value);
                p1.Add(TableUsers.TBL__users__user_date_created, DateTime.Now);
                p1.Add(TableUsers.TBL__users__user_deleted, 0);
                p1.Add(TableUsers.TBL__user_settings___setting_value, ucUserRoles.prop_ddlRoles.SelectedValue);
            }
            if(isFieldInvalid == false) {
                t1.createUserReturnId(p1);
                //t1.createUserSetting(TableUsers.TBL__user_settings___setting_key_role, ucUserRoles.prop_ddlRoles.SelectedValue, _qsId);
                Response.Redirect(URL);
            }
        } else if(plhEdit.Visible) {
            bool isFieldInvalid = false;
            Hashtable p1 = new Hashtable();
            if(!ApplicationCommon.isEmpty(ucUserAccount.prop_txtEditNewEmail.Text)) {
                if(!t1.hasEmail(ucUserAccount.prop_txtEditNewEmail.Text)) {
                    p1.Add(TableUsers.TBL__users__user_email, ucUserAccount.prop_txtEditNewEmail.Text);
                } else {
                    lblError.Text = ResourceCommon.msgError_AccountInvalid;
                    isFieldInvalid = true;
                }
            }
            if(!ApplicationCommon.isEmpty(ucUserAccount.prop_txtEditNewPassword.Text)) {
                p1.Add(TableUsers.TBL__users__user_password, ApplicationCommon.getGeneratedHash(ucUserAccount.prop_txtEditNewPassword.Text));
            }
            if(isFieldInvalid == false) {
                p1.Add(TableUsers.TBL__users__user_alias, ucUserAccount.prop_txtEditUserAlias.Text);
                p1.Add(TableUsers.TBL__users__user_name_first, ucUserAccount.prop_txtEditNameFirst.Text);
                p1.Add(TableUsers.TBL__users__user_name_last, ucUserAccount.prop_txtEditNameLast.Text);
                p1.Add(TableUsers.TBL__users__user_activated, 1);
                p1.Add(TableUsers.TBL__users__user_date_activation_created, DBNull.Value);
                t1.dynamicSqlUpdate(p1, TableUsers.TBL__users, TableUsers.TBL__users___PK__user_id + " = " + _qsId);
                t1.setUserSetting(TableUsers.TBL__user_settings___setting_key_role, ucUserRoles.prop_ddlRoles.SelectedValue, _qsId);
                // Set user profile to database.
                if(ucUserProfile.Visible) {
                    Hashtable p2 = new Hashtable();
                    p2.Add(TableUsers.TBL__user_profiles, _qsId);
                    p2.Add(TableLocations.TBL__locations__location_name, ucUserProfile.prop_txtOrganizationName.Text);
                    p2.Add(TableLocations.TBL__locations__location_address1, ucUserProfile.prop_txtOrganizationAddress1.Text);
                    p2.Add(TableLocations.TBL__locations__location_address2, ucUserProfile.prop_txtOrganizationAddress2.Text);
                    p2.Add(TableLocations.TBL__locations__location_city, ucUserProfile.prop_txtOrganizationCity.Text);
                    p2.Add(TableLocations.TBL__locations__location_state, ucUserProfile.prop_ddlOrganizationState.SelectedValue);
                    p2.Add(TableLocations.TBL__locations__location_zip, ucUserProfile.prop_txtOrganizationZip.Text);
                    p2.Add(TableLocations.TBL__locations__location_country, ucUserProfile.prop_ddlOrganizationCountry.SelectedValue);
                    p2.Add(TableUsers.TBL__users__user_name_first, ucUserProfile.prop_txtNameFirst.Text);
                    p2.Add(TableUsers.TBL__user_profiles__user_name_middle, ucUserProfile.prop_txtNameMiddle.Text);
                    p2.Add(TableUsers.TBL__users__user_name_last, ucUserProfile.prop_txtNameLast.Text);
                    p2.Add(TableUsers.TBL__user_profiles__user_occupation, ucUserProfile.prop_txtOccupation.Text);
                    p2.Add(TableUsers.TBL__user_profiles__user_phone, ucUserProfile.prop_txtPhone.Text);
                    t1.setUserProfile(p2);
                }
                Response.Redirect(URL);
            }
        }
    }
    protected void btnFormCancel_Click(Object sender, EventArgs e) {
        Response.Redirect(URL);
    }

#endregion

}