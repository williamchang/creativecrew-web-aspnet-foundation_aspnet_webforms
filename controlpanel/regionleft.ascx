<%@ import namespace="ent" %>
<script runat="server" language="C#">
	protected void Page_Init(Object sender, EventArgs e) {
		switch(ApplicationCommon.getRole(Session)) {
			case ApplicationCommon.enumRoles.Administrator:
				plhRegionUser.Visible = true;
				plhRegionApplication.Visible = true;
				break;
			case ApplicationCommon.enumRoles.Moderator:
				plhRegionUser.Visible = false;
				plhRegionApplication.Visible = false;
				break;
			case ApplicationCommon.enumRoles.Author:
				plhRegionUser.Visible = false;
				plhRegionApplication.Visible = false;
				break;
		}
	}
</script>
<%@ register src="~/common/controlpanel_regionleft_directory.ascx" tagname="directory" tagprefix="region" %>
				<div id="region-left-menu">
					<p style="text-align:center;">.::: Control Panel :::.</p>
					<ul>
						<region:directory id="ucRegionDirectory" runat="server"/>
<asp:placeholder id="plhRegionUser" runat="server">
						<li class="header">User Management</li>
						<li><asp:hyperlink navigateurl="~/controlpanel/users_crud.aspx" runat="server"><span>Users</span></asp:hyperlink></li>
						<li><asp:hyperlink navigateurl="~/controlpanel/user_roles_crud.aspx" runat="server"><span>Roles</span></asp:hyperlink></li>
</asp:placeholder>
<asp:placeholder id="plhRegionApplication" runat="server">
						<li class="header">Application Management</li>
						<li style="display:none;"><asp:hyperlink navigateurl="~/controlpanel/email_mass.aspx" runat="server"><span>Mass Email</span></asp:hyperlink></li>
						<li><asp:hyperlink navigateurl="~/controlpanel/email_list.aspx" runat="server"><span>Email List</span></asp:hyperlink></li>
</asp:placeholder>
					</ul>
				</div>