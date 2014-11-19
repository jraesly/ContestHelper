﻿
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
	[Activity (Label = "Contest Helper", MainLauncher = true, Icon="@drawable/Icon")]

	public class DatePicker : Activity
	{

	private TextView dateDisplay;
	private Button pickDate;
	private DateTime date;

	const int DATE_DIALOG_ID = 0;
	
	protected override void OnCreate (Bundle savedInstanceState)
	{
		base.OnCreate (savedInstanceState);
		SetContentView (Resource.Layout.Main);

		// capture our View elements
		dateDisplay = FindViewById<TextView> (Resource.Id.dateDisplay);
		pickDate = FindViewById<Button> (Resource.Id.pickDate);

		// add a click event handler to the button
		pickDate.Click += delegate { ShowDialog (DATE_DIALOG_ID); };

		// get the current date
		date = DateTime.Today;

		// display the current date (this method is below)
		UpdateDisplay ();
	}
	
		// updates the date in the TextView
		private void UpdateDisplay ()
		{
			dateDisplay.Text = date.ToString ("D");
		}

		// the event received when the user "sets" the date in the dialog
		void OnDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			this.date = e.Date;
			UpdateDisplay ();
		}

		protected override Dialog OnCreateDialog (int id)
		{
			switch (id) {
			case DATE_DIALOG_ID:
				return new DatePickerDialog (this, OnDateSet, date.Year, date.Month - 1, date.Day); 
			}
			return null;
		}

	}
}
			

	



