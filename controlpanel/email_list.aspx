<%@ import namespace="System.Data" %>
<%@ import namespace="ent" %>
<script runat="server" language="C#">
	private TableUsers d1 = new TableUsers();
	private TableLists d2 = new TableLists();
	
	protected void Page_Load(Object sender, EventArgs e) {
		btnSelectAll.Attributes["onclick"] = "selectAll('" + txtOutput.ClientID + "');return false;";
		viewSelection();
	}
	protected void viewSelection() {
		if(!Page.IsPostBack) {
			// Get list from database.
			/*ucUserRoles.prop_cblRoles.DataSource = d2.dynamicSqlSelect(null, TableLists.TBL__user_rolelist, "");
			ucUserRoles.prop_cblRoles.DataTextField = TableLists.TBL__user_rolelist__list_name;
			ucUserRoles.prop_cblRoles.DataValueField = TableLists.TBL__user_rolelist__list_id;
			ucUserRoles.prop_cblRoles.DataBind();*/
			ucUserRoles.prop_cblRoles.DataSource = ApplicationCommon.getListRoles(); ;
			ucUserRoles.prop_cblRoles.DataTextField = "Key";
			ucUserRoles.prop_cblRoles.DataValueField = "Value";
			ucUserRoles.prop_cblRoles.DataBind();
		}
		plhSelection.Visible = true;
	}
	protected void viewOutput(String selection) {
		plhOutput.Visible = true;
		String s = String.Empty;
		bool hasSeparator = false;
		Hashtable p1 = new Hashtable();
		p1.Add(TableUsers.TBL__users__user_email, null);
		DataTable dt1 = d1.dynamicSqlSelect(p1, TableUsers.TBL__users + ", " + TableUsers.TBL__user_settings, TableUsers.TBL__users___PK__user_id + " = " + TableUsers.TBL__user_settings___FK__setting_user_id + " AND " + TableUsers.TBL__user_settings___setting_key + " = " + DatabaseCommon.sanitize(TableUsers.TBL__user_settings___setting_key_role) + " AND ( " + selection + " ) GROUP BY " + TableUsers.TBL__users__user_email);
		foreach(DataRow row in dt1.Rows) {
			if(hasSeparator) {
				s += ", ";
			}
			s += row[TableUsers.TBL__users__user_email].ToString().Trim();
			hasSeparator = true;
		}
		txtOutput.Text = s;
	}
	protected void btnSelectionQuery_Click(Object sender, EventArgs e) {
		String s = String.Empty;
		bool hasSeparator = false;
		foreach(ListItem li in ucUserRoles.prop_cblRoles.Items) {
			if(li.Selected) {
				if(hasSeparator) {
					s += " OR ";
				}
				s += TableUsers.TBL__user_settings___setting_value + " = " + DatabaseCommon.sanitize(li.Value);
				hasSeparator = true;
			}
		}
		if(!ApplicationCommon.isEmpty(s)) {
			lblError.Text = String.Empty;
			viewOutput(s);
		} else {
			plhOutput.Visible = false;
			lblError.Text = ResourceCommon.frmError_CheckboxNoSelection;
		}
	}
</script>
<%@ title="Email List" masterpagefile="~/template/base/template_base.master" %>
<%@ mastertype virtualpath="~/template/base/template_base.master" %>
<%@ register src="~/template/base/templatewrap_base_begin.ascx" tagname="begin" tagprefix="templatewrap_base" %>
<%@ register src="~/template/base/templatewrap_base_end.ascx" tagname="end" tagprefix="templatewrap_base" %>
<%@ register src="~/form/user_roles.ascx" tagname="user_roles" tagprefix="registration" %>
<asp:content id="Content1" contentplaceholderid="RegionSpecial" runat="server">
<script type="text/javascript">
<!--
function selectAll(id) {
	var element = document.getElementById(id);
	element.focus();
	element.select();
}
//-->
</script>
</asp:content>
<asp:content id="Content3" contentplaceholderid="RegionMiddle" runat="server">
<h3>Email List</h3>
<p>After pressing the "query" button, press the "select all" button to highlight all and use the "copy" command (keyboard CTRL+C or mouse right-click copy) to get the list from the textbox and paste into the BCC (blind carbon copy) textbox of your favorite email application.</p>
<asp:placeholder id="plhSelection" runat="server" visible="false">
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<registration:user_roles id="ucUserRoles" runat="server"/>
		<div class="footer">
			<asp:button id="btnSelectionQuery" runat="server" text="Query" cssclass="btnquery" onclick="btnSelectionQuery_Click"/>
		</div>
	</div>
	<templatewrap_base:end runat="server"/>
</asp:placeholder>
<asp:placeholder id="plhOutput" runat="server" visible="false">
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<asp:textbox id="txtOutput" runat="server" textmode="multiline" style="width:646px;height:300px;margin-top:4px;"/>
		<br/>
		<div class="footer center">
			<asp:button id="btnSelectAll" runat="server" text="Select All" cssclass="btncommon" style="width:84px;"/>
		</div>
	</div>
	<templatewrap_base:end runat="server"/>
</asp:placeholder>
<asp:label id="lblError" runat="server" cssclass="error"/>
<asp:label id="lblLog" runat="server" cssclass="log"/>
</asp:content>
