/**
@file
    TableUsers.cs
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

/// <summary>Class TableUsers extends TableCommon</summary>
public class TableUsers : TableCommon {

#region Fields

    public const String TBL__users = "users";
    public const String TBL__users___PK__user_id = "user_id";
    public const String TBL__users___deleted = "user_deleted";
    public const String TBL__users__user_id = "user_id";
    public const String TBL__users__user_alias = "user_alias";
    public const String TBL__users__user_name_first = "user_name_first";
    public const String TBL__users__user_name_last = "user_name_last";
    public const String TBL__users__user_password = "user_password";
    public const String TBL__users__user_email = "user_email";
    public const String TBL__users__user_activation_key = "user_activation_key";
    public const String TBL__users__user_activated = "user_activated";
    public const String TBL__users__user_session_key = "user_session_key";
    public const String TBL__users__user_date_login = "user_date_login";
    public const String TBL__users__user_date_activation_created = "user_date_activation_created";
    public const String TBL__users__user_date_session_created = "user_date_session_created";
    public const String TBL__users__user_date_created = "user_date_created";
    public const String TBL__users__user_deleted = "user_deleted";

    public const String TBL__user_profiles = "user_profiles";
    public const String TBL__user_profiles___FK__user_id = "user_id";
    public const String TBL__user_profiles___FK__user_organization_location_id = "user_organization_location_id";
    public const String TBL__user_profiles__user_id = "user_id";
    public const String TBL__user_profiles__user_name_salutation = "user_name_salutation";
    public const String TBL__user_profiles__user_name_middle = "user_name_middle";
    public const String TBL__user_profiles__user_occupation = "user_occupation";
    public const String TBL__user_profiles__user_phone = "user_phone";
    public const String TBL__user_profiles__user_phone_extension = "user_phone_extension";

    public const String TBL__user_settings = "user_settings";
    public const String TBL__user_settings___PK__setting_id = "setting_id";
    public const String TBL__user_settings___FK__setting_user_id = "setting_user_id";
    public const String TBL__user_settings___setting_id = "setting_id";
    public const String TBL__user_settings___setting_user_id = "setting_user_id";
    public const String TBL__user_settings___setting_key = "setting_key";
    public const String TBL__user_settings___setting_value = "setting_value";

    // Dictionary collection of of key/value pairs.
    public const String TBL__user_settings___setting_key_role = "role";

#endregion

    /// <summary>Default constructor.</summary>
	public TableUsers() {}
    /// <summary>Get relevant columns for user.</summary>
    public static System.Collections.SortedList getColumnsRelevantUsers() {
        System.Collections.SortedList p = new System.Collections.SortedList();

        p.Add(1, TBL__users__user_id);
        p.Add(2, TBL__users__user_alias);
        p.Add(3, TBL__users__user_name_first);
        p.Add(4, TBL__users__user_name_last);
        p.Add(5, TBL__users__user_email);
        p.Add(6, TBL__users__user_activated);
        p.Add(7, TBL__users__user_date_activation_created);
        p.Add(8, TBL__users__user_date_created);
        p.Add(9, TBL__users__user_date_login);
        return p;
    }
    /// <summary>Is required parameters valid.</summary>
    public bool isValid() {
        return true;
    }

#region Stored Procedures: User

    /// <summary>Create (INSERT) to database item and return id from database using stored procedure.</summary>
    public int createUserReturnId(System.Collections.Hashtable parameters) {
        return Convert.ToInt32(((DataTable)runStoredProcedure("spInsertUser", parameters, DatabaseCommon.enumSqlDataReturn.DataTable)).Rows[0][0]);
    }
    /// <summary>Remove (DELETE) database item using stored procedure.</summary>
    public bool removeUser(System.Collections.Hashtable parameters) {
        return (bool)runStoredProcedure("spDeleteUser", parameters, DatabaseCommon.enumSqlDataReturn.Boolean);
    }
    /// <summary>Remove (DELETE) database item using stored procedure.</summary>
    public bool removeUsers() {
        return (bool)runStoredProcedure("spDeleteUsers", null, DatabaseCommon.enumSqlDataReturn.Boolean);
    }

#endregion

#region Stored Procedures: User Profile

    /// <summary>Create (INSERT) database item using stored procedure.</summary>
    public bool createUserProfile(System.Collections.Hashtable parameters) {
        if(!isValid()) {
            return false;
        } else {
            return (bool)runStoredProcedure("spInsertUserProfile", parameters, DatabaseCommon.enumSqlDataReturn.Boolean);
        }
    }
    /// <summary>Get (SELECT) database item using stored procedure.</summary>
    public DataTable getUserProfile(System.Collections.Hashtable parameters) {
        return (DataTable)runStoredProcedure("spSelectUserProfile", parameters, DatabaseCommon.enumSqlDataReturn.DataTable);
    }
    /// <summary>Set (UPDATE) database item using stored procedure.</summary>
    public bool setUserProfile(System.Collections.Hashtable parameters) {
        if(!isValid()) {
            return false;
        } else {
            return (bool)runStoredProcedure("spUpdateUserProfile", parameters, DatabaseCommon.enumSqlDataReturn.Boolean);
        }
    }
    /// <summary>Remove (DELETE) database item using stored procedure.</summary>
    public bool removeUserProfile(System.Collections.Hashtable parameters) {
        return (bool)runStoredProcedure("spDeleteUserProfile", parameters, DatabaseCommon.enumSqlDataReturn.Boolean);
    }

#endregion

#region Stored Procedures: User Contacts

    /// <summary>Get (SELECT) database item using stored procedure.</summary>
    public DataTable getUserContacts(System.Collections.Hashtable parameters) {
        return (DataTable)runStoredProcedure("spSelectUserContacts", parameters, DatabaseCommon.enumSqlDataReturn.DataTable);
    }

#endregion

#region Dynamic SQL

    /// <summary>Create user setting.</summary>
    public void createUserSetting(String fieldKey, String fieldValue, int userId) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__user_settings___setting_user_id, userId);
        p1.Add(TBL__user_settings___setting_key, fieldKey);
        p1.Add(TBL__user_settings___setting_value, fieldValue);
        dynamicSqlInsert(p1, TBL__user_settings);
    }
    /// <summary>Get user for session.</summary>
    public DataRow getUser(String user_email) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__users__user_id, null);
        p1.Add(TBL__users__user_alias, null);
        p1.Add(TBL__users__user_name_first, null);
        p1.Add(TBL__users__user_name_last, null);
        p1.Add(TBL__users__user_email, null);
        return base.dynamicSqlSelect(p1, TBL__users, TBL__users__user_email + " = " + DatabaseCommon.sanitize(user_email)).Rows[0];
    }
    /// <summary>Get users.</summary>
    public DataTable getUsers(System.Collections.IDictionary parameters) {
        return base.dynamicSqlSelect(parameters, TBL__users, TBL__users___deleted + " = 0");
    }
    /// <summary>Get users (brief).</summary>
    public DataTable getUsersBrief() {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__users__user_id, null);
        p1.Add(TBL__users__user_alias, null);
        p1.Add(TBL__users__user_name_first, null);
        p1.Add(TBL__users__user_name_last, null);
        p1.Add(TBL__users__user_email, null);
        return base.dynamicSqlSelect(p1, TBL__users, TBL__users___deleted + " = 0");
    }
    /// <summary>Get trashed users.</summary>
    public DataTable getUsersTrashed(System.Collections.IDictionary parameters) {
        return base.dynamicSqlSelect(parameters, TBL__users, TBL__users___deleted + " = 1");
    }
    /// <summary>Get user id.</summary>
    public int getUserId(String user_name) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__users___PK__user_id, null);
        return (int)base.dynamicSqlSelect(p1, TBL__users, TBL__users__user_alias + " = " + DatabaseCommon.sanitize(user_name)).Rows[0][TBL__users___PK__user_id];
    }
    /// <summary>Get user settings.</summary>
    public System.Collections.Hashtable getUserSettings(int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__user_settings___setting_key, null);
        p1.Add(TBL__user_settings___setting_value, null);
        DataTable dt1 = base.dynamicSqlSelect(p1, TBL__user_settings, TBL__user_settings___FK__setting_user_id + " = " + DatabaseCommon.sanitize(id));
        return convertDataTableToHashtable(dt1, TBL__user_settings___setting_key, TBL__user_settings___setting_value);
    }
    /// <summary>Get user role.</summary>
    public int getUserRole(int id) {
        int role = System.Convert.ToInt32(getUserSettings(id)[TBL__user_settings___setting_key_role]);
        return role;
    }
    /// <summary>Get user email.</summary>
    public String getUserEmail(int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__users___PK__user_id, null);
        p1.Add(TBL__users__user_alias, null);
        p1.Add(TBL__users__user_email, null);
        return (String)base.dynamicSqlSelect(p1, TBL__users, TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(id)).Rows[0][TBL__users__user_email];
    }
    /// <summary>Get user human first and last name.</summary>
    public String getUserHumanName(int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__users__user_name_first, null);
        p1.Add(TBL__users__user_name_last, null);
        System.Data.DataRow dr1 = base.dynamicSqlSelect(p1, TBL__users, TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(id)).Rows[0];
        return (String)dr1[TBL__users__user_name_first] + " " + (String)dr1[TBL__users__user_name_last];
    }
    /// <summary>Get user human formal (full) name.</summary>
    public String getUserHumanNameFormal(int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__user_profiles__user_name_salutation, null);
        p1.Add(TBL__users__user_name_first, null);
        p1.Add(TBL__user_profiles__user_name_middle, null);
        p1.Add(TBL__users__user_name_last, null);
        try {
            System.Data.DataRow dr1 = base.dynamicSqlSelect(p1, TBL__users + ", " + TBL__user_profiles, TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(id) + " AND " + TBL__user_profiles___FK__user_id + " = " + DatabaseCommon.sanitize(id)).Rows[0];
            return (String)dr1[TBL__user_profiles__user_name_salutation] + " " + dr1[TBL__users__user_name_first] + " " + (String)dr1[TBL__user_profiles__user_name_middle] + " " + (String)dr1[TBL__users__user_name_last];
        } catch {
            return "No human name found.";
        }
    }
    /// <summary>Set user setting.</summary>
    public void setUserSetting(String fieldKey, String fieldValue, int userId) {      
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__user_settings___setting_value, fieldValue);
        dynamicSqlUpdate(p1, TBL__user_settings, TBL__user_settings___FK__setting_user_id + " = " + DatabaseCommon.sanitize(userId) + " AND " + TBL__user_settings___setting_key + " = " + DatabaseCommon.sanitize(fieldKey));
    }
    /// <summary>Set login date to user.</summary>
    public void setUserDateLogin(int id, DateTime dtn) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__users__user_date_login, dtn);
        dynamicSqlUpdate(p1, TBL__users, TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(id));
    }
    /// <summary>Count user accounts (against user alias/email and password).</summary>
    public int countUserAccounts(String user, String password) {
        if(System.Text.RegularExpressions.Regex.IsMatch(user, @"\S+@\S+\.\S+")) {
            // By user email.
            return base.dynamicSqlCount(TBL__users, "LOWER(" + TBL__users__user_email + ") = " + DatabaseCommon.sanitize(user.ToLower()) + " AND " + TBL__users__user_password + " = " + DatabaseCommon.sanitize(password) + " AND " + TBL__users__user_activated + " = 1" + " AND " + TBL__users___deleted + " = 0");
        } else {
            // By user alias.
            //return base.dynamicSqlCount(TBL__users, "LOWER(" + TBL__users__user_alias + ") = " + DatabaseCommon.sanitize(user.ToLower()) + " AND " + TBL__users__user_password + " = " + DatabaseCommon.sanitize(password) + " AND " + TBL__users__user_activated + " = 1" + " AND " + TBL__users___deleted + " = 0");
            return 0;
        }       
    }
    /// <summary>Count usernames.</summary>
    public int countUsernames(String username) {
        return base.dynamicSqlCount(TBL__users, "LOWER(" + TBL__users__user_alias + ") = " + DatabaseCommon.sanitize(username.ToLower()));
    }
    /// <summary>Count emails.</summary>
    public int countEmails(String email) {
        return base.dynamicSqlCount(TBL__users, "LOWER(" + TBL__users__user_email + ") = " + DatabaseCommon.sanitize(email.ToLower()));
    }
    /// <summary>Existence of username.</summary>
    public bool hasUsername(String username) {
        return base.dynamicSqlExists(null, TBL__users, "LOWER(" + TBL__users__user_alias + ") = " + DatabaseCommon.sanitize(username.ToLower()));
    }
    /// <summary>Existence of password.</summary>
    public bool hasPassword(String password) {
        return base.dynamicSqlExists(null, TBL__users, TBL__users__user_password + " = " + DatabaseCommon.sanitize(password));
    }
    /// <summary>Existence of email.</summary>
    public bool hasEmail(String email) {
        return base.dynamicSqlExists(null, TBL__users, "LOWER(" + TBL__users__user_email + ") = " + DatabaseCommon.sanitize(email.ToLower()));
    }
    /// <summary>Existence of username or email.</summary>
    public bool hasUsernameOrEmail(String username, String email) {
        return base.dynamicSqlExists(null, TBL__users, "LOWER(" + TBL__users__user_alias + ") = " + DatabaseCommon.sanitize(username.ToLower()) + " OR " + "LOWER(" + TBL__users__user_email + ") = " + DatabaseCommon.sanitize(email.ToLower()));
    }
    /// <summary>Existence of activation key.</summary>
    public bool hasActivationKey(String key) {
        return base.dynamicSqlExists(null, TBL__users, TBL__users__user_activation_key + " = " + DatabaseCommon.sanitize(key));
    }
    /// <summary>Existence of user profile.</summary>
    public bool hasUserProfile(int id) {
        return base.dynamicSqlExists(null, TBL__user_profiles, TBL__user_profiles___FK__user_id + " = " + DatabaseCommon.sanitize(id));
    }
    /// <summary>Existence of session key.</summary>
    public bool hasSessionKey(String key) {
        return base.dynamicSqlExists(null, TBL__users, TBL__users__user_session_key + " = " + DatabaseCommon.sanitize(key));
    }
    /// <summary>Get activation key and set (UPDATE) new activation key to database.</summary>
    public String getActivationKey(String email) {
        String key = getRandomKeyUsingGuid(30);
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__users__user_activation_key, key);
        base.dynamicSqlUpdate(p1, TBL__users, "LOWER(" + TBL__users__user_email + ") = " + DatabaseCommon.sanitize(email.ToLower()));
        return key;
    }
    /// <summary>Get session key and set (UPDATE) new session key to database.</summary>
    public String getSessionKey(int id, DateTime dtn) {
        String key = getRandomKeyUsingGuid(8);
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__users__user_session_key, key);
        p1.Add(TBL__users__user_date_session_created, dtn);
        base.dynamicSqlUpdate(p1, TBL__users, TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(id));
        return key;
    }
    /// <summary>Remove session key.</summary>
    public void removeSessionKey(int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TBL__users__user_session_key, String.Empty);
        p1.Add(TBL__users__user_date_session_created, DBNull.Value);
        base.dynamicSqlUpdate(p1, TBL__users, TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(id));
    }
    /// <summary>Restore trashed user.</summary>
    public bool restoreUser(int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TableUsers.TBL__users___deleted, 0);
        return base.dynamicSqlUpdate(p1, TBL__users, TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(id));
    }
    /// <summary>Trash user.</summary>
    public bool trashUser(int id) {
        System.Collections.Hashtable p1 = new System.Collections.Hashtable();
        p1.Add(TableUsers.TBL__users___deleted, 1);
        return base.dynamicSqlUpdate(p1, TBL__users, TBL__users___PK__user_id + " = " + DatabaseCommon.sanitize(id));
    }

#endregion

    /// <summary>Is datetime's age expired by hours.</summary>
    public bool isDateTimeAgeExpiredByHours(DateTime record, int ageHours) {
        TimeSpan span = getDateTime().Subtract(record);
        if(span.Hours > ageHours) {
            return true;
        } else {
            return false;
        }
    }
    /// <summary>Get random key using GUID.</summary>
    /// <remarks>http://aspnet.4guysfromrolla.com/articles/101205-1.aspx</remarks>
    public String getRandomKeyUsingGuid(int length) {
        // Get the GUID
        string guidResult = System.Guid.NewGuid().ToString();
        // Remove the hyphens
        guidResult = guidResult.Replace("-", String.Empty);
        // Make sure length is valid
        if(length <= 0 || length > guidResult.Length)
            throw new ArgumentException("Length must be between 1 and " + guidResult.Length + ".");
        // Return the first length bytes
        return guidResult.Substring(0, length);
    }
}

} // END namespace ent