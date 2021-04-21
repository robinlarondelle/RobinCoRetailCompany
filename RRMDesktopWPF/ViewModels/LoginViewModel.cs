using System;
using System.Threading.Tasks;
using Caliburn.Micro;

using RRMDesktopWPF.EventModels;
using RRMDesktopWPF.Library.Api;
using RRMDesktopWPF.Library.Models;
using RRMDesktopWPF.Utils;

namespace RRMDesktopWPF.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username;
        private string _password;
        private readonly IApiHelper _apiHelper;
        private string _errorMessage;
        private IEventAggregator _event;

        public LoginViewModel(IApiHelper apiHelper, IEventAggregator eventAggregator)
        {
            _apiHelper = apiHelper;
            _event = eventAggregator;
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool IsErrorVisible
        {
            get => ErrorMessage?.Length > 0;
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        //Caliburn Micro automatically maps this CanLogIn function with the CanExecute function of the MVVM pattern
        public bool CanLogIn => Username?.Length > 0 && Password?.Length > 0;


        //This function is the x:Name property of the Login button in the ViewModel
        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                AuthenticatedUser result = await _apiHelper.Authenticate(Username, Password);
                await _apiHelper.GetLoggedInUserInformation(result.Access_Token);
                await _event.PublishOnUIThreadAsync(new LoginEvent());
                     
                //Capture more information about the user
                /*LoggedInUserModel loggedInUserModel;*/
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
    } 
}