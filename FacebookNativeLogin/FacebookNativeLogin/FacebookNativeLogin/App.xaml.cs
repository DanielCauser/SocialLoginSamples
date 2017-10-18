using Prism.Unity;
using FacebookNativeLogin.Views;
using Xamarin.Forms;

namespace FacebookNativeLogin
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

		protected override void OnInitialized()
		{
			InitializeComponent();

			NavigationService.NavigateAsync("NavigationPage/MainPage?title=Facebook Native Login");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<NavigationPage>();
			Container.RegisterTypeForNavigation<MainPage>();
		}
	}
}
