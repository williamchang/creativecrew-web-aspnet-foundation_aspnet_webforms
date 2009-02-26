<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register_user_account.aspx.cs" Inherits="main_register_user_account"
title="User Account Registration" masterpagefile="~/template/base/template_base.master" %>
<%@ register src="~/template/base/templatewrap_base_begin.ascx" tagname="begin" tagprefix="templatewrap_base" %>
<%@ register src="~/template/base/templatewrap_base_end.ascx" tagname="end" tagprefix="templatewrap_base" %>
<%@ register src="~/form/user_account.ascx" tagname="user_account" tagprefix="registration" %>
<%@ register src="~/form/user_directory_registration.ascx" tagname="user_directory_registration" tagprefix="registration" %>
<asp:content id="Content3" contentplaceholderid="RegionMiddle" runat="server">
<h3>User Account Registration</h3>
<asp:placeholder id="plhDirectoryRegistration" runat="server" visible="false">
	<p>This is a directory to supplement your registration. You can register another program or modify an existing registration.</p>
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<registration:user_directory_registration id="ucDirectoryRegistration" runat="server"/>
	</div>
	<templatewrap_base:end runat="server"/>
	<br/>
</asp:placeholder>
<asp:placeholder id="plhDirectoryApplication" runat="server" visible="false">
	<p>This is a directory to applications and tools.</p>
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<div class="subheader">Application Directory</div>
		<table class="tbl" cellspacing="0" cellpadding="0">
			<tr class="row" id="rowApplication1" runat="server" visible="false">
				<td class="labels"><asp:label id="lblApplication1" runat="server">SECME Judge Application</asp:label></td>
				<td class="controls"><asp:button id="btnApplication1Proceed" runat="server" text="Proceed" cssclass="btnproceed"  causesvalidation="false" postbackurl="~/secme/judge/default.aspx"/></td>
			</tr>
		</table>
	</div>
	<templatewrap_base:end runat="server"/>
</asp:placeholder>
<asp:placeholder id="plhForm" runat="server">
	<p>All fields are required to register an account. The email address you will provide is used to retrieve your lost account.</p>
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<registration:user_account id="ucUserAccount" runat="server" visible="true"/>
		<div class="footer">
			<asp:button id="btnSubmit" runat="server" text="Submit" cssclass="btnsubmit" onclick="btnSubmit_Click"/>
		</div>
	</div>
	<templatewrap_base:end runat="server"/>
</asp:placeholder>
<asp:label id="lblError" runat="server" cssclass="error"/>
<asp:label id="lblLog" runat="server" cssclass="log"/>
</asp:content>
