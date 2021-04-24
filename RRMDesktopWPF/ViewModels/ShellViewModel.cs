using System.Threading;
using System.Threading.Tasks;

using Caliburn.Micro;

using RRMDesktopWPF.EventModels;

namespace RRMDesktopWPF.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LoginEvent>
	{
		private readonly IEventAggregator _event;
		private readonly SalesViewModel _salesViewModel;

		public ShellViewModel( SalesViewModel salesViewModel , IEventAggregator eventAggregator )
		{
			_event = eventAggregator;
			_salesViewModel = salesViewModel;

			_event.SubscribeOnUIThread( this );
			ActivateItemAsync( IoC.Get<LoginViewModel>() );
		}

		public Task HandleAsync( LoginEvent message , CancellationToken cancellationToken ) => ActivateItemAsync( _salesViewModel );
	}
}