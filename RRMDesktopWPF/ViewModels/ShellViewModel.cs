using System.Threading;
using System.Threading.Tasks;

using Caliburn.Micro;

using RRMDesktopWPF.EventModels;
using RRMDesktopWPF.Library.Api;
using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LoginEvent>
	{
		private readonly IEventAggregator _event;
		private readonly SalesViewModel _salesViewModel;
		private readonly ILoggedInUserModel _user;
		private readonly IApiHelper _apiHelper;

		public bool IsLoggedIn => !string.IsNullOrWhiteSpace( _user.AccessToken );

		public ShellViewModel( SalesViewModel salesViewModel , IEventAggregator eventAggregator , ILoggedInUserModel user, IApiHelper apiHelper )
		{
			_event = eventAggregator;
			_salesViewModel = salesViewModel;
			_user = user;
			_apiHelper = apiHelper;

			_event.SubscribeOnUIThread( this );
			ActivateItemAsync( IoC.Get<LoginViewModel>() );
		}

		public async Task ExitApplication() => await TryCloseAsync();
		public void LogOut()
		{
			_user.Clear();
			_apiHelper.LogOffUser();
			ActivateItemAsync( IoC.Get<LoginViewModel>() );
			NotifyOfPropertyChange( () => IsLoggedIn );
		}

		public Task HandleAsync( LoginEvent message , CancellationToken cancellationToken )
		{
			ActivateItemAsync( _salesViewModel );
			NotifyOfPropertyChange( () => IsLoggedIn );
			return null;
		}
	}
}