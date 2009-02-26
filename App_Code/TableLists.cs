/**
@file
    TableLists.cs
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

/// <summary>Class TableLists extends TableCommon</summary>
public class TableLists : TableCommon {

#region Enumerations

    public enum enumContentPageAssignedTo {
        Authors = -2,
        Administrators = -1,
        Moderators = 0
    }
    public enum enumContentPageOrderChildren {
        Default = 1,
        DatePublishedAsc = 2,
        DateModifiedAsc = 3,
        DateCreatedAsc = 4,
        HeaderAsc = 5
    }
    public enum enumContentPageType {
        Single = 1,
        Sequential = 2
    }
    public enum enumContentPostStatus {
        Publish = 1,
        Draft = 2
    }
    public enum enumContentPostType {
        Record = 1,
        Template = 2,
        Style = 3,
        System = 4
    }

#endregion

#region Fields

    public const String TBL__user_rolelist = "user_rolelist";
    public const String TBL__user_rolelist___PK__rolelist_id = "list_id";
    public const String TBL__user_rolelist___deleted = "list_deleted";
    public const String TBL__user_rolelist__list_id = "list_id";
    public const String TBL__user_rolelist__list_name = "list_name";
    public const String TBL__user_rolelist__list_deleted = "list_deleted";

    public const String TBL__content_page_orderchildrenlist = "content_page_orderchildrenlist";
    public const String TBL__content_page_orderchildrenlist___PK__orderchildrenlist_id = "list_id";
    public const String TBL__content_page_orderchildrenlist___deleted = "list_deleted";
    public const String TBL__content_page_orderchildrenlist__list_id = "list_id";
    public const String TBL__content_page_orderchildrenlist__list_name = "list_name";
    public const String TBL__content_page_orderchildrenlist__list_deleted = "list_deleted";

    public const String TBL__content_page_typelist = "content_page_typelist";
    public const String TBL__content_page_typelist___PK__rolelist_id = "list_id";
    public const String TBL__content_page_typelist___deleted = "list_deleted";
    public const String TBL__content_page_typelist__list_id = "list_id";
    public const String TBL__content_page_typelist__list_name = "list_name";
    public const String TBL__content_page_typelist__list_deleted = "list_deleted";

    public const String TBL__content_post_statuslist = "content_post_statuslist";
    public const String TBL__content_post_statuslist___PK__rolelist_id = "list_id";
    public const String TBL__content_post_statuslist___deleted = "list_deleted";
    public const String TBL__content_post_statuslist__list_id = "list_id";
    public const String TBL__content_post_statuslist__list_name = "list_name";
    public const String TBL__content_post_statuslist__list_deleted = "list_deleted";

    public const String TBL__content_post_typelist = "content_post_typelist";
    public const String TBL__content_post_typelist___PK__rolelist_id = "list_id";
    public const String TBL__content_post_typelist___deleted = "list_deleted";
    public const String TBL__content_post_typelist__list_id = "list_id";
    public const String TBL__content_post_typelist__list_name = "list_name";
    public const String TBL__content_post_typelist__list_deleted = "list_deleted";

    public const String TBL__file_typelist = "file_typelist";
    public const String TBL__file_typelist___PK__rolelist_id = "list_id";
    public const String TBL__file_typelist___deleted = "list_deleted";
    public const String TBL__file_typelist__list_id = "list_id";
    public const String TBL__file_typelist__list_name = "list_name";
    public const String TBL__file_typelist__list_deleted = "list_deleted";

#endregion

    /// <summary>Default constructor.</summary>
	public TableLists() {}
    /// <summary>Get relevant columns for user.</summary>
    public static System.Collections.SortedList getColumnsRelevantList() {
        System.Collections.SortedList p = new System.Collections.SortedList();

        p.Add(1, "list_id");
        p.Add(2, "list_name");
        p.Add(3, "list_deleted");
        return p;
    }

#region Dynamic SQL

    /// <summary>Get name from list by id.</summary>
    public String getName(String tableName, int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();

        p1.Add("list_name", null);
        return (String)base.dynamicSqlSelect(p1, tableName, "list_id = " + id).Rows[0]["list_name"];
    }
    /// <summary>Get list.</summary>
    public DataTable getList(System.Collections.IDictionary parameters, String tableName) {
        return base.dynamicSqlSelect(parameters, tableName, "list_deleted = 0");
    }
    /// <summary>Get trashed list.</summary>
    public DataTable getListTrashed(System.Collections.IDictionary parameters, String tableName) {
        return base.dynamicSqlSelect(parameters, tableName, "list_deleted = 1");
    }
    /// <summary>Remove list.</summary>
    public bool removeList(String tableName, int id) {
        return base.dynamicSqlDelete(tableName, "list_id = " + DatabaseCommon.sanitize(id));
    }
    /// <summary>Restore trashed list.</summary>
    public bool restoreList(String tableName, int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add("list_deleted", 0);
        return base.dynamicSqlUpdate(p1, tableName, "list_id = " + DatabaseCommon.sanitize(id));
    }
    /// <summary>Trash list.</summary>
    public bool trashList(String tableName, int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add("list_deleted", 1);
        return base.dynamicSqlUpdate(p1, tableName, "list_id = " + DatabaseCommon.sanitize(id));
    }

#endregion

    /// <summary>Get assigned to by converting from role.</summary>
    public static int getAssignedToByRole(System.Web.SessionState.HttpSessionState s) {
        int who = -1000;
        switch(ApplicationCommon.getRole(s)) {
            case ApplicationCommon.enumRoles.Administrator:
                who = (int)TableLists.enumContentPageAssignedTo.Administrators;
                break;
            case ApplicationCommon.enumRoles.Moderator:
                who = (int)TableLists.enumContentPageAssignedTo.Moderators;
                break;
            case ApplicationCommon.enumRoles.Author:
                who = (int)TableLists.enumContentPageAssignedTo.Authors;
                break;
            default:
                who = -1000;
                break;
        }
        return who;
    }
}

} // END namespace ent