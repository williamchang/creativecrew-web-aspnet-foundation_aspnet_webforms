using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using ent;

public partial class main_register_user_account : System.Web.UI.Page {
    public String URL = ApplicationCommon.getPageThis();

    private String qsMode = String.Empty;
    private String qsActivationKey = String.Empty;

    private String qsCreated = String.Empty;
    private String qsNotice = String.Empty;

    private int idUser = 0;

    private TableUsers d1 = new TableUsers();

    protected void Page_Load(Object sender, EventArgs e) {
        qsMode = Request.QueryString["mode"];
        qsActivationKey = Request.QueryString["actkey"];

        qsCreated = Request.QueryString["c"];
        qsNotice = Request.QueryString["n"];

        authenticateUser();

        // Mode.
        if(String.Equals(qsMode, "edit")) {
            viewEdit();
        } else if(String.Equals(qsMode, "activation") && !ApplicationCommon.isEmpty(qsActivationKey)) {
            viewActiviation();
        }
        // Notice.
        if(!ApplicationCommon.isEmpty(qsNotice)) {
            lblError.Text = qsNotice;
        }
        // Creation.
        if(String.Equals(qsCreated, "1")) {
            plhForm.Visible = false;
            lblLog.Text = ResourceCommon.msgLog_AccountCreationSuccess;
        } else if(String.Equals(qsCreated, "2")) {
            plhForm.Visible = false;
            lblError.Text = ResourceCommon.msgError_RegistrationUnknown;
        } else if(String.Equals(qsCreated, "3")) {
            plhForm.Visible = false;
            lblError.Text = ResourceCommon.msgError_RegistrationDoesNotExist;
        } else if(String.Equals(qsCreated, "4")) {
            plhForm.Visible = false;
            lblLog.Text = ResourceCommon.msgLog_AccountActivationSuccess;
        } else if(String.Equals(qsCreated, "5")) {
            plhForm.Visible = false;
            lblLog.Text = ResourceCommon.msgLog_RegistrationSuccess;
        } else if(String.Equals(qsCreated, "6")) {
            plhForm.Visible = false;
            lblLog.Text = ResourceCommon.msgError_AccountActivationInvalid;
        }
    }
    /// <summary>Authenticate user.</summary>
    protected void authenticateUser() {
        if(ApplicationCommon.isValidSession(Session)) {
            plhForm.Visible = false;
            plhDirectoryRegistration.Visible = true;
        } else {
            viewAdd();
        }
    }
    /// <summary>View in add mode.</summary>
    protected void viewAdd() {
        plhForm.Visible = true;
        plhDirectoryRegistration.Visible = false;
        ucUserAccount.prop_rowUserAlias.Visible = true;
        ucUserAccount.prop_rowEmail.Visible = true;
        ucUserAccount.prop_rowEmailConfirm.Visible = true;
        ucUserAccount.prop_rowPassword.Visible = false;
        ucUserAccount.prop_rowPasswordConfirm.Visible = false;
    }
    /// <summary>View in activiation mode.</summary>
    protected void viewActiviation() {
        plhForm.Visible = true;
        plhDirectoryRegistration.Visible = false;
        ucUserAccount.prop_rowUserAlias.Visible = false;
        ucUserAccount.prop_rowEmail.Visible = false;
        ucUserAccount.prop_rowEmailConfirm.Visible = false;
        ucUserAccount.prop_rowPassword.Visible = true;
        ucUserAccount.prop_rowPasswordConfirm.Visible = true;
        ucUserAccount.prop_lblTypicalHeader.Text = "Activation";
        if(!d1.hasActivationKey(qsActivationKey)) {
            Response.Redirect(URL + "?c=6");
        }
    }
    /// <summary>View in edit mode.</summary>
    protected void viewEdit() {
        plhForm.Visible = true;
        plhDirectoryRegistration.Visible = false;
        ucUserAccount.prop_plhTypical.Visible = false;
        ucUserAccount.prop_plhEdit.Visible = true;

        idUser = (int)Session["user_id"];
        if(!Page.IsPostBack) {
            DataRow dr1 = d1.dynamicSqlSelect(null, TableUsers.TBL__users, TableUsers.TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(idUser)).Rows[0];
            ucUserAccount.prop_lblEditCurrentEmailValue.Text = (String)dr1[TableUsers.TBL__users__user_email];
            ucUserAccount.prop_txtEditUserAlias.Text = (String)dr1[TableUsers.TBL__users__user_alias];
            ucUserAccount.prop_txtEditNameFirst.Text = (String)dr1[TableUsers.TBL__users__user_name_first];
            ucUserAccount.prop_txtEditNameLast.Text = (String)dr1[TableUsers.TBL__users__user_name_last];
        }
    }
    protected void btnSubmit_Click(Object sender, EventArgs e) {
        if(qsCreated != "1") {
            Boolean success = false;
            Boolean isFieldInvalid = false;

            //try {
                Hashtable p1 = new Hashtable();
                if(String.Equals(qsMode, "edit")) {
                    if(d1.hasPassword(ApplicationCommon.getGeneratedHash(ucUserAccount.prop_txtEditCurrentPassword.Text))) {
                        if(!ApplicationCommon.isEmpty(ucUserAccount.prop_txtEditNewEmail.Text)) {
                            if(!d1.hasEmail(ucUserAccount.prop_txtEditNewEmail.Text)) {
                                p1.Add(TableUsers.TBL__users__user_email, ucUserAccount.prop_txtEditNewEmail.Text.ToLower());
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
                            success = d1.dynamicSqlUpdate(p1, TableUsers.TBL__users, TableUsers.TBL__users___PK__user_id + " = " + idUser);
                        }
                    } else {
                        lblError.Text = ResourceCommon.msgError_AccountInvalid;
                        isFieldInvalid = true;
                    }
                } else if(String.Equals(qsMode, "activation")) {
                    p1.Add(TableUsers.TBL__users__user_password, ApplicationCommon.getGeneratedHash(ucUserAccount.prop_txtPassword.Text));
                    p1.Add(TableUsers.TBL__users__user_activated, 1);
                    success = d1.dynamicSqlUpdate(p1, TableUsers.TBL__users, TableUsers.TBL__users__user_activation_key + " = " + DatabaseCommon.sanitize(qsActivationKey));
                } else {
                    ApplicationEmail ae1 = new ApplicationEmail();
                    if(d1.hasUsernameOrEmail(ucUserAccount.prop_txtUserAlias.Text, ucUserAccount.prop_txtEmail.Text)) {
                        lblError.Text = ResourceCommon.msgError_AccountDuplicate;
                        isFieldInvalid = true;
                    } else {
                        p1.Add(TableUsers.TBL__users__user_alias, ucUserAccount.prop_txtUserAlias.Text);
                        p1.Add(TableUsers.TBL__users__user_name_first, ucUserAccount.prop_txtNameFirst.Text);
                        p1.Add(TableUsers.TBL__users__user_name_last, ucUserAccount.prop_txtNameLast.Text);
                        p1.Add(TableUsers.TBL__users__user_password, String.Empty);
                        p1.Add(TableUsers.TBL__users__user_email, ucUserAccount.prop_txtEmail.Text.ToLower());
                        p1.Add(TableUsers.TBL__users__user_activation_key, String.Empty);
                        p1.Add(TableUsers.TBL__users__user_activated, 0);
                        p1.Add(TableUsers.TBL__users__user_session_key, String.Empty);
                        p1.Add(TableUsers.TBL__users__user_date_login, DBNull.Value);
                        p1.Add(TableUsers.TBL__users__user_date_activation_created, DateTime.Now);
                        p1.Add(TableUsers.TBL__users__user_date_session_created, DBNull.Value);
                        p1.Add(TableUsers.TBL__users__user_date_created, DateTime.Now);
                        p1.Add(TableUsers.TBL__users__user_deleted, 0);
                        /*if(d1.dynamicSqlInsert(p1, TableUsers.TBL__users)) {
                            success = ae1.sendEmailAccountActivation(ucUserAccount.prop_txtUserAlias.Text, d1.getActivationKey(ucUserAccount.prop_txtEmail.Text), ucUserAccount.prop_txtEmail.Text);
                        }*/
                        if(d1.createUserReturnId(p1) > 0) {
                            success = ae1.sendEmailAccountActivation(ucUserAccount.prop_txtUserAlias.Text, d1.getActivationKey(ucUserAccount.prop_txtEmail.Text), ucUserAccount.prop_txtEmail.Text);
                        }
                    }
                }
            //} catch { success = false; }
            Hashtable qs1 = new Hashtable();
            if(success == true && isFieldInvalid == false) {
                if(String.Equals(qsMode, "edit")) {
                    qs1.Add("mode", "edit");
                    qs1.Add("c", "5");
                } else if(String.Equals(qsMode, "activation")) {
                    qs1.Add("c", "4");
                } else {
                    qs1.Add("c", "1");
                }
            } else if(isFieldInvalid == false) {
                if(String.Equals(qsMode, "edit")) {
                    qs1.Add("mode", "edit");
                }
                qs1.Add("c", "2");
            }
            if(isFieldInvalid == false) {
                Response.Redirect(ApplicationCommon.setQueryString(qs1, URL));
            }
        }
    }
}
