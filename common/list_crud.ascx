<%@ control language="C#" autoeventwireup="true" codefile="list_crud.ascx.cs" inherits="common_list_crud" %>
<%@ register src="~/template/base/templatewrap_base_begin.ascx" tagname="begin" tagprefix="templatewrap_base" %>
<%@ register src="~/template/base/templatewrap_base_end.ascx" tagname="end" tagprefix="templatewrap_base" %>
<asp:placeholder id="plhClientScript" runat="server" visible="false">
	<link href="<%#ResolveClientUrl("~/js/yui/build/datatable/assets/skins/sam/datatable.css")%>" media="screen, projection" rel="stylesheet" type="text/css"/>
	<link href="<%#ResolveClientUrl("~/css/component/datatable/screen.css")%>" media="screen, projection" rel="stylesheet" type="text/css"/>

	<script src="<%#ResolveClientUrl("~/js/yui/build/yahoo-dom-event/yahoo-dom-event.js")%>" type="text/javascript"></script>
	<script src="<%#ResolveClientUrl("~/js/yui/build/element/element-min.js")%>" type="text/javascript"></script>
	<script src="<%#ResolveClientUrl("~/js/yui/build/datasource/datasource-min.js")%>" type="text/javascript"></script>
	<script src="<%#ResolveClientUrl("~/js/yui/build/datatable/datatable-min.js")%>" type="text/javascript"></script>
	<script src="<%#ResolveClientUrl("~/js/yui/build/json/json-min.js")%>" type="text/javascript"></script>

	<script type="text/javascript">
	//<![CDATA[
classDataCrud = function(idRegion, idDataTable) {
	var memberPublic = this;
	memberPublic.init = init;
	memberPublic.idRegion = idRegion;
	memberPublic.idDataTable = idDataTable;
	memberPublic.onToolbarButtonClick = onToolbarButtonClick;
	var _eleRegion, _eleDataTable, _eleDataTableSelected;
	var _intDataId = 0;
	var _yuiDataTable;
	
	function onToolbarButtonClick(strName) {
		switch(strName) {
			case 'Add':
				__doPostBack('onMode', 'action=add'); // Call ASP.NET postback function.
				break;
			case 'Edit':
				if(_intDataId <= 0) {return false;}
				__doPostBack('onMode', 'action=edit&id=' + _intDataId); // Call ASP.NET postback function.
				break;
			case 'Delete':
				if(_intDataId <= 0) {return false;}
				if(idDataTable == 'datatable2') {
					_removeDataRow(_intDataId, 'removeList');
				} else {
					if(confirm('Are you sure you want to trash the selected item?')) {_removeDataRow(_intDataId, 'trashList');}
				}
				break;
			case 'Trash':
				__doPostBack('onMode', 'action=trash'); // Call ASP.NET postback function.
				break;
			case 'Restore':
				if(_intDataId <= 0) {return false;}
				_removeDataRow(_intDataId, 'restoreList');
				break;
			case 'Back':
				__doPostBack('onMode', 'action=back'); // Call ASP.NET postback function.
				break;
		}
		return false;
	}
	function _createToolbarButtonTemplate(strLabel, strCss) {
		return '<div class=\"fbutton\"><div><span class=\"' + strCss + '\" style=\"padding-left:20px;\" onclick=\"clsThis.onToolbarButtonClick(\'' + strLabel + '\')\">' + strLabel + '</span></div></div>'
	}
	function _createToolbarTemplate(strButtons) {
		return '<div class=\"datatable-toolbar\"><div class=\"tb1\"><div class=\"tb2\">' + strButtons + '<div class=\"btnseparator\"></div></div><div style=\"clear:both;\"></div></div></div>';
	}
	function _createToolbar() {
		if(idDataTable == 'datatable2') {
			$(_eleDataTable).before(_createToolbarTemplate(
				_createToolbarButtonTemplate('Back', 'back') +
				_createToolbarButtonTemplate('Restore', 'restore') +
				_createToolbarButtonTemplate('Delete', 'delete')
			));
			return;
		}
		$(_eleDataTable).before(_createToolbarTemplate(
			_createToolbarButtonTemplate('Add', 'add') +
			_createToolbarButtonTemplate('Edit', 'edit') +
			_createToolbarButtonTemplate('Delete', 'delete') +
			_createToolbarButtonTemplate('Trash', 'trash')
		));
	}
	function _createDataTable(strJson) {
		var crudColumnDefs = [
			{key:'<%=TABLE1_C1%>', label:'<%=TABLE1_C1_LABEL%>', sortable:true},
			{key:'<%=TABLE1_C2%>', label:'<%=TABLE1_C2_LABEL%>', sortable:true},
			{key:'<%=TABLE1_DELETED%>', label:'<%=TABLE1_DELETED_LABEL%>', sortable:true}
		];
		var crudDataSource = new YAHOO.util.DataSource(YAHOO.lang.JSON.parse(strJson));
		crudDataSource.responseType = YAHOO.util.DataSource.TYPE_JSON;
		crudDataSource.responseSchema = {
			resultsList:'<%=TABLE1_NAME%>',
			fields:[
				'<%=TABLE1_C1%>',
				'<%=TABLE1_C2%>',
				'<%=TABLE1_DELETED%>'
			]
		};
		var crudConfigs = {selectionMode:'single', scrollable:true, width:'650px', height:'490px'};
		var crudDataTable = new YAHOO.widget.DataTable(idDataTable, crudColumnDefs, crudDataSource, crudConfigs);
		crudDataTable.subscribe('rowMouseoverEvent', crudDataTable.onEventHighlightRow);
		crudDataTable.subscribe('rowMouseoutEvent', crudDataTable.onEventUnhighlightRow);
		crudDataTable.subscribe('rowClickEvent', crudDataTable.onEventSelectRow);

		function onRowSelect(e) {
			var record = crudDataTable.getRecord(YAHOO.util.Event.getTarget(e));
			_eleDataTableSelected = YAHOO.util.Event.getTarget(e);
			_intDataId = record.getData('<%=TABLE1_PK%>');
		}
		crudDataTable.subscribe('rowClickEvent', onRowSelect);
		
		_yuiDataTable = crudDataTable;
	}
	function _removeDataRow(intId, strAjaxMethod) {
		// Create data interchange format.
		var objJson = {
			"strTableName":"<%=TABLE1_NAME%>",
			"intId":intId
		};
		var strJson = YAHOO.lang.JSON.stringify(objJson);
		// Perform AJAX.
		jsonAjaxWebservice('<%#ResolveClientUrl("~/common/lists.asmx")%>', strAjaxMethod, strJson, null, function(status, data) {
			if(status == 'success' && data.d == true) {
				_yuiDataTable.deleteRow(_eleDataTableSelected);
			} else {
				console.log(status);
				console.log(data);
			}
		});
	}
	function _getDataRows() {
		// Set ajax method.
		var strAjaxMethod = 'getList';
		if(idDataTable == 'datatable2') {strAjaxMethod = 'getListTrashed';}
		// Create data interchange format.
		var objJson = {
			"strTableName":"<%=TABLE1_NAME%>"
		};
		var strJson = YAHOO.lang.JSON.stringify(objJson);
		// Perform AJAX.
		jsonAjaxWebservice('<%#ResolveClientUrl("~/common/lists.asmx")%>', strAjaxMethod, strJson, null, function(status, data) {
			if(status == 'success' && !isEmpty(data.d)) {
				_createDataTable(data.d);
			} else {
				console.log(status);
				console.log(data);
			}
		});
	}
	function init() {
		_eleRegion = $('#' + idRegion).get(0);
		_eleDataTable = $('#' + idDataTable, _eleRegion).get(0);
		// Create toolbar.
		_createToolbar();
		// Get data rows and create table.
		_getDataRows();
	}
}
	//]]>
	</script>
</asp:placeholder>
<asp:placeholder id="plhJavaScriptDataTableCrud" runat="server" visible="false">
	<script type="text/javascript">
	//<![CDATA[
var clsThis = new classDataCrud('region-middle', 'datatable1');
$(document).ready(function() {clsThis.init();});
	//]]>
	</script>
</asp:placeholder>
<asp:placeholder id="plhJavaScriptDataTableEdit" runat="server" visible="false">
	<script type="text/javascript">
	//<![CDATA[
var clsThis = new classDataCrud('region-middle', 'datatable2');
$(document).ready(function() {clsThis.init();});
	//]]>
	</script>
</asp:placeholder>
<asp:placeholder id="plhCrud" runat="server" visible="false">
	<h3><%=TITLE%> Management</h3>
	<p>To add an item press the "add" button. If you want to edit or delete, then first select inside the table to highlight the row and press either the "edit" or "delete" button.</p>
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<div id="datatable1"></div>
	</div>
	<templatewrap_base:end runat="server"/>
</asp:placeholder>
<asp:placeholder id="plhAdd" runat="server" visible="false">
	<h3>Add <%=TITLE%></h3>
	<p>All fields are required to add an item.</p>
</asp:placeholder>
<asp:placeholder id="plhEdit" runat="server" visible="false">
	<h3>Edit <%=TITLE%></h3>
	<p>Update an item with new information.</p>
</asp:placeholder>
<asp:placeholder id="plhTrash" runat="server" visible="false">
	<h3>Trash</h3>
	<p>To provide a safety net when deleting. Items in the trash remain there until you decide to permanently delete them from your database. These items still take up database space and can be undeleted or restored back to their original location.</p>
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<div id="datatable2"></div>
	</div>
	<templatewrap_base:end runat="server"/>
</asp:placeholder>
<asp:placeholder id="plhForm" runat="server" visible="false">
	<templatewrap_base:begin runat="server"/>
	<div class="frmcommon">
		<table class="tbl" cellspacing="0" cellpadding="0">
			<tr class="row">
				<td class="labels"><asp:label id="lblName" runat="server">Name</asp:label></td>
				<td class="controls"><asp:textbox id="txtName" runat="server" width="256px" cssclass="txtcommon"/><asp:requiredfieldvalidator id="rfvName" runat="server" errormessage="*" controltovalidate="txtName" cssclass="error"/></td>
			</tr>
		</table>
		<br/>
		<div class="footer">
			<asp:button id="btnFormOkay" runat="server" text="OK" cssclass="btnokay" onclick="btnFormOkay_Click"/> <asp:button id="btnFormCancel" runat="server" text="Cancel" cssclass="btncancel" causesvalidation="false" onclick="btnFormCancel_Click"/>
		</div>
	</div>
	<templatewrap_base:end runat="server"/>
</asp:placeholder>
<asp:label id="lblError" runat="server" cssclass="error"/>
<asp:label id="lblLog" runat="server" cssclass="log"/>