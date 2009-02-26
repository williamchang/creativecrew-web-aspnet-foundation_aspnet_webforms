//---------------------------------------------------------------------
// File       : template_base.master, template_base.master.cs
// Version    : 1.0
// Created    : 2008-08-29
// Modified   : 2008-11-20
//
// Author     : William Chang
// Email      : william@babybluebox.com
// Website    : http://www.babybluebox.com
//
// Compatible : Asp.Net 2.0, Asp.Net 3.5
//---------------------------------------------------------------------
/// <summary>
/// The template use Web User Controls (ascx) to hold the real
/// content. The Web Forms (aspx) is use to only provide linkage between
/// the template and the content, and to set the template's properties.
/// </summary>
///
/// <remarks>
/// Code-Behind Model Reference:
/// http://www.c-sharpcorner.com/Code/2005/May/CodingandCompilation1.gif
///
/// Master Pages References:
/// http://www.odetocode.com/Articles/419.aspx
/// http://odetocode.com/Articles/450.aspx
///
/// Object Cloning References:
/// http://www.larkware.com/Articles/CloninginVB.NET.html
/// http://www.ondotnet.com/pub/a/dotnet/2002/11/25/copying.html
///
/// Root and Paths References:
/// http://west-wind.com/weblog/posts/269.aspx
/// http://www.informit.com/articles/article.asp?p=101145&rl=1
///
/// Enumerations and Encapsulate UI References:
/// http://aspnet.4guysfromrolla.com/articles/042804-1.aspx
/// http://weblogs.asp.net/eporter/archive/2003/06/29/9452.aspx
/// </remarks>
//---------------------------------------------------------------------

using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ent {

public partial class TemplateBase : System.Web.UI.MasterPage {

#region Enumerations

    public enum enumTemplateBaseMode {
        One = 0,
        OneBare = 1,
        Two = 2,
        TwoRight = 3
    }

#endregion

#region Fields

    // For-Your-Information Declarations.
    public const String CREDIT = "DieHardAspnetTemplate Version 1.1";

    // Create HTML for Regions.
    public String RegionSpecialHtml;
    public String RegionHeaderHtml;
    public String RegionRightHtml;
    public String RegionMiddleHtml;
    public String RegionBottomHtml;

    // Create HTML for Head.
    public String ScriptLink;

    // Create HTML for Menus.

    // Create HTML for Breadcrumb Bar.
    public String TitlePage;

    // Create HTML for CSS.
    public String FramePostfixHtml;
    public Boolean isColumnManual = false;
    private TemplateBase.enumTemplateBaseMode tplMode;

    // Create HTML for Footer.
    public String CreditHtml1;
    public String CreditHtml2;

    // Create Web Controls.
    public System.Web.UI.WebControls.PlaceHolder RegionHtmlHead = new System.Web.UI.WebControls.PlaceHolder();
    public System.Web.UI.HtmlControls.HtmlInputHidden hidBrowserWidth = new System.Web.UI.HtmlControls.HtmlInputHidden();

    // Root of Web Application.
    public String Root = String.Empty;
    public String RootConfig = System.Configuration.ConfigurationManager.AppSettings["ApplicationStartPath"];
    public String RootRelative = "../";
    public String RootFilename = String.Empty;
    public String RootEndFolder = String.Empty;

#endregion

#region Properties

    public TemplateBase.enumTemplateBaseMode prop_tplMode {
        get {return tplMode;}
        set {tplMode = value;}
    }
    public PlaceHolder prop_plhRegionLeft {
        get {return plhRegionLeft;}
        set {plhRegionLeft = value;}
    }

#endregion

    protected void Page_Init(Object sender, EventArgs e) {
        this.RootFilename = System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath).ToLower();
        this.RootEndFolder = HttpContext.Current.Request.Path.ToLower();
    }
    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);
        this.Page.Header.DataBind();
    }
    protected void Page_Load(Object sender, EventArgs e) {
        this.authenticateUser();

        // MasterPage Overhead.
        this.createHtmlCommon();
        this.setTemplateMode();
    }
    /// <summary>Authenticate user.</summary>
    protected void authenticateUser() {
        // If there is a separate login page, then a query string is appended
        // to conveniently return to the last URL the user came from.
        // Required source code: String qsReturn = HttpUtility.UrlEncode(Request.QueryString["return"]);
        if(!hypLogin.NavigateUrl.Contains(RootFilename)) {
            hypLogin.NavigateUrl = hypLogin.NavigateUrl + "?return=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl);
        }
        // If there is a separate logout page, then a query string is appended
        // to conveniently return to the last URL the user came from.
        // Required source code: String qsReturn = HttpUtility.UrlEncode(Request.QueryString["return"]);
        if(!hypLogout.NavigateUrl.Contains(RootFilename)) {
            hypLogout.NavigateUrl = hypLogout.NavigateUrl + "?return=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl);
        }

        if(ApplicationCommon.isValidSession(Session)) {
            if(!isUserFolder()) {
                Response.Redirect("~/main/register_user_account.aspx");
            } else {
                getRegionLeft();
            }
            hypUser.Text = "<strong>User: </strong>" + Session["user_name_first"].ToString() + " " + Session["user_name_last"].ToString();
            liControlPanel.Visible = true;
            liLogin.Visible = false;
            liLogout.Visible = true;
        } else {
            if(!isGuestFolder()) {
                Response.Redirect("~/main/account_logout.aspx");
            }
            hypUser.Text = "<strong>User: </strong>" + "Guest";
            liControlPanel.Visible = false;
            liLogin.Visible = true;
            liLogout.Visible = false;
        }
    }
    /// <summary>Is folder have guest access privilege.</summary>
    public Boolean isGuestFolder() {
        if(RootEndFolder.Contains("/controlpanel/")) {
            return false;
        } else if(RootEndFolder.Contains("/controlpaneladmin/")) {
            return false;
        } else if(RootEndFolder.Contains("/controlpanelmod/")) {
            return false;
        } else {
            return true;
        }
    }
    /// <summary>Is folder have user role access privilege.</summary>
    public Boolean isUserFolder() {
        ApplicationCommon.enumRoles enumRole = ApplicationCommon.getRole(Session);
        if((enumRole != ApplicationCommon.enumRoles.Administrator && enumRole != ApplicationCommon.enumRoles.Moderator) && RootEndFolder.Contains("/controlpanel/")) {
            return false;
        } else if(enumRole != ApplicationCommon.enumRoles.Moderator && RootEndFolder.Contains("/controlpanelmod/")) {
            return false;
        } else if(enumRole != ApplicationCommon.enumRoles.Administrator && RootEndFolder.Contains("/controlpaneladmin/")) {
            return false;
        } else {
            return true;
        }
    }
    /// <summary>Get left region based on what folder the user is on.</summary>
    public void getRegionLeft() {
        if(RootEndFolder.Contains("/secme/controlpanel/")) {
            UserControl ucRegionLeft = (UserControl)Page.LoadControl("~/secme/controlpanel/regionleft.ascx");
            ucRegionLeft.ID = "ucRegionLeft";
            plhRegionLeft.Controls.Add(ucRegionLeft);
        } else if(RootEndFolder.Contains("/tutoring/controlpanel/")) {
            UserControl ucRegionLeft = (UserControl)Page.LoadControl("~/tutoring/controlpanel/regionleft.ascx");
            ucRegionLeft.ID = "ucRegionLeft";
            plhRegionLeft.Controls.Add(ucRegionLeft);
        } else if(RootEndFolder.Contains("/controlpanel/")) {
            UserControl ucRegionLeft = (UserControl)Page.LoadControl("~/controlpanel/regionleft.ascx");
            ucRegionLeft.ID = "ucRegionLeft";
            plhRegionLeft.Controls.Add(ucRegionLeft);
        } else if(RootEndFolder.Contains("/controlpanelmod/")) {
        } else if(RootEndFolder.Contains("/controlpaneladmin/")) {
        }
    }

#region Methods: Master Page

    /// <summary>Create common markup code.</summary>
    private void createHtmlCommon() {
        String TitleTemplate = " :: Engineering Technology";
        String Title = this.Page.Header.Title;

        TitlePage = this.Page.Header.Title;

        if(isEmpty(Title)) {
            Title = "Untitled" + TitleTemplate;
        } else {
            Title = TitlePage + TitleTemplate;
        }

        this.Page.Header.Title = Title;

        CreditHtml1 = "<a href=\"#\" title=\"Terms and Conditions\">Terms of Use</a> | <a href=\"#\" title=\"Legal Notice\">Privacy Policy</a> | <a href=\"http://www.babybluebox.com\" target=\"_blank\" title=\"William Chang\">Webmaster</a>";
        CreditHtml2 = "University of Central Florida &bull; 4000 Central Florida Blvd &bull; Orlando, Florida 32816 &bull; 407-823-3466";
    }
    /// <summary>Set template mode.</summary>
    protected void setTemplateMode() {
        if(!isGuestFolder() || prop_tplMode == TemplateBase.enumTemplateBaseMode.Two) {
            FramePostfixHtml = "cp";
        } else if(RegionRight.Controls.Count != 0 || prop_tplMode == TemplateBase.enumTemplateBaseMode.TwoRight) {
            FramePostfixHtml = "2right";
        } else if(prop_tplMode == TemplateBase.enumTemplateBaseMode.OneBare) {
            FramePostfixHtml = "1bare";
        } else {
            FramePostfixHtml = "1";
        }
    }

#endregion

    /// <summary>Is empty.</summary>
    public static Boolean isEmpty(String s) {
        if(s == null || s == String.Empty) {
            return true;
        } else {
            return false;
        }
    }
}

} // END namespace ent