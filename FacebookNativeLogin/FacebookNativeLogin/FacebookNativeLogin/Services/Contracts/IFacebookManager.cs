using FacebookNativeLogin.Models;
using System;

namespace FacebookNativeLogin.Services.Contracts
{
	public interface IFacebookManager
	{				
		void Login(Action<FacebookUser, string> onLoginComplete);

		void Logout();
	}
}
