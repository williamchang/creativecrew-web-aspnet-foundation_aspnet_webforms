<%@ reference control="~/template/base/template_base.master" %>
<script runat="server" language="C#">
	protected void Page_Load(Object sender, EventArgs e) {
		Session.Clear(); // Clear values from session.
		Session.Abandon(); // End current session.
		Response.Redirect("~/default.aspx");
	}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Account Logout :: UCF CECS Diversity</title>
</head>
<body>
<form id="form1" runat="server">
	<h1>Account Logout</h1>
	<p>Your session is cleared and abandoned. If your browser doesn't redirect to its new location, click <asp:hyperlink navigateurl="~/default.aspx" runat="server"><strong>here</strong></asp:hyperlink>.</p>
</form>
</body>
</html>