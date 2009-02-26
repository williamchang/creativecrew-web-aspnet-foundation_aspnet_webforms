/**
@file
    ServiceCommon.cs
@brief
    Copyright 2009 Company. All rights reserved.
@author
    William Chang
@version
    0.1
@date
    - Created: 2008-09-09
    - Modified: 2009-01-30
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

/// <summary>Class ServiceCommon extends WebService</summary>
[System.Web.Script.Services.ScriptService()]
public class ServiceCommon : System.Web.Services.WebService {
    /// <summary>Default constructor.</summary>
    public ServiceCommon() {}
}

} // END namespace ent