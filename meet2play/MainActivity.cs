using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Auth0.SDK;
using Android.Locations;
using System.Threading.Tasks;

namespace meet2play
{
	[Activity(Label = "meet2play", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		LocationManager locationManager;
		Auth0User user;

		Location location;

		protected async override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Console.WriteLine("*****************oncreate*****************");
			SetContentView(Resource.Layout.Main);
           
			await EnsureUserIsAuthenticated();

			locationManager = GetSystemService(Context.LocationService) as LocationManager;

			FindViewById<Button>(Resource.Id.btnMap).Click += (sender, e) => {
				var geoUri = Android.Net.Uri.Parse (string.Format( "geo:{0},{1}", location.Latitude, location.Longitude));

				var mapIntent = new Intent (Intent.ActionView, geoUri);
				StartActivity (mapIntent);
			};
		}

		protected override void OnResume()
		{
			base.OnResume();
			Console.WriteLine("*****************onresume*****************");

			var provider = locationManager.GetBestProvider(new Criteria(){
				Accuracy = Accuracy.Coarse,
				PowerRequirement = Power.Medium
			}, true);

			if (provider != null) {
				//location = locationManager.GetLastKnownLocation(provider);
				//SetLocationResult("Location: " + location.Latitude + " " + location.Longitude);


			} else {
				SetTextResult("No Location Providers");
			}
		}

		async Task EnsureUserIsAuthenticated()
		{
			try {
				var auth0 = new Auth0Client(
					            "m2p.eu.auth0.com",
					            "UsAmBBUVXHJevNFlD6xFRrLP1O0y0sqp");

				user = await auth0.LoginAsync(this);
				SetTextResult("Witaj " + user.Profile["nickname"]);
				// get facebook/google/twitter/etc access token => user.Profile["identities"][0]["access_token"]
			} catch (AggregateException e) {
				SetTextResult(e.Flatten().Message);
			} catch (Exception e) {
				SetTextResult(e.Message);
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


