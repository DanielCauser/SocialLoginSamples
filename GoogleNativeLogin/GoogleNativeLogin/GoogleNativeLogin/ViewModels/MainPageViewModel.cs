using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using GoogleNativeLogin.Services.Contracts;
using Prism.Services;
using GoogleNativeLogin.Models;

namespace GoogleNativeLogin.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
        private readonly IGoogleManager _googleManager;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand GoogleLoginCommand { get; set; }
        public DelegateCommand GoogleLogoutCommand { get; set; }

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

        private GoogleUser _googleUser;

        public GoogleUser GoogleUser
        {
            get { return _googleUser; }
            set { SetProperty(ref _googleUser, value); }
        }

        public MainPageViewModel(IGoogleManager googleManager, IPageDialogService dialogService)
		{
            _googleManager = googleManager;
            _dialogService = dialogService;

            IsLogedIn = false;
            GoogleLoginCommand = new DelegateCommand(GoogleLogin);
            GoogleLogoutCommand = new DelegateCommand(GoogleLogout);
		}

        private void GoogleLogout()
        {
            _googleManager.Logout();
            IsLogedIn = false;
        }

        private void GoogleLogin()
        {
            _googleManager.Login(OnLoginComplete);

        }

        private void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                GoogleUser = googleUser;
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
				Title = (string)parameters["title"] + " and Prism";
		}
	}
}
