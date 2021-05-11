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


		private UserModel _selectedUser;
		public UserModel SelectedUser
		{
			get => _selectedUser;
			set
			{
				_selectedUser = value;
				SelectedUserName = value.Email;
				UserRoles = new BindingList<string>( value.Roles.Select( x => x.Value ).ToList() );
				_ = InitializeRoles();
				NotifyOfPropertyChange( () => SelectedUser );
			}
		}


		private string _selectedUserName;
		public string SelectedUserName
		{
			get => _selectedUserName;
			set
			{
				_selectedUserName = value;
				NotifyOfPropertyChange( () => SelectedUserName );
			}
		}


		private BindingList<string> _userRoles = new BindingList<string>();
		public BindingList<string> UserRoles
		{
			get => _userRoles;
			set
			{
				_userRoles = value;
				NotifyOfPropertyChange( () => UserRoles );
			}
		}


		private BindingList<string> _availableRoles = new BindingList<string>();
		public BindingList<string> AvailableRoles
		{
			get => _availableRoles;
			set
			{
				_availableRoles = value;
				NotifyOfPropertyChange( () => AvailableRoles );
			}
		}


		private string _selectedUserRole;
		public string SelectedUserRole
		{
			get => _selectedUserRole;
			set
			{
				_selectedUserRole = value;
				NotifyOfPropertyChange( () => SelectedUserRole );
			}
		}


		private string _selectedAvailableRole;
		public string SelectedAvailableRole
		{
			get => _selectedAvailableRole;
			set
			{
				_selectedAvailableRole = value;
				NotifyOfPropertyChange( () => SelectedAvailableRole );
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

		private async Task InitializeRoles()
		{
			var roles = await _userEndpoint.GetAllRoles();

			foreach(var role in roles)
			{
				if (UserRoles.IndexOf(role.Value) < 0)
				{
					AvailableRoles.Add( role.Value );
				}
			}
		}

		public async void AddSelectedRole()
		{
			await _userEndpoint.AddUserToRole( SelectedUser.Id , SelectedAvailableRole );

			UserRoles.Add( SelectedAvailableRole );
			AvailableRoles.Remove( SelectedAvailableRole );
		}

		public async void RemoveSelectedRole()
		{
			await _userEndpoint.RemoveUserFromRole( SelectedUser.Id , SelectedUserRole );

			AvailableRoles.Add( SelectedUserRole );
			UserRoles.Remove( SelectedUserRole );
		}
	}
}
