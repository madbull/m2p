using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Widget;
using Auth0.SDK;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Environment = System.Environment;
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

	    private IMobileServiceTable<DataObjects.Activity> _activities;

	    const string LocalDbFilename = "localstore.db";

		protected async override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Console.WriteLine("*****************oncreate*****************");
			SetContentView(Resource.Layout.Main);
           
			//await EnsureUserIsAuthenticated();

		    //await InitLocalStoreAsync();
            //CurrentPlatform.Init();

		    await Authenticate();

		    _activities = MobileService.GetTable<DataObjects.Activity>();
		    //await table.PullAsync("allActivities",table.CreateQuery());
            
		    


            

			_locationManager = GetSystemService(LocationService) as LocationManager;

			FindViewById<Button>(Resource.Id.btnMap).Click += OpenMap;

		    RefreshActivities();
		}

	    private async void RefreshActivities()
	    {
            var tList = await _activities.ReadAsync();
            var list = tList.ToList();
            //		    var list = await _activities.Select(a => a).ToListAsync().ConfigureAwait(false);
            SetLocationResult(string.Join(", ", list.Select(a => a.Name).ToList()));
	    }

	    private void OpenMap(object sender, EventArgs e)
	    {
	        var geoUri = Uri.Parse(string.Format("geo:{0},{1}", _location.Latitude, _location.Longitude));

	        var mapIntent = new Intent(Intent.ActionView, geoUri);
	        StartActivity(mapIntent);
	    }

	    private async Task InitLocalStoreAsync()
        {
            // new code to initialize the SQLite store
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), LocalDbFilename);

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<DataObjects.Activity>();

            // Uses the default conflict handler, which fails on conflict
            // To use a different conflict handler, pass a parameter to InitializeAsync. For more details, see http://go.microsoft.com/fwlink/?LinkId=521416
            await MobileService.SyncContext.InitializeAsync(store);
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

		/*async Task EnsureUserIsAuthenticated()
		{
			try {
				var auth0 = new Auth0Client(
					            "m2p.eu.auth0.com",
					            "UsAmBBUVXHJevNFlD6xFRrLP1O0y0sqp");

				_user = await auth0.LoginAsync(this);
				SetTextResult("Witaj " + _user.Profile["nickname"]);
				// get facebook/google/twitter/etc access token => user.Profile["identities"][0]["access_token"]
			} catch (AggregateException e) {
				SetTextResult(e.Flatten().Message);
			} catch (Exception e) {
				SetTextResult(e.Message);
			}
		}*/

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


