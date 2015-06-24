using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Widget;
using meet2play.DataObjects;
using Microsoft.WindowsAzure.MobileServices;
using Activity = Android.App.Activity;
using Location = Android.Locations.Location;
using Uri = Android.Net.Uri;

namespace meet2play
{
	[Activity(Label = "meet2play", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		LocationManager _locationManager;
		//Auth0User _user;

		Location _location;

        private MobileServiceUser _user;

        public static MobileServiceClient MobileService = new MobileServiceClient(
            //"http://10.71.34.1:58705/",
            "https://m2p.azure-mobile.net/",
            "RxtyTIAceTWhtMUblGfudEWaGwghjT29"
        );

	    private IMobileServiceTable<DataObjects.Activity> _activitiesTable;
		private IMobileServiceTable<Profile> _profileTable;
		private Profile _profile;

		protected async override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Console.WriteLine("*****************oncreate*****************");
			SetContentView(Resource.Layout.Main);
           
		    await Authenticate();

		    _activitiesTable = MobileService.GetTable<DataObjects.Activity>();
			_profileTable = MobileService.GetTable<Profile>();
				

			_locationManager = GetSystemService(LocationService) as LocationManager;

			FindViewById<Button>(Resource.Id.btnMap).Click += OpenMap;

		    RefreshActivities();
			RefreshProfile();
			var a = 1;
		}

	    private async void RefreshActivities()
	    {
            var tList = await _activitiesTable.ReadAsync();
            var list = tList.ToList();
            SetLocationResult(string.Join(", ", list.Select(a => a.Name).ToList()));
	    }

		private async void RefreshProfile()
		{
			_profile = await _profileTable.LookupAsync(_user.UserId);	
		}

	    private void OpenMap(object sender, EventArgs e)
	    {
	        var geoUri = Uri.Parse(string.Format("geo:{0},{1}", _location.Latitude, _location.Longitude));

	        var mapIntent = new Intent(Intent.ActionView, geoUri);
	        StartActivity(mapIntent);
	    }

		protected async override void OnResume()
		{
			base.OnResume();
			Console.WriteLine("*****************onresume*****************");

			/*var provider = locationManager.GetBestProvider(new Criteria(){
				Accuracy = Accuracy.Coarse,
				PowerRequirement = Power.Medium
			}, true);

			if (provider != null) {
				//location = locationManager.GetLastKnownLocation(provider);
				//SetLocationResult("Location: " + location.Latitude + " " + location.Longitude);


			} else {
				SetTextResult("No Location Providers");
			}*/
		}

        private async Task Authenticate()
		{
			try
			{
				_user = await MobileService.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);

				SetTextResult(string.Format("you are now logged in - {0}", _user.UserId));
			} 
			catch (Exception ex)
			{
				SetTextResult("Authentication failed");
			}
		}

		private void SetTextResult(string text)
		{
			this.FindViewById<TextView>(Resource.Id.txtResult).Text = text;
		}

		private void SetLocationResult(string text)
		{
			this.FindViewById<TextView>(Resource.Id.txtLocation).Text = text;
		}
	}
}


