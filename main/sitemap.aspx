<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sitemap.aspx.cs" Inherits="main_sitemap"
title="Sitemap" masterpagefile="~/template/base/template_base.master" %>
<%@ mastertype virtualpath="~/template/base/template_base.master" %>
<asp:content id="Content4" contentplaceholderid="RegionMiddle" runat="server">
<h3><asp:label id="lblSitemap" runat="server">Sitemap</asp:label></h3>
<div class="frmsitemap">
	<asp:repeater id="rptrSitemap" runat="server" enableviewstate="true" onitemdatabound="rptrSitemap_ItemDataBound" onitemcommand="rptrSitemap_ItemCommand">
	<headertemplate>
	<ul>
	</headertemplate>
	<itemtemplate>
		<li><asp:hyperlink id="hypPageHeader" runat="server"/> <span class="frmstiemap-controlpanel"><asp:label id="lblPageStatus" runat="server"/></span></li>
	</itemtemplate>
	<footertemplate>
	</ul>
	</footertemplate>
	</asp:repeater>
</div>
<asp:label id="lblError" runat="server" cssclass="error"/>
<asp:label id="lblLog" runat="server" cssclass="log"/>
</asp:content>