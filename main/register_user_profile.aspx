<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register_user_profile.aspx.cs" Inherits="main_register_user_profile"
title="User Profile Registration" masterpagefile="~/template/base/template_base.master" %>
<%@ mastertype virtualpath="~/template/base/template_base.master" %>
<%@ register src="~/template/base/templatewrap_base_begin.ascx" tagname="begin" tagprefix="templatewrap_base" %>
<%@ register src="~/template/base/templatewrap_base_end.ascx" tagname="end" tagprefix="templatewrap_base" %>
<%@ register src="~/form/user_profile.ascx" tagname="user_profile" tagprefix="registration" %>
<%@ register src="~/form/user_directory_registration.ascx" tagname="user_directory_registration" tagprefix="registration" %>
<asp:content id="Content3" contentplaceholderid="RegionMiddle" runat="server">
<h3>User Profile Registration</h3>
<asp:placeholder id="plhDirectory" runat="server" visible="false">
	<p>Use this directory to supplement your registration.</p>
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<registration:user_directory_registration id="ucDirectoryRegistration" runat="server"/>
	</div>
	<templatewrap_base:end runat="server"/>
</asp:placeholder>
<asp:placeholder id="plhForm" runat="server">
	<p>Please fill the form's fields to complete your user profile.</p>
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<registration:user_profile id="ucUserProfile" runat="server" visible="true"/>
		<div class="footer">
			<asp:button id="btnSubmit" runat="server" text="Submit" cssclass="btnsubmit" onclick="btnSubmit_Click"/>
		</div>
	</div>
	<templatewrap_base:end runat="server"/>
</asp:placeholder>
<asp:label id="lblError" runat="server" cssclass="error"/>
<asp:label id="lblLog" runat="server" cssclass="log"/>
</asp:content>