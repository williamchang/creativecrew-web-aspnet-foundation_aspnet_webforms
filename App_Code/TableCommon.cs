/**
@file
    TableCommon.cs
@brief
    Copyright 2009 Company. All rights reserved.
@author
    William Chang
@version
    0.1
@date
    - Created: 2008-08-29
    - Modified: 2008-09-12
    .
@note
    References:
    - General:
        - http://en.wikipedia.org/wiki/C_Sharp_(programming_language)
        .
    - Reflection:
        - objType.InvokeMember("", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static, null, null, new Object[0]);
        .
    .
*/

using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

namespace ent {

/// <summary>Class TableCommon</summary>
public class TableCommon : DatabaseCommon {
    /// <summary>Default constructor.</summary>
	public TableCommon() {}
    /// <summary>Find (to validate) a exact match from every table's paramemters.</summary>
    public static Object isValidParameter(String parameterUnknown, bool returnColumns, String methodColumnsName) {
        String[] types = {
            "ent.TableApplication",
            "ent.TableLists",
            "ent.TableLocations",
            "ent.TableUsers",
        };

        for(int i = 0;i < types.Length;i++) {
            System.Type objType = System.Web.Compilation.BuildManager.GetType(types[i], true, true);
            System.Reflection.FieldInfo[] objFields = objType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy);
            for(int j = 0;j < objFields.Length;j++) {
                if(objFields[j].IsLiteral && !objFields[j].IsInitOnly) {
                    String val = (String)objFields[j].GetValue(null);
                    if(String.Equals(parameterUnknown, val)) {
                        if(returnColumns) {
                            return (System.Collections.IDictionary)objType.InvokeMember(methodColumnsName, System.Reflection.BindingFlags.InvokeMethod, null, null, new Object[0]);
                        } else {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}

} // END namespace ent