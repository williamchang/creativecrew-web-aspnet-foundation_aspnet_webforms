using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ent;

public partial class form_user_profile : System.Web.UI.UserControl {

#region Properties

    public HtmlTableRow prop_rowOrganizationName {
        get {return rowOrganizationName;}
        set {rowOrganizationName = value;}
    }
    public HtmlTableRow prop_rowOrganizationAddress1 {
        get {return rowOrganizationAddress1;}
        set {rowOrganizationAddress1 = value;}
    }
    public HtmlTableRow prop_rowOrganizationAddress2 {
        get {return rowOrganizationAddress2;}
        set {rowOrganizationAddress2 = value;}
    }
    public HtmlTableRow prop_rowOrganizationCity {
        get {return rowOrganizationCity;}
        set {rowOrganizationCity = value;}
    }
    public HtmlTableRow prop_rowOrganizationState {
        get {return rowOrganizationState;}
        set {rowOrganizationState = value;}
    }
    public HtmlTableRow prop_rowOrganizationZip {
        get {return rowOrganizationZip;}
        set {rowOrganizationZip = value;}
    }
    public HtmlTableRow prop_rowOrganizationCountry {
        get {return rowOrganizationCountry;}
        set {rowOrganizationCountry = value;}
    }
    public DropDownList prop_ddlNameSalutation {
        get {return ddlNameSalutation;}
        set {ddlNameSalutation = value;}
    }
    public TextBox prop_txtNameFirst {
        get {return txtNameFirst;}
        set {txtNameFirst = value;}
    }
    public TextBox prop_txtNameMiddle {
        get {return txtNameMiddle;}
        set {txtNameMiddle = value;}
    }
    public TextBox prop_txtNameLast {
        get {return txtNameLast;}
        set {txtNameLast = value;}
    }
    public TextBox prop_txtOccupation {
        get {return txtOccupation;}
        set {txtOccupation = value;}
    }
    public TextBox prop_txtPhone {
        get {return txtPhone;}
        set {txtPhone = value;}
    }
    public TextBox prop_txtPhoneExtension {
        get {return txtPhoneExtension;}
        set {txtPhoneExtension = value;}
    }
    public TextBox prop_txtOrganizationName {
        get {return txtOrganizationName;}
        set {txtOrganizationName = value;}
    }
    public TextBox prop_txtOrganizationAddress1 {
        get {return txtOrganizationAddress1;}
        set {txtOrganizationAddress1 = value;}
    }
    public TextBox prop_txtOrganizationAddress2 {
        get {return txtOrganizationAddress2;}
        set {txtOrganizationAddress2 = value;}
    }
    public TextBox prop_txtOrganizationCity {
        get {return txtOrganizationCity;}
        set {txtOrganizationCity = value;}
    }
    public DropDownList prop_ddlOrganizationState {
        get {return ddlOrganizationState;}
        set {ddlOrganizationState = value;}
    }
    public TextBox prop_txtOrganizationZip {
        get {return txtOrganizationZip;}
        set {txtOrganizationZip = value;}
    }
    public DropDownList prop_ddlOrganizationCountry {
        get {return ddlOrganizationCountry;}
        set {ddlOrganizationCountry = value;}
    }

#endregion

    protected void Page_Init(Object sender, EventArgs e) {
        if(!Page.IsPostBack) {
            // Populate form control with salutations.
            ddlNameSalutation.DataSource = ResourceCommon.getListSalutations();
            ddlNameSalutation.DataBind();
            // Populate form control with states.
            foreach(DictionaryEntry li in ResourceCommon.getListStates()) {
                ddlOrganizationState.Items.Add(new ListItem(li.Key.ToString(), li.Value.ToString()));
            }
            // Populate form control with countries.
            ddlOrganizationCountry.DataSource = ResourceCommon.getListCountries();
            ddlOrganizationCountry.DataBind();
            ddlOrganizationCountry.Items.FindByText("United States of America").Selected = true;
        }
    }
}
