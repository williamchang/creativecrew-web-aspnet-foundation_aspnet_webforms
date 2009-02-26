<%@ page language="C#" autoeventwireup="true" codefile="account_login.aspx.cs" inherits="main_account_login"
title="Account Login" masterpagefile="~/template/base/template_base.master" %>
<%@ mastertype virtualpath="~/template/base/template_base.master" %>
<%@ register src="~/template/base/templatewrap_base_begin.ascx" tagname="begin" tagprefix="templatewrap_base" %>
<%@ register src="~/template/base/templatewrap_base_end.ascx" tagname="end" tagprefix="templatewrap_base" %>
<asp:content id="Content4" contentplaceholderid="RegionMiddle" runat="server">
<asp:placeholder id="plhLogin" runat="server" visible="false">
	<h3>Account Login</h3>
	<p><asp:linkbutton id="lbLoginForgot" runat="server" causesvalidation="false" onclick="lbLoginForgot_Click">Cannot access account?</asp:linkbutton> | <asp:linkbutton id="lbLoginRegister" runat="server" causesvalidation="false" postbackurl="~/main/register_user_account.aspx">Register</asp:linkbutton></p>
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<table class="tbl" cellspacing="0" cellpadding="0">
			<tr class="row">
				<td class="labels"><asp:label id="lblLoginUser" runat="server" text="Email"/></td>
				<td class="controls"><asp:textbox id="txtLoginUser" runat="server" width="192px" cssclass="txtcommon"/></td>
			</tr>
			<tr class="rowalternate">
				<td class="labels"><asp:label id="lblLoginPassword" runat="server" text="Password" cssclass="lblcommon/></td>
				<td class="controls"><asp:textbox id="txtLoginPassword" textmode="password" runat="server" width="192px" cssclass="txtcommon"/></td>
			</tr>
		</table>
		<div class="footer">
			<asp:button id="btnLoginSubmit" runat="server" text="Sign in" cssclass="btnsubmit" onclick="btnLoginSubmit_Click" causesvalidation="false"/>
		</div>
	</div>
	<templatewrap_base:end runat="server"/>
	<asp:label id="lblLoginError" runat="server" cssclass="error"/>
</asp:placeholder>
<asp:placeholder id="plhForgot" runat="server" visible="false">
	<h3>Account Login</h3>
	<p><asp:linkbutton id="lbForgotLogin" runat="server" causesvalidation="false" onclick="lbForgotLogin_Click">Login</asp:linkbutton> | <asp:linkbutton id="lbForgotRegister" runat="server" causesvalidation="false" postbackurl="~/main/register_user_account.aspx">Register</asp:linkbutton></p>
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<table class="tbl" cellspacing="0" cellpadding="0">
			<tr class="row">
				<td class="labels"><asp:label id="lblForgotEmail" runat="server" text="Send reset password by email:"/></td>
				<td class="controls"><asp:textbox id="txtForgotEmail" runat="server" width="192px" cssclass="txtcommon"/></td>
			</tr>
		</table>
		<div class="footer">
			<asp:button id="btnForgotSubmit" runat="server" text="Submit" cssclass="btnsubmit" causesvalidation="false" onclick="btnForgotSubmit_Click"/>
		</div>
	</div>
	<templatewrap_base:end runat="server"/>
	<asp:label id="lblForgotError" runat="server" cssclass="error"/>
 </asp:placeholder>
</asp:content>