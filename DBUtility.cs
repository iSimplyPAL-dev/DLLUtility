using System;
using log4net;
using System.Data;
using System.Data.SqlClient;

namespace Utility
{
    /// <summary>
    /// Classe di utilità generale per la conversione in stringa
    /// </summary>
    /// <revisionHistory>
    /// <revision date="12/04/2019">
    /// <strong>Qualificazione AgID-analisi_rel01</strong>
    /// <em>Esportazione completa dati</em>
    /// </revision>
    /// </revisionHistory>
    public class DBUtility
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DBUtility));
        private string DBType;
        private string ConnectionString;
        /// <summary>
        /// Classe per la definizione di tutte le stringhe di connessione
        /// </summary>
        public class objDBConnection
        {
            /// <summary>
            /// Definisce il tipo di database utilizzato MySQL/SQL
            /// </summary>
            public string TypeDB = "SQL";
            /// <summary>
            /// connessione al db generale
            /// </summary>
            public string StringConnection = default(string);
            /// <summary>
            /// connessione al db stradario
            /// </summary>
            public string StringConnectionStrade = default(string);
            /// <summary>
            /// connessione al db anagrafe
            /// </summary>
            public string StringConnectionAnag = default(string);
            /// <summary>
            /// connessione al db ici/tasi
            /// </summary>
            public string StringConnectionICI = default(string);
            /// <summary>
            /// connessione al db tarsu
            /// </summary>
            public string StringConnectionTARSU = default(string);
            /// <summary>
            /// connessione al db osap/scuole
            /// </summary>
            public string StringConnectionOSAP = default(string);
            /// <summary>
            /// connessione al db h2o
            /// </summary>
            public string StringConnectionH2O = default(string);
            /// <summary>
            /// connessione al db accertamenti
            /// </summary>
            public string StringConnectionProvv = default(string);
        }
        public DBUtility(string TypeDB, string myConnectionString)
        {
            DBType = TypeDB;
            ConnectionString = myConnectionString;
        }
        /// <summary>
        /// Funzione unica per la gestione della registrazione degli eventi.
        /// L'azione potrebbe esse codifica in base <em>public const int AZIONE</em> quindi va decodificata in stringa prima dell'inserimento.
        /// </summary>
        /// <param name="dataEvento">DateTime momento dell'evento</param>
        /// <param name="userEvento">string operatore</param>
        /// <param name="Argomento">string Argomento in base a LogEventArgument o libera dicitura</param>
        /// <param name="Funzione">string Funzione {Page_Load, CmdSalva, ecc...}</param>
        /// <param name="Azione">string Azione {chiesto consultazione, uscito pagina, ecc...}</param>
        /// <param name="idTributo">string tributo</param>
        /// <param name="idEnte">string ente</param>
        /// <param name="IdRiga">int identificativo della riga che ha generato l'evento</param>
        /// <revisionHistory>
        /// <revision date="12/04/2019">
        /// <strong>Qualificazione AgID-analisi_rel01</strong>
        /// <em>Analisi eventi</em>
        /// </revision>
        /// </revisionHistory>
        public void LogActionEvent(DateTime dataEvento, string userEvento, string Argomento, string Funzione, string Azione, string idTributo, string idEnte, int IdRiga)
        {
            try
            {
                switch (Azione)
                {
                    case "0":
                        Azione = "Nuovo inserimento";
                        break;
                    case "1":
                        Azione = "Modifica";
                        break;
                    case "2":
                        Azione = "Cancellazione";
                        break;
                    case "3":
                        Azione = "Consultazione";
                        break;
                    default:
                        break;
                }
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    ctx.nTry = 0;
                    ReDo:
                    try
                    {
                        string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure,"prc_TBLLOGACTIONEVENT_IU", "DATA_EVENTO", "USER_EVENTO", "EVENTO", "ID_TRIBUTO", "IDENTE");
                        ctx.ExecuteNonQuery(sSQL, ctx.GetParam("DATA_EVENTO", dataEvento)
                                , ctx.GetParam("USER_EVENTO", userEvento)
                                , ctx.GetParam("EVENTO", Argomento + "|" + Funzione + "|" + Azione + "|" + IdRiga.ToString())
                                , ctx.GetParam("ID_TRIBUTO", idTributo)
                                , ctx.GetParam("IDENTE", idEnte)
                            );
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper().Contains("AN EXISTING CONNECTION WAS FORCIBLY CLOSED BY THE REMOTE HOST") && ctx.nTry <= 3)
                        {
                            ctx.nTry += 1;
                            goto ReDo;
                        }
                        Log.Debug(idEnte + " - DBUtility.LogActionEvent.errore: ", ex);
                    }
                    finally
                    {
                        ctx.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Debug("DBUtility.LogActionEvent::errore::", ex);
            }
        }

        public string CStrToDB(object vInput, bool clearSpace)
        {
            string sTesto;
            string retValue = "";

            retValue = "''";

            if (vInput != null)
            {
                sTesto = System.Convert.ToString(vInput);
                if (clearSpace)
                {
                    sTesto = sTesto.Trim();
                }
                if (sTesto.Trim().Length != 0)
                {

                    retValue = "'" + sTesto.Replace("'", "''") + "'";

                }
            }
            return retValue;
        }
    }

    // *** 20130923 - gestione modifiche tributarie ***
    /// <summary>
    /// Classe di utilità generale per la registrazione delle modifiche tributarie
    /// </summary>
    public class ModificheTributarie
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ModificheTributarie));
        /// <summary>
        /// 
        /// </summary>
        public enum ModificheTributarieCausali
        {
            /// <summary>
            /// 
            /// </summary>
            VariazioneRiferimentiCatastali = 1,
            /// <summary>
            /// 
            /// </summary>
            VariazionePeriodo = 2,
            /// <summary>
            /// 
            /// </summary>
            VariazioneCodiceRendita = 3,
            /// <summary>
            /// 
            /// </summary>
            VariazioneCategoriaCatastale = 4,
            /// <summary>
            /// 
            /// </summary>
            VariazioneClasse = 5,
            /// <summary>
            /// 
            /// </summary>
            VariazioneConsistenza = 6,
            /// <summary>
            /// 
            /// </summary>
            VariazioneRenditaValore = 7,
            /// <summary>
            /// 
            /// </summary>
            VariazioneTipoPossesso = 8,
            /// <summary>
            /// 
            /// </summary>
            VariazionePercentualePossesso = 9,
            /// <summary>
            /// 
            /// </summary>
            VariazioneFlagAbitazionePrincipale = 10,
            /// <summary>
            /// 
            /// </summary>
            VariazioneContribuente = 11,
            /// <summary>
            /// 
            /// </summary>
            VariazioneComponenti = 12,
            /// <summary>
            /// 
            /// </summary>
            VariazioneStatoOccupazione = 13,
            /// <summary>
            /// 
            /// </summary>
            VariazioneTitoloOccupazione = 14,
            /// <summary>
            /// 
            /// </summary>
            VariazioneDestinazionedUso = 15,
            /// <summary>
            /// 
            /// </summary>
            VariazioneTipoVano = 16,
            /// <summary>
            /// 
            /// </summary>
            VariazioneMq = 17,
            /// <summary>
            /// 
            /// </summary>
            NuovaDichiarazione = 18,
            /// <summary>
            /// 
            /// </summary>
            VariazioneCategoria = 19,
            /// <summary>
            /// 
            /// </summary>
            VariazioneEsenzione = 20
        }

        /// <summary>
        /// 
        /// </summary>
        public enum DBOperation
        {
            /// <summary>
            /// 
            /// </summary>
            Read = 0,
            /// <summary>
            /// 
            /// </summary>
            Insert = 1,
            /// <summary>
            /// 
            /// </summary>
            Update = 2,
            /// <summary>
            /// 
            /// </summary>
            Delete = 3,
            /// <summary>
            /// 
            /// </summary>
            Calcola = 4
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myConnectionString"></param>
        /// <param name="DBOperation"></param>
        /// <param name="IDVARIAZIONE"></param>
        /// <param name="IDENTE"></param>
        /// <param name="TRIBUTO"></param>
        /// <param name="IDCAUSALE"></param>
        /// <param name="FOGLIO"></param>
        /// <param name="NUMERO"></param>
        /// <param name="SUBALTERNO"></param>
        /// <param name="DATAVARIAZIONE"></param>
        /// <param name="OPERATORE"></param>
        /// <param name="IDOGGETTOTRIBUTI"></param>
        /// <param name="DATATRATTATO"></param>
        /// <returns></returns>
        public bool SetModificheTributarie(string myConnectionString, int DBOperation, int IDVARIAZIONE, string IDENTE, string TRIBUTO, int IDCAUSALE, string FOGLIO, string NUMERO, string SUBALTERNO, DateTime DATAVARIAZIONE, string OPERATORE, int IDOGGETTOTRIBUTI, DateTime DATATRATTATO)
        {
            using (DBModel ctx = new DBModel(new DBUtility.objDBConnection().TypeDB, myConnectionString))
            {
                try
                {
                    string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_SetVariazioniBancaDati", "DBOperation", "IDVARIAZIONE", "IDENTE", "TRIBUTO", "IDCAUSALE", "FOGLIO", "NUMERO", "SUBALTERNO", "DATAVARIAZIONE", "OPERATORE", "IDOGGETTOTRIBUTI", "DATATRATTATO");
                    ctx.ExecuteNonQuery(sSQL, ctx.GetParam("DBOperation", DBOperation)
                            , ctx.GetParam("IDVARIAZIONE", IDVARIAZIONE)
                            , ctx.GetParam("IDENTE", IDENTE)
                            , ctx.GetParam("TRIBUTO", TRIBUTO)
                            , ctx.GetParam("IDCAUSALE", IDCAUSALE)
                            , ctx.GetParam("FOGLIO", FOGLIO)
                            , ctx.GetParam("NUMERO", NUMERO)
                            , ctx.GetParam("SUBALTERNO", SUBALTERNO)
                            , ctx.GetParam("DATAVARIAZIONE", DATAVARIAZIONE)
                            , ctx.GetParam("OPERATORE", OPERATORE)
                            , ctx.GetParam("IDOGGETTOTRIBUTI", IDOGGETTOTRIBUTI)
                            , ctx.GetParam("DATATRATTATO",DATATRATTATO )
                        );
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Debug(IDENTE + " - SetModificheTributarie.errore: ", ex);
                    return false;
                }
                finally
                {
                    ctx.Dispose();
                }
            }
        }
        //public bool SetModificheTributarie(string myConnectionString, int DBOperation, int IDVARIAZIONE, string IDENTE, string TRIBUTO, int IDCAUSALE, string FOGLIO, string NUMERO, string SUBALTERNO, DateTime DATAVARIAZIONE, string OPERATORE, int IDOGGETTOTRIBUTI, DateTime DATATRATTATO)
        //{
        //    SqlCommand cmdMyCommand = new SqlCommand();
        //    try
        //    {
        //        cmdMyCommand.Connection = new SqlConnection(myConnectionString);
        //        cmdMyCommand.Connection.Open();
        //        cmdMyCommand.CommandTimeout = 0;
        //        cmdMyCommand.CommandType = CommandType.StoredProcedure;

        //        cmdMyCommand.CommandText = "prc_SetVariazioniBancaDati";
        //        cmdMyCommand.Parameters.Clear();
        //        cmdMyCommand.Parameters.Add("@DBOperation", SqlDbType.Int).Value = DBOperation;
        //        cmdMyCommand.Parameters.Add("@IDVARIAZIONE", SqlDbType.Int).Value = IDVARIAZIONE;
        //        cmdMyCommand.Parameters.Add("@IDENTE", SqlDbType.NVarChar).Value = IDENTE;
        //        cmdMyCommand.Parameters.Add("@TRIBUTO", SqlDbType.NVarChar).Value = TRIBUTO;
        //        cmdMyCommand.Parameters.Add("@IDCAUSALE", SqlDbType.Int).Value = IDCAUSALE;
        //        cmdMyCommand.Parameters.Add("@FOGLIO", SqlDbType.NVarChar).Value = FOGLIO;
        //        cmdMyCommand.Parameters.Add("@NUMERO", SqlDbType.NVarChar).Value = NUMERO;
        //        cmdMyCommand.Parameters.Add("@SUBALTERNO", SqlDbType.NVarChar).Value = SUBALTERNO;
        //        cmdMyCommand.Parameters.Add("@DATAVARIAZIONE", SqlDbType.DateTime).Value = DATAVARIAZIONE;
        //        cmdMyCommand.Parameters.Add("@OPERATORE", SqlDbType.NVarChar).Value = OPERATORE;
        //        cmdMyCommand.Parameters.Add("@IDOGGETTOTRIBUTI", SqlDbType.Int).Value = IDOGGETTOTRIBUTI;
        //        cmdMyCommand.Parameters.Add("@DATATRATTATO", SqlDbType.DateTime).Value = DATATRATTATO;

        //        string sValParametri = Costanti.GetValParamCmd(cmdMyCommand);
        //        Log.Debug("SetModificheTributarie::SQL::" + cmdMyCommand.CommandText + "::VALUES::" + sValParametri);
        //        // eseguo la query
        //        cmdMyCommand.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch (Exception Err)
        //    {
        //        Log.Debug("SetModificheTributarie::si è verificato il seguente errore::", Err);
        //        return false;
        //    }
        //    finally
        //    {
        //        cmdMyCommand.Connection.Close();
        //        cmdMyCommand.Dispose();
        //    }
        //}
    }

    /*public class DBEngineFactory
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(DBEngineFactory));
	
		public static DAL.DBEngine GetDBEngine(string StringConnection)
		{
			DAL.DALEnum.eDBProvider dbProvider_;
			DAL.DBEngine dbEngine_;
			
			dbProvider_ = DAL.DALEnum.eDBProvider.SqlClient;
			dbEngine_ = new DAL.DBEngine(StringConnection, dbProvider_);
			
			return dbEngine_;			
		}		
	}*/
    // *** ***
}