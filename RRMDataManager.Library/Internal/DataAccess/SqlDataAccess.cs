using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Dapper;

namespace RRMDataManager.Library.Internal.DataAccess
{
	internal class SqlDataAccess : IDisposable
	{
		public string GetConnectionString( string name ) => ConfigurationManager.ConnectionStrings[ name ].ConnectionString;

		public List<T> LoadData<T, U>( string storedProcedure , U parameters , string connectionStringName )
		{
			string connectionString = GetConnectionString( connectionStringName );
			using ( IDbConnection connection = new SqlConnection( connectionString ) )
			{
				List<T> rows = connection.Query<T>( storedProcedure , parameters , commandType: CommandType.StoredProcedure ).ToList();
				return rows;
			}
		}

		public void SaveData<T>( string storedProcedure , T parameters , string connectionStringName )
		{
			string connectionString = GetConnectionString( connectionStringName );
			using ( IDbConnection connection = new SqlConnection( connectionString ) )
			{
				connection.Execute( storedProcedure , parameters , commandType: CommandType.StoredProcedure );
			}
		}

		private IDbConnection _connection;
		private IDbTransaction _transaction;
		private bool IsConnectionOpen = false;

		public void StartTransaction( string connectionString )
		{
			_connection = new SqlConnection( GetConnectionString( connectionString ) );
			_connection.Open();
			IsConnectionOpen = true;
			_transaction = _connection.BeginTransaction();
		}
		public void SaveDataInTransaction<T>( string storedProcedure , T parameters ) => 
			_connection.Execute( storedProcedure , parameters , commandType: CommandType.StoredProcedure , transaction: _transaction );

		public List<T> LoadDataInTransaction<T, U>( string storedProcedure , U parameters ) =>
			_connection.Query<T>( storedProcedure , parameters , commandType: CommandType.StoredProcedure , transaction: _transaction ).ToList();

		public void CommitTransaction()
		{
			_transaction?.Commit();
			_connection.Close();
			IsConnectionOpen = false;
		}

		public void RollbackTransaction()
		{
			_transaction?.Rollback();
			_connection.Close();
			IsConnectionOpen = false;
		}

		public void Dispose()
		{
			if (IsConnectionOpen)
			{
				CommitTransaction();
			}

			_transaction = null;
			_connection = null;
			IsConnectionOpen = false;
		}
	}
}
