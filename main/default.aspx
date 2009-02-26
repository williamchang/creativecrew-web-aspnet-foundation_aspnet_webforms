<script runat="server" language="C#">
	protected void Page_Load(Object sender, EventArgs e) {}
</script>
<%@ title="Welcome" masterpagefile="~/template/base/template_base.master" %>
<asp:content id="Content3" contentplaceholderid="RegionMiddle" runat="server">
<h3>Welcome</h3>
<p>Hello World!</p>
<h4>Quick Links</h4>
<ul>
	<li><asp:hyperlink navigateurl="~/main/sitemap.aspx" runat="server">Goto sitemap.</asp:hyperlink></li>
</ul>
</asp:content>