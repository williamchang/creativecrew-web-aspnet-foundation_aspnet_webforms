<%@ page language="C#" autoeventwireup="true" codefile="page_base_sample.aspx.cs" inherits="template_page_base_sample"
title="Sample" masterpagefile="~/template/base/template_base.master" %>
<asp:content id="Content1" contentplaceholderid="HtmlHead" runat="server">
</asp:content>
<asp:content id="Content4" contentplaceholderid="RegionMiddle" runat="server">
<asp:repeater id="rptrSampleData" runat="server"><itemtemplate>
	<div>User Id: <%#Eval(ent.TableUsers.TBL__users__user_id)%></div>
	<div>Human Name: <%#Eval(ent.TableUsers.TBL__users__user_name_first)%> <%#Eval(ent.TableUsers.TBL__users__user_name_last)%></div>
</itemtemplate></asp:repeater>
</asp:content>