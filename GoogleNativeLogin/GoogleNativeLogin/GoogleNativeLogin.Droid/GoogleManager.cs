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

namespace GoogleNativeLogin.Droid
{
    public class GoogleManager : Java.Lang.Object, IGoogleManager, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener 
    {
        public static GoogleApiClient _googleApiClient { get; set; }

        Action<GoogleSignInResult> SigninResult; 

        public GoogleManager()
        {
            _googleApiClient.RegisterConnectionCallbacks(this);
        }

        public void Login()
        {
            Auth.GoogleSignInApi.SilentSignIn(_googleApiClient).SetResultCallback<GoogleSignInResult>(SigninResult);
        }

        public void Logout()
        {
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