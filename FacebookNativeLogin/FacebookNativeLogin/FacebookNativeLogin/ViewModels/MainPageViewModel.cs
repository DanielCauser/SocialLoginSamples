using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using FacebookNativeLogin.Services.Contracts;
using Prism.Services;
using FacebookNativeLogin.Models;

namespace FacebookNativeLogin.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		private readonly IFacebookManager _facebookManager;
		private readonly IPageDialogService _dialogService;
		
		public DelegateCommand FacebookLoginCommand { get; set; }
		public DelegateCommand FacebookLogoutCommand { get; set; }

		private FacebookUser _facebookUser;

		public FacebookUser FacebookUser
		{
			get { return _facebookUser; }
			set { SetProperty(ref _facebookUser, value); }
		}

		private string _title;

		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		private bool _isLogedIn;

		public bool IsLogedIn
		{
			get { return _isLogedIn; }
			set { SetProperty(ref _isLogedIn, value); }
		}

		public MainPageViewModel(IFacebookManager facebookManager, IPageDialogService dialogService)
		{
			_facebookManager = facebookManager;
			_dialogService = dialogService;

			IsLogedIn = false;
			FacebookLoginCommand = new DelegateCommand(FacebookLogin);
			FacebookLogoutCommand = new DelegateCommand(FacebookLogout);
		}

		private void FacebookLogout()
		{
			_facebookManager.Logout();
			IsLogedIn = false;
		}

		private void FacebookLogin()
		{
			_facebookManager.Login(OnLoginComplete);
		}

		private void OnLoginComplete(FacebookUser facebookUser, string message)
		{
			if (facebookUser != null)
			{
				FacebookUser = facebookUser;
				IsLogedIn = true;
			}
			else
			{
				_dialogService.DisplayAlertAsync("Error", message, "Ok");
			}
		}
		
		public void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{

		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("title"))
				Title = (string)parameters["title"];
		}
	}
}
