using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ContestHelper
{
	[Activity (Label = "Contest Helper", MainLauncher = true, Icon="@drawable/Icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);



			// Get our button from the layout resource,
			// and attach an event to it
			
			Button choosedate = FindViewById<Button>(Resource.Id.Choosedate);
			Button addcontest = FindViewById<Button>(Resource.Id.Addcontest);

			choosedate.Click += (object sender, EventArgs e) => {
				var chosendate = new Intent (this, typeof(chosenDate));

				StartActivity (chosendate);
			};
			addcontest.Click += (object sender, EventArgs e) => {
				var addevent = new Intent (this, typeof(addnewevent));
				StartActivity (addevent);
			};
		}

	}
}



