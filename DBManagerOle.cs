using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using log4net; 

namespace Utility
{
    /// <summary>
    /// Classe di utilità generale per l'interazione ole con il database
    /// </summary>
    public class DBManagerOle
	{

		private OleDbConnection objConn;
		private OleDbTransaction  objSqlTransaction;
		private Boolean bUseTransaction = false;
	
		//Definizione LOG4NET
		private static readonly ILog Log = LogManager.GetLogger(typeof(DBManagerOle));
		
		public DBManagerOle()
		{
		
		}
		public DBManagerOle(string connectionString)
		{
			
			objConn = new OleDbConnection();
			// TODO: Modify the connection string and include any
			// additional required properties for your database.
			objConn.ConnectionString = connectionString;

			Log.Info("Costruttore DbMAnager inizializzato correttamente. Istanza Connessione eseguita");
		}


		

		private void OpenConnection()
		{
			try
			{
				objConn.Open();
			}
			catch(SqlException Err)
			{
				Log.Error(Err.Message,Err); 
				throw;
			}
		}

		//Start transazione
		public void BeginTransaction()
		{
			OpenConnection();
			bUseTransaction = true;
			objSqlTransaction = objConn.BeginTransaction();
		}

		//Commit Transazione
		public void CommitTransaction()
		{
			bUseTransaction = false;
			objSqlTransaction.Commit();
			CloseConnection();
		}


		//RollBack Transazione
		public void RollBackTransaction()
		{
			bUseTransaction = false;
			objSqlTransaction.Rollback();
			CloseConnection();
		}


		public string ConnectionState()
		{
			return objConn.State.ToString();
		}


		public OleDbDataReader GetDataReader(string SQL)
		{
			OleDbCommand objCommand= new OleDbCommand ();
			try
			{
				string sConnState = ConnectionState();
				if (sConnState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
				if (bUseTransaction == true)
				{
					objCommand.Transaction =  objSqlTransaction;
				}
				objCommand.Connection = objConn;
				objCommand.CommandText = SQL;
				objCommand.CommandTimeout = 0;

				if (bUseTransaction == true)
				{
					return objCommand.ExecuteReader();
				}
				else
				{
					return objCommand.ExecuteReader(CommandBehavior.CloseConnection);
				}
			}
			catch (SqlException Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
		}


		public OleDbDataReader GetDataReader(OleDbCommand comandoSQL)
		{
			OleDbCommand objCommand= comandoSQL;
			try
			{
				string sConnState = ConnectionState();
				if (sConnState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
				if (bUseTransaction == true)
				{
					objCommand.Transaction = objSqlTransaction;
				}
				objCommand.Connection = objConn;
				//objCommand.CommandText = SQL;
				objCommand.CommandTimeout = 0;

				if (bUseTransaction == true)
				{
					return objCommand.ExecuteReader();
				}
				else
				{
					return objCommand.ExecuteReader(CommandBehavior.CloseConnection);
				}
			}
			catch (SqlException Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
		}


		public DataView GetDataView (string SQL,string DataSetTable)
		{          
			//*****************************************
			//* Purpose: Getting DataReader for the given Procedure
			//* Input parameters:
			//* strConnect----Connection string
			//* ProcName ---StoredProcedures name
			//* DataSetTable--DataSetTable name sting
			//* Returns :
			//* DataView contains data
			//* ****************************************

			string strCommandText= SQL;

			try
			{
				//create a new Command using the CommandText and Connection object
				OleDbCommand objCommand= new  OleDbCommand();
				//declare a variable to hold a DataAdaptor object
				string sConnState = ConnectionState(); 
				if (sConnState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
					if (bUseTransaction == true)
				{
					objCommand.Transaction = objSqlTransaction;
				}
				objCommand.Connection = objConn;
				objCommand.CommandText = SQL;
				objCommand.CommandTimeout = 0;
			
				OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();

				//open the connection and execute the command
				objDataAdapter.SelectCommand = objCommand;
				DataSet objDataSet;
				objDataSet = new DataSet(DataSetTable);
				objDataAdapter.Fill(objDataSet);
				DataView objDataView ;
				objDataView = new DataView(objDataSet.Tables[0]);
				return objDataView;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			finally
			{
				CloseConnection();
			}

		} 


		public DataView GetDataView (OleDbCommand comandoSQL,string DataSetTable)
		{          
			//*****************************************
			//* Purpose: Getting DataReader for the given Procedure
			//* Input parameters:
			//* strConnect----Connection string
			//* ProcName ---StoredProcedures name
			//* DataSetTable--DataSetTable name sting
			//* Returns :
			//* DataView contains data
			//* ****************************************

			

			try
			{
				//create a new Command using the CommandText and Connection object
				OleDbCommand objCommand= comandoSQL;
				//declare a variable to hold a DataAdaptor object
				string sConnState = ConnectionState(); 
				if (sConnState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
				if (bUseTransaction == true)
				{
					objCommand.Transaction = objSqlTransaction;
				}
				objCommand.Connection = objConn;
				//objCommand.CommandText = SQL;
				objCommand.CommandTimeout = 0;
			
				OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();

				//open the connection and execute the command
				objDataAdapter.SelectCommand = objCommand;
				DataSet objDataSet;
				objDataSet = new DataSet(DataSetTable);
				objDataAdapter.Fill(objDataSet);
				DataView objDataView ;
				objDataView = new DataView(objDataSet.Tables[0]);
				return objDataView;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			finally
			{
				CloseConnection();
			}

		} 



		public DataSet GetDataSet	(string SQL , string DataTable)
		{
			DataSet dstEorder = new DataSet();
			OleDbDataAdapter dadEorder; 
			OleDbCommand objCommand = new OleDbCommand ();

			try
			{
				string sConnState = ConnectionState();
				if (sConnState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}

				//Transazione si/no
				if (bUseTransaction == true)
				{
					objCommand.Transaction = objSqlTransaction;
				}
				objCommand.Connection = objConn;
				objCommand.CommandTimeout = 0;
				
				dadEorder = new OleDbDataAdapter(SQL, objConn);
				dadEorder.Fill(dstEorder, DataTable);

				return dstEorder;
			}
			catch ( Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw Err;                                  
			}
			finally
			{
				CloseConnection();
			}
		}
			

		public DataSet GetDataSet	(OleDbCommand comandoSQL , string DataTable)
		{
			DataSet dstEorder = new DataSet();
			OleDbDataAdapter dadEorder; 
			OleDbCommand objCommand = comandoSQL;

			try
			{
				string sConnState = ConnectionState();
				if (sConnState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}

				//Transazione si/no
				if (bUseTransaction == true)
				{
					objCommand.Transaction = objSqlTransaction;
				}
				objCommand.Connection = objConn;
				objCommand.CommandTimeout = 0;
				
				dadEorder = new OleDbDataAdapter(objCommand);
				dadEorder.Fill(dstEorder, DataTable);

				return dstEorder;
			}
			catch ( Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw Err;                                  
			}
			finally
			{
				CloseConnection();
			}
		}





		private void CloseConnection()
		{
			objConn.Close();
		}

		public void updateDB(string SQL)
		{
			OleDbCommand objCommand= new OleDbCommand ();
			try
			{
				string sConnState = ConnectionState();
				if (sConnState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
				if (bUseTransaction == true)
				{
					objCommand.Transaction = objSqlTransaction;
				}
				objCommand.Connection = objConn;
				objCommand.CommandText = SQL;
				objCommand.CommandTimeout = 0;
				objCommand.ExecuteNonQuery();
			}
			catch (SqlException Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}			
			finally
			{
				if (bUseTransaction == false)
				{
					CloseConnection();
				}
			}
		}



		public void updateDB(OleDbCommand comandoSQL)
		{
			OleDbCommand objCommand= comandoSQL;
			try
			{
				string sConnState = ConnectionState();
				if (sConnState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
				if (bUseTransaction == true)
				{
					objCommand.Transaction = objSqlTransaction;
				}
				objCommand.Connection = objConn;
				//objCommand.CommandText = SQL;
				objCommand.CommandTimeout = 0;
				objCommand.ExecuteNonQuery();
			}
			catch (SqlException Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}			
			finally
			{
				if (bUseTransaction == false)
				{
					CloseConnection();
				}
			}
		}


		
		public int ExecuteScalar(string sql)
		{
			try
			{
				OleDbCommand objectCommand= new OleDbCommand();
				string connState = ConnectionState();
				
				if (connState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
				if (bUseTransaction == true)
				{
					objectCommand.Transaction = objSqlTransaction;
				}
				objectCommand.Connection = objConn;
				objectCommand.CommandText = sql;
				objectCommand.CommandTimeout = 0;
				
				return (int) objectCommand.ExecuteScalar();


			}
			catch (SqlException Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}			
			finally
			{
				if (bUseTransaction == false)
				{
					CloseConnection();
				}
			}
		}
		

		public int ExecuteScalar(OleDbCommand comandoSQL)
		{
			try
			{
				OleDbCommand objectCommand= comandoSQL;
				string connState = ConnectionState();
				
				if (connState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
				if (bUseTransaction == true)
				{
					objectCommand.Transaction = objSqlTransaction;
				}
				objectCommand.Connection = objConn;
				//objectCommand.CommandText = sql;
				objectCommand.CommandTimeout = 0;
				
				return (int) objectCommand.ExecuteScalar();


			}
			catch (SqlException Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}			
			finally
			{
				if (bUseTransaction == false)
				{
					CloseConnection();
				}
			}
		}




		public int ExecuteNonQuery(string sql)
		{
			try
			{
				OleDbCommand objectCommand= new OleDbCommand();
				string connState = ConnectionState();
				
				if (connState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
				if (bUseTransaction == true)
				{
					objectCommand.Transaction = objSqlTransaction;
				}
				objectCommand.Connection = objConn;
				objectCommand.CommandText = sql;
				objectCommand.CommandTimeout = 0;
				
				return (int) objectCommand.ExecuteNonQuery ();


			}
			catch (SqlException Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}			
			finally
			{
				if (bUseTransaction == false)
				{
					CloseConnection();
				}
			}
		}

		public int ExecuteNonQuery(OleDbCommand comandoSQL)
		{
			try
			{
				OleDbCommand objectCommand= comandoSQL;
				string connState = ConnectionState();
				
				if (connState.CompareTo("Open")!=0)
				{
					OpenConnection();
				}
				if (bUseTransaction == true)
				{
					objectCommand.Transaction = objSqlTransaction;
				}
				objectCommand.Connection = objConn;
				//objectCommand.CommandText = sql;
				objectCommand.CommandTimeout = 0;
				
				return (int) objectCommand.ExecuteNonQuery ();


			}
			catch (SqlException Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}
			catch (Exception Err)
			{
				Log.Error(Err.Message ,Err);
				throw;
			}			
			finally
			{
				if (bUseTransaction == false)
				{
					CloseConnection();
				}
			}
		}




	}
}



