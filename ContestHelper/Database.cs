//Required assemblies
using Android.Database.Sqlite;
using System.IO;

namespace ContestHelper
{
	class Database
	{
		//SQLiteDatabase object for database handling
		private SQLiteDatabase sqldb;
		//String for Query handling
		private string sqldb_query;
		//String for Message handling
		private string sqldb_message;
		//Bool to check for database availability
		private bool sqldb_available;
		//Zero argument constructor, initializes a new instance of Database class
		public Database()
		{
			sqldb_message = "";
			sqldb_available = false;
		}
		//One argument constructor, initializes a new instance of Database class with database name parameter
		public Database(string sqldb_name)
		{
			try
			{
				sqldb_message = "";
				sqldb_available = false;
				CreateDatabase(sqldb_name);
			}
			catch (SQLiteException ex) 
			{
				sqldb_message = ex.Message;
			}
		}
		//Gets or sets value depending on database availability
		public bool DatabaseAvailable
		{
			get{ return sqldb_available; }
			set{ sqldb_available = value; }
		}
		//Gets or sets the value for message handling
		public string Message
		{
			get{ return sqldb_message; }
			set{ sqldb_message = value; }
		}
		//Creates a new database which name is given by the parameter
		public void CreateDatabase(string sqldb_name)
		{
			try
			{
				sqldb_message = "";
				string sqldb_location = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				string sqldb_path = Path.Combine(sqldb_location, sqldb_name);
				bool sqldb_exists = File.Exists(sqldb_path);
				if(!sqldb_exists)
				{
					sqldb = SQLiteDatabase.OpenOrCreateDatabase(sqldb_path,null);
					sqldb_query = "CREATE TABLE IF NOT EXISTS MyTable (FirstName TEXT, LastName TEXT, Email BLOB, Phonenumber BLOB);";
					sqldb.ExecSQL(sqldb_query);
					sqldb_message = "Database: " + sqldb_name + " created";
				}
				else
				{
					sqldb = SQLiteDatabase.OpenDatabase(sqldb_path, null, DatabaseOpenFlags.OpenReadwrite);
					sqldb_message = "Database: " + sqldb_name + " opened";
				}
				sqldb_available=true;
			}
			catch(SQLiteException ex) 
			{
				sqldb_message = ex.Message;
			}
		}
		//Adds a new record with the given parameters
		public void AddRecord(string sFirstName, string sLastName, string sEmail, int iPhone)
		{
			try
			{
				sqldb_query = "INSERT INTO MyTable (FirstName, LastName, Email, Phone) VALUES ('" + sFirstName + "','" + sLastName + "', " + sEmail + ", " + iPhone + ");";
				sqldb.ExecSQL(sqldb_query);
				sqldb_message = "Record saved";
			}
			catch(SQLiteException ex) 
			{
				sqldb_message = ex.Message;
			}
		}
		//Updates an existing record with the given parameters depending on id parameter
		public void UpdateRecord(string sFirstName, string sLastName, string sEmail, int iPhone)
		{
			try
			{
				sqldb_query="UPDATE MyTable SET FirstName ='" + sFirstName + "', LastName ='" + sLastName + "', Email ='" + sEmail + "', Phone ='" + iPhone + "';";
				sqldb.ExecSQL(sqldb_query);
				sqldb_message = "Record updated";
			}
			catch(SQLiteException ex)
			{
				sqldb_message = ex.Message;
			}
		}
		//Deletes the record associated to id parameter
		public void DeleteRecord(string sFirstName)
		{
			try
			{
				sqldb_query = "DELETE FROM MyTable WHERE FirstName ='" + sFirstName + "';";
				sqldb.ExecSQL(sqldb_query);
				sqldb_message = "Record Deleted";
			}
			catch(SQLiteException ex) 
			{
				sqldb_message = ex.Message;
			}
		}
		//Searches a record and returns an Android.Database.ICursor cursor
		//Shows all the records from the table
		public Android.Database.ICursor GetRecordCursor()
		{
			Android.Database.ICursor sqldb_cursor = null;
			try
			{
				sqldb_query = "SELECT*FROM MyTable;";
				sqldb_cursor = sqldb.RawQuery(sqldb_query, null);
				if(!(sqldb_cursor != null))
				{
					sqldb_message = "Record not found";
				}
			}
			catch(SQLiteException ex) 
			{
				sqldb_message = ex.Message;
			}
			return sqldb_cursor;
		}
		//Searches a record and returns an Android.Database.ICursor cursor
		//Shows records according to search criteria
		public Android.Database.ICursor GetRecordCursor(string sColumn, string sValue)
		{
			Android.Database.ICursor sqldb_cursor = null;
			try
			{
				sqldb_query = "SELECT*FROM MyTable WHERE " + sColumn + " LIKE '" + sValue + "%';";
				sqldb_cursor = sqldb.RawQuery(sqldb_query, null);
				if(!(sqldb_cursor != null))
				{
					sqldb_message = "Record not found";
				}
			}
			catch(SQLiteException ex) 
			{
				sqldb_message = ex.Message;
			}
			return sqldb_cursor;
		}
	}
}