using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using FacebookNativeLogin.Services.Contracts;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using FacebookNativeLogin.Models;
using Org.Json;

namespace FacebookNativeLogin.Droid
{
	public class FacebookManager : Java.Lang.Object, IFacebookManager, IFacebookCallback, GraphRequest.IGraphJSONObjectCallback
	{
		public Action<FacebookUser, string> _onLoginComplete;
		public ICallbackManager _callbackManager;

		public FacebookManager()
		{
			_callbackManager = CallbackManagerFactory.Create();
			LoginManager.Instance.RegisterCallback(_callbackManager, this);
		}

		public void Login(Action<FacebookUser, string> onLoginComplete)
		{
			_onLoginComplete = onLoginComplete;
			LoginManager.Instance.SetLoginBehavior(LoginBehavior.NativeWithFallback);
			LoginManager.Instance.LogInWithReadPermissions(Xamarin.Forms.Forms.Context as Activity, new List<string> { "public_profile", "email" });
		}

		public void Logout()
		{
			LoginManager.Instance.LogOut();
		}

		#region IFacebookCallback
		public void OnSuccess(Java.Lang.Object result)
		{
			var n = result as LoginResult;
			if (n != null)
			{
				var request = GraphRequest.NewMeRequest(n.AccessToken, this);
				var bundle = new Android.OS.Bundle();
				bundle.PutString("fields", "id, first_name, email, last_name, picture.width(500).height(500)");
				request.Parameters = bundle;
				request.ExecuteAsync();
			}
		}

		public void OnCancel()
		{
			_onLoginComplete?.Invoke(null, "Canceled!");
		}

		public void OnError(FacebookException error)
		{
			_onLoginComplete?.Invoke(null, error.Message);
		}
		public void OnCompleted(JSONObject p0, GraphResponse p1)
		{
			var id = string.Empty;
			var first_name = string.Empty;
			var email = string.Empty;
			var last_name = string.Empty;
			var pictureUrl = string.Empty;

			if (p0.Has("id"))
				id = p0.GetString("id");

			if (p0.Has("first_name"))
				first_name = p0.GetString("first_name");

			if (p0.Has("email"))
				email = p0.GetString("email");

			if (p0.Has("last_name"))
				last_name = p0.GetString("last_name");

			if (p0.Has("picture"))
			{
				var p2 = p0.GetJSONObject("picture");
				if (p2.Has("data"))
				{
					var p3 = p2.GetJSONObject("data");
					if (p3.Has("url"))
					{
						pictureUrl = p3.GetString("url");
					}
				}
			}

			_onLoginComplete?.Invoke(new FacebookUser(id, AccessToken.CurrentAccessToken.Token, first_name, first_name, email, pictureUrl), string.Empty);
		}
		#endregion
	}
}