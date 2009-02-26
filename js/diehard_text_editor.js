/**
@file
    diehard_text_editor.js
@brief
    Copyright 2009 Creative Crew. All rights reserved.
@author
    William Chang
    Email: william@babybluebox.com
    Website: http://www.babybluebox.com
@version
    0.1
@date
    - Created: 2008-09-10
    - Modified: 2009-02-07
    .
@note
    Prerequisites:
    - jQuery http://www.jquery.com/
    - TinyMCE http://tinymce.moxiecode.com/
    .
    References:
    - General:
        - http://tinymce.moxiecode.com/
        - http://wiki.moxiecode.com/index.php/TinyMCE:Commands
        - http://hamisageek.blogspot.com/2007/11/multiple-tinymce-3-instances-on-one.html
        .
    - Validation:
        - http://tinymce.moxiecode.com/punbb/viewtopic.php?id=27
        .
    - HTML encode and decode:
        - http://tinymce.moxiecode.com/punbb/viewtopic.php?id=9587
        .
    - Dynamic load css:
        - http://tinymce.moxiecode.com/punbb/viewtopic.php?id=6697
        - http://wiki.moxiecode.com/index.php/TinyMCE:Configuration/content_css
        .
    - Auto saving:
        - http://webnv.net/articles/autosaving-with-jquery-and-tinymce
        .
    .
*/

/// Text editor widget.
classTextEditor = function(idRegion, idInputText) {
    var memberPublic = this;
    memberPublic.init = init;
    memberPublic.onLoadTextEditor = onLoadTextEditor;
    memberPublic.onUnloadTextEditor = onUnloadTextEditor;
    memberPublic.getContent = getContent;
    memberPublic.setContent = setContent;
    memberPublic.encodeHtml = encodeHtml;
    memberPublic.decodeHtml = decodeHtml;
    memberPublic.idRegion = idRegion;
    memberPublic.idInputText = idInputText;
    var _eleRegion, _eleInputText;
    var _objTextEditorConfigs = [];
    var _isLoaded;
    
    // Load text editor.
    function onLoadTextEditor() {
        tinyMCE.settings = _objTextEditorConfigs[0];
        tinyMCE.execCommand('mceAddControl', false, idInputText);
        _isLoaded = true;
    }
    // Unload text editor.
    function onUnloadTextEditor() {
        if(_isLoaded) {
            tinyMCE.execCommand('mceRemoveControl', false, idInputText);
            _setEvents();
            _isLoaded = false;
        }
        $(_eleInputText).val('');
    }
    // Clean up content.
    function _onCleanup(strType, strContent) {
        if(strType == 'insert_to_editor') {
            strContent = decodeHtml(strContent);
        }
        return strContent;
    }
    // Save content.
    function _onSaveContent(idInputText, strContent, eleBody) {
        return encodeHtml(strContent);
    }
    // After text editor is loaded.
    function _onTextEditorLoadFinish(idEditor, eleEditorBody, eleEditorDocument) {
        tinyMCE.execCommand('mceFocus', false, idEditor);
    }
    // Get content.
    function getContent() {
        if(tinyMCE.get(idInputText) != undefined) {
            return tinyMCE.get(idInputText).getContent();
        } else {
            return $(_eleInputText).val();
        }
    }
    // Set content.
    function setContent(str) {
        tinyMCE.get(idInputText).setContent(str);
    }
    // Encode HTML.
    function encodeHtml(str) {
        str = str.replace(/</gi, '&lt;');
        str = str.replace(/>/gi, '&gt;');
        return str;
    }
    // Decode HTML.
    function decodeHtml(str) {
        str = str.replace(/&lt;/gi, '<');
        str = str.replace(/&gt;/gi, '>');
        return str;
    }
    // Set events.
    function _setEvents() {
        $('#' + idInputText, _eleRegion).unbind('focus').bind('focus', function(e) {
            onLoadTextEditor();
            $(this).unbind('focus');
        });
    }
    // Set text editor configurations.
    function _setTextEditorConfigs() {
        _objTextEditorConfigs = [{
            mode:'none',
            theme:'advanced',
            skin:'o2k7',
            plugins:'inlinepopups,preview,paste,visualchars,safari',
            theme_advanced_buttons1:'cut,copy,paste,pastetext,pasteword,|,bullist,numlist,outdent,indent,|,undo,redo,|,link,unlink,|,removeformat,cleanup,|,visualchars,visualaid,code,preview',
            theme_advanced_buttons2:'styleselect,formatselect,|,bold,italic,underline,strikethrough,blockquote,|,justifyleft,justifycenter,justifyright,justifyfull,|,forecolor,backcolor',
            theme_advanced_buttons3:'',
            theme_advanced_toolbar_location:'top',
            theme_advanced_toolbar_align:'left',
            theme_advanced_statusbar_location:'bottom',
            theme_advanced_resizing:false,
            accessibility_focus:false,
            setupcontent_callback:_onTextEditorLoadFinish,
            cleanup_callback:_onCleanup,
            save_callback:_onSaveContent,
            dialog_type:'window',
            content_css:'/css/content/document.css',
            invalid_elements:'script,object,applet,iframe'
        }];
    }
    // Init this class.
    function init() {
        _eleRegion = $('#' + idRegion).get(0);
        _eleInputText = $('#' + idInputText, _eleRegion).get(0);
        _isLoaded = false;
        _setTextEditorConfigs();
        _setEvents();
    }
}