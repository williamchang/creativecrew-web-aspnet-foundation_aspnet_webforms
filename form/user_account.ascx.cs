using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class form_user_account : System.Web.UI.UserControl {

#region Properties

    public Label prop_lblTypicalHeader {
        get {return lblTypicalHeader;}
        set {lblTypicalHeader = value;}
    }
    public HtmlTableRow prop_rowUserAlias {
        get {return rowUserAlias;}
        set {rowUserAlias = value;}
    }
    public HtmlTableRow prop_rowNameFirst {
        get {return rowNameFirst;}
        set {rowNameFirst = value;}
    }
    public HtmlTableRow prop_rowNameLast {
        get {return rowNameLast;}
        set {rowNameLast = value;}
    }
    public HtmlTableRow prop_rowEmail {
        get {return rowEmail;}
        set {rowEmail = value;}
    }
    public HtmlTableRow prop_rowEmailConfirm {
        get {return rowEmailConfirm;}
        set {rowEmailConfirm = value;}
    }
    public HtmlTableRow prop_rowPassword {
        get {return rowPassword;}
        set {rowPassword = value;}
    }
    public HtmlTableRow prop_rowPasswordConfirm {
        get {return rowPasswordConfirm;}
        set {rowPasswordConfirm = value;}
    }
    public PlaceHolder prop_plhTypical {
        get {return plhTypical;}
        set {plhTypical = value;}
    }
    public TextBox prop_txtUserAlias {
        get {return txtUserAlias;}
        set {txtUserAlias = value;}
    }
    public TextBox prop_txtNameFirst {
        get {return txtNameFirst;}
        set {txtNameFirst = value;}
    }
    public TextBox prop_txtNameLast {
        get {return txtNameLast;}
        set {txtNameLast = value;}
    }
    public TextBox prop_txtEmail {
        get {return txtEmail;}
        set {txtEmail = value;}
    }
    public TextBox prop_txtPassword {
        get {return txtPassword;}
        set {txtPassword = value;}
    }
    public TextBox prop_txtPasswordConfirm {
        get {return txtPasswordConfirm;}
        set {txtPasswordConfirm = value;}
    }
    public PlaceHolder prop_plhEdit {
        get {return plhEdit;}
        set {plhEdit = value;}
    }
    public HtmlTableRow prop_rowEditUserAlias {
        get {return rowEditUserAlias;}
        set {rowEditUserAlias = value;}
    }
    public HtmlTableRow prop_rowEditNameFirst {
        get {return rowEditNameFirst;}
        set {rowEditNameFirst = value;}
    }
    public HtmlTableRow prop_rowEditNameLast {
        get {return rowEditNameLast;}
        set {rowEditNameLast = value;}
    }
    public HtmlTableRow prop_rowEditCurrentEmail {
        get {return rowEditCurrentEmail;}
        set {rowEditCurrentEmail = value;}
    }
    public Label prop_lblEditCurrentEmailValue {
        get {return lblEditCurrentEmailValue;}
        set {lblEditCurrentEmailValue = value;}
    }
    public HtmlTableRow prop_rowEditCurrentPassword {
        get {return rowEditCurrentPassword;}
        set {rowEditCurrentPassword = value;}
    }
    public TextBox prop_txtEditUserAlias {
        get {return txtEditUserAlias;}
        set {txtEditUserAlias = value;}
    }
    public TextBox prop_txtEditNameFirst {
        get {return txtEditNameFirst;}
        set {txtEditNameFirst = value;}
    }
    public TextBox prop_txtEditNameLast {
        get {return txtEditNameLast;}
        set {txtEditNameLast = value;}
    }
    public TextBox prop_txtEditCurrentPassword {
        get {return txtEditCurrentPassword;}
        set {txtEditCurrentPassword = value;}
    }
    public TextBox prop_txtEditNewEmail {
        get {return txtEditNewEmail;}
        set {txtEditNewEmail = value;}
    }
    public TextBox prop_txtEditNewPassword {
        get {return txtEditNewPassword;}
        set {txtEditNewPassword = value;}
    }
    public TextBox prop_txtEditNewPasswordConfirm {
        get {return txtEditNewPasswordConfirm;}
        set {txtEditNewPasswordConfirm = value;}
    }

#endregion

    protected void Page_Init(Object sender, EventArgs e) {}
}
