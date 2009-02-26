/**
@file
    TableLocations.cs
@brief
    Copyright 2009 Company. All rights reserved.
@author
    William Chang
@version
    0.1
@date
    - Created: 2008-08-29
    - Modified: 2009-02-26
    .
@note
    References:
    - General:
        - http://en.wikipedia.org/wiki/C_Sharp_(programming_language)
        .
    .
*/

using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

namespace ent {

/// <summary>Class TableLocations extends TableCommon</summary>
public class TableLocations : TableCommon {
	
#region Fields

    public const String TBL__locations = "locations";
    public const String TBL__locations___PK__location_id = "location_id";
    public const String TBL__locations___deleted = "location_deleted";
    public const String TBL__locations__location_id = "location_id";
    public const String TBL__locations__location_name = "location_name";
    public const String TBL__locations__location_address1 = "location_address1";
    public const String TBL__locations__location_address2 = "location_address2";
    public const String TBL__locations__location_city = "location_city";
    public const String TBL__locations__location_state = "location_state";
    public const String TBL__locations__location_zip = "location_zip";
    public const String TBL__locations__location_country = "location_country";
    public const String TBL__locations__location_deleted = "location_deleted";

#endregion

    /// <summary>Default constructor.</summary>
    public TableLocations() {}
    /// <summary>Get id.</summary>
    public int getId(String name) {
        System.Collections.Hashtable p = new System.Collections.Hashtable();

        p.Add(TBL__locations___PK__location_id, null);
        return (int)base.dynamicSqlSelect(p, TBL__locations, TBL__locations__location_name + " = " + DatabaseCommon.sanitize(name)).Rows[0][TBL__locations___PK__location_id];
    }
}

} // END namespace ent