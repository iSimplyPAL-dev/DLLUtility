using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;
using log4net;

namespace Utility
{
    /// <summary>
    /// Classe di utilità generale per l'interazione con il database
    /// </summary>
    public class DBModel : IDisposable
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DBModel));
        private static string TypeDB;
        private object objConn;
        private object objSqlTransaction;
        private Boolean bUseTransaction = false;
        /// <summary>
        /// 
        /// </summary>
        protected bool AlreadyOpened;
        /// <summary>
        /// 
        /// </summary>
        public int nTry = 0;
        /// <summary>
        /// 
        /// </summary>
        public enum TypeQuery
        {
            ///Query di tipo select
            View,
            ///Qeury fdi esecuzione di stored
            StoredProcedure
        }
        void IDisposable.Dispose() { }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (TypeDB == "MySQL")
            {
                if (((MySqlConnection)objConn).State == System.Data.ConnectionState.Open)
                    ((MySqlConnection)objConn).Close();
            }
            else {
                if (((SqlConnection)objConn).State == System.Data.ConnectionState.Open)
                    ((SqlConnection)objConn).Close();
            }
        }
        /// <summary>
        /// Inizializzazione della stringa di connessione; possono essere gestite connessioni verso MySQL o verso SQL Server
        /// </summary>
        /// <param name="DBType"></param>
        /// <param name="connectionString"></param>
        public DBModel(string DBType, string connectionString)
        {
            TypeDB = DBType;
            string conString = connectionString;

            if (TypeDB == "MySQL")
            {
                objConn = new MySqlConnection(conString);
            }
            else {
                objConn = new SqlConnection(conString);
            }
            //Log.Info("Costruttore DBManager inizializzato correttamente. Istanza Connessione eseguita");
        }
        /// <summary>
        /// Valorizzazione della sintassi per eseguire una stored procedure in base alla tipologia di database
        /// </summary>
        private static string ToExecSP
        {
            get
            {
                if (TypeDB == "MySQL")
                    return "CALL ";
                else
                    return "EXEC ";
            }
        }
        /// <summary>
        /// Valorizzazione della sintassi per richiamare un parametro in base alla tipologia di database
        /// </summary>
        private static string PrefVarSP
        {
            get
            {
                if (TypeDB == "MySQL")
                    return "@var";
                else
                    return "@";
            }
        }
        /// <summary>
        /// Valorizzazione della stringa da eseguire secondo la sintassi specifica per la tipologia di database
        /// </summary>
        /// <param name="myType"></param>
        /// <param name="sSQL"></param>
        /// <param name="myParam"></param>
        /// <returns></returns>
        public string GetSQL(TypeQuery myType,string sSQL, params string[] myParam)
        {
            string sRet = "";
            foreach (string myItem in myParam)
            {
                if (sRet != string.Empty)
                    sRet += ",";
                sRet += PrefVarSP + myItem;
            }

            if (TypeDB == "MySQL")
            {
                if (myType == TypeQuery.StoredProcedure)
                    sRet = ToExecSP+ sSQL + " (" + sRet + ")";
                else
                sRet= sSQL + " (" + sRet + ")";
            }
            else
            {
                if(myType==TypeQuery.StoredProcedure)
                sRet = ToExecSP+ sSQL + " " + sRet;
                else
                sRet = sSQL + " " + sRet;
            }
            return sRet;
        }
        /// <summary>
        /// Valorizzazione dei parametri da eseguire secondo la sintassi specifica per la tipologia di database
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public object GetParam(string Name, object Value)
        {
            if (TypeDB == "MySQL")
                return new MySqlParameter(PrefVarSP + Name, Value);
            else
                return new SqlParameter(PrefVarSP + Name, Value);
        }
        /// <summary>
        /// Apertura della connessione al database
        /// </summary>
        private void OpenConnection()
        {
            try
            {
                if (TypeDB == "MySQL")
                {
                    ((MySqlConnection)objConn).Open();
                }
                else {
                    ((SqlConnection)objConn).Open();
                }
            }
            catch (SqlException Err)
            {
                Log.Error("OpenConnection."+Err.Message, Err);
                throw;
            }
        }
        /// <summary>
        /// Chiusura della connessione al database
        /// </summary>
        private void CloseConnection()
        {
            if (TypeDB == "MySQL")
            {
                ((MySqlConnection)objConn).Close();
            }
            else {
                ((SqlConnection)objConn).Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void BeginTransaction()
        {
            OpenConnection();
            bUseTransaction = true;
            if (TypeDB == "MySQL")
            {
                objSqlTransaction = ((MySqlConnection)objConn).BeginTransaction();
            }
            else {
                objSqlTransaction = ((SqlConnection)objConn).BeginTransaction();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void CommitTransaction()
        {
            bUseTransaction = false;
            if (TypeDB == "MySQL")
            {
                ((MySqlTransaction)objSqlTransaction).Commit();
            }
            else {
                ((SqlTransaction)objSqlTransaction).Commit();
            }
            CloseConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        public void RollBackTransaction()
        {
            bUseTransaction = false;
            if (TypeDB == "MySQL")
            {
                ((MySqlTransaction)objSqlTransaction).Rollback();
            }
            else {
                ((SqlTransaction)objSqlTransaction).Rollback();
            }
            CloseConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ConnectionState()
        {
            if (TypeDB == "MySQL")
            {
                return ((MySqlConnection)objConn).State.ToString();
            }
            else {
                return ((SqlConnection)objConn).State.ToString();
            }
        }
        /// <summary>
        /// Permette di eseguire un'istruzione Transact-SQL in base alla connessione e restituisce un oggetto DataView contenente la vista della tabella dati associata a questo modello
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="SrcTable"></param>
        /// <param name="myParam"></param>
        /// <returns></returns>
        public DataView GetDataView(string SQL, string SrcTable, params object[] myParam)
        {
            Log.Debug("GetDataView.IN");
            try
            {
                DataSet objDataSet = new DataSet(SrcTable);
                //declare a variable to hold a DataAdaptor object
                string sConnState = ConnectionState();
                if (sConnState.CompareTo("Open") != 0)
                {
                    OpenConnection();
                }
                //create a new Command using the CommandText and Connection object
                if (TypeDB == "MySQL")
                {
                    MySqlCommand objCommand = new MySqlCommand();
                    nTry = 0;
                    ReDo:
                    try
                    {
                        objCommand.Connection = (MySqlConnection)objConn;
                        objCommand.CommandTimeout = 0;
                        objCommand.CommandText = SQL;
                        foreach (object myItem in myParam)
                        {
                            objCommand.Parameters.Add(myItem);
                        }
                        MySqlDataAdapter objDataAdapter = new MySqlDataAdapter();
                        Log.Debug("GetDataView->" + Costanti.LogQuery(objCommand));
                        //open the connection and execute the command
                        objDataAdapter.SelectCommand = objCommand;
                        objDataAdapter.Fill(objDataSet);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper().Contains("AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST") && nTry <= 3)
                        {
                            nTry += 1;
                            goto ReDo;
                        }
                        Log.Debug("GetDataView.errore: ", ex);
                    }
                }
                else {
                    SqlCommand objCommand = new SqlCommand();
                    nTry = 0;
                    ReDo:
                    try
                    {
                        objCommand.Connection = (SqlConnection)objConn;
                        objCommand.CommandTimeout = 0;
                        objCommand.CommandText = SQL;
                        foreach (object myItem in myParam)
                        {
                            objCommand.Parameters.Add(myItem);
                        }
                        SqlDataAdapter objDataAdapter = new SqlDataAdapter();

                        Log.Debug("GetDataView->" + Costanti.LogQuery(objCommand));
                        //open the connection and execute the command
                        objDataAdapter.SelectCommand = objCommand;
                        objDataAdapter.Fill(objDataSet);
                        Log.Debug("GetDataView.fatto");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper().Contains("AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST") && nTry <= 3)
                        {
                            Log.Debug("GetDataView.AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST.",ex);
                            nTry += 1;
                            goto ReDo;
                        }
                        Log.Debug("GetDataView.errore: ", ex);
                    }
                }

                DataView objDataView = new DataView(objDataSet.Tables[0]);
                Log.Debug("GetDataView.OUT");
                return objDataView;
            }
            catch (Exception Err)
            {
                Log.Error(Err.Message, Err);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// Permette di eseguire un'istruzione Transact-SQL in base alla connessione e restituisce un oggetto DataReader contenente la vista della tabella dati associata a questo modello
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="myParam"></param>
        /// <returns></returns>
        public object GetDataReader(string SQL, params object[] myParam)
        {
            Log.Debug("GetDataReader.IN");
            try
            {
                //declare a variable to hold a DataAdaptor object
                string sConnState = ConnectionState();
                if (sConnState.CompareTo("Open") != 0)
                {
                    OpenConnection();
                }
                //create a new Command using the CommandText and Connection object
                if (TypeDB == "MySQL")
                {
                    MySqlCommand objCommand = new MySqlCommand();
                    nTry = 0;
                    ReDo:
                    try
                    {
                        objCommand.Connection = (MySqlConnection)objConn;
                        objCommand.CommandTimeout = 0;
                        objCommand.CommandText = SQL;
                        foreach (object myItem in myParam)
                        {
                            objCommand.Parameters.Add(myItem);
                        }
                        Log.Debug("GetDataReader->" + Costanti.LogQuery(objCommand));
                        //open the connection and execute the command
                        MySqlDataReader objReader = objCommand.ExecuteReader();
                        return (object)objReader;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper().Contains("AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST") && nTry <= 3)
                        {
                            nTry += 1;
                            goto ReDo;
                        }
                        Log.Debug("GetDataReader.errore: ", ex);
                        throw;
                    }
                }
                else {
                    SqlCommand objCommand = new SqlCommand();
                    nTry = 0;
                    ReDo:
                    try
                    {
                        objCommand.Connection = (SqlConnection)objConn;
                        objCommand.CommandTimeout = 0;
                        objCommand.CommandText = SQL;
                        foreach (object myItem in myParam)
                        {
                            objCommand.Parameters.Add(myItem);
                        }
                        Log.Debug("GetDataReader->" + Costanti.LogQuery(objCommand));
                        //open the connection and execute the command
                        SqlDataReader objReader = objCommand.ExecuteReader();
                        Log.Debug("GetDataReader.fatto");
                        return (object)objReader;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper().Contains("AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST") && nTry <= 3)
                        {
                            Log.Debug("GetDataReader.AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST.", ex);
                            nTry += 1;
                            goto ReDo;
                        }
                        Log.Debug("GetDataReader.errore: ", ex);
                        throw;
                    }
                }
            }
            catch (Exception Err)
            {
                Log.Error("GetDataReader." + Err.Message, Err);
                throw;
            }
        }
        /// <summary>
        /// Permette di eseguire un'istruzione Transact-SQL in base alla connessione e restituisce un oggetto DataSet contenente la vista della tabella dati associata a questo modello
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="SrcTable"></param>
        /// <param name="myParam"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string SQL, string SrcTable, params object[] myParam)
        {
            Log.Debug("GetDataSet.IN");
            try
            {
                DataSet objDataSet = new DataSet(SrcTable);
                //declare a variable to hold a DataAdaptor object
                string sConnState = ConnectionState();
                if (sConnState.CompareTo("Open") != 0)
                {
                    OpenConnection();
                }
                //create a new Command using the CommandText and Connection object
                if (TypeDB == "MySQL")
                {
                    MySqlCommand objCommand = new MySqlCommand();
                    nTry = 0;
                    ReDo:
                    try
                    {
                        objCommand.Connection = (MySqlConnection)objConn;
                        objCommand.CommandTimeout = 0;
                        objCommand.CommandText = SQL;
                        foreach (object myItem in myParam)
                        {
                            objCommand.Parameters.Add(myItem);
                        }
                        MySqlDataAdapter objDataAdapter = new MySqlDataAdapter();

                        Log.Debug("GetDataSet->" + Costanti.LogQuery(objCommand));
                        //open the connection and execute the command
                        objDataAdapter.SelectCommand = objCommand;
                        objDataAdapter.Fill(objDataSet, SrcTable);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper().Contains("AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST") && nTry <= 3)
                        {
                            nTry += 1;
                            goto ReDo;
                        }
                        Log.Debug("GetDataSet.errore: ", ex);
                    }
                }
                else {
                    SqlCommand objCommand = new SqlCommand();
                    nTry = 0;
                    ReDo:
                    try
                    {
                        objCommand.Connection = (SqlConnection)objConn;
                        objCommand.CommandTimeout = 0;
                        objCommand.CommandText = SQL;
                        foreach (object myItem in myParam)
                        {
                            objCommand.Parameters.Add(myItem);
                        }
                        SqlDataAdapter objDataAdapter = new SqlDataAdapter();

                        Log.Debug("GetDataSet->" + Costanti.LogQuery(objCommand));
                        //open the connection and execute the command
                        objDataAdapter.SelectCommand = objCommand;
                        objDataAdapter.Fill(objDataSet, SrcTable);
                        Log.Debug("GetDataSet.fatto");
                   }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper().Contains("AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST") && nTry <= 3)
                        {
                            Log.Debug("GetDataSet.AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST.", ex);
                            nTry += 1;
                            goto ReDo;
                        }
                        Log.Debug("GetDataSet.errore: ", ex);
                    }
                }
                Log.Debug("GetDataSet.OUT");
                return objDataSet;
            }
            catch (Exception Err)
            {
                Log.Error(Err.Message, Err);
                throw Err;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// Permette di eseguire un'istruzione Transact-SQL, ad esempio UPDATE, INSERT o DELETE, in base alla connessione e restituisce un numero di righe modificate.
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="myParam"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string SQL, params object[] myParam)
        {
            Log.Debug("ExecuteNonQuery.IN");
            try
            {
                int RetVal = -1;
                string connState = ConnectionState();
                if (connState.CompareTo("Open") != 0)
                {
                    OpenConnection();
                }
                if (TypeDB == "MySQL")
                {
                    MySqlCommand objCommand = new MySqlCommand();
                    nTry = 0;
                    ReDo:
                    try
                    {
                        if (bUseTransaction == true)
                        {
                            objCommand.Transaction = (MySqlTransaction)objSqlTransaction;
                        }
                        objCommand.Connection = (MySqlConnection)objConn;
                        objCommand.CommandTimeout = 0;
                        objCommand.CommandText = SQL;
                        foreach (object myItem in myParam)
                        {
                            objCommand.Parameters.Add(myItem);
                        }
                        Log.Debug("ExecuteNonQuery->" + Costanti.LogQuery(objCommand));
                        RetVal = (int)objCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper().Contains("AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST") && nTry <= 3)
                        {
                            nTry += 1;
                            goto ReDo;
                        }
                        Log.Debug("ExecuteNonQuery.errore: ", ex);
                    }
                }
                else {
                    SqlCommand objCommand = new SqlCommand();
                    nTry = 0;
                    ReDo:
                    try
                    {
                        if (bUseTransaction == true)
                        {
                            objCommand.Transaction = (SqlTransaction)objSqlTransaction;
                        }
                        objCommand.Connection = (SqlConnection)objConn;
                        objCommand.CommandTimeout = 0;
                        objCommand.CommandText = SQL;
                        foreach (object myItem in myParam)
                        {
                            objCommand.Parameters.Add(myItem);
                        }
                        Log.Debug("ExecuteNonQuery->" + Costanti.LogQuery(objCommand));
                        RetVal = (int)objCommand.ExecuteNonQuery();
                        Log.Debug("ExecuteNonQuery.fatto");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper().Contains("AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST") && nTry <= 3)
                        {
                            Log.Debug("ExecuteNonQuery.AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST.", ex);
                            nTry += 1;
                            goto ReDo;
                        }
                        Log.Debug("ExecuteNonQuery.errore: ", ex);
                    }
                }
                Log.Debug("ExecuteNonQuery.OUT");
                return RetVal;
            }
            catch (MySqlException Err)
            {
                Log.Error(Err.Message, Err);
                throw;
            }
            catch (Exception Err)
            {
                Log.Error(Err.Message, Err);
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



