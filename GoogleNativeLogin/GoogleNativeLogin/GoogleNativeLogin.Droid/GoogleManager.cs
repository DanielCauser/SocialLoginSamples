using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GoogleNativeLogin.Services.Contracts;
using Xamarin.Forms;

namespace GoogleNativeLogin.Droid
{
    public class GoogleManager : Java.Lang.Object, IGoogleManager, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener 
    {
        public static GoogleApiClient _googleApiClient { get; set; }

		public GoogleManager()
		{
			GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
															 .RequestEmail()
															 .Build();

			_googleApiClient = new GoogleApiClient.Builder(((MainActivity)Forms.Context).ApplicationContext)
				.AddConnectionCallbacks(this)
				.AddOnConnectionFailedListener(this)
				.AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
				.AddScope(new Scope(Scopes.Profile))
				.Build();
		}

		public void Login()
        {
			Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
			((MainActivity)Forms.Context).StartActivityForResult(signInIntent, 1);
			_googleApiClient.Connect();
		}

        public void Logout()
        {
			_googleApiClient.Disconnect();
			//Auth.GoogleSignInApi.SignOut(_googleApiClient).SetResultCallback();
		}

        public void OnConnected(Bundle connectionHint)
        {
            
        }

        public void OnConnectionSuspended(int cause)
        {
            
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            
        }
    }
}