/**
@file
    diehard_gallery_image.js
@brief
    Copyright 2009 Creative Crew. All rights reserved.
@author
    William Chang
    Email: william@babybluebox.com
    Website: http://www.babybluebox.com
@version
    0.1
@date
    - Created: 2009-02-06
    - Modified: 2009-02-25
    .
@note
    Prerequisites:
    - jQuery http://www.jquery.com/
    - jQuery UI http://ui.jquery.com/
        - Dialog http://docs.jquery.com/UI/Dialog
        - Tabs http://docs.jquery.com/UI/Tabs
        .
    - Yahoo! UI Library (YUI) http://developer.yahoo.com/yui/
        - JSON http://developer.yahoo.com/yui/json/
        .
    .
    References:
    - General:
        - http://blog.jeremymartin.name/2008/02/building-your-first-jquery-plugin-that.html
        - http://en.wikipedia.org/wiki/Object_copy
        .
    .
*/

// Image gallery widget.
(function($){
// Plugin for jQuery.
$.fn.diehard_gallery_image = function(options) {
// Validate prerequisites.
if(!$.fn.dialog) {throw('Dependency Error: jQuery UI Dialog widget is missing.');}
if(!$.fn.tabs) {throw('Dependency Error: jQuery UI Tabs widget is missing.');}
// Iterate and return each selected element back to library's chain.
return this.each(function(_intIndex) {
    /** On event, item add. */
    function _onItemAdd(e) { 
        var eleFormFooter = $('.footer', _elePanelAdd).get(0);
        var eleFormClose = $('.ajax-dialog-cancel', _elePanelAdd).unbind().bind('click', function(e) {
            _elePanelItems.style.display = 'block';
            _elePanelAdd.style.display = 'none';
            return false;
        }).get(0);
        _elePanelItems.style.display = 'none';
        _elePanelAdd.style.display = 'block';
        // Create iframe.
        var eleIframe = _createIframeDialog(_elePanelAdd, 'iframe_gallery_add', '', function(e) {
            if(isEmpty(this.contentWindow.result)) {
                // Get iframe's input element.
                var eleIframeInput = $(this).contents().find('form input.ajax-form-submit').get(0);
                // Get existing external's input element.
                var eleDialogInput = document.getElementById(eleIframeInput.id);
                // Remove existing external's element if exist.
                if(!isEmpty(eleDialogInput)) {$(eleDialogInput).remove();}
                // Prepend input markup code and create external's input element (deep copy).
                eleDialogInput = $('<input type=\"button\" id=\"' + eleIframeInput.id + '\" class=\"' + eleIframeInput.className +  '\" value=\"' + eleIframeInput.value + '\"/>', eleIframe).prependTo(eleFormFooter).get(0);
                // Bind event to external's input element.
                $(eleDialogInput).unbind().bind('click', function(e) {
                    // Trigger event on iframe's input element.
                    $(eleIframeInput).trigger('click');
                });
            } else {
                // Get result from iframe's server-side postback.
                result = this.contentWindow.result;
                // Close panel.
                $(eleFormClose).trigger('click');
                // Select tab.
                if(result.boolItemPublic) {
                    $(_elePanelItemsTabs).tabs('select', 1);
                } else {
                    $(_elePanelItemsTabs).tabs('select', 0);
                }
                // Append id to data.
                var intPageSelected = _intAryPageSelected[_objCurrent.intGroupIndex] - 1;
                if(isEmpty(_strAryItemIds[_objCurrent.intGroupIndex][intPageSelected])) {
                    _strAryItemIds[_objCurrent.intGroupIndex][intPageSelected] += result.intItemId.toString();
                } else {
                    _strAryItemIds[_objCurrent.intGroupIndex][intPageSelected] += ',' + result.intItemId;
                }
                // Create item by id.
                $(_objCurrent.eleItems).append(_createItem(result.intItemId));
            }
        });
    }
    /** On event, item select. */
    function _onItemSelect(e) {
        // Get id for database.
        var intCharacterIndex = this.id.lastIndexOf('_') + 1;
        var strValue = this.id.substring(intCharacterIndex, this.id.length);
        // Get inner.
        var eleItemInner = $('img', this).get(0);
        // Create markup code for selected item's container.
        var eleItemSelected = $('<div class=\"' + _eleThis.options.strWindowId + '-item-selected\"><span>Click here for image gallery.</span></div>');
        // Clone and prepend selected item to container.
        $(eleItemInner).clone().prependTo(eleItemSelected);
        // Insert selected item after the external event element.
        $(eleItemSelected).insertAfter(_objCurrent.eleExchange).bind('click', _onWindowOpen);
        // Remove external event element.
        $(_objCurrent.eleExchange).remove();
        // Set new external event element.
        _objCurrent.eleExchange = eleItemSelected;
        // Set selected value to input hidden.
        var eleHiddenDataSelected = $(eleItemSelected).next('input[type=\"hidden\"]').get(0);
        if(!isEmpty(eleHiddenDataSelected)) {
            eleHiddenDataSelected.value = parseInt(strValue, 10);
        } else {
            throw('Error: The input hidden element next to the external event element is missing, it is require to save selection to database.');
        }
        // Close dialog.
        $(_eleDialogClose).trigger('click');
        // Prevent default action.
        return false;
    }
    /** On event, page change. */
    function _onPageChange(e) {
        var intPageIndex = this.selectedIndex;
        _intAryPageSelected[_objCurrent.intGroupIndex] = parseInt(this.options[intPageIndex].value, 10);
        // Create items by id.
        var intPageSelected = _intAryPageSelected[_objCurrent.intGroupIndex] - 1;
        if(isEmpty(_strAryItemIds[_objCurrent.intGroupIndex][intPageSelected])) {return;}
        _createItems(_strAryItemIds[_objCurrent.intGroupIndex][intPageSelected]);
    }
    /** On event, tab show. */
    function _onTabShow(e, ui) {
        // Set current object.
        _objCurrent.intGroupIndex = ui.index;
        _objCurrent.eleItems = ui.panel;
        // Create number of pages.
        _createPaging(_strAryItemIds[_objCurrent.intGroupIndex].length, _intAryPageSelected[_objCurrent.intGroupIndex]);
        // Create (load) items after opening the dialog.
        if($(_eleWindow).dialog('isOpen')) {$(_elePaging).trigger('change');}
    }
    /** On event, window open. */
    function _onWindowOpen(e) {
        _objCurrent.eleExchange = this;
        _elePanelItems.style.display = 'block';
        _createWindow();
        // Load items.
        $(_elePaging).trigger('change');
        // Prevent default action.
        return false;
    }
    /** Parse items from query string. */
    function _parseQueryStringItems() {  
        for(var intIndex1 = 0;intIndex1 < _eleAryHiddenData.length;intIndex1++) {
            // Get data from input hidden, get pages from query string.
            var intPageLength = parseInt(getQueryStringValue(_eleAryHiddenData[intIndex1].value, 'pageslength'), 10);
            _strAryItemIds[intIndex1] = [];
            for(var intIndex2 = 0;intIndex2 < intPageLength;intIndex2++) {
                // Get data from input hidden, get items from query string
                _strAryItemIds[intIndex1][intIndex2] = getQueryStringValue(_eleAryHiddenData[intIndex1].value, 'images_' + (intIndex2 + 1));
            }
        }
    }
    /** Create iframe for dialog. */
    function _createIframeDialog(elePlaceholder, strFrameId, strQueryString, fnCallback) {
        // Get iframe location.		
        var eleLocation = $('.iframe_location', elePlaceholder).get(0);
        var iframeSrc = $(eleLocation, elePlaceholder).html();
        if(iframeSrc.indexOf('?') < 0){
            if(!isEmpty(strQueryString)) {strQueryString = '?' + strQueryString;}
        } else {
            if(!isEmpty(strQueryString)) {strQueryString = '&' + strQueryString;}
        }
        // Init.
        var eleForm = $(eleLocation).parent().get(0);
        var eleExist = document.getElementById(strFrameId);
        if(!isEmpty(eleExist)) {
            eleForm.removeChild(eleExist);
        }
        // Load and prepend to dialog.
        var eleIframe = $('<iframe src=\"' + iframeSrc + strQueryString + '\" id=\"' + strFrameId + '\" name=\"' + strFrameId + '\" class=\"iframe\" frameborder=\"0\"></iframe>').prependTo(eleForm).get(0);
        // Bind load event for function callback.
        $(eleIframe).bind('load', function(e) {
            if(jQuery.isFunction(fnCallback)) {fnCallback.call(this, e);}
        });
        return eleIframe;
    }
    /** Create dialog (require jQuery UI Dialog). */
    function _createDialog(ele) {
        $(ele).dialog({
            modal:true,
            resizable:false,
            draggable:false,
            width:_eleThis.options.intWindowWidth,
            height:_eleThis.options.intWindowHeight
        });
        ele.style.display = 'block';
        // Bind close event.
        $(_eleDialogClose).unbind().bind('click', function(e) {$(ele).dialog('destroy');return false;});
    }
    /** Create an item. */
    function _createItem(intItemId) {
        // Create element from markup code.
        var ele = $('<div id=\"' + _eleThis.options.strWindowId + '-item_' + intItemId + '\" class=\"' + _eleThis.options.strWindowId + '-item\" title=\"Click here to select.\"><img src=\"' + _eleThis.options.strUrlItem + '&id=' + intItemId + '\"/></div>').get(0);
        // Bind item event.
        $(ele).bind('click', _onItemSelect);
        // Return element.
        return ele;
    }
    /** Create items. */
    function _createItems(strItems) {
        // Parse delimited string.
        var strValues = strItems.split(',');
        // Remove existing items.
        $('*', _objCurrent.eleItems).remove();
        // Create items.
        for(var intIndex = 0;intIndex < strValues.length;intIndex++) {
            var intId = parseInt(strValues[intIndex], 10);
            // Append to items.
            $(_objCurrent.eleItems).append(_createItem(intId));
        }
    }
    /** Create paging. */
    function _createPaging(intLength, intSelected) {
        // Remove existing items.
        $('*', _elePaging).remove();
        // Create items.
        for(var intIndex = 1;intIndex <= intLength;intIndex++) {
            var strAttribute = '';
            if(intSelected == intIndex) {strAttribute = ' selected=\"selected\"';}
            // Create markup code and append to list.
            $(_elePaging).append('<option' + strAttribute + ' value=\"' + intIndex + '\">' + intIndex + '</option>');
        }
    }
    /** Create window. */
    function _createWindow() {
        // Create modal dialog.
        _createDialog(_eleWindow);
    }
    /** Remove URL before the location hash (aka pound sign or anchor). Fix is only for Microsoft Internet Explorer 7. */
    function _removeUrlBeforeLocationHash(eleLinks) {
        if(!$.browser.msie && parseInt($.browser.version, 10) != 7) {return;}
        $(eleLinks).each(function(intIndex) {
            // Get hash after the location hash (aka pound sign or anchor).
            var strHref = $('*[href]', this).eq(0).attr('href');
            var intCharacterIndex = strHref.lastIndexOf('#');
            var strHash = strHref.substring(intCharacterIndex, strHref.length); 
            $('*[href]', this).eq(0).attr('href', strHash);
        });
    }
    /** Create items panel from makeup code. */
    function _createPanelItems() {
        _elePanelItems = $('<div id=\"' + _eleThis.options.strWindowId + '-panel-items\"></div>').appendTo(_eleWindow).get(0);
        _elePanelItems.style.display = 'none';
        // Create footer from markup code.
        _eleFooter = $('<div id=\"' + _eleThis.options.strWindowId + '-footer\">Select Image From Gallery</div>').appendTo(_elePanelItems).get(0);
        _eleControlPanelLeft = $('<div class=\"controlpanel-left\">Page </div>').prependTo(_eleFooter).get(0);
        _eleControlPanelRight = $('<div class=\"controlpanel-right\"></div>').prependTo(_eleFooter).get(0);
        _elePaging = $('<select id=\"' + _eleThis.options.strWindowId + '-paging\" name=\"' + _eleThis.options.strWindowId + '_paging\"></select>').appendTo(_eleControlPanelLeft).get(0);
        _eleListAdd = $('<input type="button" value=\"Upload\" name=\"' + _eleThis.options.strWindowId + '_trigger_add\" class=\"btnupload ' + _eleThis.options.strWindowId + '-tigger_add\"/>').appendTo(_eleControlPanelRight).get(0);
        _eleDialogClose = $('<input type="button" value=\"Close\" name=\"' + _eleThis.options.strWindowId + '_trigger_close\" class=\"btncancel ' + _eleThis.options.strWindowId + '-tigger_close\"/>').appendTo(_eleControlPanelRight).get(0);

        // Create frame for the tabs and panels.
        var elePanelItemsTabsFrame = $('<div id=\"' + _eleThis.options.strWindowId + '-items-tabs-frame\"> </div>').prependTo(_elePanelItems).get(0);
        // Create tabs from markup code.      
        _elePanelItemsTabs = $('<ul id=\"' + _eleThis.options.strWindowId + '-items-tabs\"> </ul>').appendTo(elePanelItemsTabsFrame).get(0);
        $('<li class=\"' + _eleThis.options.strWindowId + '-items-tab\"><a href=\"#' + _eleThis.options.strWindowId + '-items1\"><span>Private</span></a></li>').appendTo(_elePanelItemsTabs);
        $('<li class=\"' + _eleThis.options.strWindowId + '-items-tab\"><a href=\"#' + _eleThis.options.strWindowId + '-items2\"><span>Public</span></a></li>').appendTo(_elePanelItemsTabs);
        _removeUrlBeforeLocationHash($('> *', _elePanelItemsTabs).get());
        // Create panels for tabs from markup code.
        $('<div class=\"' + _eleThis.options.strWindowId + '-items\" id=\"' + _eleThis.options.strWindowId + '-items1\"> </div>').appendTo(elePanelItemsTabsFrame);
        $('<div class=\"' + _eleThis.options.strWindowId + '-items\" id=\"' + _eleThis.options.strWindowId + '-items2\"> </div>').appendTo(elePanelItemsTabsFrame);
        // Create tabs.
        $(_elePanelItemsTabs).tabs({show:_onTabShow});

        // Parse makeup code for tabs.
        /*var elePanelItemsTabsFrame = $('#' + _eleThis.options.strWindowId + '-items-tabs-frame').prependTo(_elePanelItems).get(0);
        elePanelItemsTabsFrame.style.display = 'block';
        // Create tabs.
        _elePanelItemsTabs = $('#' + _eleThis.options.strWindowId + '-items-tabs', elePanelItemsTabsFrame).tabs({show:_onTabShow}).get(0);*/
        
        // Bind events.
        $(_elePaging).unbind().bind('change', _onPageChange);
        $(_eleListAdd).bind('click', _onItemAdd);
    }
    /** Create add panel from makeup code. */
    function _createPanelAdd() {
        _elePanelAdd = $('<div id=\"' + _eleThis.options.strWindowId + '-panel-add\"></div>').appendTo(_eleWindow).get(0);
        _elePanelAdd.style.display = 'none';
        $('<div class=\"frmiframe\"><div class=\"iframe_location\" style="\display:none;\">' + _eleThis.options.strUrlAdd + '</div><div class=\"footer\">&nbsp;<input type=\"button\" value=\"Cancel\" class=\"ajax-dialog-cancel btncancel\"/></div></div>').appendTo(_elePanelAdd);
    }
    /** Create external events. */
    function _createExternalEvents() {
        $('.' + _eleThis.options.strWindowId + '-trigger_open', _eleRegion).each(function(intIndex) {
            $(this).unbind().bind('click', _onWindowOpen);
        });
    }
    /** Load widget. */
    this.load = function _load() {
        _eleRegion = $('#' + _eleThis.options.strRegionId).get(0);
        _eleWindow = $('#' + _eleThis.options.strWindowId, _eleRegion).get(0);
        _eleAryHiddenData[0] = $('#' + _eleThis.options.strHiddenDataPrivateId, _eleRegion).get(0);
        _eleAryHiddenData[1] = $('#' + _eleThis.options.strHiddenDataPublicId, _eleRegion).get(0);

        _eleWindow.style.display = 'none';
        _parseQueryStringItems();
        _createPanelItems();
        _createPanelAdd();
        _createExternalEvents();
    }
    /** Init library. */
    this.init = function init(options) {
        var defaults = {
            strRegionId:'region-middle',
            strWindowId:'gallery',
            intWindowWidth:640,
            intWindowHeight:480,
            strHiddenDataPublicId:'',
            strHiddenDataPrivateId:'',
            strUrlItem:'',
            strUrlAdd:''
        };
        return $.extend(defaults, options);
    }

    // Fields.
    var _eleRegion, _eleWindow, _elePanelItems, _elePanelItemsTabs, _elePanelAdd;
    var _eleControlPanelLeft, _eleControlPanelRight, _elePaging, _eleListAdd, _eleDialogClose;
    var _eleAryHiddenData = [];
    var _strAryItemIds = []; // Two dimensional array, later.
    var _intAryPageSelected = [1, 1];
    var _objCurrent = {
        intGroupIndex:null,
        eleItems:null,
        eleExchange:null
    };
    // Procedural.
    var _eleThis = this;
    _eleThis.options = _eleThis.init(options);
    _eleThis.load();
});
};
})(jQuery);