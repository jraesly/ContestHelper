using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ContestHelper
{
	[Activity (Label = "Second Screen")]            
	public class chosenDate : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

		// Set the view from the "Datelist" layout resource
			SetContentView (Resource.Layout.Datelist);


		}
	}
}