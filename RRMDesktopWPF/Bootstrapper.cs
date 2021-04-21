using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Caliburn.Micro;

using RRMDesktopWPF.Library.Api;
using RRMDesktopWPF.Library.Models;
using RRMDesktopWPF.Utils;
using RRMDesktopWPF.ViewModels;

namespace RRMDesktopWPF
{
	public class Bootstrapper : BootstrapperBase
	{
		// Dependency Injection Container
		private readonly SimpleContainer _container = new SimpleContainer();

		public Bootstrapper()
		{
			Initialize();

			//PasswordBox helper for Caliburn Micro
			ConventionManager.AddElementConvention<PasswordBox>(
				BoundPasswordBox.PasswordBoxHelper.BoundPasswordProperty ,
				"Password" ,
 				"PasswordChanged" );
		}

		protected override void Configure()
		{
			_container.Instance( _container );

			//Tell the DI container what concrete implementations to use for each Interface
			_container
				.Singleton<IWindowManager, WindowManager>()
				.Singleton<IEventAggregator, EventAggregator>()
				.Singleton<IApiHelper, ApiHelper>()
				.Singleton<ILoggedInUserModel, LoggedInUserModel>();

			GetType().Assembly.GetTypes() //Get all the types
				.Where( ( type ) => type.IsClass ) //Filter all the types of this applications by IsClass
				.Where( ( type ) => type.Name.EndsWith( "ViewModel" ) ) //filter all the ViewModels
				.ToList() //create a list with all the ViewModels to use ForEach
				.ForEach( viewModelType =>
				 {
					//Register each ViewModel in the Dependency Injection Container
					//This will provide a new instance everytime a ViewModel is requested
					_container.RegisterPerRequest( viewModelType , viewModelType.ToString() , viewModelType );
				 } );
		}

		//Tell WPF to use Caliburn.Micro's ShellViewModel instead of the default Startup Method
		protected override void OnStartup( object sender , StartupEventArgs e )
			=> DisplayRootViewFor<ShellViewModel>();

		//Overrides to use the Dependency Injection Container instead of the default instance
		protected override object GetInstance( Type service , string key )
			=> _container.GetInstance( service , key );

		protected override IEnumerable<object> GetAllInstances( Type service )
			=> _container.GetAllInstances( service );

		protected override void BuildUp( object instance )
			=> _container.BuildUp( instance );
	}
}