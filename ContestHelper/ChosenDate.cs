using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ContestHelper
{
	[Activity (Label = "Contest Helper", Icon="@drawable/Icon")]            
	public class chosenDate : Activity
	{
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set the view from the "Datelist" layout resource
			SetContentView (Resource.Layout.Datelist);
			ActionBar.SetHomeButtonEnabled (true);
			ActionBar.SetDisplayHomeAsUpEnabled (true);

		}
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

		}
	}
