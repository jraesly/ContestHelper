using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ContestHelper
{
	//Main activity for app launching
	[Activity (Label = "Contest Helper", Icon="@drawable/Icon")]
	public class addnewevent : Activity
	{
		//Database class new object
		Database sqldb;
		//Name, LastName and Age EditText objects for data input
		EditText txtName, txtAge, txtLastName;
		//Message TextView object for displaying data
		TextView shMsg;
		//Add, Edit, Delete and Search ImageButton objects for events handling
		ImageButton imgAdd, imgEdit, imgDelete, imgSearch;
		//ListView object for displaying data from database
		ListView listItems;
		//Launches the Create event for app
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				Finish();
				return true;

			default:
				return base.OnOptionsItemSelected(item);
			}
		}
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			ActionBar.SetHomeButtonEnabled (true);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
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
			txtAge = FindViewById<EditText> (Resource.Id.txtAge);
			txtLastName = FindViewById<EditText> (Resource.Id.txtLastName);
			txtName = FindViewById<EditText> (Resource.Id.txtName);
			//Gets TextView object instances
			shMsg = FindViewById<TextView> (Resource.Id.shMsg);
			//Gets ListView object instance
			listItems = FindViewById<ListView> (Resource.Id.listItems);
			//Sets Database class message property to shMsg TextView instance
			shMsg.Text = sqldb.Message;
			//Creates ImageButton click event for imgAdd, imgEdit, imgDelete and imgSearch
			imgAdd.Click += delegate {
				//Calls function AddRecord for adding a new record
				sqldb.AddRecord (txtName.Text, txtLastName.Text, int.Parse (txtAge.Text));
				shMsg.Text = sqldb.Message;
				txtName.Text = txtAge.Text = txtLastName.Text = "";
				GetCursorView();
			};

			imgEdit.Click += delegate {
				int iId = int.Parse(shMsg.Text);
				//Calls UpdateRecord function for updating an existing record
				sqldb.UpdateRecord (iId, txtName.Text, txtLastName.Text, int.Parse (txtAge.Text));
				shMsg.Text = sqldb.Message;
				txtName.Text = txtAge.Text = txtLastName.Text = "";
				GetCursorView();
			};

			imgDelete.Click += delegate {
				int iId = int.Parse(shMsg.Text);
				//Calls DeleteRecord function for deleting the record associated to id parameter
				sqldb.DeleteRecord (iId);
				shMsg.Text = sqldb.Message;
				txtName.Text = txtAge.Text = txtLastName.Text = "";
				GetCursorView();
			};

			imgSearch.Click += delegate {
				//Calls GetCursorView function for searching all records or single record according to search criteria
				string sqldb_column = "";
				if (txtName.Text.Trim () != "") 
				{
					sqldb_column = "Name";
					GetCursorView (sqldb_column, txtName.Text.Trim ());
				} else
					if (txtLastName.Text.Trim () != "") 
					{
						sqldb_column = "LastName";
						GetCursorView (sqldb_column, txtLastName.Text.Trim ());
					} else
						if (txtAge.Text.Trim () != "") 
						{
							sqldb_column = "Age";
							GetCursorView (sqldb_column, txtAge.Text.Trim ());
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
			TextView shId = e.View.FindViewById<TextView> (Resource.Id.Id_row);
			TextView shName = e.View.FindViewById<TextView> (Resource.Id.Name_row);
			TextView shLastName = e.View.FindViewById<TextView> (Resource.Id.LastName_row);
			TextView shAge = e.View.FindViewById<TextView> (Resource.Id.Age_row);
			//Reads values and sets to EditText object instances
			txtName.Text = shName.Text;
			txtLastName.Text = shLastName.Text;
			txtAge.Text = shAge.Text;
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
				string[] from = new string[] {"_id","Name","LastName","Age" };
				int[] to = new int[] {
					Resource.Id.Id_row,
					Resource.Id.Name_row,
					Resource.Id.LastName_row,
					Resource.Id.Age_row
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
				string[] from = new string[] {"_id","Name","LastName","Age" };
				int[] to = new int[] 
				{
					Resource.Id.Id_row,
					Resource.Id.Name_row,
					Resource.Id.LastName_row,
					Resource.Id.Age_row
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