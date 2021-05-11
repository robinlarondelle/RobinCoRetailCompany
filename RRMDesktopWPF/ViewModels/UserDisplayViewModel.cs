using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Caliburn.Micro;

using RRMDesktopWPF.Library.Api;
using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.ViewModels
{
	public class UserDisplayViewModel : Screen
	{
		private StatusInfoViewModel _status;
		private IWindowManager _window;
		private IUserEndpoint _userEndpoint;
		private BindingList<UserModel> _users;

		public BindingList<UserModel> Users
		{
			get => _users;
			set
			{
				_users = value;
				NotifyOfPropertyChange( () => Users );
			}
		}


		public UserDisplayViewModel(
			StatusInfoViewModel status, 
			IWindowManager window, 
			IUserEndpoint userEndpoint)
		{
			_status = status;
			_window = window;
			_userEndpoint = userEndpoint;
		}

		protected override async void OnViewLoaded( object view )
		{
			base.OnViewLoaded( view );

			try
			{
				await InitializeUsers();
			}
			catch ( Exception ex )
			{
				dynamic settings = new ExpandoObject();
				settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				settings.ResizeMode = ResizeMode.NoResize;
				settings.Title = "System Error";

				if ( ex.Message == "Unauthorized" )
				{
					_status.UpdateMessage( "Unauthorized Access" , "You do not have permission to interact with the Sales form" );
					await _window.ShowDialogAsync( _status , null , settings );
					await TryCloseAsync();
				}
				else
				{
					_status.UpdateMessage( "Exception" , ex.Message );
					await _window.ShowDialogAsync( _status , null , settings );
					await TryCloseAsync();
				}
			}
		}

		private async Task InitializeUsers()
		{
			List<UserModel> userList = await _userEndpoint.GetAllUsers();
			Users = new BindingList<UserModel>( userList );
		}
	}
}
