/*
//-------------------------------------------------------------------
// File       : main.js
// Version    : 1.3
// Created    : 2008-08-29
// Modified   : 2009-01-30
//
// Author     : William Chang
// Email      : william@babybluebox.com
// Website    : http://www.babybluebox.com
//
// Compatible : Microsoft Internet Explorer 6+, Mozilla Firefox 3+, Apple Safari 3+, Google Chrome 1+
//-------------------------------------------------------------------
/// <summary>
/// Main (onload event) JavaScript of template.
/// </summary>
///
/// <remarks>
/// References:
/// http://www.accessify.com/features/tutorials/the-perfect-popup/
/// http://docs.jquery.com/Main_Page
///
/// Markup Code Example:
/// Head:
/// <script src="js/jquery/jquery.js" type="text/javascript"></script>
/// <script src="css/base/main.js" type="text/javascript"></script>
/// Embed:
/// <script type="text/javascript">
/// //<![CDATA[
///
/// //]]>
/// </script>
///
/// Markup Code References:
/// http://en.wikipedia.org/wiki/Client-side_JavaScript
/// http://en.wikipedia.org/wiki/XHTML
/// </remarks>
//-------------------------------------------------------------------
*/

/// <reference path="../../js/jquery/jquery.js"/>

// <!-- BEGIN: Namespaces -->
if(typeof DieHard == "undefined" || !DieHard) {var DieHard = {};}
// <!-- END: Namespaces -->

// <!-- BEGIN: Utilities -->
// Is empty.
function isEmpty(mixed_var) {return (mixed_var == undefined || mixed_var === '' || mixed_var === 0 || mixed_var === '0' || mixed_var === null || mixed_var === false || (isArray(mixed_var) && mixed_var.length === 0));}
// Is array.
function isArray(mixed_var) {return (mixed_var instanceof Array);}
// Encode HTML to prevent ASP.NET HttpRequestValidationException.
function encodeHtml(str) {str = str.replace(/</gi, '&lt;');str = str.replace(/>/gi, '&gt;');return str;}
// Decode HTML.
function decodeHtml(str) {str = str.replace(/&lt;/gi, '<');str = str.replace(/&gt;/gi, '>');return str;}
// <!-- END: Utilities -->

// <!-- BEGIN: Perfect Pop-up Embedded -->
// Reference: http://www.accessify.com/features/tutorials/the-perfect-popup/
var newWindow = null;
function closeWin() {
    if(newWindow != null) {
        if(!newWindow.closed) {newWindow.close();}
    }
}
function popUpWin(url, type, strWidth, strHeight) {
    closeWin();
    type = type.toLowerCase();
    var centerLeft = 0;var centerTop = 0;
    if(type == 'fullscreen') {
        strWidth = screen.availWidth;
        strHeight = screen.availHeight;
    } else {
        centerLeft = (screen.width) ? (screen.width - strWidth) / 2 : 100;
        centerTop = (screen.height) ? (screen.height - strHeight) / 2 : 100;
    }
    var tools = '';
    if(type == 'standard') tools = 'resizable,toolbar=yes,location=yes,scrollbars=yes,menubar=yes,width=' + strWidth + ',height=' + strHeight + ',top=' + centerTop + ',left=' + centerLeft;
    if(type == 'console' || type == "fullscreen") tools = 'resizable,toolbar=no,location=no,scrollbars=yes,width=' + strWidth + ',height=' + strHeight + ',top=' + centerTop + ',left=' + centerLeft;
    newWindow = window.open(url, 'newWin', tools);
    newWindow.focus();
}
function doPopUp(e) {
    // Set defaults - if nothing in rel attrib, these will be used.
    var t = 'standard';
    var w = '780';
    var h = '580';
    // Look for parameters.
    attribs = this.rel.split(' ');
    if(attribs[1] != null) {t = attribs[1];}
    if(attribs[2] != null) {w = attribs[2];}
    if(attribs[3] != null) {h = attribs[3];}
    // Call the popup script.
    popUpWin(this.href, t, w, h);
    // Cancel the default link action if pop-up activated.
    if(window.event) {
        window.event.returnValue = false;
        window.event.cancelBubble = true;
    }
    else if(e) {
        e.stopPropagation();
        e.preventDefault();
    }
}
function findPopUps() {
    var popups = document.getElementsByTagName('a');
    for(i = 0;i < popups.length;i++) {
        if(popups[i].rel.indexOf('popup') != -1) {
            // Attach popup behaviour.
            popups[i].onclick = doPopUp;
            // Add popup indicator.
            if(popups[i].rel.indexOf('noicon') == -1) {
                popups[i].style.backgroundImage = 'url(pop-up.gif)';
                popups[i].style.backgroundPosition = '0 center';
                popups[i].style.backgroundRepeat = 'no-repeat';
                popups[i].style.paddingLeft = '15px';
            }
            // Add info to title attribute to alert fact that it's a pop-up window.
            popups[i].title = popups[i].title + ' [Opens in pop-up window]';
        }
    }
}
// <!-- END: Perfect Pop-up Embedded -->

// <!-- BEGIN: jQuery JavaScript Library Embedded -->
classMinHeight = function(id) {
    var memberPublic = this;
    memberPublic.init = init
    memberPublic.id = id;
    var element;var height;
    function _getOffset() {
        if(document.getElementById('framecp') != null) { // Is control panel.
            return 120;
        } else if(document.getElementById('frame1bare') != null) { // Is bare.
            return 120;
        } else {
            return 120;
        }
    }
    function _apply() {
        var offset = _getOffset();
        if($.browser.msie && parseInt($.browser.version, 10) <= 6) {
            element.style.height = (height - offset) + 'px';
            element.style.overflow = 'visible';
        } else if($.browser.mozilla || $.browser.msie) {
            element.style.minHeight = (height - offset) + 'px';
        } else {
            element.style.minHeight = (height - offset - 10) + 'px';
        }
    }
    function _resize(e) {
        height = $(window).height();
        _apply();
    }
    function init() {
        element = document.getElementById(id);
        _resize(null);
        $(window).bind('resize', _resize);
    }
}
classDashboard = function(idRegion, idMin, idMax, idDrawers) {
    var memberPublic = this;
    memberPublic.init = init;
    memberPublic.idRegion = idRegion;
    memberPublic.idMin = idMin;
    memberPublic.idMax = idMax;
    memberPublic.idDrawers = idDrawers;
    var idMaxToggle;
    var maxHeightHideShow;
    var eleDrawers = [];
    var drawersHeightHideShow = [];
    var previousElement = null;

    function _toggleDashboardMax(e) {
        $(this).unbind('click', _toggleDashboardMax);
        var vista = (parseInt($('#' + idMax).height()) == '0') ? maxHeightHideShow : '0';
        $('#' + idMax).animate({ height: vista + 'px' }, 300);
        $(this).bind('click', _toggleDashboardMax);
    }
    function _toggleDashboardDrawer(e) {
        $(this).unbind('click', _toggleDashboardDrawer);
        for(var i = 0;i < eleDrawers.length;i++) {
            if(eleDrawers[i].id == this.id.substring(0, this.id.length - 7)) {
                _openPanel(i);
                break;
            }
        }
        $(this).bind('click', _toggleDashboardDrawer);
    }
    function _onClickClosePanel(e) {
        $(e.data.element).animate({height:0, opacity:0}, 300);
        previousElement = e.data.element;
    }
    function _openPanel(index) {
        if(previousElement !== eleDrawers[index]) {
            $.each(eleDrawers, function() {$(this).animate({height:0, opacity: 0}, 300);});
        }
        var vista = (parseInt($(eleDrawers[index]).height()) == '0') ? drawersHeightHideShow[index] : '0';
        if(vista == 0) {
            $(eleDrawers[index]).animate({height:vista + 'px', opacity:0}, 300);
        } else {
            $(eleDrawers[index]).animate({height:vista + 'px', opacity:1}, 300);
        }
        previousElement = eleDrawers[index];
    }
    function _createDrawerEvents(id) {
        var eleDrawer = $('#' + id).get(0);
        $('#' + id + '-toggle').bind('click', _toggleDashboardDrawer).css('cursor', 'pointer');
        $('.' + id + '-close').bind('click', {element:eleDrawer}, _onClickClosePanel).css('cursor', 'pointer');
    }
    function init() {
        // Dashboard Drawers.
        var cssClassDrawer = idDrawers.substring(0, idDrawers.length - 1);
        eleDrawers = $('#' + idDrawers + ' > div.' + cssClassDrawer).get();
        for(var i = 0;i < eleDrawers.length;i++) {
            _createDrawerEvents(eleDrawers[i].id);
            drawersHeightHideShow[i] = parseInt($(eleDrawers[i].childNodes[0]).css('height'));
            if(parseInt($(eleDrawers[i]).css('height')) != '0') {previousElement = eleDrawers[i];}
        }
        // Dashboard Max.
        idMaxToggle = idMax + '-toggle';
        maxHeightHideShow = parseInt(document.getElementById(idMax).childNodes[0].offsetHeight);
        $('#' + idMaxToggle).bind('click', _toggleDashboardMax).css('cursor', 'pointer');
    }
}
classCollapsiblePanel = function(idRegion, cssCollapsiblePanel, cssCollapsibleHeader, cssCollapsibleBody) {
    var memberPublic = this;
    memberPublic.init = init;
    memberPublic.setCollapsiblePanel = setCollapsiblePanel;
    memberPublic.idRegion = idRegion;
    memberPublic.cssCollapsiblePanel = cssCollapsiblePanel;
    memberPublic.cssCollapsibleHeader = cssCollapsibleHeader;
    memberPublic.cssCollapsibleBody = cssCollapsibleBody
    var eleRegion, elePanels = [];

    function _hideCollapsiblePanel(idPanel, eleHeader, eleBody) {
        $(eleHeader).addClass('collapsible-selected-hide');
        $(eleBody).get(0).style.display = 'none';
    }
    function _showCollapsiblePanel(idPanel, eleHeader, eleBody) {
        $(eleHeader).removeClass('collapsible-selected-hide');
        $(eleBody).get(0).style.display = 'block';
    }
    function setCollapsiblePanel(idPanel, strCommand) {
        // Find elements by id.
        for(var i = 0;i < elePanels.length;i++) {
            if(elePanels[i].id == idPanel) {
                // Get header and body from panel.
                var eleHeader = $('> .' + cssCollapsibleHeader, elePanels[i]).get(0);
                var eleBody = $('> .' + cssCollapsibleBody, elePanels[i]).get(0);
                // Execute command.
                switch(strCommand.toLowerCase()) {
                    case '1': case 'show': case 'open':
                        _showCollapsiblePanel(idPanel, eleHeader, eleBody);break;
                    case '0': case 'hide': case 'close':
                        _hideCollapsiblePanel(idPanel, eleHeader, eleBody);break;
                    default:
                        return false;break;
                }
                // Return element found.
                return elePanels[i];
            }
        }
        return false;
    }
    function _createCollapsiblePanelEvents(idPanel, eleHeader, eleBody) {
        $(eleHeader).toggle(function(e) {
            // Has toggled, then trigger next event.
            if($(eleBody).css('display') == 'none') {$(this).trigger('click');return;}
            // Animate hide.
            $(eleBody).animate({opacity:'toggle', height:'toggle'}, 'fast', function() {
                // Hide panel.
                _hideCollapsiblePanel(idPanel, eleHeader, eleBody);
            });
        }, function(e) {
            // Animate show.
            $(eleBody).animate({opacity:'toggle', height:'toggle'}, 'fast', function() {
                // Show panel.
                _showCollapsiblePanel(idPanel, eleHeader, eleBody);
                if($.browser.msie) {$(this).get(0).style.removeAttribute('filter');}
            });
        });
    }
    function _createCollapsiblePanels() {
        // Get panels.
        elePanels = $('.' + cssCollapsiblePanel, eleRegion).get();
        for(var i = 0;i < elePanels.length;i++) {
            // Get header and body from panel.
            var eleHeader = $('> .' + cssCollapsibleHeader, elePanels[i]).get(0);
            var eleBody = $('> .' + cssCollapsibleBody, elePanels[i]).get(0);
            // Prepend and append markup code to header.
            var eleHeaderLeft = $('<span class=\"' + cssCollapsibleHeader + '-left\"></span>').prependTo(eleHeader).get(0);
            var eleHeaderRight = $('<span class=\"' + cssCollapsibleHeader + '-right\"></span>').appendTo(eleHeader).get(0);
            // Restore state.
            if(!$(eleHeader).hasClass('collapsible-selected-hide') && $(eleBody).css('display') == 'none') {$(eleHeader).addClass('collapsible-selected-hide');}
            // Bind events to header.
            _createCollapsiblePanelEvents(elePanels[i].id, eleHeader, eleBody);
        }
    }
    function init() {
        eleRegion = $('#' + idRegion).get(0);
        _createCollapsiblePanels();
    }
}
// Perform AJAX to ASP.NET Web Services using JSON.
function jsonAjaxWebservice(strAjaxClass, strAjaxMethod, ajaxArguments, eleOutput, fnCallback) {
    if(typeof strAjaxMethod === 'string' && (typeof ajaxArguments === 'string' || ajaxArguments === null)) {
        if(isEmpty(ajaxArguments)) {
            ajaxArguments = '{}'; // http://encosia.com/2008/06/05/3-mistakes-to-avoid-when-using-jquery-with-aspnet-ajax/
        } else if('{' != ajaxArguments.charAt(0) && ajaxArguments.charAt(ajaxArguments.length - 1) != '}') {
            ajaxArguments = '{' + ajaxArguments + '}';
        }
    } else {return false;}
    if(strAjaxClass.lastIndexOf('.asmx') < 0) {
        strAjaxClass += '.asmx';
    }
    $.ajax({
        type:'POST',
        url:strAjaxClass + '/' + strAjaxMethod,
        beforeSend:function(xhr) {
            xhr.setRequestHeader('Content-type', 'application/json;charset=utf-8');
        },
        contentType:'application/json;charset=utf-8', // Require for browser Microsoft Internet Explorer.
        data:ajaxArguments,
        dataType:'json',
        timeout:3000,
        error:function(xhr, status, ex) {
            if(jQuery.isFunction(fnCallback)) {fnCallback.call(this, status, null);}
            status = '<h5>AJAX Error</h5>' + status + ', ' + strAjaxMethod + '(' + ajaxArguments + ')<br/>';
            if(!isEmpty(eleOutput)) {$(eleOutput).trigger('output', [null, status]);}
        },
        success:function(data, status) {
            if(jQuery.isFunction(fnCallback)) {fnCallback.call(this, status, data);}
            status = '<h5>AJAX Response</h5>' + status + ', ' + strAjaxMethod + '(' + ajaxArguments + ')<br/>';
            if(!isEmpty(eleOutput)) {$(eleOutput).trigger('output', [data, status]);}
        }
    });
    return true;
}

// Register ready event to be executed when the DOM document has finished loading.
var globalMinHeight = new classMinHeight('region-middle');
var globalDashboard = new classDashboard('dashboard', 'dashboard-min', 'dashboard-max', 'dashboard-drawers');
$(document).ready(function() {
    globalMinHeight.init();
    globalDashboard.init();
});
// <!-- END: jQuery JavaScript Library Embedded -->