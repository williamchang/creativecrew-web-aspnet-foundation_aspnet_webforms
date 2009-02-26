<%@ control language="C#" autoeventwireup="true" codefile="user_account.ascx.cs" inherits="form_user_account" %>
<asp:placeholder id="plhTypical" runat="server" visible="true">
<div class="subheader"><asp:label id="lblTypicalHeader" runat="server">Account Information</asp:label></div>
<table class="tbl" cellspacing="0" cellpadding="0">
	<tr class="row" id="rowUserAlias" runat="server">
		<td class="labels"><asp:label id="lblUsername" runat="server">User Alias</asp:label></td>
		<td class="controls"><asp:textbox id="txtUserAlias" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvUserAlias" runat="server" errormessage="*" controltovalidate="txtUserAlias" cssclass="error"/> <asp:regularexpressionvalidator id="revUserAlias" runat="server" errormessage="<br/>No spaces & punctuations. Amount between 3 and 32 characters." controltovalidate="txtUserAlias" display="dynamic" validationexpression="\w{3,32}" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate" id="rowNameFirst" runat="server">
		<td class="labels"><asp:label id="lblNameFirst" runat="server">First Name</asp:label></td>
		<td class="controls"><asp:textbox id="txtNameFirst" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvNameFirst" runat="server" errormessage="*" controltovalidate="txtNameFirst" cssclass="error"/></td>
	</tr>
	<tr class="row" id="rowNameLast" runat="server">
		<td class="labels"><asp:label id="lblNameLast" runat="server">Last Name</asp:label></td>
		<td class="controls"><asp:textbox id="txtNameLast" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvNameLast" runat="server" errormessage="*" controltovalidate="txtNameLast" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate" id="rowEmail" runat="server">
		<td class="labels"><asp:label id="lblEmail" runat="server">Email</asp:label></td>
		<td class="controls"><asp:textbox id="txtEmail" runat="server" width="192px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvEmail" runat="server" errormessage="*" controltovalidate="txtEmail" cssclass="error"/> <asp:regularexpressionvalidator id="revEmail" runat="server" errormessage="Email is invalid." controltovalidate="txtEmail" display="dynamic" validationexpression="\S+@\S+\.\S+" cssclass="error"/></td>
	</tr>
		<tr class="row" id="rowEmailConfirm" runat="server">
		<td class="labels"><asp:label id="lblEmailConfirm" runat="server">Confirm Email</asp:label></td>
		<td class="controls"><asp:textbox id="txtEmailConfirm" runat="server" width="192px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvEmailConfirm" runat="server" errormessage="*" controltovalidate="txtEmailConfirm" cssclass="error"/> <asp:comparevalidator id="cvEmailConfirm" runat="server" errormessage="Emails do not match." controltocompare="txtEmail" controltovalidate="txtEmailConfirm" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate" id="rowPassword" runat="server">
		<td class="labels"><asp:label id="lblPassword" runat="server">Password</asp:label></td>
		<td class="controls"><asp:textbox id="txtPassword" runat="server" textmode="password" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvPassword" runat="server" errormessage="*" controltovalidate="txtPassword" cssclass="error"/> <asp:regularexpressionvalidator id="revPassword" runat="server" errormessage="<br/>No spaces & punctuations. Limit between 6 to 32 characters." controltovalidate="txtPassword" display="dynamic" validationexpression="\w{6,32}" cssclass="error"/></td>
	</tr>
	<tr class="row" id="rowPasswordConfirm" runat="server">
		<td class="labels"><asp:label id="lblPasswordConfirm" runat="server">Confirm Password</asp:label></td>
		<td class="controls"><asp:textbox id="txtPasswordConfirm" runat="server" textmode="password" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvPasswordConfirm" runat="server" errormessage="*" controltovalidate="txtPasswordConfirm" cssclass="error"/> <asp:comparevalidator id="cvPasswordConfirm" runat="server" errormessage="Passwords do not match." controltocompare="txtPassword" controltovalidate="txtPasswordConfirm" cssclass="error"/></td>
	</tr>
</table>
<br/>
</asp:placeholder>
<asp:placeholder id="plhEdit" runat="server" visible="false">
<div class="subheader">Account Information</div>
<table class="tbl" cellspacing="0" cellpadding="0">
	<tr class="row" id="rowEditCurrentPassword" runat="server">
		<td class="labels"><asp:label id="lblEditCurrentPassword" runat="server">Current Password</asp:label></td>
		<td class="controls"><asp:textbox id="txtEditCurrentPassword" runat="server" textmode="password" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvEditCurrentPassword" runat="server" errormessage="*" controltovalidate="txtEditCurrentPassword" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate" id="rowEditCurrentEmail" runat="server">
		<td class="labels"><asp:label id="lblEditCurrentEmail" runat="server">Current Email</asp:label></td>
		<td class="controls"><asp:label id="lblEditCurrentEmailValue" runat="server">None</asp:label></td>
	</tr>
	<tr class="row" id="rowEditUserAlias" runat="server">
		<td class="labels"><asp:label id="lblEditUserAlias" runat="server">User Alias</asp:label></td>
		<td class="controls"><asp:textbox id="txtEditUserAlias" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvEditUserAlias" runat="server" errormessage="*" controltovalidate="txtEditUserAlias" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate" id="rowEditNameFirst" runat="server">
		<td class="labels"><asp:label id="lblEditNameFirst" runat="server">First Name</asp:label></td>
		<td class="controls"><asp:textbox id="txtEditNameFirst" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvEditNameFirst" runat="server" errormessage="*" controltovalidate="txtEditNameFirst" cssclass="error"/></td>
	</tr>
	<tr class="row" id="rowEditNameLast" runat="server">
		<td class="labels"><asp:label id="lblEditNameLast" runat="server">Last Name</asp:label></td>
		<td class="controls"><asp:textbox id="txtEditNameLast" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvEditNameLast" runat="server" errormessage="*" controltovalidate="txtEditNameLast" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate">
		<td class="labels"><asp:label id="lblEditNewEmail" runat="server">New Email</asp:label></td>
		<td class="controls"><asp:textbox id="txtEditNewEmail" runat="server" width="192px" cssclass="txtcommon"/> <asp:regularexpressionvalidator id="revEditNewEmail" runat="server" errormessage=" Email is invalid." controltovalidate="txtEditNewEmail" display="dynamic" validationexpression="\S+@\S+\.\S+" cssclass="error"/></td>
	</tr>
	<tr class="row">
		<td class="labels"><asp:label id="lblEditNewEmailConfirm" runat="server">Confirm New Email</asp:label></td>
		<td class="controls"><asp:textbox id="txtEditNewEmailConfirm" runat="server" width="192px" cssclass="txtcommon"/> <asp:comparevalidator id="cvEditNewEmailConfirm" runat="server" errormessage=" Emails do not match." controltocompare="txtEditNewEmail" controltovalidate="txtEditNewEmailConfirm" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate">
		<td class="labels"><asp:label id="lblEditNewPassword" runat="server">New Password</asp:label></td>
		<td class="controls"><asp:textbox id="txtEditNewPassword" runat="server" textmode="password" width="150px" cssclass="txtcommon"/> <asp:regularexpressionvalidator id="revEditNewPassword" runat="server" errormessage="<br/>No spaces & punctuations. Limit between 6 to 32 characters." controltovalidate="txtEditNewPassword" display="dynamic" validationexpression="\w{6,32}" cssclass="error"/></td>
	</tr>
	<tr class="row">
		<td class="labels"><asp:label id="lblEditNewPasswordConfirm" runat="server">Confirm New Password</asp:label></td>
		<td class="controls"><asp:textbox id="txtEditNewPasswordConfirm" runat="server" textmode="password" width="150px" cssclass="txtcommon"/> <asp:comparevalidator id="cvEditNewPasswordConfirm" runat="server" errormessage=" Passwords do not match." controltocompare="txtEditNewPassword" controltovalidate="txtEditNewPasswordConfirm" cssclass="error"/></td>
	</tr>
</table>
<br/>
</asp:placeholder>