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

namespace GoogleNativeLogin.Droid
{
	[Activity(Label = "GoogleNativeLogin", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.tabs;
			ToolbarResource = Resource.Layout.toolbar;

			base.OnCreate(bundle);

            //GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
            //                                                 .RequestEmail()
            //                                                 .Build();

			global::Xamarin.Forms.Forms.Init(this, bundle);

            DependencyService.Register<IGoogleManager, GoogleManager>();
			LoadApplication(new App(new AndroidInitializer()));
		}
	}

	public class AndroidInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IUnityContainer container)
		{

		}
	}
}

