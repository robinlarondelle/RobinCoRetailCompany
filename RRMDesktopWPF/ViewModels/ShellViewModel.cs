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
		private readonly SimpleContainer _simpleContainer;

		public ShellViewModel( SalesViewModel salesViewModel , IEventAggregator eventAggregator , SimpleContainer simpleContainer )
		{
			_event = eventAggregator;
			_salesViewModel = salesViewModel;
			_simpleContainer = simpleContainer;

			_event.SubscribeOnUIThread( this );
			ActivateItemAsync( _simpleContainer.GetInstance<LoginViewModel>() );
		}

		public Task HandleAsync( LoginEvent message , CancellationToken cancellationToken ) => ActivateItemAsync( _salesViewModel, cancellationToken );
	}
}