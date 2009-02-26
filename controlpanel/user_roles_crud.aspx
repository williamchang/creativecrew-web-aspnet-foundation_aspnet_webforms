<%@ import namespace="ent" %>
<script runat="server" language="C#">
	private TableLists d1 = new TableLists();

	protected override void OnLoad(EventArgs e) {
		ucListCrud.prop_TITLE = this.Page.Header.Title;
		ucListCrud.prop_TABLE1_NAME = TableLists.TBL__user_rolelist;
		ucListCrud.prop_TABLE1_PK = TableLists.TBL__user_rolelist___PK__rolelist_id;
		ucListCrud.prop_TABLE1_DELETED = TableLists.TBL__user_rolelist___deleted;
		ucListCrud.prop_TABLE1_C1 = TableLists.TBL__user_rolelist__list_id;
		ucListCrud.prop_TABLE1_C2 = TableLists.TBL__user_rolelist__list_name;

		plhClientScript.Controls.Add(ucListCrud.prop_plhClientScript);
		plhClientScript.Controls.Add(ucListCrud.prop_plhJavaScriptDataTableCrud);
		plhClientScript.Controls.Add(ucListCrud.prop_plhJavaScriptDataTableEdit);
	}
</script>
<%@ title="User Roles" masterpagefile="~/template/base/template_base.master" %>
<%@ register src="~/common/list_crud.ascx" tagname="crud" tagprefix="common" %>
<asp:content id="Content1" contentplaceholderid="HtmlHead" runat="server">
<asp:placeholder id="plhClientScript" runat="server"/>
</asp:content>
<asp:content id="Content4" contentplaceholderid="RegionMiddle" runat="server">
<common:crud id="ucListCrud" runat="server"/>
</asp:content>
