using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using GoogleNativeLogin.Services.Contracts;
using Prism.Services;

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
            _googleManager.Login();
            IsLogedIn = true;
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
