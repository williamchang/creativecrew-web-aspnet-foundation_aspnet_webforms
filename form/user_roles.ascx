<%@ control language="C#" autoeventwireup="true" codefile="user_roles.ascx.cs" inherits="form_user_roles" %>
<div class="subheader">Roles</div>
<table class="tbl" cellspacing="0" cellpadding="0">
	<tr class="row">
		<td class="labels"><asp:label id="lblRoles" runat="server">&nbsp;</asp:label></td>
		<td class="controls"><asp:checkboxlist id="cblRoles" runat="server" repeatdirection="vertical" repeatcolumns="1" repeatlayout="flow" cssclass="cblcommon"/><asp:dropdownlist id="ddlRoles" runat="server" cssclass="ddlcommon" width="320px" visible="false"/></td>
	</tr>
</table>
<br/>