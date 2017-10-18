using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using FacebookNativeLogin.Services.Contracts;
using FacebookNativeLogin.Models;

namespace FacebookNativeLogin.iOS
{
	public class FacebookManager : IFacebookManager
	{
		public Action<FacebookUser, string> _onLoginComplete;
		

		public void Login(Action<FacebookUser, string> onLoginComplete)
		{
			_onLoginComplete = onLoginComplete;
			_onLoginComplete?.Invoke(null, "User Loged in!");
		}

		public void Logout()
		{
			_onLoginComplete?.Invoke(null, "User Loged out!");
		}
	}
}