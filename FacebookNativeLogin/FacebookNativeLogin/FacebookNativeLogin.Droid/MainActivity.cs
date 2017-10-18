using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using FacebookNativeLogin.Services.Contracts;
using Xamarin.Forms;
using Android.Content;

namespace FacebookNativeLogin.Droid
{
	[Activity(Label = "FacebookNativeLogin", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.tabs;
			ToolbarResource = Resource.Layout.toolbar;

			base.OnCreate(bundle);
			FacebookSdk.SdkInitialize(this);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			DependencyService.Register<IFacebookManager, FacebookManager>();
			
			LoadApplication(new App(new AndroidInitializer()));
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			var manager = DependencyService.Get<IFacebookManager>();
			if (manager != null)
			{
				(manager as FacebookManager)._callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
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

