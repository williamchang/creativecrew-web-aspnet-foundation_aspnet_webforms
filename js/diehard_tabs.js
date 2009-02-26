/**
@file
    diehard_tabs.js
@brief
    Copyright 2009 Creative Crew. All rights reserved.
@author
    William Chang
    Email: william@babybluebox.com
    Website: http://www.babybluebox.com
@version
    0.1
@date
    - Created: 2009-02-09
    - Modified: 2009-02-25
    .
@note
    Prerequisites:
    - jQuery http://www.jquery.com/
    .
    References:
    - General:
        - http://blog.jeremymartin.name/2008/02/building-your-first-jquery-plugin-that.html
        .
    .
*/

// Tabs widget.
(function($){
// Plugin for jQuery.
$.fn.diehard_tabs = function(options) {
// Iterate and return each selected element back to library's chain.
return this.each(function(_intIndex) {
    /** On event, tab open. */
    function _onTabOpen(e) {
        _eleThis.openTab(e.data.intTabIndex);
        // Set state.
        _setWindowLocationHash(_elePanels[e.data.intTabIndex].id);
        // Prevent default action.
        return false;
    }
    /** Open tab. */
    this.openTab = function(intIndex) {
        var strPanelSelectedCss = _eleThis.options.strSelectedPanelCss;
        var strTabSelectedCss = _eleThis.options.strSelectedTabCss;

        // Hide panels and remove css from panels.
        $(_elePanels).hide().removeClass(strPanelSelectedCss);
        // Show panel and add css to open panel.
        $(_elePanels[intIndex]).show().addClass(strPanelSelectedCss);
        // Remove css from tabs.
        $(_eleTabs).removeClass(strTabSelectedCss);
        // Add css to open tab.
        $(_eleTabs[intIndex]).addClass(strTabSelectedCss);
        // Set open tab index.
        _eleThis.options.intTabOpenIndex = intIndex;
    }
    /** Get window location hash (state). */
    function _getWindowLocationHash() {
		if(window.location.hash != '') {
			var strLocationHash = window.location.hash;
			strLocationHash = strLocationHash.replace(/^.*#/, '');
			return strLocationHash;
		} else {
			return '';
		}
	}
	/** Set window location hash (state). */
	function _setWindowLocationHash(strHash) {
		window.location.hash = '#tab=' + strHash;
	}
    /** Parse panels from markup code. */
    function _parseMarkupPanels() {
        for(var intIndex = 0;intIndex < _strPanelsId.length;intIndex++) {
            _elePanels[intIndex] = $('#' + _strPanelsId[intIndex], _eleRegion).hide().get(0);
        }
    }
    /** Parse tabs from markup code. */
    function _parseMarkupTabs() {
        _eleTabs = $('> *', _eleThis).get();
        $(_eleTabs).each(function(intIndex) {
            // Get id after the location hash (aka pound sign or anchor).
            var strHref = $('*[href]', this).eq(0).attr('href');
            var intCharacterIndex = strHref.lastIndexOf('#') + 1;
            _strPanelsId[intIndex] = strHref.substring(intCharacterIndex, strHref.length);
            // Bind event.
            $(this).unbind().bind('click', {intTabIndex:intIndex}, _onTabOpen);
        });
    }
    /** Load widget. */
    this.load = function _load() {
        _eleRegion = $('#' + _eleThis.options.strRegionId).get(0);

        _parseMarkupTabs();
        _parseMarkupPanels();

        // Open tab from state.
        var boolTabDefault = true;
        var strHash = _getWindowLocationHash();
        var intCharacterIndex = strHash.indexOf('tab=');
        if(intCharacterIndex >= 0) {
            var strValue = strHash.substring(intCharacterIndex + 4, strHash.length);
            // Find a match by id.
            for(var intIndex = 0;intIndex < _elePanels.length;intIndex++) {
                if(_elePanels[intIndex].id == strValue) {
                    this.openTab(intIndex);
                    boolTabDefault = false;
                    break;
                }
            }
        }
        // Open tab from options, if no state.
        if(boolTabDefault) {this.openTab(_eleThis.options.intTabOpenIndex);}
    }
    /** Init library. */
    this.init = function init(options) {
        var defaults = {
            strRegionId:'region-middle',
            strContainerPanelsSelector:'.panels',
            strSelectedTabCss:'tab-selected',
            strSelectedPanelCss:'panel-selected',
            intTabOpenIndex:0
        };
        return $.extend(defaults, options);
    }

    // Fields.
    var _eleRegion;
    var _eleTabs = [], _elePanels = [];
    var _strPanelsId = [];
    // Procedural.
    var _eleThis = this;
    _eleThis.options = _eleThis.init(options);
    _eleThis.load();
});
};
})(jQuery);