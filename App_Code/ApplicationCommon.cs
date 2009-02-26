/**
@file
    ApplicationCommon.cs
@brief
    Copyright 2009 Company. All rights reserved.
@author
    William Chang
@version
    0.2
@date
    - Created: 2008-08-29
    - Modified: 2008-11-21
    .
@note
    References:
    - General:
        - http://www.stack.nl/~dimitri/doxygen/index.html
        - http://msdn2.microsoft.com/en-us/library/ms178472.aspx
        - http://blogs.thesitedoctor.co.uk/tim/2006/06/30/Complete+Lifecycle+Of+An+ASPNet+Page+And+Controls.aspx
        - http://blogs.msdn.com/carloc/archive/2007/12/19/application-page-and-control-lifecycle.aspx
        - http://weblogs.asp.net/infinitiesloop/archive/2006/08/25/TRULY-Understanding-Dynamic-Controls-_2800_Part-1_2900_.aspx
        - http://blog.fluentconsulting.com//archives/2004/01/creating_templa.html
        .
    - Debugging (Windows Sysinternals: DebugView)
        - http://technet.microsoft.com/en-us/sysinternals/default.aspx
        - http://technet.microsoft.com/en-us/sysinternals/bb896647.aspx
        - System.Diagnostics.Debug.Write("Console Message");
        .
    - Encode and Decode HTML Securely:
        - http://dotnetslackers.com/articles/aspnet/Encode_and_Display_HTML_Securely_in_ASP_NET_2_0.aspx
        .
    - Passing Information Between Content and Master Pages:
        - http://aspnet.4guysfromrolla.com/articles/013107-1.aspx
        .
    - Path:
        - http://dotnettogo.com/blogs/emad/archive/2007/01/05/Resolving-URL-in-ASP.net-using-Tilda-_28007E002900_.aspx
        - http://msdn2.microsoft.com/en-us/library/ms178116.aspx
        - http://www.csharper.net/blog/using_the_tilde__~__in_asp_net_everywhere___not_just_controls_.aspx
        .
    - Microsoft ASP.NET AJAX WebServices (ScriptServices):
        - http://www.singingeels.com/Articles/Consuming_Web_Services_With_ASPNET_AJAX.aspx
        - http://weblogs.asp.net/mschwarz/archive/2008/01/09/how-to-use-class-libraries-with-asp-net-ajax-like-ajaxpro.aspx
        - http://www.ridgway.co.za/archive/2007/10/09/using-asp.net-ajax-webservices-scriptservices-in-extjs.aspx
        .
    .
*/

using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;

namespace ent {

/// <summary>Class ApplicationCommon</summary>
public class ApplicationCommon {

#region Enumerations

    public enum enumRoles {
        Administrator = 1,
        Moderator = 2,
        Author = 3,
        Subscriber = 4,
        Guest = 5
    }

#endregion

#region Fields

    private String _appUrl;
    private String _appSmtp;

    private String _appAdministratorUsernames;
    private String _appModeratorUsernames;

    private String _appEmailAdministration;
    private String _appEmailManagement;
    private String _appEmailMaintenance;
    private String _appEmailNotification1;
    private String _appEmailNotification2;

    private String _appEmailAdministrationName;
    private String _appEmailManagementName;
    private String _appEmailMaintenanceName;
    private String _appEmailNotification1Name;
    private String _appEmailNotification2Name;

#endregion

#region Properties

    public String appUrl {
        get {return _appUrl;}
        set {_appUrl = value;}
    }
    public String appSmtp {
        get {return _appSmtp;}
        set {_appSmtp = value;}
    }
    public String appAdministratorUsernames {
        get {return _appAdministratorUsernames;}
        set {_appAdministratorUsernames = value;}
    }
    public String appModeratorUsernames {
        get {return _appModeratorUsernames;}
        set {_appModeratorUsernames = value;}
    }
    public String appEmailAdministration {
        get {return _appEmailAdministration;}
        set {_appEmailAdministration = value;}
    }
    public String appEmailMaintenance {
        get {return _appEmailMaintenance;}
        set {_appEmailMaintenance = value;}
    }
    public String appEmailManagement {
        get {return _appEmailManagement;}
        set {_appEmailManagement = value;}
    }
    public String appEmailNotification1 {
        get {return _appEmailNotification1;}
        set {_appEmailNotification1 = value;}
    }
    public String appEmailNotification2 {
        get {return _appEmailNotification2;}
        set {_appEmailNotification2 = value;}
    }
    public String appEmailAdministrationName {
        get {return _appEmailAdministrationName;}
        set {_appEmailAdministrationName = value;}
    }
    public String appEmailMaintenanceName {
        get {return _appEmailMaintenanceName;}
        set {_appEmailMaintenanceName = value;}
    }
    public String appEmailManagementName {
        get {return _appEmailManagementName;}
        set {_appEmailManagementName = value;}
    }
    public String appEmailNotification1Name {
        get {return _appEmailNotification1Name;}
        set {_appEmailNotification1Name = value;}
    }
    public String appEmailNotification2Name {
        get {return _appEmailNotification2Name;}
        set {_appEmailNotification2Name = value;}
    }

#endregion

    /// <summary>Default constructor.</summary>
	public ApplicationCommon() {
        // Get spplication settings.
        try {
            AppSettingsReader appSettings = new AppSettingsReader();
            _appUrl = (String)appSettings.GetValue("URL", typeof(String));
            _appSmtp = (String)appSettings.GetValue("SMTP", typeof(String));

            _appAdministratorUsernames = (String)appSettings.GetValue("AdministratorUsernames", typeof(String));
            _appModeratorUsernames = (String)appSettings.GetValue("ModeratorUsernames", typeof(String));
            
            _appEmailAdministration = (String)appSettings.GetValue("EmailAdministration", typeof(String));
            _appEmailMaintenance = (String)appSettings.GetValue("EmailMaintenance", typeof(String));
            _appEmailManagement = (String)appSettings.GetValue("EmailManagement", typeof(String));
            _appEmailNotification1 = (String)appSettings.GetValue("EmailNotification1", typeof(String));
            _appEmailNotification2 = (String)appSettings.GetValue("EmailNotification2", typeof(String));

            _appEmailAdministrationName = (String)appSettings.GetValue("EmailAdministrationName", typeof(String));
            _appEmailMaintenanceName = (String)appSettings.GetValue("EmailMaintenanceName", typeof(String));
            _appEmailManagementName = (String)appSettings.GetValue("EmailManagementName", typeof(String));
            _appEmailNotification1Name = (String)appSettings.GetValue("EmailNotification1Name", typeof(String));
            _appEmailNotification2Name = (String)appSettings.GetValue("EmailNotification2Name", typeof(String));
        } catch {
            // Default settings entered by a user.
            // No action needs to be performed here.
        }
    }

#region Roles

    /// <summary>Get list of roles.</summary>
    public static System.Collections.Generic.Dictionary<String, int> getListRoles() {
        //System.Collections.SortedList list = new System.Collections.SortedList();
        System.Collections.Generic.Dictionary<String, int> list = new System.Collections.Generic.Dictionary<String, int>();
        list.Add("Administrator", 1);
        list.Add("Moderator", 2);
        list.Add("Author", 3);
        list.Add("Subscriber", 4);
        return list;
    }
    /// <summary>Get role from session.</summary>
    /// <remarks>Source code depends on TableUsers class.</remarks>
    public static ApplicationCommon.enumRoles getRole(System.Web.SessionState.HttpSessionState s) {
        Hashtable userSettings = (Hashtable)s[TableUsers.TBL__user_settings];
        if(ApplicationCommon.isValidSession(s)) {
            return (ApplicationCommon.enumRoles)System.Convert.ToInt32(userSettings[TableUsers.TBL__user_settings___setting_key_role]);
        } else {
            return ApplicationCommon.enumRoles.Guest;
        }
    }
    /// <summary>Get application administrators.</summary>
    /// <remarks>http://www.csharp-station.com/HowTo/StringJoinSplit.aspx</remarks>
    public String[] getAppAdministrators() {
        System.Collections.Hashtable p = new System.Collections.Hashtable();
        char[] separator = {','};
        String[] tokens = _appAdministratorUsernames.Split(separator);
        return tokens;
    }
    /// <summary>Get application moderators.</summary>
    /// <remarks>http://www.csharp-station.com/HowTo/StringJoinSplit.aspx</remarks>
    public String[] getAppModerators() {
        System.Collections.Hashtable p = new System.Collections.Hashtable();
        char[] separator = {','};
        String[] tokens = _appModeratorUsernames.Split(separator);
        return tokens;
    }
    /// <summary>Is session and role valid.</summary>
    /// <remarks>Source code depends on TableUsers class.</remarks>
    public static bool isValidSessionRole(System.Web.SessionState.HttpSessionState s, ApplicationCommon.enumRoles r) {
        Hashtable userSettings = (Hashtable)s[TableUsers.TBL__user_settings];
        int roleId = System.Convert.ToInt32(userSettings[TableUsers.TBL__user_settings___setting_key_role]);

        if(roleId == (int)r) {
            return true;
        } else {
            return false;
        }
    }

#endregion

#region Security

    /// <summary>Get generated hash.</summary>
    public static String getGeneratedHash(String SourceText) {
        System.Text.UnicodeEncoding Ue = new System.Text.UnicodeEncoding();
        byte[] ByteSourceText = Ue.GetBytes(SourceText);
        System.Security.Cryptography.MD5CryptoServiceProvider Md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] ByteHash = Md5.ComputeHash(ByteSourceText);
        return Convert.ToBase64String(ByteHash);
    }
    /// <summary>Is login valid.</summary>
    public static bool isValidLogin(String user, String password) {
        TableUsers d = new TableUsers();
        String hashedPassword = getGeneratedHash(password);

        return Convert.ToBoolean(d.countUserAccounts(user, hashedPassword));
    }
    /// <summary>Is session valid.</summary>
    public static bool isValidSession(System.Web.SessionState.HttpSessionState s) {
        if(s["user_id"] == null || s["user_id"].ToString() == String.Empty) {
            return false;
        } else {
            return true;
        }
    }

#endregion

#region Hypertext Transfer Protocol

    /// <summary>Get this page.</summary>
    /// <remarks>http://www.pluralsight.com/blogs/keith/archive/2007/02/16/46127.aspx</remarks>
    public static String getPageThis() {
        return System.Web.HttpContext.Current.Request.Path;
    }
    /// <summary>Get web application's path excluding root.</summary>
    /// <remarks>http://timstall.dotnetdevelopersjournal.com/understanding_httprequest_urls.htm</remarks>
    public static String getPath() {
        return System.Web.HttpContext.Current.Request.Path;
    }
    /// <summary>Set query strings to append url.</summary>
    public static String setQueryString(System.Collections.IDictionary parameters, String url) {
        int i = 0;

        foreach(System.Collections.DictionaryEntry entry in parameters) {
            if(i == 0 && !url.Contains("?") && !isEmpty(entry.Value.ToString())) {
                url += "?" + entry.Key.ToString() + "=" + entry.Value.ToString();
            } else if(!isEmpty(entry.Value.ToString())) {
                url += "&" + entry.Key.ToString() + "=" + entry.Value.ToString();
            }
            i++;
        }
        return url;
    }

#endregion

#region Utilities

    /// <summary>Is empty.</summary>
    public static bool isEmpty(String s) {
        if(s == null || s == String.Empty) {
            return true;
        } else {
            return false;
        }
    }
    /// <summary>Shorten string.</summary>
    public static String shortString(String source, int limit) {
        if(source.Length > limit) {
            return source.Substring(0, limit) + " ...";
        } else {
            return source;
        }
    }
    /// <summary>Get string in between two strings.</summary>
    /// <remarks>http://www.mycsharpcorner.com//Post.aspx?postID=15</remarks>
    /// <returns>An array of System.String instance containing the string in the middle.</returns>
    public static String[] getStringInBetween(String strBegin, String strEnd, String strSource, bool includeBegin, bool includeEnd) {
        String[] result = {String.Empty, String.Empty};
        int iIndexOfBegin = strSource.IndexOf(strBegin);
        if(iIndexOfBegin != -1) {
            // include the Begin string if desired
            if(includeBegin) {
                iIndexOfBegin -= strBegin.Length;
            }
            strSource = strSource.Substring(iIndexOfBegin + strBegin.Length);
            int iEnd = strSource.IndexOf(strEnd);
            if(iEnd != -1) {
                // include the End string if desired
                if(includeEnd) {
                    iEnd += strEnd.Length;
                }
                result[0] = strSource.Substring(0, iEnd);
                // advance beyond this segment
                if(iEnd + strEnd.Length < strSource.Length) {
                    result[1] = strSource.Substring(iEnd + strEnd.Length);
                }
            }
        } else {
            // stay where we are
            result[1] = strSource;
        }
        return result;
    }
    /// <summary>Remove string in between two strings.</summary>
    /// <remarks>http://www.mycsharpcorner.com//Post.aspx?postID=15</remarks>
    /// <returns>An array of System.String instance containing the string in the middle.</returns>
    public static String removeStringInBetween(String strBegin, String strEnd, String strSource, bool removeBegin, bool removeEnd) {
        String[] result = getStringInBetween(strBegin, strEnd, strSource, removeBegin, removeEnd);
        if(result[0] != String.Empty) {
            return strSource.Replace(result[0], String.Empty);
        }
        // nothing found between begin & end
        return strSource;
    }
    /// <summary>Remove extra whitespces from string. (Another regular expression: @"\s{2,}" pattern.)</summary>
    /// <remarks>
    /// http://nlakkakula.wordpress.com/2008/09/16/removing-additional-white-spaces-in-sentence-c/
    /// http://authors.aspalliance.com/stevesmith/articles/removewhitespace.asp
    /// </remarks>
    public static String removeWhitespaces(String source) {
        return System.Text.RegularExpressions.Regex.Replace(source.Trim(), @"\s+", " ");
    }
    /// <summary>Strip (remove) all HTML from string.</summary>
    public static String stripHtml(String source) {
        return System.Text.RegularExpressions.Regex.Replace(source, @"<(.|\n)*?>", String.Empty);
    }
    /// <summary>
    /// Uppercase the first letter in a string.
    /// Modify character in-place with ToCharArray.
    /// </summary>
    /// <remarks>http://dotnetperls.com/Content/Uppercase-First-Letter.aspx</remarks>
    /// <param name="strSource">The string you want to uppercase the first letter.</param>
    /// <returns>The new uppercased string.</returns>
    public static String toUpperFirst(String strSource) {
        if(String.IsNullOrEmpty(strSource)) {
            return String.Empty;
        }
        char[] letters = strSource.ToCharArray();
        letters[0] = char.ToUpper(letters[0]);
        return new String(letters);
    }
    /// <summary>To HTML from sequence (special characters).</summary>
    public static String toHtmlFromSequence(String src) {
        src = src.Replace("\n", "<br/>");
        return src;
    }
    /// <summary>To HTML from user control file (ascx).</summary>
    public static String toHtml(System.Web.UI.UserControl uc) {
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter writer = new HtmlTextWriter(sw);
        String contents = String.Empty;
        //try {
            uc.RenderControl(writer);
            contents = sw.ToString();
        //} catch {
        //    return "HTML Conversion Error";
        //}
        return contents;
    }
    /// <summary>
    /// This static function will take an encoded string and
    /// convert certain tags to their original format leaving
    /// tags like <script></script> in their encoded state.
    /// </summary>
    /// <param name="oldHTML">encoded string coming in</param>
    /// <returns>return string with certain tags decoded</returns>
    public static String convertEncodedHtmlToValidHtml(string oldHTML) {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(oldHTML);
        sb.Replace("\r\n", "<br/>");
        sb.Replace("&lt;strong&gt;", "<strong>");
        sb.Replace("&lt;/strong&gt;", "</strong>");
        sb.Replace("&lt;/b&gt;", "</b>");
        sb.Replace("&lt;b&gt;", "<b>");
        sb.Replace("&lt;/b&gt;", "</b>");
        sb.Replace("&lt;i&gt;", "<i>");
        sb.Replace("&lt;/i&gt;", "</i>");
        sb.Replace("&lt;p&gt;", "<p>");
        sb.Replace("&lt;/p&gt;", "</p>");
        sb.Replace("&lt;u&gt;", "<u>");
        sb.Replace("&lt;/u&gt;", "</u>");
        sb.Replace("&lt;br&gt;", "<br>");
        sb.Replace("&lt;br/&gt;", "<br/>");
        sb.Replace("&lt;br /&gt;", "<br />");
        return sb.ToString();
    }

#endregion

}

} // END namespace ent

/**
@mainpage Base Code (Foundation)
@section intro_sec Introduction
    Written in <a target="_blank" href="http://en.wikipedia.org/wiki/C_Sharp_(programming_language)">C#</a>, a multi-paradigm programming
    language. The web application is built from Microsoft ASPNET (Server-side), Microsoft IIS (Web Server), Microsoft SQL Server (Database Server).
    Integrating the most cutting edge web technologies <a target="_blank" href="http://en.wikipedia.org/wiki/AJAX">AJAX</a>,
    <a target="_blank" href="http://www.json.org/">JSON</a>, and <a target="_blank" href="http://json-rpc.org/">JSON RPC</a>.
    The client-side is powered by <a target="_blank" href="http://jquery.com/">jQuery</a>.

    <strong>External:</strong>
    - Creative Crew <a target="_blank" href="http://www.creativecrew.org/">[LINK]</a>
    - Google Code <a target="_blank" href="http://creativecrew.googlecode.com/">[LINK]</a>
    - Baby Blue Box <a target="_blank" href="http://www.babybluebox.com/">[LINK]</a>
    - William Chang <a target="_blank" href="http://www.williamchang.org/">[LINK]</a>
    .
    
    <strong>Server Tools:</strong>
    - Microsoft ASPNET
    - Microsoft IIS (Internet Information Services)
    - Microsoft SQL Server
    - Subversion <a target="_blank" href="http://subversion.tigris.org/">[LINK]</a>
    - FileZilla <a target="_blank" href="http://filezilla-project.org/">[LINK]</a>
    .

    <strong>Development Tools:</strong>
    - Microsoft Visual Studio
    - Adobe Photoshop
    - GIMP <a target="_blank" href="http://www.gimp.org/">[LINK]</a>
    - TortoiseSVN <a target="_blank" href="http://tortoisesvn.net/">[LINK]</a>
    - Mozilla Firefox <a target="_blank" href="http://www.mozilla.com/firefox/">[LINK]</a>
        - Firebug <a target="_blank" href="http://getfirebug.com/">[LINK]</a>
        - Web Developer <a target="_blank" href="http://chrispederick.com/work/web-developer/">[LINK]</a>
        - ColorZilla <a target="_blank" href="http://www.colorzilla.com/firefox/">[LINK]</a>
        .
    - Notepad++ <a target="_blank" href="http://notepad-plus.sourceforge.net">[LINK]</a>
    .

    <strong>Libraries:</strong>
    - jQuery <a target="_blank" href="http://jquery.com/">[LINK]</a>
    - YUI (Yahoo! User Interface Library) <a target="_blank" href="http://developer.yahoo.com/yui/">[LINK]</a>
    - TinyMCE <a target="_blank" href="http://tinymce.moxiecode.com/">[LINK]</a>
    .
@section new Features:

    <strong>Fundamental Features:</strong>
        - Updated to be compatible with at least Microsoft ASPNET 2.0. <strong>[DONE]</strong>
        - Simple User Management.
            - User registration.
            - Password retrieval.
            - Administrator ability to create, remove, update, and delete (as known as, CRUD).
            - Impersonate user for debugging, technical support.
            .
        - Simple Role Management. <strong>[DONE]</strong>
            - Single role assignment.
            - Static role: Administrator, Moderator, Author, Subscriber.
            .
        - Simple Mass Email System. <strong>[DONE]</strong>
            - Show listing of email addresses based on roles to copy and paste into your "favorite" email client (or web email like Gmail). Remark, I find it better and easier to implement than building another competing interface that users will miss, because of the rich features provided on their own email client.
            .
        - General design and template (Master Page) of all overall pages and website. <strong>[DONE]</strong>
        - Control Panel design and template (Master Page) for high level roles only. <strong>[DONE]</strong>
        .

    <strong>Upcoming Features:</strong>
        - Include simple content management system.
        .
    @section install Installation
        TODO
    @section compiling Compiling
        TODO
    @section license License
        <strong>Creative Crew</strong>

        "Foundation" is licensed under the <a target="_blank" href="http://www.gnu.org/licenses/lgpl.txt">GNU Lesser Public License (LGPL)</a>.

        Under the LGPL you may use "Foundation" for any purpose you wish, as long as you:
        -# Release any modifications to the "Foundation" source back to the community.
        -# Pass on the source to "Foundation" with all the copyrights intact.
        -# Make it clear where you have customised it. 
*/