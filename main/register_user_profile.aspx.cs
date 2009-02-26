using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using ent;
public partial class main_register_user_profile : System.Web.UI.Page {
    /*
    public String URL = "register_user_profile.aspx";
    public String URL_USER_ACCOUNT = "~/main/register_user_account.aspx";

    private String qsMode = String.Empty;

    private String qsCreated = String.Empty;
    private String qsNotice = String.Empty;

    private TableUser d1 = new TableUser();
    private TableLocation d2 = new TableLocation();

    protected void Page_Load(Object sender, EventArgs e) {
        qsMode = Request.QueryString["mode"];

        qsCreated = Request.QueryString["c"];
        qsNotice = Request.QueryString["n"];

        authenticateUser();

        // Mode.
        if(String.Equals(qsMode, "edit")) {
            viewEdit();
        }
        // Notice.
        if(!ApplicationCommon.isNullOrEmpty(qsNotice)) {
            lblError.Text = qsNotice;
        }
        // Creation.
        if(String.Equals(qsCreated, "1")) {
            plhForm.Visible = false;
            plhDirectory.Visible = true;
            lblLog.Text = ResourceCommon.msgLog_RegistrationSuccess;
        } else if(String.Equals(qsCreated, "2")) {
            plhForm.Visible = false;
            lblError.Text = ResourceCommon.msgError_RegistrationUnknown;
        } else if(String.Equals(qsCreated, "3")) {
            plhForm.Visible = false;
            lblError.Text = ResourceCommon.msgError_RegistrationDoesNotExist;
        }
    }
    /// <summary>Authenticate user.</summary>
    protected void authenticateUser() {
        if(!ApplicationCommon.isValidSession(Session)) {
            Response.Redirect(URL_USER_ACCOUNT + "?n=" + ResourceCommon.qsError_RegistrationInvalidSession);
        } else {
            if((qsMode != "edit" && ApplicationCommon.isNullOrEmpty(qsCreated)) && d1.dynamicSqlCount(d1.TABLE2_NAME, d1.TABLE2_FK1 + " = " + Session["user_id"].ToString()) == 1) {
                Response.Redirect(URL + "?mode=edit");
            }
        }
    }
    /// <summary>View in edit mode.</summary>
    protected void viewEdit() {
        Boolean success = false;

        try {
            if(!Page.IsPostBack) {
                Hashtable p1 = new Hashtable();
                p1.Add(d1.TABLE2_FK1, (int)Session["user_id"]);
                DataRow dr1 = d1.getUserProfile(p1).Rows[0];
                ucUserProfile.prop_txtNameFirst.Text = (String)dr1[d1.TABLE2_C2];
                ucUserProfile.prop_txtNameMiddle.Text = (String)dr1[d1.TABLE2_C3];
                ucUserProfile.prop_txtNameLast.Text = (String)dr1[d1.TABLE2_C4];
                ucUserProfile.prop_txtOccupation.Text = (String)dr1[d1.TABLE2_C5];
                ucUserProfile.prop_txtPhone.Text = (String)dr1[d1.TABLE2_C6];
                ucUserProfile.prop_txtOrganizationName.Text = (String)dr1[d2.TABLE1_C2];
                ucUserProfile.prop_txtOrganizationAddress1.Text = (String)dr1[d2.TABLE1_C3];
                ucUserProfile.prop_txtOrganizationAddress2.Text = (String)dr1[d2.TABLE1_C4];
                ucUserProfile.prop_txtOrganizationCity.Text = (String)dr1[d2.TABLE1_C5];
                ucUserProfile.prop_ddlOrganizationState.ClearSelection();
                ucUserProfile.prop_ddlOrganizationState.Items.FindByValue((String)dr1[d2.TABLE1_C6]).Selected = true;
                ucUserProfile.prop_txtOrganizationZip.Text = (String)dr1[d2.TABLE1_C7];
                ucUserProfile.prop_ddlOrganizationCountry.ClearSelection();
                ucUserProfile.prop_ddlOrganizationCountry.Items.FindByValue((String)dr1[d2.TABLE1_C8]).Selected = true;
            }
            success = true;
        } catch { success = false; }
        if(success == false) {
            Response.Redirect(URL + "?c=3");
        }
    }*/
    protected void btnSubmit_Click(Object sender, EventArgs e) {/*
        if(!String.Equals(qsCreated, "1")) {
            Boolean success = false;

            try {
                Hashtable p1 = new Hashtable();
                p1.Add(d1.TABLE2_FK1, (int)Session["user_id"]);
                p1.Add(d2.TABLE1_C2, ucUserProfile.prop_txtOrganizationName.Text);
                p1.Add(d2.TABLE1_C3, ucUserProfile.prop_txtOrganizationAddress1.Text);
                p1.Add(d2.TABLE1_C4, ucUserProfile.prop_txtOrganizationAddress2.Text);
                p1.Add(d2.TABLE1_C5, ucUserProfile.prop_txtOrganizationCity.Text);
                p1.Add(d2.TABLE1_C6, ucUserProfile.prop_ddlOrganizationState.SelectedValue);
                p1.Add(d2.TABLE1_C7, ucUserProfile.prop_txtOrganizationZip.Text);
                p1.Add(d2.TABLE1_C8, ucUserProfile.prop_ddlOrganizationCountry.SelectedValue);
                p1.Add(d1.TABLE2_C2, ucUserProfile.prop_txtNameFirst.Text);
                p1.Add(d1.TABLE2_C3, ucUserProfile.prop_txtNameMiddle.Text);
                p1.Add(d1.TABLE2_C4, ucUserProfile.prop_txtNameLast.Text);
                p1.Add(d1.TABLE2_C5, ucUserProfile.prop_txtOccupation.Text);
                p1.Add(d1.TABLE2_C6, ucUserProfile.prop_txtPhone.Text);
                if(String.Equals(qsMode, "edit")) {
                    success = d1.setUserProfile(p1);
                } else {
                    success = d1.createUserProfile(p1);
                }
            } catch { success = false; }
            Hashtable qs1 = new Hashtable();
            if(success == true) {
                if(String.Equals(qsMode, "edit")) {
                    qs1.Add("mode", "edit");
                }
                qs1.Add("c", "1");
            } else {
                if(String.Equals(qsMode, "edit")) {
                    qs1.Add("mode", "edit");
                }
                qs1.Add("c", "2");
            }
            Response.Redirect(ApplicationCommon.setQueryString(qs1, URL));
        }*/
    }
}
