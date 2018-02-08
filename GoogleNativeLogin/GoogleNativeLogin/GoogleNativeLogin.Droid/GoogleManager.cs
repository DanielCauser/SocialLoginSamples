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
using GoogleNativeLogin.Models;
using GoogleNativeLogin.Services.Contracts;
using Xamarin.Forms;

namespace GoogleNativeLogin.Droid
{
	public class GoogleManager : Java.Lang.Object, IGoogleManager, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
	{
		public Action<GoogleUser, string> _onLoginComplete;
		public static GoogleApiClient _googleApiClient { get; set; }
		public static GoogleManager Instance { get; private set; }

		public GoogleManager()
		{
			Instance = this;
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

		public void Login(Action<GoogleUser, string> onLoginComplete)
		{
			_onLoginComplete = onLoginComplete;
			Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
			((MainActivity)Forms.Context).StartActivityForResult(signInIntent, 1);
			_googleApiClient.Connect();
		}

		public void Logout()
		{
			_googleApiClient.Disconnect();
		}

		public void OnAuthCompleted(GoogleSignInResult result)
		{
			if (result.IsSuccess)
			{
				GoogleSignInAccount accountt = result.SignInAccount;
				_onLoginComplete?.Invoke(new GoogleUser()
				{
					Name = accountt.DisplayName,
					Email = accountt.Email,
					Picture = new Uri((accountt.PhotoUrl != null ? $"{accountt.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"))
				}, string.Empty);
			}
			else
			{
				_onLoginComplete?.Invoke(null, "An error occured!");
			}
		}

		public void OnConnected(Bundle connectionHint)
		{

		}

		public void OnConnectionSuspended(int cause)
		{
			_onLoginComplete?.Invoke(null, "Canceled!");
		}

		public void OnConnectionFailed(ConnectionResult result)
		{
			_onLoginComplete?.Invoke(null, result.ErrorMessage);
		}
	}
}
