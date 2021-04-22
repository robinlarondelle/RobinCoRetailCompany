using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.Library.Api
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient { get; set; }
        private ILoggedInUserModel _loggedInUserModel { get; set; }

        public ApiHelper(ILoggedInUserModel loggedInUserModel)
        {
            InitializeHttpClient();
            _loggedInUserModel = loggedInUserModel;
        }

        public HttpClient ApiClient
		{
            get => _apiClient;
		}

        private void InitializeHttpClient()
        {
            string api = ConfigurationManager.AppSettings[ "api" ];
            _apiClient = new HttpClient {BaseAddress = new Uri(api)};
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
        }

        public async Task<AuthenticatedUser> Authenticate( string username , string password )
        {
            FormUrlEncodedContent data = new FormUrlEncodedContent( new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            } );

            using ( HttpResponseMessage response = await _apiClient.PostAsync( "/token" , data ) )
            {
                if ( response.IsSuccessStatusCode )
                {
                    AuthenticatedUser result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception( response.ReasonPhrase );
                }

            }

        }

        public async Task GetLoggedInUserInformation(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");

            using (HttpResponseMessage response = await _apiClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    LoggedInUserModel result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedInUserModel.Id = result.Id;
                    _loggedInUserModel.CreatedTime = result.CreatedTime;
                    _loggedInUserModel.EmailAddress = result.EmailAddress;
                    _loggedInUserModel.FirstName = result.FirstName;
                    _loggedInUserModel.LastName = result.LastName;
                    _loggedInUserModel.AccessToken = token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}