
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ContestHelper
{
	//Main activity for app launching
	[Activity (Label = "ContestHelper")]
	public class addnewevent : Activity
	{
		//Database class new object
		Database sqldb;
		//Name, LastName and Age EditText objects for data input
		EditText txtFirstname, txtLastname, txtEmail, txtnumber;
		//Message TextView object for displaying data
		TextView shMsg;
		//Add, Edit, Delete and Search ImageButton objects for events handling
		ImageButton imgAdd, imgEdit, imgDelete, imgSearch;
		//ListView object for displaying data from database
		ListView listItems;
		//Launches the Create event for app
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//Set our Main layout as default view
			SetContentView (Resource.Layout.Addnewevent);
			//Initializes new Database class object
			sqldb = new Database("person_db");
			//Gets ImageButton object instances
			imgAdd = FindViewById<ImageButton> (Resource.Id.imgAdd);
			imgDelete = FindViewById<ImageButton> (Resource.Id.imgDelete);
			imgEdit = FindViewById<ImageButton> (Resource.Id.imgEdit);
			imgSearch = FindViewById<ImageButton> (Resource.Id.imgSearch);
			//Gets EditText object instances
			txtnumber = FindViewById<EditText> (Resource.Id.txtnumber);
			txtEmail = FindViewById<EditText> (Resource.Id.txtEmail);
			txtLastname = FindViewById<EditText> (Resource.Id.txtLastname);
			txtFirstname = FindViewById<EditText> (Resource.Id.txtFirstname);
			//Gets TextView object instances
			shMsg = FindViewById<TextView> (Resource.Id.shMsg);
			//Gets ListView object instance
			listItems = FindViewById<ListView> (Resource.Id.listItems);
			//Sets Database class message property to shMsg TextView instance
			shMsg.Text = sqldb.Message;
			//Creates ImageButton click event for imgAdd, imgEdit, imgDelete and imgSearch
			imgAdd.Click += delegate {
				//Calls function AddRecord for adding a new record
				sqldb.AddRecord (txtFirstname.Text, txtLastname.Text, txtEmail.Text, int.Parse (txtnumber.Text));
				shMsg.Text = sqldb.Message;
				txtFirstname.Text = txtLastname.Text = txtEmail.Text = txtnumber.Text = "";
				GetCursorView();
			};

			imgEdit.Click += delegate {
				int iPhone = int.Parse(shMsg.Text);
				//Calls UpdateRecord function for updating an existing record
				sqldb.UpdateRecord (txtFirstname.Text, txtLastname.Text, txtEmail.Text, int.Parse (txtnumber.Text));
				shMsg.Text = sqldb.Message;
				txtFirstname.Text = txtLastname.Text = txtEmail.Text = txtnumber.Text = "";
				GetCursorView();
			};

			imgDelete.Click += delegate {
				int iPhone = int.Parse(shMsg.Text);
				//Calls DeleteRecord function for deleting the record associated to id parameter
				sqldb.DeleteRecord (iPhone);
				shMsg.Text = sqldb.Message;
				txtFirstname.Text = txtLastname.Text = txtEmail.Text = txtnumber.Text = "";
				GetCursorView();
			};

			imgSearch.Click += delegate {
				//Calls GetCursorView function for searching all records or single record according to search criteria
				string sqldb_column = "";
				if (txtFirstname.Text.Trim () != "") 
				{
					sqldb_column = "Firstname";
					GetCursorView (sqldb_column, txtFirstname.Text.Trim ());
				} else
					if (txtLastname.Text.Trim () != "") 
					{
						sqldb_column = "LastName";
						GetCursorView (sqldb_column, txtLastname.Text.Trim ());
					} else
						if (txtnumber.Text.Trim () != "") 
						{
							sqldb_column = "Phone";
							GetCursorView (sqldb_column, txtnumber.Text.Trim ());
						} else 
						{
							GetCursorView ();
							sqldb_column = "All";
						}
				shMsg.Text = "Search " + sqldb_column + ".";
			};
			//Add ItemClick event handler to ListView instance
			listItems.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs> (item_Clicked);
		}
		//Launched when a ListView item is clicked
		void item_Clicked (object sender, AdapterView.ItemClickEventArgs e)
		{
			//Gets TextView object instance from record_view layout
			TextView shId = e.View.FindViewById<TextView> (Resource.Id.Firstname_row);
			TextView shName = e.View.FindViewById<TextView> (Resource.Id.Lastname_row);
			TextView shLastName = e.View.FindViewById<TextView> (Resource.Id.Email_row);
			TextView shAge = e.View.FindViewById<TextView> (Resource.Id.Phone_row);
			//Reads values and sets to EditText object instances
			txtFirstname.Text = shName.Text;
			txtLastname.Text = shLastName.Text;
			txtnumber.Text = shAge.Text;
			//Displays messages for CRUD operations
			shMsg.Text = shId.Text;
		}
		//Gets the cursor view to show all records
		void GetCursorView()
		{
			Android.Database.ICursor sqldb_cursor = sqldb.GetRecordCursor ();
			if (sqldb_cursor != null) 
			{
				sqldb_cursor.MoveToFirst ();
				string[] from = new string[] {"Firstname","Lastname","Email","Phone" };
				int[] to = new int[] {
					Resource.Id.Firstname_row,
					Resource.Id.Lastname_row,
					Resource.Id.Email_row,
					Resource.Id.Phone_row
				};
				//Creates a SimplecursorAdapter for ListView object
				SimpleCursorAdapter sqldb_adapter = new SimpleCursorAdapter (this, Resource.Layout.Recordslayout, sqldb_cursor, from, to);
				listItems.Adapter = sqldb_adapter;
			} 
			else 
			{
				shMsg.Text = sqldb.Message;
			}
		}
		//Gets the cursor view to show records according to search criteria
		void GetCursorView (string sqldb_column, string sqldb_value)
		{
			Android.Database.ICursor sqldb_cursor = sqldb.GetRecordCursor (sqldb_column, sqldb_value);

			if (sqldb_cursor != null) 
			{
				sqldb_cursor.MoveToFirst ();
				string[] from = new string[] {"Firstname","Lastname","Email","Phone" };
				int[] to = new int[] 
				{
					Resource.Id.Firstname_row,
					Resource.Id.Lastname_row,
					Resource.Id.Email_row,
					Resource.Id.Phone_row
				};
				SimpleCursorAdapter sqldb_adapter = new SimpleCursorAdapter (this, Resource.Layout.Recordslayout, sqldb_cursor, from, to);
				listItems.Adapter = sqldb_adapter;
			} 
			else 
			{
				shMsg.Text = sqldb.Message;
			}
		}
	}
}
