/**
@file
    ServiceUsers.cs
@brief
    Copyright 2009 Company. All rights reserved.
@author
    William Chang
@version
    0.1
@date
    - Created: 2009-01-27
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

/// <summary>Class ServiceUsers extends ServiceCommon</summary>
[System.Web.Script.Services.ScriptService()]
public class ServiceUsers : ServiceCommon {
    /// <summary>Default constructor.</summary>
    public ServiceUsers() {}
    /// <summary>Get users.</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public String getUsers() {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return String.Empty;}

        TableUsers t1 = new TableUsers();
        return DatabaseCommon.toJson(t1.getUsers(TableUsers.getColumnsRelevantUsers()));
    }
    /// <summary>Get trashed users.</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public String getUsersTrashed() {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return String.Empty;}

        TableUsers t1 = new TableUsers();
        return DatabaseCommon.toJson(t1.getUsersTrashed(TableUsers.getColumnsRelevantUsers()));
    }
    /// <summary>Remove user (permanent).</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public bool removeUser(int intId) {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return false;}

        TableUsers t1 = new TableUsers();
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();

        p1.Add(TableUsers.TBL__users, intId);
        return t1.removeUser(p1);
    }
    /// <summary>Restore trashed user.</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public bool restoreUser(int intId) {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return false;}

        TableUsers t1 = new TableUsers();
        return t1.restoreUser(intId);
    }
    /// <summary>Trash user (impermanent).</summary>
    /// <remarks>User must have authenticate session to proceed.</remarks>
    [System.Web.Services.WebMethod(EnableSession = true)]
    public bool trashUser(int intId) {
        // Authenticate.
        if(!ApplicationCommon.isValidSession(Session)) {return false;}

        TableUsers t1 = new TableUsers();
        return t1.trashUser(intId);
    }
}

} // END namespace ent