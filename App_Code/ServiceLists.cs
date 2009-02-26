/**
@file
    ServiceLists.cs
@brief
    Copyright 2009 Company. All rights reserved.
@author
    William Chang
@version
    0.1
@date
    - Created: 2008-09-09
    - Modified: 2009-01-28
    .
@note
    References:
    - General:
        - http://encosia.com/2008/03/27/using-jquery-to-consume-aspnet-json-web-services/
        - http://www.asp.net/learn/ajax/tutorial-03-cs.aspx
        - http://www.dev102.com/2008/04/30/call-aspnet-webmethod-from-jquery/
        .
    .
*/

using System;
using System.Web.Services;

namespace ent {

/// <summary>Class ServiceLists extends ServiceCommon</summary>
[System.Web.Script.Services.ScriptService()]
public class ServiceLists : ServiceCommon {
    /// <summary>Default constructor.</summary>
    public ServiceLists() {}
    /// <summary>Get list.</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public String getList(String strTableName) {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return String.Empty;}

        TableLists t1 = new TableLists();
        return DatabaseCommon.toJson(t1.getList(TableLists.getColumnsRelevantList(), strTableName));
    }
    /// <summary>Get trashed list.</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public String getListTrashed(String strTableName) {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return String.Empty;}

        TableLists t1 = new TableLists();
        return DatabaseCommon.toJson(t1.getListTrashed(TableLists.getColumnsRelevantList(), strTableName));
    }
    /// <summary>Remove list (permanent).</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public bool removeList(String strTableName, int intId) {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return false;}

        TableLists t1 = new TableLists();
        return t1.removeList(strTableName, intId);
    }
    /// <summary>Restore trashed list.</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public bool restoreList(String strTableName, int intId) {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return false;}

        TableLists t1 = new TableLists();
        return t1.restoreList(strTableName, intId);
    }
    /// <summary>Trash list (impermanent).</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public bool trashList(String strTableName, int intId) {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return false;}

        TableLists t1 = new TableLists();
        return t1.trashList(strTableName, intId);
    }
}

} // END namespace ent