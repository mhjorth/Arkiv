using System;
using MongoDB.Driver;

namespace Arkiv
{
	public class MongoDb
	{
		private string _connectionString = "mongodb://localhost";
		private MongoClient _client;
		private string _dbName;
		private MongoServer _server;
		private MongoDatabase _database;


		public MongoDb (string dbName)
		{
			_client = new MongoClient (_connectionString);
			_dbName = dbName;
			_server = _client.GetServer ();
			_database = _server.GetDatabase (_dbName);
		}

		public MongoDatabase getDatabase()
		{
			return _database;
		}

	}
}

