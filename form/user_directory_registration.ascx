<%@ control language="C#" autoeventwireup="true" codefile="user_directory_registration.ascx.cs" inherits="form_user_directory_registration" %>
<div class="subheader">Registration Directory</div>
<table class="tbl" cellspacing="0" cellpadding="0">
	<tr class="row">
		<td class="labels"><asp:label id="lblRegistration1" runat="server">User Account</asp:label></td>
		<td class="controls"><asp:button id="btnRegistration1Proceed" runat="server" text="Proceed" cssclass="btnproceed" causesvalidation="false" onclick="btnRegistration1Proceed_Click"/></td>
	</tr>
	<tr class="rowalternate">
		<td class="labels"><asp:label id="lblRegistration2" runat="server">User Profile</asp:label></td>
		<td class="controls"><asp:button id="btnRegistration2Proceed" runat="server" text="Proceed" cssclass="btnproceed" causesvalidation="false" postbackurl="~/main/register_user_profile.aspx"/></td>
	</tr>
</table>
<br/>