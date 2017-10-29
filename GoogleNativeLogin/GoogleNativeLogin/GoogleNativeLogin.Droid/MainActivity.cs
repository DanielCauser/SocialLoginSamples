using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;
using Android.Gms.Auth.Api.SignIn;
using GoogleNativeLogin.Services.Contracts;
using Xamarin.Forms;
using Android.Gms.Common.Apis;
using static Android.Views.View;
using Android.Gms.Common;
using Android.Gms.Auth.Api;
using Android.Content;

namespace GoogleNativeLogin.Droid
{
    [Activity(Label = "GoogleNativeLogin", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public void LaunchIntent(Intent intent)
        {
            StartActivityForResult(intent, 1);
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                                             .RequestEmail()
                                                             .Build();

            GoogleNativeLogin.Droid.GoogleManager._googleApiClient = new GoogleApiClient.Builder(this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            DependencyService.Register<IGoogleManager, GoogleManager>();

            LoadApplication(new App(new AndroidInitializer()));
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                //HandleSignInResult(result);
            }
        }
    }

	public class AndroidInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IUnityContainer container)
		{

		}
	}
}

