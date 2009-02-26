<%@ control language="C#" autoeventwireup="true" codefile="user_profile.ascx.cs" inherits="form_user_profile" %>
<div class="subheader">User Profile Information</div>
<table class="tbl" cellspacing="0" cellpadding="0">
	<tr class="row">
		<td class="labels"><asp:label id="lblNameSalutation" runat="server">Salutation</asp:label></td>
		<td class="controls"><asp:dropdownlist id="ddlNameSalutation" runat="server" cssclass="ddlcommon"/><asp:requiredfieldvalidator id="rfvNameSalutation" runat="server" errormessage="*" controltovalidate="ddlNameSalutation" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate">
		<td class="labels"><asp:label id="lblNameFirst" runat="server">First Name</asp:label></td>
		<td class="controls"><asp:textbox id="txtNameFirst" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvNameFirst" runat="server" errormessage="*" controltovalidate="txtNameFirst" cssclass="error"/></td>
	</tr>
	<tr class="row">
		<td class="labels"><asp:label id="lblNameMiddle" runat="server">Middle Name</asp:label></td>
		<td class="controls"><asp:textbox id="txtNameMiddle" runat="server" width="150px" cssclass="txtcommon"/></td>
	</tr>
	<tr class="rowalternate">
		<td class="labels"><asp:label id="lblNameLast" runat="server">Last Name</asp:label></td>
		<td class="controls"><asp:textbox id="txtNameLast" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvNameLast" runat="server" errormessage="*" controltovalidate="txtNameLast" cssclass="error"/></td>
	</tr>
	<tr class="row">
		<td class="labels"><asp:label id="lblOccupation" runat="server">Occupation</asp:label></td>
		<td class="controls"><asp:textbox id="txtOccupation" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvOccupation" runat="server" errormessage="*" controltovalidate="txtOccupation" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate">
		<td class="labels"><asp:label id="lblPhone" runat="server">Phone</asp:label></td>
		<td class="controls"><asp:textbox id="txtPhone" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvPhone" runat="server" errormessage="*" controltovalidate="txtPhone" cssclass="error"/> <asp:regularexpressionvalidator id="revPhone" runat="server" errormessage="Numeric only. No spaces, hyphens, & parentheses." controltovalidate="txtPhone" display="dynamic" validationexpression="\d*" cssclass="error"/></td>
	</tr>
	<tr class="row">
		<td class="labels"><asp:label id="lblPhoneExtension" runat="server">Phone Extension</asp:label></td>
		<td class="controls"><asp:textbox id="txtPhoneExtension" runat="server" width="48px" cssclass="txtcommon"/> <asp:regularexpressionvalidator id="revPhoneExtension" runat="server" errormessage="Numeric only. No spaces, hyphens, & parentheses." controltovalidate="txtPhoneExtension" display="dynamic" validationexpression="\d*" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate" id="rowOrganizationName" runat="server">
		<td class="labels"><asp:label id="lblOrganizationName" runat="server">Organization Name</asp:label></td>
		<td class="controls"><asp:textbox id="txtOrganizationName" runat="server" width="192px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvOrganizationName" runat="server" errormessage="*" controltovalidate="txtOrganizationName" cssclass="error"/></td>
	</tr>
	<tr class="row" id="rowOrganizationAddress1" runat="server">
		<td class="labels"><asp:label id="lblOrganizationAddress1" runat="server">Organization Address 1</asp:label></td>
		<td class="controls"><asp:textbox id="txtOrganizationAddress1" runat="server" width="256px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvOrganizationAddress1" runat="server" errormessage="*" controltovalidate="txtOrganizationAddress1" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate" id="rowOrganizationAddress2" runat="server">
		<td class="labels"><asp:label id="lblOrganizationAddress2" runat="server">Organization Address 2</asp:label></td>
		<td class="controls"><asp:textbox id="txtOrganizationAddress2" runat="server" width="256px" cssclass="txtcommon"/></td>
	</tr>
	<tr class="row" id="rowOrganizationCity" runat="server">
		<td class="labels"><asp:label id="lblOrganizationCity" runat="server">Organization City</asp:label></td>
		<td class="controls"><asp:textbox id="txtOrganizationCity" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvOrganizationCity" runat="server" errormessage="*" controltovalidate="txtOrganizationCity" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate" id="rowOrganizationState" runat="server">
		<td class="labels"><asp:label id="lblOrganizationState" runat="server">Organization State</asp:label></td>
		<td class="controls"><asp:dropdownlist id="ddlOrganizationState" runat="server" width="150px" cssclass="ddlcommon"/><asp:requiredfieldvalidator id="rfvOrganizationState" runat="server" errormessage="*" controltovalidate="ddlOrganizationState" cssclass="error"/></td>
	</tr>
	<tr class="row" id="rowOrganizationZip" runat="server">
		<td class="labels"><asp:label id="lblOrganizationZip" runat="server">Organization Zip</asp:label></td>
		<td class="controls"><asp:textbox id="txtOrganizationZip" runat="server" width="150px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvOrganizationZip" runat="server" errormessage="*" controltovalidate="txtOrganizationZip" cssclass="error"/> <asp:regularexpressionvalidator id="revOrganizationZip" runat="server" errormessage="Numeric only. No spaces, hyphens, & parentheses." controltovalidate="txtOrganizationZip" display="dynamic" validationexpression="\d*" cssclass="error"/></td>
	</tr>
	<tr class="rowalternate" id="rowOrganizationCountry" runat="server">
		<td class="labels"><asp:label id="lblOrganizationCountry" runat="server">Organization Country</asp:label></td>
		<td class="controls"><asp:dropdownlist id="ddlOrganizationCountry" runat="server" cssclass="ddlcommon"/><asp:requiredfieldvalidator id="rfvOrganizationCountry" runat="server" errormessage="*" controltovalidate="ddlOrganizationCountry" cssclass="error"/></td>
	</tr>
</table>
<br/>