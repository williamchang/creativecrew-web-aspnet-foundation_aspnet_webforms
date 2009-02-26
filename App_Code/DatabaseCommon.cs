/**
@file
    DatabaseCommon.cs
@brief
    Copyright 2009 Company. All rights reserved.
@author
    William Chang
@version
    0.1
@date
    - Created: 2008-08-29
    - Modified: 2009-01-16
    .
@note
    References:
    - General:
        - http://www.sqlteam.com/article/introduction-to-dynamic-sql-part-1
    - Stored Procedures:
        - http://en.wikipedia.org/wiki/Stored_procedure
        - http://www.tonymarston.net/php-mysql/stored-procedures-are-evil.html
        - http://weblogs.asp.net/fbouma/archive/2003/11/18/38178.aspx
        .
    - Stored Procedure with sp_executesql:
        - http://www.vishalseth.com/post/2008/07/10/Dynamic-SQL-sp_executesql.aspx
        - http://weblogs.sqlteam.com/brettk/archive/2005/01/27/4029.aspx
        .
    - System.Collections.IDictionary:
        - http://blog.bodurov.com/Post.aspx?postID=18
        - http://anehir.blogspot.com/2008/05/dictionary-performance.html
        - http://www.richardjonas.com/blog/2006/10/c-collection-classes-performance.html
        .
    .
*/

using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;

namespace ent {

/// <summary>Class ApplicationCommon</summary>
public class DatabaseCommon {

#region Enumerations

    public enum enumSqlDataReturn {
        DataSet = 1,
        DataTable = 2,
        Json = 3,
        String = 4,
        Integer = 5,
        Boolean = 7,
        None = 8
    }
    public enum enumDictionarySelect {
        Both = 1,
        Key = 2,
        Value = 3
    }

#endregion

#region Fields

    protected String _ConnectionString;

#endregion

#region Properties

    public String ConnectionString {
        get {return _ConnectionString;}
        set {_ConnectionString = value;}
    }

#endregion

	public DatabaseCommon() {
        // Gets Connection from Application Settings.
        // This code may be be modified if the ConnectionString is not obtained
        // from the Applications Settings or using a different setting.
        try {
            AppSettingsReader appSettings = new AppSettingsReader();
            _ConnectionString = (String)appSettings.GetValue("ConnectionString_Development", typeof(String));
        } catch {
            // This is the case that the ConnectionString would be entered by a user.
            // No action needs to be performed here.
        }
    }
    /// <summary>Argument constructor.</summary>
    public DatabaseCommon(String customConnectionString) {
        _ConnectionString = customConnectionString;
    }

#region Database

    /// <summary>Is OLEDB connection.</summary>
    public bool isOLEDBConnection() {
        // Helps to handle both SQL and OleDB connections
        if(_ConnectionString.ToUpper().Contains("OLEDB"))
            return true;
        return false;
    }
    /// <summary>Get data connection.</summary>
    private Object getConnection() {
        if(isOLEDBConnection()) {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(_ConnectionString);
            return conn;
        } else {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(_ConnectionString);
            return conn;
        }
    }
    /// <summary>Get data command.</summary>
    private Object getCommand() {
        if(isOLEDBConnection()) {
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
            return cmd;
        } else {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            return cmd;
        }
    }
    /// <summary>Get data adapter.</summary>
    private Object getAdapter(object cmd) {
        if(isOLEDBConnection()) {
            System.Data.OleDb.OleDbDataAdapter apt = new System.Data.OleDb.OleDbDataAdapter((System.Data.OleDb.OleDbCommand)cmd);
            return apt;
        } else {
            System.Data.SqlClient.SqlDataAdapter apt = new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)cmd);
            return apt;
        }
    }
    /// <summary>Get datetime.</summary>
    public static DateTime getDateTime() {
        return DateTime.Now;
    }
    /// <summary>Get date string for SQL.</summary>
    public static String getSqlDate(DateTime date) {
        return date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString() + " " + date.Hour.ToString() + ":" + date.Minute.ToString() + ":" + date.Second.ToString();
    }
    /// <summary>Return data after SQL query.</summary>
    public Object returnData(DatabaseCommon.enumSqlDataReturn returnType, System.Data.SqlClient.SqlDataAdapter sqlApt, System.Data.SqlClient.SqlCommand sqlCmd, System.Data.SqlClient.SqlConnection sqlCon) {
        switch(returnType) {
            case DatabaseCommon.enumSqlDataReturn.DataSet:
                DataSet ds = new DataSet();
                sqlApt.Fill(ds);
                sqlCmd.Dispose();
                sqlCon.Close();
                return ds;
            case DatabaseCommon.enumSqlDataReturn.DataTable:
                DataTable dt = new DataTable();
                sqlApt.Fill(dt);
                sqlCmd.Dispose();
                sqlCon.Close();
                return dt;
            case DatabaseCommon.enumSqlDataReturn.Json:
                return false;
            case DatabaseCommon.enumSqlDataReturn.Integer:
                int c = (int)sqlCmd.ExecuteScalar();
                sqlCmd.Dispose();
                sqlCon.Close();
                return c;
            case DatabaseCommon.enumSqlDataReturn.Boolean:
                bool b = System.Convert.ToBoolean((int)sqlCmd.ExecuteScalar());
                sqlCmd.Dispose();
                sqlCon.Close();
                return b;
            case DatabaseCommon.enumSqlDataReturn.None:
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
                sqlCon.Close();
                return null;
            default:
                throw new Exception("The enumeration argument is out of scope.");
        }
    }

#endregion

#region Manual SQL

    /// <summary>Run SQL statement.</summary>
    public Object runSqlStatement(String statement, DatabaseCommon.enumSqlDataReturn returnType) {
        // Create the connection object.
        System.Data.SqlClient.SqlConnection sqlCon = (System.Data.SqlClient.SqlConnection)getConnection();
        // Create the command object.
        System.Data.SqlClient.SqlCommand sqlCmd = (System.Data.SqlClient.SqlCommand)getCommand();
        sqlCmd.Connection = sqlCon;
        sqlCmd.CommandText = statement;
        sqlCmd.CommandType = CommandType.Text;
        // Create an adapter to return data.
        System.Data.SqlClient.SqlDataAdapter sqlApt = (System.Data.SqlClient.SqlDataAdapter)getAdapter(sqlCmd);
        // Open SQL connection.
        sqlCon.Open();
        // Perform query.
        return returnData(returnType, sqlApt, sqlCmd, sqlCon);
    }

#endregion

#region Dynamic SQL

    /// <summary>Select columns from table using dynamic SQL.</summary>
    protected Object dynamicSqlStatement(String statement, DatabaseCommon.enumSqlDataReturn returnType) {
        return runSqlStatement(statement, returnType);
    }
    /// <summary>Select columns from table using dynamic SQL.</summary>
    /// <remarks>
    /// Statement Example: SELECT column1, column2 FROM tablename;
    /// ht.Add("column1", null);
    /// ht.Add("column2", null);
    /// dc.dynamicSqlSelect(ht, "tablename", "");
    /// </remarks>
    public DataTable dynamicSqlSelect(System.Collections.IDictionary parameters, String tableName, String whereClause) {
        DataTable dt = new DataTable(tableName);
        String selectClause = "SELECT *";
        String fieldColumns = String.Empty;
        bool hasSeparator = false;

        if(parameters != null) {
            foreach(System.Collections.DictionaryEntry entry in parameters) {
                // Separated by a comma for next object.
                if(hasSeparator) {
                    fieldColumns += ", ";
                }
                hasSeparator = true;
                // Has override order (integer) and assuming to be a
                // sorted dictionary, then get columns from dictionary value instead.
                if(entry.Key is int) {
                    fieldColumns += entry.Value.ToString();
                } else {
                    fieldColumns += entry.Key.ToString();
                }
            }
            selectClause = "SELECT " + fieldColumns;
        }
        if(!ApplicationCommon.isEmpty(whereClause)) {
            whereClause = " WHERE " + whereClause;
        }
        dt = (DataTable)dynamicSqlStatement(selectClause + " FROM " + tableName + whereClause + ";", DatabaseCommon.enumSqlDataReturn.DataTable);
        dt.TableName = tableName;
        return dt;
    }
    /// <summary>Count selected rows from table using dynamic SQL.</summary>
    public int dynamicSqlCount(String tableName, String whereClause) {
        if(!ApplicationCommon.isEmpty(whereClause)) {
            whereClause = " WHERE " + whereClause;
        }
        return (int)dynamicSqlStatement("SELECT COUNT(*) FROM " + tableName + whereClause + ";", DatabaseCommon.enumSqlDataReturn.Integer);
    }
    /// <summary>Existence of condition using dynamic SQL.</summary>
    public bool dynamicSqlExists(String selectClause, String tableName, String whereClause) {
        if(!ApplicationCommon.isEmpty(selectClause)) {
            selectClause = "SELECT " + selectClause;
        } else {
            selectClause = "SELECT *";
        }
        if(!ApplicationCommon.isEmpty(whereClause)) {
            whereClause = " WHERE " + whereClause;
        }
        return (bool)dynamicSqlStatement("IF EXISTS ( " + selectClause + " FROM " + tableName + whereClause + " ) SELECT 1 AS [exists] ELSE SELECT 0 AS [exists];", DatabaseCommon.enumSqlDataReturn.Boolean);
    }
    /// <summary>Insert row into table using dynamic SQL.</summary>
    /// <remarks>
    /// Statement Example: INSERT INTO tablename ( column1, column2, column3 ) VALUES ( 'string', 0, 9 );
    /// ht.Add("column1", "string");
    /// ht.Add("column2", false);
    /// ht.Add("column3", 9);
    /// dc.dynamicSqlInsert(ht, "tablename");
    /// </remarks>
    public bool dynamicSqlInsert(System.Collections.IDictionary parameters, String tableName) {
        String fieldColumns = String.Empty;
        String fieldValues = String.Empty;
        bool hasSeparator = false;

        foreach(System.Collections.DictionaryEntry entry in parameters) {
            // Separated by a comma for next object.
            if(hasSeparator) {
                fieldColumns += ", ";
                fieldValues += ", ";
            }
            hasSeparator = true;
            fieldColumns += entry.Key.ToString();
            fieldValues += sanitize(entry.Value);
        }
        dynamicSqlStatement("INSERT INTO " + tableName + " ( " + fieldColumns + " ) VALUES ( " + fieldValues + " );", DatabaseCommon.enumSqlDataReturn.None);
        return true;
    }
    /// <summary>Update table columns using dynamic SQL.</summary>
    /// <remarks>
    /// Statement Example: UPDATE tablename SET column1 = 'string', column2 = 0, column3 = 9 WHERE column_id = 4;
    /// ht.Add("column1", "string");
    /// ht.Add("column2", false);
    /// ht.Add("column3", 9);
    /// dc.dynamicSqlUpdate(ht, "tablename", "column_id = 4");
    /// </remarks>
    public bool dynamicSqlUpdate(System.Collections.IDictionary parameters, String tableName, String whereClause) {
        String setClause = String.Empty;
        bool hasSeparator = false;

        if(!ApplicationCommon.isEmpty(whereClause)) {
            whereClause = " WHERE " + whereClause;
        }
        foreach(System.Collections.DictionaryEntry entry in parameters) {
            // Separated by a comma for next object.
            if(hasSeparator) {
                setClause += ", ";
            }
            hasSeparator = true;
            setClause += entry.Key.ToString() + " = " + sanitize(entry.Value);
        }
        dynamicSqlStatement("UPDATE " + tableName + " SET " + setClause + whereClause + ";", DatabaseCommon.enumSqlDataReturn.None);
        return true;
    }
    /// <summary>Insert into or update, if exists, table using dynamic SQL.</summary>
    public bool dynamicSqlComboInsertUpdate(System.Collections.IDictionary parameters, String tableName, String existWhereClause, String updateWhereClause) {
        // Code for UPDATE clause.
        String setClause = String.Empty;
        // Code for INSERT clause.
        String fieldColumns = String.Empty;
        String fieldValues = String.Empty;
        bool hasSeparator = false;

        foreach(System.Collections.DictionaryEntry entry in parameters) {
            // Separated by a comma for next object.
            if(hasSeparator) {
                // Code for UPDATE clause.
                setClause += ", ";
                // Code for INSERT clause.
                fieldColumns += ", ";
                fieldValues += ", ";
            }
            hasSeparator = true;
            // Code for UPDATE clause.
            setClause += entry.Key.ToString() + " = " + sanitize(entry.Value);
            // Code for INSERT clause.
            fieldColumns += entry.Key.ToString();
            fieldValues += sanitize(entry.Value);
        }
        String ExistBegin = "IF EXISTS ( SELECT * FROM " + tableName + " WHERE " + existWhereClause + " ) ";
        String Update = "UPDATE " + tableName + " SET " + setClause + " WHERE " + updateWhereClause;
        String ExistBetween = " ELSE ";
        String Insert = "INSERT INTO " + tableName + " ( " + fieldColumns + " ) VALUES ( " + fieldValues + " ) ";
        String ExistEnd = ";";
        dynamicSqlStatement(ExistBegin + Update + ExistBetween + Insert + ExistEnd, DatabaseCommon.enumSqlDataReturn.None);
        return true;
    }
    /// <summary>Delete row from table using dynamic SQL.</summary>
    public bool dynamicSqlDelete(String tableName, String whereClause) {
        if(!ApplicationCommon.isEmpty(whereClause)) {
            whereClause = " WHERE " + whereClause;
        }
        dynamicSqlStatement("DELETE FROM " + tableName + whereClause + ";", DatabaseCommon.enumSqlDataReturn.None);
        return true;
    }
    /// <summary>Create table using dynamic SQL.</summary>
    /// <remarks>
    /// Statement Example: CREATE TABLE tablename ( column_id integer PRIMARY KEY, column1 varchar(30) NOT NULL, column2 varchar(30) );
    /// ht.Add("column_id", "int PRIMARY KEY");
    /// ht.Add("column1", "varchar(30) NOT NULL");
    /// ht.Add("column2", "datetime");
    /// dc.dynamicSqlCreateTable(ht, "tablename");
    /// </remarks>
    public bool dynamicSqlCreateTable(System.Collections.IDictionary parameters, String tableName) {
        String setClause = String.Empty;
        bool hasSeparator = false;

        foreach(System.Collections.DictionaryEntry entry in parameters) {
            // Separated by a comma for next object.
            if(hasSeparator) {
                setClause += ", ";
            }
            hasSeparator = true;
            setClause += entry.Key.ToString() + " " + entry.Value.ToString();
        }
        dynamicSqlStatement("CREATE TABLE " + tableName + " ( " + setClause + " );", DatabaseCommon.enumSqlDataReturn.None);
        return true;
    }
    /// <summary>Truncate table (clear data in table) using dynamic SQL.</summary>
    public bool dynamicSqlTruncateTable(String tableName) {
        dynamicSqlStatement("TRUNCATE TABLE " + tableName + ";", DatabaseCommon.enumSqlDataReturn.None);
        return true;
    }

#endregion

#region Stored Procedure

    /// <summary>Run stored procedure.</summary>
    public Object runStoredProcedure(String spName, System.Collections.IDictionary spParams, DatabaseCommon.enumSqlDataReturn returnType) {
        // Create the connection object.
        System.Data.SqlClient.SqlConnection sqlCon = (System.Data.SqlClient.SqlConnection)getConnection();
        // Create the command object.
        System.Data.SqlClient.SqlCommand sqlCmd = (System.Data.SqlClient.SqlCommand)getCommand();
        sqlCmd.Connection = sqlCon;
        sqlCmd.CommandText = spName;
        sqlCmd.CommandType = CommandType.StoredProcedure;
        // Create an adapter to return data.
        System.Data.SqlClient.SqlDataAdapter sqlApt = (System.Data.SqlClient.SqlDataAdapter)getAdapter(sqlCmd);
        // Fill the SQL command object with the parameters.
        if(spParams != null) {
            System.Collections.IDictionaryEnumerator item = spParams.GetEnumerator();
            while(item.MoveNext()) {
                sqlCmd.Parameters.AddWithValue((String)item.Key, item.Value);
            }
        }
        // Open SQL connection.
        sqlCon.Open();
        // Perform query.
        return returnData(returnType, sqlApt, sqlCmd, sqlCon);
    }

#endregion

#region Utilities

    /// <summary>Clean (sanitize) value for use in a SQL statement before performing a query.</summary>
    public static String sanitize(Object value) {
        if(value is DBNull) {
            return "NULL";
        } else if(value is String) {
            return "'" + sanitizeString((String)value) + "'";
        } else if(value is Char) {
            return "'" + sanitizeString((String)value) + "'";
        } else if(value is DateTime) {
            return "'" + getSqlDate((DateTime)value) + "'";
        } else if(value is Boolean) {
            return System.Convert.ToInt32(value).ToString();
        } else {
            return value.ToString();
        }
    }
    /// <summary>Get clean string for SQL.</summary>
    public static String sanitizeString(String s) {
        return escapeString(trimAll(s));
    }
    /// <summary>Get escaped string for SQL.</summary>
    /// <remarks>Alternatively, use SQL parameters.</remarks>
    public static String escapeString(String s) {
        if(ApplicationCommon.isEmpty(s)) {return s;}
        if(s.IndexOf("'") > -1) {           
            //s = s.Replace("'", @"\'"); // MySQL
            s = s.Replace("'", "''"); // Microsoft SQL Server
        }
        return s;
    }
    /// <summary>Remove all leading, trailing, and extra spaces from string.</summary>
    public static String trimAll(String s) {
        if(ApplicationCommon.isEmpty(s)) {return s;}
        s = s.Trim();
        s = System.Text.RegularExpressions.Regex.Replace(s, "\\s{2,}", " ");
        return s;
    }
    /// <summary>Convert rich text to database (Encode).</summary>
    public String convertTextRichToDatabase(String msgClient) {
        return msgClient;
    }
    /// <summary>Convert rich text from database (Decode). </summary>
    public String convertTextRichFromDatabase(String msgDatabase) {
        return ApplicationCommon.convertEncodedHtmlToValidHtml(HttpUtility.HtmlEncode(msgDatabase));
    }
    /// <summary>Convert datatable to hashtable.</summary>
    /// <remarks>http://networkprogramming.spaces.live.com/blog/cns!D79966C0BAAE2C7D!370.entry</remarks>
    public static System.Collections.Hashtable convertDataTableToHashtable(DataTable dt, String fieldKey, String fieldValue) {
        System.Collections.Hashtable ht = new System.Collections.Hashtable();
        foreach(DataRow dr in dt.Rows) {
            ht.Add(dr[fieldKey].ToString(), dr[fieldValue].ToString());
        }
        return ht;
    }
    /// <summary>Truncate (resize) datatable.</summary>
    /// <remarks>http://dotnetslackers.com/DataSet/re-63088_Limit_Rows_In_DataTable_or_DataSet.aspx</remarks>
    public static DataTable truncateDataTable(DataTable source, int limit) {
        if(source.Rows.Count <= limit) {return source;}
        try {
            DataTable myTable = source.Clone(); // in standard RSS, the feed items are here
            DataRow[] myRows = source.Select();
            for(int i = 0;i < limit;i++) {
                if(i < myRows.Length) {
                    myTable.ImportRow(myRows[i]);
                    myTable.AcceptChanges();
                }
            }
            return myTable;
        } catch {
            return new DataTable();
        }
    }
    /// <summary>Parse arguments to a valid SQL statement.</summary>
    public String parseArguments(System.Data.InternalDataCollectionBase args) {
        String s = String.Empty;
        bool comma = true;

        foreach(Object o in args) {
            if(!comma) s += (", ");
            s += sanitize(o);
            comma = false;
        }
        return s;
    }
    /// <summary>Get string of collection.</summary>
    public static String getCollectionString(System.Collections.IDictionary parameters, String before, String between, String after, DatabaseCommon.enumDictionarySelect mode) {
        String s = String.Empty;
        int i = 0;

        foreach(System.Collections.DictionaryEntry entry in parameters) {
            if(mode == DatabaseCommon.enumDictionarySelect.Key) {
                s += before + entry.Key.ToString() + after;
            } else if(mode == DatabaseCommon.enumDictionarySelect.Value) {
                s += before + entry.Value.ToString() + after;
            } else {
                s += before + entry.Key.ToString() + between + entry.Value.ToString() + after;
            }
            // Separated by a comma for next object.
            if(parameters.Count != i + 1) {
                s += ", ";
            }
            i++;
        }
        return s;
    }
    /// <summary>To string dataset.</summary>
    public static String toString(DataSet ds) {
        String s = String.Empty;

        // DataSet Name
        s += ds.DataSetName;
        s += "<br/>";
        foreach(DataTable tbl in ds.Tables) {
            // Table Name
            s += tbl.TableName;
            s += "<br/>";
            // Header
            foreach(DataColumn col in tbl.Columns) {
                s += col.ColumnName + ", ";
            }
            s += "<br/>";
            // Body
            foreach(DataRow row in tbl.Rows) {
                foreach(DataColumn col in tbl.Columns) {
                    s += row[col].ToString() + ", ";
                }
                s += "<br/>";
            }
        }
        // Statistics
        s += "totalTables: " + ds.Tables.Count;
        s += "<br/>";
        s += "totalTableRows: " + ds.Tables[0].Rows.Count;
        s += "<br/>";
        s += "totalTableColumns: " + ds.Tables[0].Columns.Count;
        s += "<br/>";
        return s;
    }
    /// <summary>To string datatable's column.</summary>
    public static String toString(DataTable dt, String columnName, String separator) {
        String s = String.Empty;
        bool hasSeparator = false;
        foreach(DataRow row in dt.Rows) {
            if(!ApplicationCommon.isEmpty(row[columnName].ToString())) {
                if(hasSeparator) {
                    s += separator;
                }
                s += row[columnName].ToString();
                hasSeparator = true;
            }
        }
        return s;
    }
    /// <summary>To string datatable's column.</summary>
    public static String toString(DataTable dt, String columnName) {
        return toString(dt, columnName, ", ");
    }
    /// <summary>To JSON from dataset.</summary>
    public static String toJson(DataSet ds) {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("{"); // BEGIN: Object
        int tblIndex = 0;
        foreach(DataTable tbl in ds.Tables) {
            // Table Name
            sb.Append("\"" + tbl.TableName + "\":"); // Member
            sb.Append("["); // BEGIN: Array
            int rowIndex = 0;
            foreach(System.Data.DataRow row in tbl.Rows) {
                sb.Append("{"); // BEGIN: Object
                int colIndex = 0;
                foreach(System.Data.DataColumn col in tbl.Columns) {
                    // Header
                    sb.Append("\"" + col.ColumnName + "\":"); // Name
                    // Body
                    sb.Append(getJsonObject(row[col]));
                    // Separated by a comma for next pair.
                    if(tbl.Columns.Count != colIndex + 1) {
                        sb.Append(", ");
                    }
                    colIndex++;
                }
                sb.Append("}"); // END: Object
                // Separated by a comma for next object.
                if(tbl.Rows.Count != rowIndex + 1) {
                    sb.Append(", ");
                }
                rowIndex++;
            }
            sb.Append("]"); // END: Array
            // Separated by a comma for next object.
            if(ds.Tables.Count != tblIndex + 1) {
                sb.Append(", ");
            }
            tblIndex++;
        }
        sb.Append("}"); // END: Object
        return sb.ToString();
    }
    /// <summary>To JSON from datatable.</summary>
    public static String toJson(DataTable dt) {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("{"); // BEGIN: Object
        // Table Name
        if(ApplicationCommon.isEmpty(dt.TableName)) {
            dt.TableName = "table1";
        }
        sb.Append("\"" + dt.TableName + "\":"); // Member
        sb.Append("["); // BEGIN: Array
        int rowIndex = 0;
        foreach(System.Data.DataRow row in dt.Rows) {
            sb.Append("{"); // BEGIN: Object
            int colIndex = 0;
            foreach(System.Data.DataColumn col in dt.Columns) {
                // Header
                sb.Append("\"" + col.ColumnName + "\":"); // Name
                // Body
                sb.Append(getJsonObject(row[col]));
                // Separated by a comma for next pair.
                if(dt.Columns.Count != colIndex + 1) {
                    sb.Append(", ");
                }
                colIndex++;
            }
            sb.Append("}"); // END: Object
            // Separated by a comma for next object.
            if(dt.Rows.Count != rowIndex + 1) {
                sb.Append(", ");
            }
            rowIndex++;
        }
        sb.Append("]"); // END: Array
        sb.Append("}"); // END: Object
        return sb.ToString();
    }
    /// <summary>To custom JSON from datatable.</summary>
    public static String toJsonCustom(DataTable dt, int intPage, int intRowStart, int intRowEnd, String sortColumnName, String sortOrder) {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        // Sort datatable.
        System.Data.DataRow[] rows = dt.Select(String.Empty, sortColumnName + " " + sortOrder);
        // Safety datarow end.
        if(intRowEnd > rows.Length) {
            intRowEnd = rows.Length;
        }

        sb.Append("{"); // BEGIN: Object
        sb.Append("\"rows\":");
        sb.Append("{"); // BEGIN: Object
        sb.Append("\"page\":" + intPage + ", ");
        sb.Append("\"total\":" + rows.Length + ", ");
        sb.Append("\"row\":"); // Member
        sb.Append("["); // BEGIN: Array
        bool hasSeparator1 = false;
        for(int rowIndex = intRowStart;rowIndex < intRowEnd;rowIndex++) {
            // Separated by a comma for next object.
            if(hasSeparator1) {
                sb.Append(", ");
            }
            hasSeparator1 = true;
            sb.Append("{"); // BEGIN: Object
            sb.Append("\"@id\":" + getJsonObject(rows[rowIndex][0]) + ", ");
            sb.Append("\"cell\":");
            sb.Append("["); // BEGIN: Array
            bool hasSeparator2 = false;
            for(int colIndex = 0;colIndex < dt.Columns.Count;colIndex++) {
                // Separated by a comma for next pair.
                if(hasSeparator2) {
                    sb.Append(", ");
                }
                hasSeparator2 = true;
                // Body
                sb.Append(getJsonObject(rows[rowIndex][colIndex]));
            }
            sb.Append("]"); // END: Array
            sb.Append("}"); // END: Object
        }
        sb.Append("]"); // END: Array
        sb.Append("}"); // END: Object
        sb.Append("}"); // END: Object
        return sb.ToString();
    }
    /// <summary>Get string of JSON object.</summary>
    /// <remarks>http://www.blog.activa.be/2007/08/12/WritingAFullJSONSerializerIn100LinesOfCCode.aspx</remarks>
    public static String getJsonObject(Object obj) {
        if(obj is DBNull) {
            return "null";
        } else if(obj is sbyte || obj is byte || obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is decimal || obj is double || obj is float) {
            return Convert.ToString(obj, System.Globalization.NumberFormatInfo.InvariantInfo);
        } else if(obj is bool) {
            return obj.ToString().ToLower();
        } else if(obj is char || obj is Enum || obj is Guid) {
            return "" + obj;
        } else if(obj is DateTime) {
            return "\"" + getJsonDate((DateTime)obj) + "\"";
        } else if(obj is String) {
            return "\"" + obj.ToString() + "\"";
        } else {
            return obj.ToString();
        }
    }
    /// <summary>Get date string for JSON object.</summary>
    public static String getJsonDate(DateTime date) {
        return date.Year.ToString() + "/" + date.Month.ToString() + "/" + date.Day.ToString();
    } 
    /// <summary>Append JSON data statistics.</summary>
    public static String appendJsonDataStatistics(String name, int available, int returned, int position) {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("\"" + name + "\":"); // Member
        sb.Append("{"); // BEGIN: Object
            sb.Append("\"" + "totalAvailable" + "\":"); // Name
            sb.Append(available); // Value
            sb.Append(", ");

            sb.Append("\"" + "totalReturned" + "\":"); // Name
            sb.Append(returned); // Value
            sb.Append(", ");

            sb.Append("\"" + "totalPosition" + "\":"); // Name
            sb.Append(position); // Value
            sb.Append(", ");
        return sb.ToString();
    }

#endregion

}

} // END namespace ent