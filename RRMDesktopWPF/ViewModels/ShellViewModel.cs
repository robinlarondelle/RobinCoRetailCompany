using Caliburn.Micro;

namespace RRMDesktopWPF.ViewModels
{
	public class ShellViewModel : Conductor<object>
	{
		public ShellViewModel( LoginViewModel loginViewModel )
		{
			ActivateItemAsync( loginViewModel );
		}
	}
}