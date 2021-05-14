using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using log4net;
using RemotingInterfaceMotoreTarsu.MotoreTarsuVARIABILE.Oggetti;
using CatastoInterface;

namespace Utility
{
    /// <summary>
    /// Classe di utilità generale per le costanti
    /// </summary>
    /// <revisionHistory>
    /// <revision date="12/04/2019">
    /// <strong>Qualificazione AgID-analisi_rel01</strong>
    /// <em>Analisi eventi</em>
    /// </revision>
    /// </revisionHistory>
    public class Costanti
    {
        #region Valori
        /// <summary>
        /// 
        /// </summary>
        public const int INIT_VALUE_NUMBER = -1;
        /// <summary>
        /// 
        /// </summary>
        public const string INIT_VALUE_NUMBER_STRING = "-1";
        /// <summary>
        /// 
        /// </summary>
        public const string INIT_VALUE_STRING = "";
        /// <summary>
        /// 
        /// </summary>
        public const bool INIT_VALUE_BOOL = false;
        /// <summary>
        /// 
        /// </summary>
        public const int VALUE_NUMBER_ZERO = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int VALUE_NUMBER_UNO = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int VALUE_INCREMENT = 1;
        #endregion
        #region Azioni
        /// <summary>
        /// 
        /// </summary>
        public const int AZIONE_NEW = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int AZIONE_UPDATE = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int AZIONE_DELETE = 2;
        /// <summary>
        /// 
        /// </summary>
        public const int AZIONE_LETTURA = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int AZIONE_SELEZIONE = 4;
        #endregion
        #region Tributi
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_ICI = "8852";
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_TARSU = "0434";
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_H2O = "9000";
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_OSAP = "0453";
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_SCUOLE = "9253";
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_IVA = "9800";
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_TASI = "TASI";
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_OccupazionePermanente = "3931";
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_OccupazioneTemporanea = "3932";
        /// <summary>
        /// 
        /// </summary>
        public const string TRIBUTO_Accertamento = "9999";
        #endregion
        #region Sesso
        /// <summary>
        /// 
        /// </summary>
        public const string MASCHIO = "M";
        /// <summary>
        /// 
        /// </summary>
        public const string FEMMINA = "F";
        /// <summary>
        /// 
        /// </summary>
        public const string PERSONAGIURIDICA = "G";
        #endregion
        #region TASI Inquilino
        /// <summary>
        /// 20150430 - 
        /// </summary>
        public const string TIPOTASI_PROPRIETARIO = "P";
        /// <summary>
        /// 
        /// </summary>
        public const string TIPOTASI_INQUILINO = "I";
        #endregion
        /// <summary>
        /// Classe per la gestione delle tipologie di argomenti degli eventi
        /// </summary>
        public class LogEventArgument
        {
            /// <summary>
            /// Evento su anagrafe
            /// </summary>
            public string Anagrafica { get { return "Anagrafica"; } }
            /// <summary>
            /// Evento su immobile
            /// </summary>
            public string Immobile { get { return "Immobile"; } }
            /// <summary>
            /// Evento su calcolo ruolo massivo o puntuale
            /// </summary>
            public string Elaborazioni { get { return "Ruolo"; } }
            /// <summary>
            /// Evento su pagamento
            /// </summary>
            public string Pagamento { get { return "Pagamento"; } }
            /// <summary>
            /// Evento su accertamento
            /// </summary>
            public string Provvedimenti { get { return "Accertamento"; } }
            /// <summary>
            /// Evento su avviso ordinario
            /// </summary>
            public string Sgravio { get { return "RettificaAvviso"; } }
            /// <summary>
            /// Evento su incasso accertamento
            /// </summary>
            public string Rateizzazione { get { return "Rateizzazione"; } }
        }
        /// <summary>
        /// Funzione per la scrittura unica delle query SQL nei log
        /// </summary>
        /// <param name="cmdMyCommand">SqlCommand connessione, query e parametri</param>
        /// <returns>string stringa da scrivere nel log</returns>
        public static string LogQuery(SqlCommand cmdMyCommand)
        {
            try
            {
                return "ExecQuery.query->con=" + cmdMyCommand.Connection.Database + " text=" + cmdMyCommand.CommandText + " " + GetValParamCmd(cmdMyCommand);
            }
            catch (Exception ex)
            {
                return "LogQuery.errore:" + ex.Message;
            }
        }
        /// <summary>
        /// Funzione per la scrittura unica delle query MySQL nei log
        /// </summary>
        /// <param name="cmdMyCommand">SqlCommand connessione, query e parametri</param>
        /// <returns>string stringa da scrivere nel log</returns>
        public static string LogQuery(MySql.Data.MySqlClient.MySqlCommand cmdMyCommand)
        {
            try
            {
                return "ExecQuery.query->con=" + cmdMyCommand.Connection.Database + " text=" + cmdMyCommand.CommandText + " " + GetValParamCmd(cmdMyCommand);
            }
            catch (Exception ex)
            {
                return "LogQuery.errore:" + ex.Message;
            }
        }
        /// <summary>
        /// Funzione per la composizione dei parametri della query SQL per il log
        /// </summary>
        /// <param name="MyCMD">SqlCommand parametri</param>
        /// <returns>string stringa formattata da scrivere</returns>
        public static string GetValParamCmd(SqlCommand MyCMD)
        {
            string sReturn = string.Empty;
            int x;
            for (x = 0; (x <= (MyCMD.Parameters.Count - 1)); x++)
            {
                sReturn += MyCMD.Parameters[x].ParameterName + "=";
                if (((MyCMD.Parameters[x].DbType == DbType.String) || (MyCMD.Parameters[x].DbType == DbType.DateTime)))
                {
                    sReturn += "\'" + MyCMD.Parameters[x].Value + "\',";
                }
                else
                {
                    sReturn += MyCMD.Parameters[x].Value + ",";
                }
            }
            return sReturn;
        }
        /// <summary>
        /// Funzione per la composizione dei parametri della query MySQL per il log
        /// </summary>
        /// <param name="MyCMD">SqlCommand parametri</param>
        /// <returns>string stringa formattata da scrivere</returns>
        public static string GetValParamCmd(MySql.Data.MySqlClient.MySqlCommand MyCMD)
        {
            string sReturn = string.Empty;
            int x;
            for (x = 0; (x <= (MyCMD.Parameters.Count - 1)); x++)
            {
                sReturn += MyCMD.Parameters[x].ParameterName + "=";
                if (((MyCMD.Parameters[x].DbType == DbType.String) || (MyCMD.Parameters[x].DbType == DbType.DateTime)))
                {
                    sReturn += "\'" + MyCMD.Parameters[x].Value + "\',";
                }
                else
                {
                    sReturn += MyCMD.Parameters[x].Value + ",";
                }
            }
            return sReturn;
        }
        /// <summary>
        /// Funzione per la creazione di una directory quando mancante
        /// </summary>
        /// <param name="DirPath">string directory da controllare</param>
        public static void CreateDir(string DirPath)
        {
            try
            {
                if (!System.IO.Directory.Exists(DirPath))
                    System.IO.Directory.CreateDirectory(DirPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
    /// <summary>
    /// Classe di utilità generale per la manipolazione dei dati su database IMU
    /// </summary>
    public class DichManagerICI //: DBManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DichManagerICI));
        private string DBType;
        private string ConnectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeDB"></param>
        /// <param name="myConnectionString"></param>
        public DichManagerICI(string TypeDB, string myConnectionString)
        {
            DBType = TypeDB;
            ConnectionString = myConnectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        public struct TestataRow
        {
            /// <summary>
            /// 
            /// </summary>
            public int ID;
            /// <summary>
            /// 
            /// </summary>
            public string Ente;
            /// <summary>
            /// 
            /// </summary>
            public int NumeroDichiarazione;
            /// <summary>
            /// 
            /// </summary>
            public string AnnoDichiarazione;
            /// <summary>
            /// 
            /// </summary>
            public string NumeroProtocollo;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataProtocollo;
            /// <summary>
            /// 
            /// </summary>
            public string TotaleModelli;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataInizio;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataFine;
            /// <summary>
            /// 
            /// </summary>
            public int IDContribuente;
            /// <summary>
            /// 
            /// </summary>
            public int IDDenunciante;
            /// <summary>
            /// 
            /// </summary>
            public bool Bonificato;
            /// <summary>
            /// 
            /// </summary>
            public bool Annullato;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataInizioValidità;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataFineValidità;
            /// <summary>
            /// 
            /// </summary>
            public string Operatore;
            /// <summary>
            /// 
            /// </summary>
            public bool Storico;
            /// <summary>
            /// 
            /// </summary>
            public int IDQuestionario;
            /// <summary>
            /// 
            /// </summary>
            public int IDProvenienza;
            /// <summary>
            /// 
            /// </summary>
            public OggettiRow[] listOggetti;
        }
        /// <summary>
        /// 
        /// </summary>
        public struct OggettiRow
        {
            /// <summary>
            /// 
            /// </summary>
            public int ID;
            /// <summary>
            /// 
            /// </summary>
            public string Ente;
            /// <summary>
            /// 
            /// </summary>
            public int IdTestata;
            /// <summary>
            /// 
            /// </summary>
            public string NumeroOrdine;
            /// <summary>
            /// 
            /// </summary>
            public string NumeroModello;
            /// <summary>
            /// 
            /// </summary>
            public string CodUI;
            /// <summary>
            /// 
            /// </summary>
            public int TipoImmobile;
            /// <summary>
            /// 
            /// </summary>
            public int PartitaCatastale;
            /// <summary>
            /// 
            /// </summary>
            public string Foglio;
            /// <summary>
            /// 
            /// </summary>
            public string Numero;
            /// <summary>
            /// 
            /// </summary>
            public int Subalterno;
            /// <summary>
            /// 
            /// </summary>
            public int Caratteristica;
            /// <summary>
            /// 
            /// </summary>
            public string Sezione;
            /// <summary>
            /// 
            /// </summary>
            public string NumeroProtCatastale;
            /// <summary>
            /// 
            /// </summary>
            public string AnnoDenunciaCatastale;
            /// <summary>
            /// 
            /// </summary>
            public string CodCategoriaCatastale;
            /// <summary>
            /// 
            /// </summary>
            public string CodClasse;
            /// <summary>
            /// 
            /// </summary>
            public string CodRendita;
            /// <summary>
            /// 
            /// </summary>
            public bool Storico;
            /// <summary>
            /// 
            /// </summary>
            public decimal ValoreImmobile;
            /// <summary>
            /// 
            /// </summary>
            public int IDValuta;
            /// <summary>
            /// 
            /// </summary>
            public bool FlagValoreProvv;
            /// <summary>
            /// 
            /// </summary>
            public int CodComune;
            /// <summary>
            /// 
            /// </summary>
            public string Comune;
            /// <summary>
            /// 
            /// </summary>
            public string CodVia;
            /// <summary>
            /// 
            /// </summary>
            public string Via;
            /// <summary>
            /// 
            /// </summary>
            public int NumeroCivico;
            /// <summary>
            /// 
            /// </summary>
            public string EspCivico;
            /// <summary>
            /// 
            /// </summary>
            public string Scala;
            /// <summary>
            /// 
            /// </summary>
            public string Interno;
            /// <summary>
            /// 
            /// </summary>
            public string Piano;
            /// <summary>
            /// 
            /// </summary>
            public string Barrato;
            /// <summary>
            /// 
            /// </summary>
            public int NumeroEcografico;
            /// <summary>
            /// 
            /// </summary>
            public int TitoloAcquisto;
            /// <summary>
            /// 
            /// </summary>
            public int TitoloCessione;
            /// <summary>
            /// 
            /// </summary>
            public string DescrUffRegistro;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataInizioValidità;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataFineValidità;
            /// <summary>
            /// 
            /// </summary>
            public bool Bonificato;
            /// <summary>
            /// 
            /// </summary>
            public bool Annullato;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataUltimaModifica;
            /// <summary>
            /// 
            /// </summary>
            public string Operatore;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataInizio;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataFine;
            /// <summary>
            /// 
            /// </summary>
            public int IDImmobilePertinente;
            /// <summary>
            /// 
            /// </summary>
            public string NoteIci;
            /// <summary>
            /// 
            /// </summary>
            public string Zona;
            /// <summary>
            /// 
            /// </summary>
            public decimal Rendita;
            /// <summary>
            /// 
            /// </summary>
            public decimal Consistenza;
            /// <summary>
            /// 
            /// </summary>
            public bool ExRurale;
            /// <summary>
            /// 
            /// </summary>
            public string PertinenzaDifoglio;
            /// <summary>
            /// 
            /// </summary>
            public string PertinenzaDiNumero;
            /// <summary>
            /// 
            /// </summary>
            public int PertinenzaDiSub;
            /// <summary>
            /// 
            /// </summary>
            public DettaglioTestataRow oDettaglio;
        }
        /// <summary>
        /// Definizione oggetto DettaglioTestata
        /// </summary>
        /// <revisionHistory>
        /// <revisionHistory>
        /// <revision date="26/06/2012">
        /// <strong>IMU</strong>
        /// </revision>
        /// </revisionHistory>
        /// <revision date="09/05/214">
        /// <strong>TASI</strong>
        /// </revision>
        /// </revisionHistory>
        /// <revisionHistory>
        /// <revision date="12/04/2019">
        /// <strong>Qualificazione AgID-analisi_rel01</strong>
        /// <em>Analisi eventi</em>
        /// </revision>
        /// </revisionHistory>
        public class DettaglioTestataRow
        {
            /// <summary>
            /// 
            /// </summary>
            public int ID = -1;
            /// <summary>
            /// 
            /// </summary>
            public string Ente = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public int IdTestata = -1;
            /// <summary>
            /// 
            /// </summary>
            public string NumeroOrdine = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public string NumeroModello = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public int IdOggetto = -1;
            /// <summary>
            /// 
            /// </summary>
            public int IdSoggetto = -1;
            /// <summary>
            /// 
            /// </summary>
            public int TipoUtilizzo = -1;
            /// <summary>
            /// 
            /// </summary>
            public int TipoPossesso = -1;
            /// <summary>
            /// 
            /// </summary>
            public decimal PercPossesso = 0;
            /// <summary>
            /// 
            /// </summary>
            public int MesiPossesso = 0;
            /// <summary>
            /// 
            /// </summary>
            public int MesiEsclusioneEsenzione = 0;
            /// <summary>
            /// 
            /// </summary>
            public int MesiRiduzione = 0;
            /// <summary>
            /// 
            /// </summary>
            public decimal ImpDetrazAbitazPrincipale = 0;
            /// <summary>
            /// 
            /// </summary>
            public bool Contitolare = false;
            /// <summary>
            /// 
            /// </summary>
            public int AbitazionePrincipale = 0;
            /// <summary>
            /// 
            /// </summary>
            public bool Bonificato = false;
            /// <summary>
            /// 
            /// </summary>
            public bool Annullato = false;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataInizioValidità = DateTime.MaxValue;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataFineValidità = DateTime.MaxValue;
            /// <summary>
            /// 
            /// </summary>
            public string Operatore = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public int Riduzione = 0;
            /// <summary>
            /// 
            /// </summary>
            public int Possesso = 0;
            /// <summary>
            /// 
            /// </summary>
            public int EsclusioneEsenzione = 0;
            /// <summary>
            /// 
            /// </summary>
            public int NumeroUtilizzatori = 0;
            /// <summary>
            /// 
            /// </summary>
            public int AbitazionePrincipaleAttuale = 0;
            /// <summary>
            /// 
            /// </summary>
            public bool ColtivatoreDiretto = false;
            /// <summary>
            /// 
            /// </summary>
            public int NumeroFigli = 0;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataUltimaModifica = DateTime.MaxValue;
            /// <summary>
            /// 
            /// </summary>
            public CaricoFigliRow[] ListCaricoFigli = null;
        }
        /// <summary>
        /// 
        /// </summary>
        public struct CaricoFigliRow
        {
            /// <summary>
            /// 
            /// </summary>
            public int IdDettaglioTestata;
            /// <summary>
            /// 
            /// </summary>
            public int nFiglio;
            /// <summary>
            /// 
            /// </summary>
            public double Percentuale;
        }
        //*** ***
        /// <summary>
        /// Definizione oggetto Versamento
        /// </summary>
        /// <revisionHistory>
        /// <revision date="28/08/2012">
        /// <strong>IMU adeguamento per importi statali</strong>
        /// </revision>
        /// <revisionHistory>
        /// <revision date="22/04/2013">
        /// <strong>aggiornamento IMU</strong>
        /// </revision>
        /// </revisionHistory>
        /// </revisionHistory>
        /// <revisionHistory>
        /// <revision date="30/06/2014">
        /// <strong>TASI</strong>
        /// </revision>
        /// </revisionHistory>
        /// <revisionHistory>
        /// <revision date="12/04/2019">
        /// <strong>Qualificazione AgID-analisi_rel01</strong>
        /// <em>Analisi eventi</em>
        /// </revision>
        /// </revisionHistory>
        public class VersamentiRow
        {
            /// <summary>
            /// 
            /// </summary>
            public int ID = -1;
            /// <summary>
            /// 
            /// </summary>
            public string Ente = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public int IdAnagrafico = -1;
            /// <summary>
            /// 
            /// </summary>
            public string CodTributo = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public string AnnoRiferimento = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public string CodiceFiscale = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public string PartitaIva = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoPagato = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataPagamento = DateTime.MaxValue;
            /// <summary>
            /// 
            /// </summary>
            public string NumeroBollettino = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public int NumeroFabbricatiPosseduti = default(int);
            /// <summary>
            /// 
            /// </summary>
            public bool Acconto = false;
            /// <summary>
            /// 
            /// </summary>
            public bool Saldo = false;
            /// <summary>
            /// 
            /// </summary>
            public bool RavvedimentoOperoso = false;
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoTerreni = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoAreeFabbric = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoAbitazPrincipale = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoAltrifabbric = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal DetrazioneAbitazPrincipale = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoTerreniStatale = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoAreeFabbricStatale = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoAltrifabbricStatale = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoFabRurUsoStrum = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoFabRurUsoStrumStatale = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoUsoProdCatD = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoUsoProdCatDStatale = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public string ContoCorrente = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public string ComuneUbicazioneImmobile = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public string ComuneIntestatario = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public bool Bonificato = true;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataInizioValidità = DateTime.MaxValue;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataFineValidità = DateTime.MaxValue;
            /// <summary>
            /// 
            /// </summary>
            public string Operatore = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public bool Annullato = false;
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoSoprattassa = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoPenaPecuniaria = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public decimal Interessi = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public bool Violazione = false;
            /// <summary>
            /// 
            /// </summary>
            public int IDProvenienza = default(int);
            /// <summary>
            /// 
            /// </summary>
            public string Provenienza = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public string NumeroAttoAccertamento = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataProvvedimentoViolazione = DateTime.MaxValue;
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoPagatoArrotondamento = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataRiversamento = DateTime.MaxValue;
            /// <summary>
            /// 
            /// </summary>
            public bool FlagFabbricatiExRurali = false;
            /// <summary>
            /// 
            /// </summary>
            public string NumeroProvvedimentoViolazione = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public decimal ImportoImposta = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public string Note = string.Empty;
            /// <summary>
            /// 
            /// </summary>
            public decimal DetrazioneStatale = default(decimal);
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataVariazione = DateTime.MaxValue;
        }
        /// <summary>
        /// 
        /// </summary>
        public const int Dichiarazione_FITTIZIA = 1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SetDettaglioTestataCompleta(DettaglioTestataRow Item, out int id)
        {
            bool retVal = false;
            id = 0;
            try
            {
                //*** 20140509 - TASI ***
                retVal = SetDettaglioTestata((Item.ID > 0 ? Costanti.AZIONE_UPDATE : Costanti.AZIONE_NEW), Item, out id);
                //*** ***
                if (retVal == true)
                {
                    retVal = SetPercentualeCaricoFigli(Costanti.AZIONE_DELETE, Item.ListCaricoFigli, id);
                    //*** 20120629 - IMU ***
                    if (Item.ListCaricoFigli != null)
                    {
                        //inserisco le % correnti
                        retVal = SetPercentualeCaricoFigli(Costanti.AZIONE_NEW, Item.ListCaricoFigli, id);
                    }
                    else
                    {
                        retVal = true;
                    }
                }
                //*** ***
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerICI::SetDettaglioTestataCompleta::", Err);
                retVal = false;
                id = 0;
            }
            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeOperation"></param>
        /// <param name="Item"></param>
        /// <param name="idTestata"></param>
        /// <returns></returns>
        public bool SetTestata(int TypeOperation, TestataRow Item, out int idTestata)
        {
            try
            {
                string sSQL = string.Empty;
                idTestata = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (TypeOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTESTATA_IU", "ID"
                                , "ente"
                                , "numeroDichiarazione"
                                , "annoDichiarazione"
                                , "numeroProtocollo"
                                , "dataProtocollo"
                                , "totaleModelli"
                                , "dataInizio"
                                , "dataFine"
                                , "idContribuente"
                                , "idDenunciante"
                                , "Bonificato"
                                , "annullato"
                                , "dataInizioValidità"
                                , "dataFineValidità"
                                , "operatore"
                                , "storico"
                                , "idProvenienza");
                            DataView dvMyDati = new DataView();
                            dvMyDati = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", Item.ID)
                                , ctx.GetParam("ente", Item.Ente)
                                , ctx.GetParam("numeroDichiarazione", Item.NumeroDichiarazione)
                                , ctx.GetParam("annoDichiarazione", Item.AnnoDichiarazione)
                                , ctx.GetParam("numeroProtocollo", Item.NumeroProtocollo == null ? DBNull.Value : (object)Item.NumeroProtocollo)
                                , ctx.GetParam("dataProtocollo", Item.DataProtocollo == DateTime.MinValue ? System.Data.SqlTypes.SqlDateTime.Null : (object)Item.DataProtocollo)
                                , ctx.GetParam("totaleModelli", Item.TotaleModelli == null ? DBNull.Value : (object)Item.TotaleModelli)
                                , ctx.GetParam("dataInizio", Item.DataInizio == DateTime.MinValue ? DBNull.Value : (object)Item.DataInizio.Date)
                                , ctx.GetParam("dataFine", Item.DataFine == DateTime.MinValue ? DBNull.Value : (object)Item.DataFine.Date)
                                , ctx.GetParam("idContribuente", Item.IDContribuente == 0 ? DBNull.Value : (object)Item.IDContribuente)
                                , ctx.GetParam("idDenunciante", Item.IDDenunciante == 0 ? DBNull.Value : (object)Item.IDDenunciante)
                                , ctx.GetParam("Bonificato", Item.Bonificato)
                                , ctx.GetParam("annullato", Item.Annullato)
                                , ctx.GetParam("dataInizioValidità", Item.DataInizioValidità)
                                , ctx.GetParam("dataFineValidità", Item.DataFineValidità == DateTime.MinValue ? DBNull.Value : (object)Item.DataFineValidità.Date)
                                , ctx.GetParam("operatore", Item.Operatore)
                                , ctx.GetParam("storico", Item.Storico)
                                , ctx.GetParam("idProvenienza", Item.IDProvenienza)
                                );
                            foreach (DataRowView myRow in dvMyDati)
                            {
                                idTestata = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTESTATA_D", "ID");
                            ctx.ExecuteNonQuery(sSQL, ctx.GetParam("ID", Item.ID));
                            break;
                    }
                    ctx.Dispose();
                }
                return true;
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerICI::SetTestata::", Err);
                idTestata = 0;
                return false;
            }
        }
        /// <summary>
        /// Funzione per l'inserimento di Oggetto
        /// </summary>
        /// <param name="TypeOperation">int tipo di operazione da gestire</param>
        /// <param name="myItem">OggettiRow oggetto da gestire</param>
        /// <param name="IdTestata">int id testata</param>
        /// <param name="idOggetto">out int identificativo riga</param>
        /// <returns>bool false in caso di errore altrimenti true</returns>
        /// <revisionHistory>
        /// <revision date="12/04/2019">
        /// <strong>Qualificazione AgID-analisi_rel01</strong>
        /// <em>Analisi eventi</em>
        /// </revision>
        /// </revisionHistory>
        public bool SetOggetti(int TypeOperation, OggettiRow myItem, int IdTestata, out int idOggetto)
        {
            try
            {
                idOggetto = 0;
                if (IdTestata > 0)
                    myItem.IdTestata = IdTestata;
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (TypeOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            if (TypeOperation == Costanti.AZIONE_UPDATE)
                            {
                                sSQL = "UPDATE TBLOGGETTI SET DATAULTIMAMODIFICA=@DATA WHERE ID=@ID";
                                ctx.ExecuteNonQuery(sSQL, ctx.GetParam("DATA", DateTime.Now), ctx.GetParam("ID", myItem.ID));
                                myItem.ID = -1; myItem.DataInizioValidità = DateTime.Now; myItem.DataUltimaModifica = DateTime.MaxValue;
                            }
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLOGGETTI_IU", "ID"
                                , "ente"
                                , "idTestata"
                                , "numeroOrdine"
                                , "numeroModello"
                                , "codUI"
                                , "tipoImmobile"
                                , "partitaCatastale"
                                , "foglio"
                                , "numero"
                                , "subalterno"
                                , "caratteristica"
                                , "sezione"
                                , "numeroProtCatastale"
                                , "annoDenunciaCatastale"
                                , "codCategoriaCatastale"
                                , "codClasse"
                                , "codRendita"
                                , "storico"
                                , "valoreImmobile"
                                , "idValuta"
                                , "flagValoreProvv"
                                , "codComune"
                                , "comune"
                                , "codVia"
                                , "via"
                                , "numeroCivico"
                                , "espCivico"
                                , "scala"
                                , "interno"
                                , "piano"
                                , "barrato"
                                , "numeroEcografico"
                                , "titoloAcquisto"
                                , "titoloCessione"
                                , "descrUffRegistro"
                                , "dataInizioValidità"
                                , "dataFineValidità"
                                , "dataInizio"
                                , "dataFine"
                                , "idImmobilePertinente"
                                , "Bonificato"
                                , "annullato"
                                , "dataUltimaModifica"
                                , "operatore"
                                , "noteIci"
                                , "zona"
                                , "rendita"
                                , "consistenza"
                                , "ExRurale");

                            DataView dvMyDati = new DataView();
                            dvMyDati = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myItem.ID)
                                , ctx.GetParam("ente", myItem.Ente)
                                , ctx.GetParam("idTestata", myItem.IdTestata)
                                , ctx.GetParam("numeroOrdine", myItem.NumeroOrdine)
                                , ctx.GetParam("numeroModello", myItem.NumeroModello)
                                , ctx.GetParam("codUI", myItem.CodUI)
                                , ctx.GetParam("tipoImmobile", (myItem.TipoImmobile == 0) ? DBNull.Value : (object)myItem.TipoImmobile)
                                , ctx.GetParam("partitaCatastale", myItem.PartitaCatastale)
                                , ctx.GetParam("foglio", myItem.Foglio)
                                , ctx.GetParam("numero", myItem.Numero)
                                , ctx.GetParam("subalterno", myItem.Subalterno)
                                , ctx.GetParam("caratteristica", myItem.Caratteristica)
                                , ctx.GetParam("sezione", myItem.Sezione)
                                , ctx.GetParam("numeroProtCatastale", myItem.NumeroProtCatastale)
                                , ctx.GetParam("annoDenunciaCatastale", myItem.AnnoDenunciaCatastale)
                                , ctx.GetParam("codCategoriaCatastale", (myItem.CodCategoriaCatastale == "0") ? DBNull.Value : (object)myItem.CodCategoriaCatastale)
                                , ctx.GetParam("codClasse", (myItem.CodClasse == "0") ? DBNull.Value : (object)myItem.CodClasse)
                                , ctx.GetParam("codRendita", myItem.CodRendita)
                                , ctx.GetParam("storico", (myItem.Storico == true) ? 1 : 0)
                                , ctx.GetParam("valoreImmobile", myItem.ValoreImmobile)
                                , ctx.GetParam("idValuta", myItem.IDValuta)
                                , ctx.GetParam("flagValoreProvv", (myItem.FlagValoreProvv == true) ? 1 : 0)
                                , ctx.GetParam("codComune", myItem.CodComune)
                                , ctx.GetParam("comune", myItem.Comune)
                                , ctx.GetParam("codVia", myItem.CodVia)
                                , ctx.GetParam("via", myItem.Via)
                                , ctx.GetParam("numeroCivico", myItem.NumeroCivico)
                                , ctx.GetParam("espCivico", myItem.EspCivico)
                                , ctx.GetParam("scala", myItem.Scala)
                                , ctx.GetParam("interno", myItem.Interno)
                                , ctx.GetParam("piano", myItem.Piano)
                                , ctx.GetParam("barrato", myItem.Barrato)
                                , ctx.GetParam("numeroEcografico", myItem.NumeroEcografico)
                                , ctx.GetParam("titoloAcquisto", myItem.TitoloAcquisto)
                                , ctx.GetParam("titoloCessione", myItem.TitoloCessione)
                                , ctx.GetParam("descrUffRegistro", myItem.DescrUffRegistro)
                                , ctx.GetParam("dataInizioValidità", myItem.DataInizioValidità)
                                , ctx.GetParam("dataFineValidità", myItem.DataFineValidità == DateTime.MinValue ? DateTime.MaxValue : (object)myItem.DataFineValidità.Date)
                                , ctx.GetParam("dataInizio", myItem.DataInizio.Date)
                                , ctx.GetParam("dataFine", myItem.DataFine.Date)
                                , ctx.GetParam("idImmobilePertinente", myItem.IDImmobilePertinente)
                                , ctx.GetParam("Bonificato", (myItem.Bonificato == true) ? 1 : 0)
                                , ctx.GetParam("annullato", (myItem.Annullato == true) ? 1 : 0)
                                , ctx.GetParam("dataUltimaModifica", (myItem.DataUltimaModifica == DateTime.MinValue || myItem.DataUltimaModifica == DateTime.MaxValue) ? DateTime.Now : (object)myItem.DataUltimaModifica)
                                , ctx.GetParam("operatore", myItem.Operatore)
                                , ctx.GetParam("noteIci", myItem.NoteIci)
                                , ctx.GetParam("zona", myItem.Zona)
                                , ctx.GetParam("rendita", float.Parse(myItem.Rendita.ToString()))
                                , ctx.GetParam("consistenza", float.Parse(myItem.Consistenza.ToString()))
                                , ctx.GetParam("ExRurale", (myItem.ExRurale == true) ? 1 : 0));
                            foreach (DataRowView myRow in dvMyDati)
                            {
                                idOggetto = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLOGGETTI_D", "ID");
                            ctx.ExecuteNonQuery(sSQL, ctx.GetParam("ID", myItem.ID));
                            break;
                    }
                    ctx.Dispose();
                }
                return true;
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerICI::SetOggetti::", Err);
                idOggetto = 0;
                return false;
            }
        }
        /// <summary>
        /// Funzione per l'inserimento di DettaglioTestata
        /// </summary>
        /// <param name="TypeOperation">int tipo di operazione da gestire</param>
        /// <param name="myItem">DettaglioTestataRow oggetto da gestire</param>
        /// <param name="id">out int identificativo riga</param>
        /// <returns>bool false in caso di errore altrimenti true</returns>
        /// <revisionHistory>
        /// <revision date="12/04/2019">
        /// <strong>Qualificazione AgID-analisi_rel01</strong>
        /// <em>Analisi eventi</em>
        /// </revision>
        /// </revisionHistory>
        public bool SetDettaglioTestata(int TypeOperation, DettaglioTestataRow myItem, out int id)
        {
            try
            {
                id = 0;
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (TypeOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            if (TypeOperation == Costanti.AZIONE_UPDATE)
                            {
                                sSQL = "UPDATE TBLDETTAGLIOTESTATA SET DATAULTIMAMODIFICA=@DATA WHERE ID=@ID";
                                ctx.ExecuteNonQuery(sSQL, ctx.GetParam("DATA", DateTime.Now), ctx.GetParam("ID", myItem.ID));
                                myItem.ID = -1; myItem.DataInizioValidità = DateTime.Now; myItem.DataUltimaModifica = DateTime.MaxValue;
                            }
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLDETTAGLIOTESTATA_IU", "ID"
                                , "ente"
                                , "idTestata"
                                , "numeroOrdine"
                                , "numeroModello"
                                , "idOggetto"
                                , "idSoggetto"
                                , "IdTipoUtilizzo"
                                , "IdTipoPossesso"
                                , "percPossesso"
                                , "mesiPossesso"
                                , "mesiEsclusioneEsenzione"
                                , "mesiRiduzione"
                                , "impDetrazAbitazPrincipale"
                                , "contitolare"
                                , "abitazionePrincipale"
                                , "Bonificato"
                                , "annullato"
                                , "dataInizioValidità"
                                , "dataFineValidità"
                                , "operatore"
                                , "riduzione"
                                , "possesso"
                                , "esclusioneEsenzione"
                                , "abitazionePrincipaleAttuale"
                                , "numeroUtilizzatori"
                                , "ColtivatoreDiretto"
                                , "NumeroFigli"
                                , "dataUltimaModifica");

                            DataView dvMyDati = new DataView();
                            dvMyDati = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myItem.ID)
                                , ctx.GetParam("ente", myItem.Ente)
                                , ctx.GetParam("idTestata", myItem.IdTestata)
                                , ctx.GetParam("numeroOrdine", myItem.NumeroOrdine)
                                , ctx.GetParam("numeroModello", myItem.NumeroModello)
                                , ctx.GetParam("idOggetto", myItem.IdOggetto)
                                , ctx.GetParam("idSoggetto", myItem.IdSoggetto)
                                , ctx.GetParam("IdTipoUtilizzo", myItem.TipoUtilizzo == 0 ? 1 : (object)myItem.TipoUtilizzo)
                                , ctx.GetParam("IdTipoPossesso", myItem.TipoPossesso == 0 ? 1 : (object)myItem.TipoPossesso)
                                , ctx.GetParam("percPossesso", myItem.PercPossesso)
                                , ctx.GetParam("mesiPossesso", myItem.MesiPossesso)
                                , ctx.GetParam("mesiEsclusioneEsenzione", myItem.MesiEsclusioneEsenzione)
                                , ctx.GetParam("mesiRiduzione", myItem.MesiRiduzione)
                                , ctx.GetParam("impDetrazAbitazPrincipale", myItem.ImpDetrazAbitazPrincipale)
                                , ctx.GetParam("contitolare", myItem.Contitolare)
                                , ctx.GetParam("abitazionePrincipale", myItem.AbitazionePrincipale)
                                , ctx.GetParam("Bonificato", myItem.Bonificato)
                                , ctx.GetParam("annullato", myItem.Annullato)
                                , ctx.GetParam("dataInizioValidità", myItem.DataInizioValidità)
                                , ctx.GetParam("dataFineValidità", myItem.DataFineValidità == DateTime.MinValue ? DateTime.MaxValue : (object)myItem.DataFineValidità)
                                , ctx.GetParam("operatore", myItem.Operatore)
                                , ctx.GetParam("riduzione", myItem.Riduzione)
                                , ctx.GetParam("possesso", myItem.Possesso)
                                , ctx.GetParam("esclusioneEsenzione", myItem.EsclusioneEsenzione)
                                , ctx.GetParam("abitazionePrincipaleAttuale", myItem.AbitazionePrincipaleAttuale)
                                , ctx.GetParam("numeroUtilizzatori", myItem.NumeroUtilizzatori)
                                , ctx.GetParam("ColtivatoreDiretto", myItem.ColtivatoreDiretto)
                                , ctx.GetParam("NumeroFigli", myItem.NumeroFigli)
                                , ctx.GetParam("dataUltimaModifica", (myItem.DataUltimaModifica == DateTime.MinValue || myItem.DataUltimaModifica == DateTime.MaxValue) ? DateTime.MaxValue : (object)myItem.DataUltimaModifica));
                            foreach (DataRowView myRow in dvMyDati)
                            {
                                id = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLDETTAGLIOTESTATA_D", "ID", "IDOGGETTO");
                            ctx.ExecuteNonQuery(sSQL, ctx.GetParam("ID", myItem.ID), ctx.GetParam("IDOGGETTO", myItem.IdOggetto));
                            break;
                    }
                    ctx.Dispose();
                }
                return true;
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerICI::SetDettaglioTestata::", Err);
                id = 0;
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeOperation"></param>
        /// <param name="ListCarico"></param>
        /// <param name="IdDettaglio"></param>
        /// <returns></returns>
        public bool SetPercentualeCaricoFigli(int TypeOperation, CaricoFigliRow[] ListCarico, int IdDettaglio)
        {
            bool retVal = false;
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (TypeOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            foreach (CaricoFigliRow Item in ListCarico)
                            {
                                sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLPERCENTUALECARICOFIGLI_IU", "IDDETTAGLIOTESTATA"
                                    , "NFIGLIO"
                                    , "PERCENTUALE");

                                int nRet = ctx.ExecuteNonQuery(sSQL, ctx.GetParam("IDDETTAGLIOTESTATA", IdDettaglio)
                                    , ctx.GetParam("NFIGLIO", Item.nFiglio)
                                    , ctx.GetParam("PERCENTUALE", Item.Percentuale));
                            }
                            retVal = true;
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLPERCENTUALECARICOFIGLI_D", "IDDETTAGLIOTESTATA");

                            IdDettaglio = ctx.ExecuteNonQuery(sSQL, ctx.GetParam("IDDETTAGLIOTESTATA", IdDettaglio));
                            break;
                    }
                    ctx.Dispose();
                }
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerICI::SetPercentualeCaricoFigli::", Err);
                retVal = false;
            }
            return retVal;
        }
        /// <summary>
        /// Funzione per l'inserimento del versamento
        /// </summary>
        /// <param name="TypeOperation">int tipo di operazione da gestire</param>
        /// <param name="myItem">VersamentiRow oggetto da gestire</param>
        /// <param name="idVersamento">out int identificativo riga</param>
        /// <returns>bool false in caso di errore altrimenti true</returns>
        /// <revisionHistory>
        /// <revision date="12/04/2019">
        /// <strong>Qualificazione AgID-analisi_rel01</strong>
        /// <em>Analisi eventi</em>
        /// </revision>
        /// </revisionHistory>
        public bool SetVersamenti(int TypeOperation, VersamentiRow myItem, out int idVersamento)
        {
            try
            {
                idVersamento = 0;
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (TypeOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            if (TypeOperation == Costanti.AZIONE_UPDATE)
                            {
                                sSQL = "UPDATE TBLVERSAMENTI SET DATA_VARIAZIONE=@DATA WHERE ID=@ID";
                                ctx.ExecuteNonQuery(sSQL, ctx.GetParam("DATA", DateTime.Now), ctx.GetParam("ID", myItem.ID));
                                myItem.ID = -1; myItem.DataInizioValidità = DateTime.Now; myItem.DataVariazione = DateTime.MaxValue;
                            }
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLVERSAMENTI_IU", "ID"
                                , "ente", "idAnagrafico", "annoRiferimento"
                                , "codiceFiscale", "partitaIva"
                                , "importoPagato", "dataPagamento"
                                , "numeroBollettino", "numeroFabbricatiPosseduti"
                                , "acconto", "saldo", "ravvedimentoOperoso"
                                , "impoTerreni", "importoAreeFabbric", "importoAbitazPrincipale", "importoAltrifabbric"
                                , "detrazioneAbitazPrincipale"
                                , "contoCorrente", "comuneUbicazioneImmobile", "comuneIntestatario", "Bonificato"
                                , "dataInizioValidità", "dataFineValidità"
                                , "operatore"
                                , "annullato"
                                , "importoSoprattassa", "importoPenaPecuniaria", "interessi", "violazione"
                                , "idProvenienza"
                                , "FlagFabbricatiExRurali"
                                , "dataProvvedimentoViolazione", "NumeroProvvedimentoViolazione", "numeroAttoAccertamento"
                                , "ImportoPagatoArrotondamento"
                                , "DataRiversamento"
                                , "ImportoImposta"
                                , "Note"
                                , "DetrazioneStatale"
                                , "importoFabRurUsoStrum"
                                , "IMPORTOTERRENISTATALE", "importoAreeFabbricStatale", "importoAltrifabbricStatale", "importoFabRurUsoStrumStatale"
                                , "importoUsoProdCatD", "importoUsoProdCatDStatale"
                                , "Tributo"
                                , "PROVENIENZA", "DATA_VARIAZIONE");

                            DataView dvMyDati = new DataView();

                            dvMyDati = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myItem.ID)
                                 , ctx.GetParam("ente", myItem.Ente)
                                 , ctx.GetParam("idAnagrafico", myItem.IdAnagrafico)
                                 , ctx.GetParam("annoRiferimento", myItem.AnnoRiferimento)
                                 , ctx.GetParam("codiceFiscale", myItem.CodiceFiscale)
                                 , ctx.GetParam("partitaIva", myItem.PartitaIva)
                                 , ctx.GetParam("importoPagato", myItem.ImportoPagato)
                                 , ctx.GetParam("dataPagamento", myItem.DataPagamento)
                                 , ctx.GetParam("numeroBollettino", myItem.NumeroBollettino)
                                 , ctx.GetParam("numeroFabbricatiPosseduti", myItem.NumeroFabbricatiPosseduti)
                                 , ctx.GetParam("acconto", myItem.Acconto)
                                 , ctx.GetParam("saldo", myItem.Saldo)
                                 , ctx.GetParam("ravvedimentoOperoso", myItem.RavvedimentoOperoso)
                                 , ctx.GetParam("impoTerreni", myItem.ImportoTerreni)
                                 , ctx.GetParam("importoAreeFabbric", myItem.ImportoAreeFabbric)
                                 , ctx.GetParam("importoAbitazPrincipale", myItem.ImportoAbitazPrincipale)
                                 , ctx.GetParam("importoAltrifabbric", myItem.ImportoAltrifabbric)
                                 , ctx.GetParam("detrazioneAbitazPrincipale", myItem.DetrazioneAbitazPrincipale)
                                 , ctx.GetParam("contoCorrente", myItem.ContoCorrente)
                                 , ctx.GetParam("comuneUbicazioneImmobile", myItem.ComuneUbicazioneImmobile)
                                 , ctx.GetParam("comuneIntestatario", myItem.ComuneIntestatario)
                                 , ctx.GetParam("Bonificato", myItem.Bonificato)
                                 , ctx.GetParam("dataInizioValidità", myItem.DataInizioValidità == DateTime.MinValue ? DBNull.Value : (object)myItem.DataInizioValidità)
                                 , ctx.GetParam("dataFineValidità", myItem.DataFineValidità == DateTime.MinValue ? DateTime.MaxValue : (object)myItem.DataFineValidità)
                                 , ctx.GetParam("operatore", myItem.Operatore)
                                 , ctx.GetParam("annullato", myItem.Annullato)
                                 , ctx.GetParam("importoSoprattassa", myItem.ImportoSoprattassa)
                                 , ctx.GetParam("importoPenaPecuniaria", myItem.ImportoPenaPecuniaria)
                                 , ctx.GetParam("interessi", myItem.Interessi)
                                 , ctx.GetParam("violazione", myItem.Violazione)
                                 , ctx.GetParam("idProvenienza", myItem.IDProvenienza)
                                 , ctx.GetParam("FlagFabbricatiExRurali", myItem.FlagFabbricatiExRurali)
                                 , ctx.GetParam("dataProvvedimentoViolazione", myItem.DataProvvedimentoViolazione.ToShortDateString())
                                 , ctx.GetParam("NumeroProvvedimentoViolazione", myItem.NumeroProvvedimentoViolazione)
                                 , ctx.GetParam("numeroAttoAccertamento", myItem.NumeroAttoAccertamento)
                                 , ctx.GetParam("ImportoPagatoArrotondamento", myItem.ImportoPagatoArrotondamento)
                                 , ctx.GetParam("DataRiversamento", myItem.DataRiversamento == DateTime.MinValue ? (object)myItem.DataPagamento : (object)myItem.DataRiversamento)
                                 , ctx.GetParam("ImportoImposta", myItem.ImportoImposta)
                                 , ctx.GetParam("Note", myItem.Note)
                                 , ctx.GetParam("DetrazioneStatale", myItem.DetrazioneStatale)
                                 , ctx.GetParam("importoFabRurUsoStrum", myItem.ImportoFabRurUsoStrum)
                                 , ctx.GetParam("IMPORTOTERRENISTATALE", myItem.ImportoTerreniStatale)
                                 , ctx.GetParam("importoAreeFabbricStatale", myItem.ImportoAreeFabbricStatale)
                                 , ctx.GetParam("importoAltrifabbricStatale", myItem.ImportoAltrifabbricStatale)
                                 , ctx.GetParam("importoFabRurUsoStrumStatale", myItem.ImportoFabRurUsoStrumStatale)
                                 , ctx.GetParam("importoUsoProdCatD", myItem.ImportoUsoProdCatD)
                                 , ctx.GetParam("importoUsoProdCatDStatale", myItem.ImportoUsoProdCatDStatale)
                                 , ctx.GetParam("Tributo", myItem.CodTributo)
                                 , ctx.GetParam("Provenienza", myItem.Provenienza)
                                 , ctx.GetParam("data_variazione", myItem.DataVariazione == DateTime.MinValue ? DateTime.MaxValue : (object)myItem.DataFineValidità)
                                 );
                            foreach (DataRowView myRow in dvMyDati)
                            {
                                idVersamento = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLVERSAMENTI_D", "ID");
                            ctx.ExecuteNonQuery(sSQL, ctx.GetParam("ID", myItem.ID));
                            idVersamento = myItem.ID;
                            break;
                    }
                    ctx.Dispose();
                }
                return true;
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerICI::SetVersamenti::", Err);
                idVersamento = 0;
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="IdContribuente"></param>
        /// <returns></returns>
        public int ClearBancaDatiDich(string IdEnte, int IdContribuente)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBDich", "IDENTE", "IDCONTRIBUENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte), ctx.GetParam("IDCONTRIBUENTE", IdContribuente));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBTestata");
                    dvMyView = ctx.GetDataView(sSQL, "TBL");
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerICI::ClearBancaDatiDich::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="IdContribuente"></param>
        /// <returns></returns>
        public int ClearBancaDatiVersamenti(string IdEnte, int IdContribuente)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBVersamenti", "IDENTE", "IDCONTRIBUENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte), ctx.GetParam("IDCONTRIBUENTE", IdContribuente));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerICI::ClearBancaDatiVersamenti::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public int ClearBancaDatiTipoUtilizzo(string IdEnte)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBTipoUtilizzo", "IDENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerICI::ClearBancaDatiTipoUtilizzo::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public int ClearBancaDatiTipoPossesso(string IdEnte)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBTipoPossesso", "IDENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerICI::ClearBancaDatiTipoPossesso::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public int ClearBancaDatiTipoRendita(string IdEnte)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBTipoRendita", "IDENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerICI::ClearBancaDatiTipoRendita::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="Cod"></param>
        /// <param name="Descr"></param>
        /// <returns></returns>
        public int SetTipoUtilizzo(string IdEnte, string Cod, string Descr)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTIPOUTILIZZO_IU", "IDENTE"
                        , "ID"
                        , "DESCRIZIONE"
                    );
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte)
                        , ctx.GetParam("ID", Cod)
                        , ctx.GetParam("DESCRIZIONE", Descr)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerICI::SetTipoUtilizzo::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="Cod"></param>
        /// <param name="Descr"></param>
        /// <returns></returns>
        public int SetTipoPossesso(string IdEnte, string Cod, string Descr)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTIPOPOSSESSO_IU", "IDENTE"
                        , "ID"
                        , "DESCRIZIONE"
                    );
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte)
                        , ctx.GetParam("ID", Cod)
                        , ctx.GetParam("DESCRIZIONE", Descr)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerICI::SetTipoPossesso::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="Cod"></param>
        /// <param name="Sigla"></param>
        /// <param name="Descr"></param>
        /// <returns></returns>
        public int SetTipoRendita(string IdEnte, string Cod, string Sigla, string Descr)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TIPO_RENDITA_IU", "IDENTE"
                        , "ID"
                        , "SIGLA"
                        , "DESCRIZIONE"
                    );
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte)
                        , ctx.GetParam("ID", Cod)
                        , ctx.GetParam("SIGLA", Sigla)
                        , ctx.GetParam("DESCRIZIONE", Descr)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerICI::SetTipoRendita::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codice"></param>
        /// <returns></returns>
        public string GetCodRendita(string Codice)
        {
            string Id = "0";

            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetCodRendita", "CODICE");

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("CODICE", Codice));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = myRow["COD_RENDITA"].ToString();
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception Err)
            {
                Log.Debug("Si è verificato un errore in DichManagerICI::GetCodRendita::", Err);
                return "-1";
            }
        }
    }
    /// <summary>
    /// Classe di utilità generale per la manipolazione dei dati su database TARI
    /// </summary>
    public class DichManagerTARSU //: DBManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DichManagerTARSU));
        private string DBType;
        private string ConnectionString;
        private string ConnectionStringGOV;
        private string IdEnte;
        /// <summary>
        /// 
        /// </summary>
        public struct IdAvviso
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id;
            /// <summary>
            /// 
            /// </summary>
            public int IdContribuente;
            /// <summary>
            /// 
            /// </summary>
            public string NAvviso;
            /// <summary>
            /// 
            /// </summary>
            public DateTime DataEmissione;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeDB"></param>
        /// <param name="myConnectionString"></param>
        /// <param name="myConnectionStringGOV"></param>
        /// <param name="myEnte"></param>
        public DichManagerTARSU(string TypeDB, string myConnectionString, string myConnectionStringGOV, string myEnte)
        {
            DBType = TypeDB;
            ConnectionString = myConnectionString;
            ConnectionStringGOV = myConnectionStringGOV;
            IdEnte = myEnte;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myTestata"></param>
        /// <param name="IsFromVariabile"></param>
        /// <returns></returns>
        public int SetDichiarazione(ObjTestata myTestata, string IsFromVariabile)
        {
            int IdTestata = 0;
            try
            {
                // inserisco i dati della testata
                IdTestata = SetTestata(Costanti.AZIONE_NEW, myTestata);
                if ((IdTestata <= 0))
                {
                    Log.Debug("non ho inserito testata");
                    return 0;
                }
                myTestata.Id = IdTestata;
                Log.Debug("ho questo idtestata::" + IdTestata.ToString());
                //*** X UNIONE CON BANCADATI CMGC ***
                if (IsFromVariabile == "1")
                {
                    //inserisco i dati di tessera
                    IdTestata = SetTesseraCompleta(myTestata.oTessere, IdTestata);
                    if (IdTestata <= 0)
                    {
                        return 0;
                    }
                }
                else
                {
                    //inserisco i dati di immobile
                    IdTestata = SetDettaglioTestataCompleta(myTestata.oImmobili, IdTestata, -1);
                    if (IdTestata <= 0)
                    {
                        return 0;
                    }
                }
                return IdTestata;
            }
            catch (Exception Err)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetDichiarazione::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myListTessere"></param>
        /// <param name="IdTestata"></param>
        /// <returns></returns>
        public int SetTesseraCompleta(ObjTessera[] myListTessere, int IdTestata)
        {
            int IdTessera = 0;
            try
            {
                foreach (ObjTessera myItem in myListTessere)
                {
                    // memorizzo l'id testata di Riferimento
                    myItem.IdTestata = IdTestata;
                    myItem.Id = -1;
                    // richiamo la funzione di inserimento dettaglio testata
                    IdTessera = SetTessera(0, myItem, -1);
                    if ((IdTessera == 0))
                    {
                        return 0;
                    }
                    // memorizzo l'id inserito
                    myItem.Id = IdTessera;
                    // inserisco l'immobile se presente
                    if (!(myItem.oImmobili == null))
                    {
                        IdTessera = SetDettaglioTestataCompleta(myItem.oImmobili, IdTestata, myItem.Id);
                        if ((IdTessera <= 0))
                        {
                            return 0;
                        }
                    }
                    if (!(myItem.oRiduzioni == null))
                    {
                        // inserisco i dati di riduzione
                        if ((SetRidEseApplicate(Costanti.AZIONE_NEW, myItem.oRiduzioni, ObjRidEse.TIPO_RIDUZIONI, myItem.Id, -1) == 0))
                        {
                            return 0;
                        }
                    }
                    if (!(myItem.oDetassazioni == null))
                    {
                        // inserisco i dati di Esenzione
                        if ((SetRidEseApplicate(Costanti.AZIONE_NEW, myItem.oRiduzioni, ObjRidEse.TIPO_RIDUZIONI, myItem.Id, -1) == 0))
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }
            catch (Exception Err)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetTesseraCompleta::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myListDettaglioTestata"></param>
        /// <param name="IdTestata"></param>
        /// <param name="IdTessera"></param>
        /// <returns></returns>
        public int SetDettaglioTestataCompleta(ObjDettaglioTestata[] myListDettaglioTestata, int IdTestata, int IdTessera)
        {
            int IdDettTestata = 0;
            double MQTassabili = 0;
            try
            {
                // controllo se sono in nuovo inserimento
                foreach (ObjDettaglioTestata myItem in myListDettaglioTestata)
                {
                    myItem.IdTestata = IdTestata;
                    myItem.IdTessera = IdTessera;
                    myItem.Id = -1;
                    // controllo di avere i vani 
                    if (((myItem.nVani == 0) || (myItem.nMQ == 0)))
                    {
                        Log.Debug("non ho vani o mq");
                        return 0;
                    }
                    // richiamo la funzione di inserimento dettaglio testata
                    IdDettTestata = SetDettaglioTestata(Costanti.AZIONE_NEW, myItem);
                    if ((IdDettTestata == 0))
                    {
                        return 0;
                    }
                    // memorizzo l'id inserito
                    myItem.Id = IdDettTestata;
                    // inserisco i dati di oggetti
                    // *** 20130325 - gestione mq tassabili per TARES ***
                    if ((SetOggetti(myItem.oOggetti, myItem.Id, out MQTassabili) == 0))
                    {
                        return 0;
                    }
                    myItem.nMQTassabili = MQTassabili;
                    // *** ***
                    if (!(myItem.oRiduzioni == null))
                    {
                        // inserisco i dati di riduzione
                        if ((SetRidEseApplicate(Costanti.AZIONE_NEW, myItem.oRiduzioni, ObjRidEse.TIPO_RIDUZIONI, myItem.Id, -1) == 0))
                        {
                            return 0;
                        }
                    }
                    if (!(myItem.oDetassazioni == null))
                    {
                        // inserisco i dati di Esenzione
                        if ((SetRidEseApplicate(Costanti.AZIONE_NEW, myItem.oDetassazioni, ObjRidEse.TIPO_ESENZIONI, myItem.Id, -1) == 0))
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }
            catch (Exception Err)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetDettaglioTestataCompleta::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myListOggetti"></param>
        /// <param name="IdDettaglioTestata"></param>
        /// <param name="MQTassabili"></param>
        /// <returns></returns>
        public int SetOggetti(ObjOggetti[] myListOggetti, int IdDettaglioTestata, out double MQTassabili)
        {
            MQTassabili = 0;
            try
            {
                // costruisco la query
                foreach (ObjOggetti oMyOggetto in myListOggetti)
                {
                    oMyOggetto.IdDettaglioTestata = IdDettaglioTestata;
                    oMyOggetto.IdOggetto = -1;
                    oMyOggetto.tDataInserimento = DateTime.Now;
                    if ((SetOggetto(Costanti.AZIONE_NEW, oMyOggetto) <= 0))
                    {
                        return 0;
                    }
                }
                // *** 20130325 - gestione mq tassabili per TARES ***
                MQTassabili = SetMQTassabili(myListOggetti[0].IdDettaglioTestata);
                // *** ***
                return 1;
            }
            catch (Exception Err)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetOggetti::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oMyAvviso"></param>
        /// <returns></returns>
        public int SetCartella(RemotingInterfaceMotoreTarsu.MotoreTarsuVARIABILE.Oggetti.ObjAvviso oMyAvviso)
        {
            try
            {
                //aggiorno il codice cartella calcolato e la data di emissione
                oMyAvviso.ID = SetAvviso(oMyAvviso, -1, Utility.Costanti.AZIONE_UPDATE);
                if (oMyAvviso.ID <= 0)
                    return 0;
                //inserisco il dettaglio voci
                if (oMyAvviso.oDetVoci != null)
                {
                    foreach (RemotingInterfaceMotoreTarsu.MotoreTarsuVARIABILE.Oggetti.ObjDetVoci myDet in oMyAvviso.oDetVoci)
                    {
                        if (SetDetVoci(myDet, oMyAvviso.ID, -1, Utility.Costanti.AZIONE_NEW) <= 0)
                            return 0;
                    }
                }
                return oMyAvviso.ID;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetCartella::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oMyAvviso"></param>
        /// <param name="bIsFromVariabile"></param>
        /// <param name="bIsFromSgravio"></param>
        /// <returns></returns>
        public int SetAvvisoCompleto(ObjAvviso oMyAvviso, string bIsFromVariabile, bool bIsFromSgravio)
        {
            int nIdRif = 0;
            try
            {
                //aggiorno il codice cartella calcolato e la data di emissione
                oMyAvviso.ID = SetAvviso(oMyAvviso, -1, Costanti.AZIONE_UPDATE);
                if (oMyAvviso.ID <= 0)
                    return 0;
                //inserisco gli articoli
                //*** 20141211 - legami PF-PV ***
                if (oMyAvviso.oArticoli != null)
                {
                    int nIdRuolo;
                    foreach (ObjArticolo myArt in oMyAvviso.oArticoli)
                    {
                        myArt.IdAvviso = oMyAvviso.ID;
                        //memorizzo l'id che avrò tra i legami PF - PV, se arrivo da sgravio = id se arrivo da calcolo massivo = idoggetto
                        if (bIsFromSgravio)
                        {
                            nIdRif = myArt.Id;
                        }
                        else {
                            nIdRif = myArt.IdOggetto;
                        }
                        myArt.Id = -1;
                        nIdRuolo = SetArticoloCompleto(myArt, bIsFromVariabile, -1, Costanti.AZIONE_UPDATE);
                        Log.Debug("nIdRuolo=" + nIdRuolo.ToString());
                        if (nIdRuolo == 0)
                        {
                            Log.Debug("Si è verificato un errore in DichManagerTARSU::SetAvvisoCompleto::è stato restituiro un nIdRuolo=0");
                            return 0;
                        }
                        //per l'id PF appena inserito vado a registrare nei legami il corrispettivo idruolo generato sostituendolo all'idoggetto
                        if (myArt.TipoPartita == ObjArticolo.PARTEFISSA)
                        {
                            foreach (ObjArticolo myPF in oMyAvviso.oArticoli)
                            {
                                if (myPF.TipoPartita == ObjArticolo.PARTEVARIABILE && myPF.sVia != ObjArticolo.PARTEPRECEMESSO_DESCR)
                                { //i legami sono presenti solo sugli oggetti di tipo partevariabile che non sono GIÀ EMESSO
                                    foreach (ObjLegamePFPV myLeg in myPF.ListPFvsPV)
                                        if (myLeg.IdPF == nIdRif)
                                        {
                                            myLeg.IdPF = nIdRuolo;
                                            break;
                                        }
                                }
                            }
                        }
                        else {
                            if (myArt.TipoPartita == ObjArticolo.PARTEVARIABILE && myArt.sVia != ObjArticolo.PARTEPRECEMESSO_DESCR)
                            {
                                foreach (ObjLegamePFPV myLeg in myArt.ListPFvsPV)
                                    myLeg.IdPV = nIdRuolo;
                            }
                        }
                    }
                    //dopo aver inserito tutti gli articoli inserisco i legami PF-PV
                    foreach (ObjArticolo myArt in oMyAvviso.oArticoli)
                    {
                        if (myArt.TipoPartita == ObjArticolo.PARTEVARIABILE && myArt.sVia != ObjArticolo.PARTEPRECEMESSO_DESCR)
                        {
                            foreach (ObjLegamePFPV myLeg in myArt.ListPFvsPV)
                            {
                                if (SetLegamePFPV(myLeg, Costanti.AZIONE_UPDATE) <= 0)
                                {
                                    return 0;
                                }
                            }
                        }
                    }
                    //*** ***
                }
                //controllo se inserire i dati di tessera
                if (oMyAvviso.oTessere != null)
                {
                    foreach (ObjTessera myTes in oMyAvviso.oTessere)
                    {
                        //inserisco il legame tra tessera e avviso
                        if (SetTesseraAvviso(oMyAvviso.ID, myTes.Id, -1, Utility.Costanti.AZIONE_NEW) <= 0)
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetAvvisoCompleto::", ex);
                return 0; ;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DbOperation"></param>
        /// <param name="myTestata"></param>
        /// <returns></returns>
        public int SetTestata(int DbOperation, ObjTestata myTestata)
        {
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DbOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTESTATA_IU", "ID", "IDTESTATA"
                             , "IDENTE"
                             , "IDCONTRIBUENTE"
                             , "DATA_DICHIARAZIONE"
                             , "NUMERO_DICHIARAZIONE"
                             , "DATA_PROTOCOLLO"
                             , "NUMERO_PROTOCOLLO"
                             , "IDPROVENIENZA"
                             , "NOTE_DICHIARAZIONE"
                             , "DATA_INSERIMENTO"
                             , "DATA_VARIAZIONE"
                             , "DATA_CESSAZIONE"
                             , "OPERATORE");

                            DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myTestata.Id)
                                 , ctx.GetParam("IDTESTATA", myTestata.IdTestata)
                                , ctx.GetParam("IDENTE", myTestata.sEnte)
                                , ctx.GetParam("IDCONTRIBUENTE", myTestata.IdContribuente)
                                , ctx.GetParam("DATA_DICHIARAZIONE", myTestata.tDataDichiarazione)
                                , ctx.GetParam("NUMERO_DICHIARAZIONE", myTestata.sNDichiarazione)
                                , ctx.GetParam("DATA_PROTOCOLLO", myTestata.tDataProtocollo == DateTime.MinValue ? System.DBNull.Value : (object)myTestata.tDataProtocollo)
                                , ctx.GetParam("NUMERO_PROTOCOLLO", myTestata.sNProtocollo)
                                , ctx.GetParam("IDPROVENIENZA", myTestata.sIdProvenienza)
                                , ctx.GetParam("NOTE_DICHIARAZIONE", myTestata.sNoteDichiarazione)
                                , ctx.GetParam("DATA_INSERIMENTO", myTestata.tDataInserimento)
                                , ctx.GetParam("DATA_VARIAZIONE", myTestata.tDataVariazione == DateTime.MinValue ? System.DBNull.Value : (object)myTestata.tDataVariazione)
                                , ctx.GetParam("DATA_CESSAZIONE", myTestata.tDataCessazione == DateTime.MinValue ? System.DBNull.Value : (object)myTestata.tDataCessazione)
                                , ctx.GetParam("OPERATORE", myTestata.sOperatore)
                               );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myTestata.Id = int.Parse(myRow[0].ToString());
                            }
                            if (myTestata.Id <= 0)
                            {
                                Log.Debug("OPENgovSPORTELLO.Models.SPC_DichICI.Save::errore in inserimento dichiarazione");
                                return -1;
                            }
                            myTestata.IdTestata = myTestata.Id;
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTESTATA_D", "ID"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                                , "ISERROR");

                            myTestata.Id = ctx.ExecuteNonQuery(sSQL, ctx.GetParam("ID", myTestata.Id)
                                , ctx.GetParam("DATA_VARIAZIONE", DateTime.Now)
                                , ctx.GetParam("DATA_CESSAZIONE", DateTime.Now)
                                , ctx.GetParam("ISERROR", 0)
                               );
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                }
                return myTestata.Id;
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerTARSU::SetTestata::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DbOperation"></param>
        /// <param name="myDettaglioTestata"></param>
        /// <returns></returns>
        public int SetDettaglioTestata(int DbOperation, ObjDettaglioTestata myDettaglioTestata)
        {
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DbOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLDETTAGLIOTESTATA_IU", "ID"
                                , "IDDETTAGLIOTESTATA"
                                , "IDTESSERA"
                                , "IDTESTATA"
                                , "IDPADRE"
                                , "CODVIA"
                                , "VIA"
                                , "CIVICO"
                                , "ESPONENTE"
                                , "INTERNO"
                                , "SCALA"
                                , "FOGLIO"
                                , "NUMERO"
                                , "SUBALTERNO"
                                , "DATA_INIZIO"
                                , "DATA_FINE"
                                , "GGTARSU"
                                , "IDSTATOOCCUPAZIONE"
                                , "IDTIPOCONDUTTORE"
                                , "NOMINATIVO_PROPRIETARIO"
                                , "NOMINATIVO_OCCUPANTE_PREC"
                                , "NOTEDETTAGLIOTESTATA"
                                , "DATA_INSERIMENTO"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                                , "OPERATORE"
                                , "ESTENSIONE_PARTICELLA"
                                , "ID_ASSENZA_DATI_CATASTALI"
                                , "ID_DESTINAZIONE_USO"
                                , "ID_NATURA_OCCUPANTE"
                                , "ID_TIPO_PARTICELLA"
                                , "ID_TIPO_UNITA"
                                , "ID_TITOLO_OCCUPAZIONE"
                                , "SEZIONE"
                                , "NCOMPONENTI"
                                , "MQCATASTO"
                                , "fk_IdCategoriaAteco"
                                , "MQTASSABILI"
                                , "FORZA_CALCOLAPV"
                                , "NCOMPONENTI_PV"
                                , "KEY"
                            );

                            DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myDettaglioTestata.Id)
                                , ctx.GetParam("IDDETTAGLIOTESTATA", myDettaglioTestata.IdDettaglioTestata)
                                , ctx.GetParam("IDTESSERA", myDettaglioTestata.IdTessera)
                                , ctx.GetParam("IDTESTATA", myDettaglioTestata.IdTestata)
                                , ctx.GetParam("IDPADRE", myDettaglioTestata.IdPadre)
                                , ctx.GetParam("CODVIA", myDettaglioTestata.sCodVia)
                                , ctx.GetParam("VIA", myDettaglioTestata.sVia)
                                , ctx.GetParam("CIVICO", myDettaglioTestata.sCivico)
                                , ctx.GetParam("ESPONENTE", myDettaglioTestata.sEsponente)
                                , ctx.GetParam("INTERNO", myDettaglioTestata.sInterno)
                                , ctx.GetParam("SCALA", myDettaglioTestata.sScala)
                                , ctx.GetParam("FOGLIO", myDettaglioTestata.sFoglio)
                                , ctx.GetParam("NUMERO", myDettaglioTestata.sNumero)
                                , ctx.GetParam("SUBALTERNO", myDettaglioTestata.sSubalterno)
                                , ctx.GetParam("DATA_INIZIO", myDettaglioTestata.tDataInizio == DateTime.MinValue ? System.DBNull.Value : (object)myDettaglioTestata.tDataInizio.Date)
                                , ctx.GetParam("DATA_FINE", myDettaglioTestata.tDataFine == DateTime.MinValue ? System.DBNull.Value : (DBType == "MySQL" && myDettaglioTestata.tDataFine == DateTime.MaxValue ? DateTime.MinValue.Date : (object)myDettaglioTestata.tDataFine.Date))
                                , ctx.GetParam("GGTARSU", (myDettaglioTestata.nGGTarsu > 0) ? (object)myDettaglioTestata.nGGTarsu : System.DBNull.Value)
                                , ctx.GetParam("IDSTATOOCCUPAZIONE", myDettaglioTestata.sIdStatoOccupazione)
                                , ctx.GetParam("IDTIPOCONDUTTORE", String.Empty)
                                , ctx.GetParam("NOMINATIVO_PROPRIETARIO", myDettaglioTestata.sNomeProprietario)
                                , ctx.GetParam("NOMINATIVO_OCCUPANTE_PREC", myDettaglioTestata.sNomeOccupantePrec)
                                , ctx.GetParam("NOTEDETTAGLIOTESTATA", myDettaglioTestata.sNoteUI)
                                , ctx.GetParam("DATA_INSERIMENTO", myDettaglioTestata.tDataInserimento == DateTime.MinValue ? (object)System.DBNull.Value : myDettaglioTestata.tDataInserimento)
                                , ctx.GetParam("DATA_VARIAZIONE", myDettaglioTestata.tDataVariazione == DateTime.MinValue ? (object)System.DBNull.Value : myDettaglioTestata.tDataVariazione)
                                , ctx.GetParam("DATA_CESSAZIONE", myDettaglioTestata.tDataCessazione == DateTime.MinValue ? (object)System.DBNull.Value : myDettaglioTestata.tDataCessazione)
                                , ctx.GetParam("OPERATORE", myDettaglioTestata.sOperatore)
                                , ctx.GetParam("ESTENSIONE_PARTICELLA", myDettaglioTestata.sEstensioneParticella)
                                , ctx.GetParam("ID_ASSENZA_DATI_CATASTALI", myDettaglioTestata.nIdAssenzaDatiCatastali)
                                , ctx.GetParam("ID_DESTINAZIONE_USO", myDettaglioTestata.nIdDestUso)
                                , ctx.GetParam("ID_NATURA_OCCUPANTE", myDettaglioTestata.nIdNaturaOccupaz)
                                , ctx.GetParam("ID_TIPO_PARTICELLA", myDettaglioTestata.sIdTipoParticella)
                                , ctx.GetParam("ID_TIPO_UNITA", myDettaglioTestata.sIdTipoUnita)
                                , ctx.GetParam("ID_TITOLO_OCCUPAZIONE", myDettaglioTestata.nIdTitoloOccupaz)
                                , ctx.GetParam("SEZIONE", myDettaglioTestata.sSezione)
                                , ctx.GetParam("NCOMPONENTI", myDettaglioTestata.nNComponenti)
                                , ctx.GetParam("MQCATASTO", myDettaglioTestata.nMQCatasto)
                                , ctx.GetParam("fk_IdCategoriaAteco", myDettaglioTestata.IdCatAteco)
                                , ctx.GetParam("MQTASSABILI", myDettaglioTestata.nMQTassabili)
                                , ctx.GetParam("FORZA_CALCOLAPV", myDettaglioTestata.bForzaPV)
                                , ctx.GetParam("NCOMPONENTI_PV", myDettaglioTestata.nComponentiPV)
                                , ctx.GetParam("KEY", myDettaglioTestata.Key)
                            );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myDettaglioTestata.Id = int.Parse(myRow[0].ToString());
                            }
                            myDettaglioTestata.IdDettaglioTestata = myDettaglioTestata.Id;
                            Log.Debug("ok::" + myDettaglioTestata.Id.ToString());
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLDETTAGLIOTESTATA_D", "ID"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                                , "ISERROR");

                            myDettaglioTestata.Id = ctx.ExecuteNonQuery(sSQL, ctx.GetParam("ID", myDettaglioTestata.Id)
                                , ctx.GetParam("DATA_VARIAZIONE", DateTime.Now)
                                , ctx.GetParam("DATA_CESSAZIONE", DateTime.Now)
                                , ctx.GetParam("ISERROR", 0)
                               );
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                }
                new DBUtility(DBType, ConnectionStringGOV).LogActionEvent(DateTime.Now, myDettaglioTestata.sOperatore, new Costanti.LogEventArgument().Immobile, "SetDettaglioTestata", DbOperation.ToString(), Costanti.TRIBUTO_TARSU, IdEnte, myDettaglioTestata.Id);
                return myDettaglioTestata.Id;
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerTARSU::SetDettaglioTestata::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DbOperation"></param>
        /// <param name="myTessera"></param>
        /// <param name="IdContribuente"></param>
        /// <returns></returns>
        public int SetTessera(int DbOperation, ObjTessera myTessera, int IdContribuente)
        {
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DbOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTESSERE_IU", "ID"
                                , "IDTESSERA"
                                , "IDTESTATA"
                                , "IDTIPOTESSERA"
                                , "IDCONTRIBUENTE"
                                , "IDENTE"
                                , "CODICE_UTENTE"
                                , "NUMERO_TESSERA"
                                , "CODICE_INTERNO"
                                , "DATA_RILASCIO"
                                , "NOTE_TESSERA"
                                , "DATA_INSERIMENTO"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                                , "OPERATORE");

                            DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myTessera.Id)
                                , ctx.GetParam("IDTESSERA", myTessera.IdTessera)
                                , ctx.GetParam("IDTESTATA", myTessera.IdTestata)
                                , ctx.GetParam("IDTIPOTESSERA", myTessera.IdTipoTessera)
                                , ctx.GetParam("IDCONTRIBUENTE", myTessera.IdContribuente)
                                , ctx.GetParam("IDENTE", myTessera.IdEnte)
                                , ctx.GetParam("CODICE_UTENTE", myTessera.sCodUtente)
                                , ctx.GetParam("NUMERO_TESSERA", myTessera.sNumeroTessera)
                                , ctx.GetParam("CODICE_INTERNO", myTessera.sCodInterno)
                                , ctx.GetParam("DATA_RILASCIO", myTessera.tDataRilascio == DateTime.MinValue ? System.DBNull.Value : (object)myTessera.tDataRilascio)
                                , ctx.GetParam("NOTE_TESSERA", myTessera.sNote)
                                , ctx.GetParam("DATA_INSERIMENTO", myTessera.tDataInserimento == DateTime.MinValue ? System.DBNull.Value : (object)myTessera.tDataInserimento)
                                , ctx.GetParam("DATA_VARIAZIONE", myTessera.tDataVariazione == DateTime.MinValue ? System.DBNull.Value : (object)myTessera.tDataVariazione)
                                , ctx.GetParam("DATA_CESSAZIONE", myTessera.tDataCessazione == DateTime.MinValue ? System.DBNull.Value : (object)myTessera.tDataCessazione)
                                , ctx.GetParam("OPERATORE", myTessera.sOperatore)
                               );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myTessera.Id = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTESSERE_D", "ID"
                                , "IDCONTRIBUENTE");

                            myTessera.Id = ctx.ExecuteNonQuery(sSQL, ctx.GetParam("ID", myTessera.Id)
                                , ctx.GetParam("IDCONTRIBUENTE", IdContribuente)
                               );
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                }
                return myTessera.Id;
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerTARSU::SetTessera::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DbOperation"></param>
        /// <param name="myOggetto"></param>
        /// <returns></returns>
        public int SetOggetto(int DbOperation, ObjOggetti myOggetto)
        {
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DbOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLOGGETTI_IU", "ID"
                                , "IDDETTAGLIOTESTATA"
                                , "IDCATEGORIA"
                                , "IDTIPOVANO"
                                , "NVANI"
                                , "MQ"
                                , "PROVENIENZA"
                                , "DATA_INSERIMENTO"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                                , "OPERATORE"
                                , "ESENTE"
                                , "NCOMPONENTI"
                                , "NCOMPONENTI_PV"
                                , "fk_IdCategoriaAteco"
                                , "FORZA_CALCOLAPV");

                            DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myOggetto.IdOggetto)
                                 , ctx.GetParam("IDDETTAGLIOTESTATA", myOggetto.IdDettaglioTestata)
                                 , ctx.GetParam("IDCATEGORIA", myOggetto.IdCategoria)
                                 , ctx.GetParam("IDTIPOVANO", myOggetto.IdTipoVano)
                                 , ctx.GetParam("NVANI", myOggetto.nVani)
                                 , ctx.GetParam("MQ", myOggetto.nMq)
                                 , ctx.GetParam("PROVENIENZA", myOggetto.sProvenienza)
                                 , ctx.GetParam("DATA_INSERIMENTO", myOggetto.tDataInserimento == DateTime.MinValue ? (object)System.DBNull.Value : myOggetto.tDataInserimento)
                                 , ctx.GetParam("DATA_VARIAZIONE", myOggetto.tDataVariazione == DateTime.MinValue ? (object)System.DBNull.Value : myOggetto.tDataVariazione)
                                 , ctx.GetParam("DATA_CESSAZIONE", myOggetto.tDataCessazione == DateTime.MinValue ? (object)System.DBNull.Value : myOggetto.tDataCessazione)
                                 , ctx.GetParam("OPERATORE", myOggetto.sOperatore)
                                 , ctx.GetParam("ESENTE", myOggetto.bIsEsente)
                                 , ctx.GetParam("NCOMPONENTI", myOggetto.nNC)
                                 , ctx.GetParam("NCOMPONENTI_PV", myOggetto.nNCPV)
                                 , ctx.GetParam("fk_IdCategoriaAteco", myOggetto.IdCatTARES)
                                 , ctx.GetParam("FORZA_CALCOLAPV", myOggetto.bForzaCalcolaPV));
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myOggetto.IdOggetto = int.Parse(myRow[0].ToString());
                            }
                            if (myOggetto.IdOggetto <= 0)
                            {
                                Log.Debug("OPENgovSPORTELLO.Models.SPC_DichICI.Save::errore in inserimento dichiarazione");
                                return -1;
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTESTATA_D", "ID"
                                , "DATA");

                            myOggetto.IdOggetto = ctx.ExecuteNonQuery(sSQL, ctx.GetParam("ID", myOggetto.IdOggetto)
                                , ctx.GetParam("DATA", DateTime.Now));
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                }
                Log.Debug("ok::" + myOggetto.IdOggetto.ToString());
                return myOggetto.IdOggetto;
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerTARSU::SetOggetto::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DbOperation"></param>
        /// <param name="myListItem"></param>
        /// <param name="sTabella"></param>
        /// <param name="IdRiferimento"></param>
        /// <param name="IdFlusso"></param>
        /// <returns></returns>
        public int SetRidEseApplicate(int DbOperation, ObjRidEseApplicati[] myListItem, string sTabella, int IdRiferimento, int IdFlusso)
        {
            try
            {
                int myReturn = -1;
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    foreach (ObjRidEseApplicati myItem in myListItem)
                    {
                        sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_SetRidEseApplicate", "ID"
                                , "TYPEOPERATION"
                                , "TYPERiferimento"
                                , "TABELLA"
                                , "IdRiferimento"
                                , "IDCODICE"
                                , "DATA_VARIAZIONE"
                                , "IDFLUSSO_RUOLO");
                        DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", -1)
                                , ctx.GetParam("TYPEOPERATION", DbOperation)
                                , ctx.GetParam("TYPERiferimento", myItem.Riferimento)
                                , ctx.GetParam("TABELLA", sTabella)
                                , ctx.GetParam("IdRiferimento", IdRiferimento)
                                , ctx.GetParam("IDCODICE", myItem.sCodice)
                                , ctx.GetParam("DATA_VARIAZIONE", DateTime.Now)
                                , ctx.GetParam("IDFLUSSO_RUOLO", IdFlusso)
                            );
                        foreach (DataRowView myRow in dvMyView)
                        {
                            myReturn = int.Parse(myRow[0].ToString());
                        }
                        if (myReturn != 1)
                        {
                            return 0;
                        }
                    }
                    ctx.Dispose();
                }
                return 1;
            }
            catch (Exception Err)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetRidEseApplicate::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="KeyUI"></param>
        /// <param name="CodRidEse"></param>
        /// <returns></returns>
        public int SetRidEseImport(string Type, string KeyUI, string CodRidEse)
        {
            try
            {
                string sSQL = string.Empty;
                int RetVal = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_SetRidEseImport", "TYPE"
                        , "KEYUI"
                        , "CODICE"
                     );

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("TYPE", Type)
                        , ctx.GetParam("KEYUI", KeyUI)
                        , ctx.GetParam("CODICE", CodRidEse)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        RetVal = int.Parse(myRow[0].ToString());
                    }
                    if (RetVal <= 0)
                    {
                        Log.Debug("OPENgovSPORTELLO.Models.SPC_DichTARSU.SetRidEseImport::errore in inserimento");
                        return -1;
                    }
                    ctx.Dispose();
                }
                return RetVal;
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerTARSU::SetRidEseImport::", Err);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdDettaglioTestata"></param>
        /// <returns></returns>
        public double SetMQTassabili(int IdDettaglioTestata)
        {
            double MQTassabili = 0;
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_SETMQTASSABILI", "IdDettaglioTestata");

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IdDettaglioTestata", IdDettaglioTestata));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        MQTassabili = double.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return MQTassabili;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetOggetti::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oMyAvviso"></param>
        /// <param name="nIdFlusso"></param>
        /// <param name="DBOperation"></param>
        /// <returns></returns>
        public int SetAvviso(RemotingInterfaceMotoreTarsu.MotoreTarsuVARIABILE.Oggetti.ObjAvviso oMyAvviso, int nIdFlusso, int DBOperation)
        {
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DBOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLCARTELLE_IU", "ID"
                                , "IDENTE"
                                , "IDFLUSSO_RUOLO"
                                , "CODICE_CARTELLA"
                                , "ANNO"
                                , "DATA_EMISSIONE"
                                , "COD_CONTRIBUENTE"
                                , "LOTTO_CARTELLAZIONE"
                                , "ANNI_PRESENZA_RUOLO"
                                , "COGNOME_DENOMINAZIONE"
                                , "NOME"
                                , "COD_FISCALE"
                                , "PARTITA_IVA"
                                , "VIA_RES"
                                , "CIVICO_RES"
                                , "CAP_RES"
                                , "COMUNE_RES"
                                , "PROVINCIA_RES"
                                , "FRAZIONE_RES"
                                , "NOMINATIVO_INVIO"
                                , "VIA_RCP"
                                , "CIVICO_RCP"
                                , "CAP_RCP"
                                , "COMUNE_RCP"
                                , "PROVINCIA_RCP"
                                , "FRAZIONE_RCP", "CODICECLIENTE"
                                , "IMPORTO_TOTALE"
                                , "IMPORTO_ARROTONDAMENTO"
                                , "IMPORTO_CARICO"
                                , "IMPORTO_CREDITODEBITO_PREC"
                                , "IMPORTO_DOVUTO"
                                , "NOTE"
                                , "OPERATORE"
                                , "DATA_INSERIMENTO"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                                , "IDORGIMPORT"
                            );

                            DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", oMyAvviso.ID)
                                , ctx.GetParam("IDENTE", oMyAvviso.IdEnte)
                                , ctx.GetParam("IDFLUSSO_RUOLO", oMyAvviso.IdFlussoRuolo)
                                , ctx.GetParam("CODICE_CARTELLA", oMyAvviso.sCodiceCartella)
                                , ctx.GetParam("ANNO", oMyAvviso.sAnnoRiferimento)
                                , ctx.GetParam("DATA_EMISSIONE", (oMyAvviso.tDataEmissione == DateTime.MinValue) ? System.DBNull.Value : (object)oMyAvviso.tDataEmissione)
                                , ctx.GetParam("COD_CONTRIBUENTE", oMyAvviso.IdContribuente)
                                , ctx.GetParam("LOTTO_CARTELLAZIONE", oMyAvviso.nLottoCartellazione)
                                , ctx.GetParam("ANNI_PRESENZA_RUOLO", oMyAvviso.sAnniPresenzaRuolo)
                                , ctx.GetParam("COGNOME_DENOMINAZIONE", oMyAvviso.sCognome)
                                , ctx.GetParam("NOME", oMyAvviso.sNome)
                                , ctx.GetParam("COD_FISCALE", oMyAvviso.sCodFiscale)
                                , ctx.GetParam("PARTITA_IVA", oMyAvviso.sPIVA)
                                , ctx.GetParam("VIA_RES", oMyAvviso.sIndirizzoRes)
                                , ctx.GetParam("CIVICO_RES", oMyAvviso.sCivicoRes)
                                , ctx.GetParam("CAP_RES", oMyAvviso.sCAPRes)
                                , ctx.GetParam("COMUNE_RES", oMyAvviso.sComuneRes)
                                , ctx.GetParam("PROVINCIA_RES", oMyAvviso.sProvRes)
                                , ctx.GetParam("FRAZIONE_RES", oMyAvviso.sFrazRes)
                                , ctx.GetParam("NOMINATIVO_INVIO", oMyAvviso.sNominativoCO)
                                , ctx.GetParam("VIA_RCP", oMyAvviso.sIndirizzoCO)
                                , ctx.GetParam("CIVICO_RCP", oMyAvviso.sCivicoCO)
                                , ctx.GetParam("CAP_RCP", oMyAvviso.sCAPCO)
                                , ctx.GetParam("COMUNE_RCP", oMyAvviso.sComuneCO)
                                , ctx.GetParam("PROVINCIA_RCP", oMyAvviso.sProvCO)
                                , ctx.GetParam("FRAZIONE_RCP", oMyAvviso.sFrazCO)
                                , ctx.GetParam("CODICECLIENTE", oMyAvviso.sCodiceCliente)
                                , ctx.GetParam("IMPORTO_TOTALE", Math.Round(oMyAvviso.impTotale, 2))
                                , ctx.GetParam("IMPORTO_ARROTONDAMENTO", oMyAvviso.impArrotondamento)
                                , ctx.GetParam("IMPORTO_CARICO", oMyAvviso.impCarico)
                                , ctx.GetParam("IMPORTO_CREDITODEBITO_PREC", oMyAvviso.impCreditoDebitoPrec)
                                , ctx.GetParam("IMPORTO_DOVUTO", Math.Round(oMyAvviso.impDovuto, 2))
                                , ctx.GetParam("NOTE", oMyAvviso.sNote)
                                , ctx.GetParam("OPERATORE", oMyAvviso.sOperatore)
                                , ctx.GetParam("DATA_INSERIMENTO", oMyAvviso.tDataInserimento == DateTime.MinValue ? System.DBNull.Value : (object)oMyAvviso.tDataInserimento)
                                , ctx.GetParam("DATA_VARIAZIONE", oMyAvviso.tDataVariazione == DateTime.MinValue ? System.DBNull.Value : (object)oMyAvviso.tDataVariazione)
                                , ctx.GetParam("DATA_CESSAZIONE", oMyAvviso.tDataCessazione == DateTime.MinValue ? System.DBNull.Value : (object)oMyAvviso.tDataCessazione)
                                , ctx.GetParam("IDORGIMPORT", -1)
                            );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                oMyAvviso.ID = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLCARTELLE_D", "ID"
                                , "IDFLUSSO_RUOLO");

                            oMyAvviso.ID = ctx.ExecuteNonQuery(sSQL, ctx.GetParam("ID", oMyAvviso.ID)
                                , ctx.GetParam("IDFLUSSO_RUOLO", oMyAvviso.IdFlussoRuolo)
                               );
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                }
                return oMyAvviso.ID;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetAvviso::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArticolo"></param>
        /// <param name="bIsFromVariabile"></param>
        /// <param name="nIdFlusso"></param>
        /// <param name="DBOperation"></param>
        /// <returns></returns>
        public int SetArticoloCompleto(ObjArticolo myArticolo, string bIsFromVariabile, int nIdFlusso, int DBOperation)
        {
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DBOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLARTICOLI_IU", "ID"
                                , "IDRUOLO"
                                , "IDCONTRIBUENTE"
                                , "IDENTE"
                                , "IDDETTAGLIOTESTATA"
                                , "IDOGGETTO"
                                , "CODVIA"
                                , "VIA"
                                , "CIVICO"
                                , "ESPONENTE"
                                , "INTERNO"
                                , "SCALA"
                                , "FOGLIO"
                                , "NUMERO"
                                , "SUBALTERNO"
                                , "ANNO"
                                , "IDCATEGORIA"
                                , "NCOMPONENTI"
                                , "MQ"
                                , "BIMESTRI"
                                , "DATA_INIZIO"
                                , "DATA_FINE"
                                , "ISTARSUGIORNALIERA"
                                , "IDTARIFFA"
                                , "IMPORTO_TARIFFA"
                                , "SEZIONE"
                                , "ESTENSIONE_PARTICELLA"
                                , "ID_TIPO_PARTICELLA"
                                , "ID_TITOLO_OCCUPAZIONE"
                                , "ID_NATURA_OCCUPANTE"
                                , "ID_DESTINAZIONE_USO"
                                , "ID_TIPO_UNITA"
                                , "ID_ASSENZA_DATI_CATASTALI"
                                , "IMPORTO"
                                , "IMPORTO_NETTO"
                                , "IMPORTO_RIDUZIONI"
                                , "IMPORTO_DETASSAZIONI"
                                , "IMPORTO_FORZATO"
                                , "INFORMAZIONI"
                                , "IDFLUSSO_RUOLO"
                                , "TIPO_RUOLO"
                                , "IDAVVISO"
                                , "IMPORTO_SANZIONI"
                                , "IMPORTO_INTERESSI"
                                , "IMPORTO_SPESE_NOTIFICA"
                                , "DESCRIZIONE_DIFFERENZAIMPOSTA"
                                , "DESCRIZIONE_SANZIONI"
                                , "DESCRIZIONE_INTERESSI"
                                , "DESCRIZIONE_SPESENOTIFICA"
                                , "DA_ACCERTAMENTO"
                                , "TIPOPARTITA"
                                , "DATA_INSERIMENTO"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                                , "OPERATORE"
                            );
                            DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myArticolo.Id)
                                , ctx.GetParam("IDRUOLO", myArticolo.IdArticolo)
                                , ctx.GetParam("IDCONTRIBUENTE", myArticolo.IdContribuente)
                                , ctx.GetParam("IDENTE", myArticolo.IdEnte)
                                , ctx.GetParam("IDDETTAGLIOTESTATA", myArticolo.IdDettaglioTestata)
                                //*** 20141211 - legami PF-PV ***
                                , ctx.GetParam("IDOGGETTO", myArticolo.IdOggetto)
                                //*** ***
                                , ctx.GetParam("CODVIA", myArticolo.nCodVia)
                                , ctx.GetParam("VIA", myArticolo.sVia)
                                , ctx.GetParam("CIVICO", ((myArticolo.sCivico != "-1") ? myArticolo.sCivico : ""))
                                , ctx.GetParam("ESPONENTE", myArticolo.sEsponente)
                                , ctx.GetParam("INTERNO", myArticolo.sInterno)
                                , ctx.GetParam("SCALA", myArticolo.sScala)
                                , ctx.GetParam("FOGLIO", myArticolo.sFoglio)
                                , ctx.GetParam("NUMERO", myArticolo.sNumero)
                                , ctx.GetParam("SUBALTERNO", myArticolo.sSubalterno)
                                , ctx.GetParam("ANNO", myArticolo.sAnno)
                                , ctx.GetParam("IDCATEGORIA", myArticolo.sCategoria)
                                , ctx.GetParam("NCOMPONENTI", myArticolo.nComponenti)
                                , ctx.GetParam("MQ", myArticolo.nMQ)
                                , ctx.GetParam("BIMESTRI", myArticolo.nBimestri)
                                , ctx.GetParam("DATA_INIZIO", myArticolo.tDataInizio)
                                , ctx.GetParam("DATA_FINE", ((myArticolo.tDataFine != DateTime.MinValue) ? myArticolo.tDataFine : (object)DBNull.Value))
                                , ctx.GetParam("ISTARSUGIORNALIERA", myArticolo.bIsTarsuGiornaliera)
                                , ctx.GetParam("IDTARIFFA", myArticolo.nIdTariffa)
                                , ctx.GetParam("IMPORTO_TARIFFA", myArticolo.impTariffa)
                                //***Agenzia Entrate***                                                                                                   
                                , ctx.GetParam("SEZIONE", myArticolo.sSezione)
                                , ctx.GetParam("ESTENSIONE_PARTICELLA", myArticolo.sEstensioneParticella)
                                , ctx.GetParam("ID_TIPO_PARTICELLA", myArticolo.sIdTipoParticella)
                                , ctx.GetParam("ID_TITOLO_OCCUPAZIONE", myArticolo.nIdTitoloOccupaz)
                                , ctx.GetParam("ID_NATURA_OCCUPANTE", myArticolo.nIdNaturaOccupaz)
                                , ctx.GetParam("ID_DESTINAZIONE_USO", myArticolo.nIdDestUso)
                                , ctx.GetParam("ID_TIPO_UNITA", myArticolo.sIdTipoUnita)
                                , ctx.GetParam("ID_ASSENZA_DATI_CATASTALI", myArticolo.nIdAssenzaDatiCatastali)
                                //*********************                                                                                                   
                                , ctx.GetParam("IMPORTO", myArticolo.impRuolo)
                                , ctx.GetParam("IMPORTO_NETTO", myArticolo.impNetto)
                                , ctx.GetParam("IMPORTO_RIDUZIONI", myArticolo.impRiduzione)
                                , ctx.GetParam("IMPORTO_DETASSAZIONI", myArticolo.impDetassazione)
                                , ctx.GetParam("IMPORTO_FORZATO", myArticolo.bIsImportoForzato)
                                , ctx.GetParam("INFORMAZIONI", myArticolo.sNote)
                                , ctx.GetParam("IDFLUSSO_RUOLO", myArticolo.nIdFlussoRuolo)
                                , ctx.GetParam("TIPO_RUOLO", myArticolo.sTipoRuolo)
                                , ctx.GetParam("IDAVVISO", myArticolo.IdAvviso)
                                , ctx.GetParam("IMPORTO_SANZIONI", 0)
                                , ctx.GetParam("IMPORTO_INTERESSI", 0)
                                , ctx.GetParam("IMPORTO_SPESE_NOTIFICA", 0)
                                , ctx.GetParam("DESCRIZIONE_DIFFERENZAIMPOSTA", string.Empty)
                                , ctx.GetParam("DESCRIZIONE_SANZIONI", string.Empty)
                                , ctx.GetParam("DESCRIZIONE_INTERESSI", string.Empty)
                                , ctx.GetParam("DESCRIZIONE_SPESENOTIFICA", string.Empty)
                                , ctx.GetParam("DA_ACCERTAMENTO", 0)
                                , ctx.GetParam("TIPOPARTITA", myArticolo.TipoPartita)
                                , ctx.GetParam("DATA_INSERIMENTO", DateTime.Now)
                                , ctx.GetParam("DATA_VARIAZIONE", ((myArticolo.tDataVariazione != DateTime.MinValue) ? myArticolo.tDataVariazione : (object)DBNull.Value))
                                , ctx.GetParam("DATA_CESSAZIONE", ((myArticolo.tDataCessazione != DateTime.MinValue) ? myArticolo.tDataCessazione : (object)DBNull.Value))
                                , ctx.GetParam("OPERATORE", myArticolo.sOperatore)
                            );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myArticolo.Id = int.Parse(myRow[0].ToString());
                                if (myArticolo.Id > 0)
                                {
                                    if (myArticolo.oRiduzioni != null)
                                    {
                                        foreach (ObjRidEseApplicati oRidEse in myArticolo.oRiduzioni)
                                        {
                                            //devo forzare il riferimento riduzione ad articolo altrimenti inserisce sbagliato
                                            oRidEse.Riferimento = ObjRidEseApplicati.RIF_ARTICOLO;
                                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLARTICOLI" + ObjRidEse.TIPO_RIDUZIONI + "_IU", "IDARTICOLO"
                                                    , "IDENTE"
                                                    , "DATA_VARIAZIONE"
                                                    , "IDCODICE"
                                                );
                                            DataView dvMyViewRid = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDARTICOLO", myArticolo.Id)
                                                    , ctx.GetParam("IDENTE", myArticolo.IdEnte)
                                                    , ctx.GetParam("DATA_VARIAZIONE", System.DBNull.Value)
                                                    , ctx.GetParam("IDCODICE", ((bIsFromVariabile == "1") ? oRidEse.sCodice/*** x CMGC ****/: oRidEse.ID.ToString()/*** x RIBES ***/))
                                                );
                                            foreach (DataRowView myRid in dvMyViewRid)
                                            {
                                                oRidEse.ID = int.Parse(myRid[0].ToString());
                                                if (oRidEse.ID <= 0)
                                                {
                                                    Log.Debug("Si è verificato un errore in SetArticoloCompleto::SetRidEse::oMyArticolo.IdContribuente::" + myArticolo.IdContribuente);
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (myArticolo.oDetassazioni != null)
                                    {
                                        foreach (ObjRidEseApplicati oRidEse in myArticolo.oDetassazioni)
                                        {
                                            //devo forzare il riferimento riduzione ad articolo altrimenti inserisce sbagliato
                                            oRidEse.Riferimento = ObjRidEseApplicati.RIF_ARTICOLO;
                                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLARTICOLI" + ObjRidEse.TIPO_ESENZIONI + "_IU", "IDARTICOLO"
                                                    , "IDENTE"
                                                    , "DATA_VARIAZIONE"
                                                    , "IDCODICE"
                                                );
                                            DataView dvMyViewRid = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDARTICOLO", myArticolo.Id)
                                                    , ctx.GetParam("IDENTE", myArticolo.IdEnte)
                                                    , ctx.GetParam("DATA_VARIAZIONE", System.DBNull.Value)
                                                    , ctx.GetParam("IDCODICE", ((bIsFromVariabile == "1") ? oRidEse.sCodice/*** x CMGC ****/: oRidEse.ID.ToString()/*** x RIBES ***/))
                                                );
                                            foreach (DataRowView myRid in dvMyViewRid)
                                            {
                                                oRidEse.ID = int.Parse(myRid[0].ToString());
                                                if (oRidEse.ID <= 0)
                                                {
                                                    Log.Debug("Si è verificato un errore in SetArticoloCompleto::SetRidEse::oMyArticolo.IdContribuente::" + myArticolo.IdContribuente);
                                                    return 0;
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLARTICOLI_D", "ID"
                                , "DATA_VARIAZIONE"
                                , "IDFLUSSO_RUOLO"
                            );
                            dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", ((myArticolo != null) ? myArticolo.Id : -1))
                                , ctx.GetParam("DATA_VARIAZIONE", DateTime.Now)
                                , ctx.GetParam("IDFLUSSO_RUOLO", nIdFlusso)
                            );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myArticolo.Id = int.Parse(myRow[0].ToString());
                            }
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                }
                return myArticolo.Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetArticoloCompleto::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myObj"></param>
        /// <param name="DBOperation"></param>
        /// <returns></returns>
        public int SetLegamePFPV(ObjLegamePFPV myObj, int DBOperation)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DBOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLARTICOLI_PFVSPV_IU", "IDPF"
                                , "IDPV");

                            dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDPF", myObj.IdPF)
                                , ctx.GetParam("IDPV", myObj.IdPV)
                                );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                int Ret = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetLegamePFPV::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nIdAvviso"></param>
        /// <param name="nIdTessera"></param>
        /// <param name="nIdFlusso"></param>
        /// <param name="DBOperation"></param>
        /// <returns></returns>
        public int SetTesseraAvviso(int nIdAvviso, int nIdTessera, int nIdFlusso, int DBOperation)
        {
            int myIdentity = 0;
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DBOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLCARTELLETESSERE_IU", "IDAVVISO"
                                , "IDTESSERA");

                            dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDAVVISO", nIdAvviso)
                                    , ctx.GetParam("IDTESSERA", nIdTessera)
                                );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myIdentity = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLCARTELLETESSERE_D", "IDAVVISO"
                                , "IDTESSERA"
                                , "IDFLUSSO_RUOLO"
                            );

                            dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDAVVISO", nIdAvviso)
                                , ctx.GetParam("IDTESSERA", nIdTessera)
                                , ctx.GetParam("IDFLUSSO_RUOLO", nIdFlusso)
                            );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myIdentity = int.Parse(myRow[0].ToString());
                            }
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                    return myIdentity;
                }
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetTesseraAvviso::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oMyDettaglio"></param>
        /// <param name="IdAvviso"></param>
        /// <param name="nIdFlusso"></param>
        /// <param name="DBOperation"></param>
        /// <returns></returns>
        public int SetDetVoci(ObjDetVoci oMyDettaglio, int IdAvviso, int nIdFlusso, int DBOperation)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DBOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLCARTELLEDETTAGLIOVOCI_IU", "ID"
                                , "IDAVVISO"
                                , "CODICE_CAPITOLO"
                                , "ANNO"
                                , "CODICE_VOCE"
                                , "IMPORTO"
                                , "OPERATORE"
                                , "DATA_INSERIMENTO"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                            );

                            dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", oMyDettaglio.IdDettaglio)
                                , ctx.GetParam("IDAVVISO", IdAvviso)
                                , ctx.GetParam("CODICE_CAPITOLO", oMyDettaglio.sCapitolo)
                                , ctx.GetParam("ANNO", oMyDettaglio.sAnno)
                                , ctx.GetParam("CODICE_VOCE", oMyDettaglio.CodVoce)
                                , ctx.GetParam("IMPORTO", oMyDettaglio.impDettaglio)
                                , ctx.GetParam("OPERATORE", oMyDettaglio.sOperatore)
                                , ctx.GetParam("DATA_INSERIMENTO", DateTime.Now)
                                , ctx.GetParam("DATA_VARIAZIONE", (oMyDettaglio.tDataVariazione == DateTime.MinValue) ? System.DBNull.Value : (object)oMyDettaglio.tDataVariazione)
                                , ctx.GetParam("DATA_CESSAZIONE", (oMyDettaglio.tDataCessazione == DateTime.MinValue) ? System.DBNull.Value : (object)oMyDettaglio.tDataCessazione)
                            );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                oMyDettaglio.IdDettaglio = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLCARTELLEDETTAGLIOVOCI_D", "ID"
                                 , "DATA_VARIAZIONE"
                                , "IDFLUSSO_RUOLO");

                            dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", oMyDettaglio.IdDettaglio)
                                , ctx.GetParam("DATA_VARIAZIONE", (oMyDettaglio.tDataVariazione == DateTime.MinValue) ? System.DBNull.Value : (object)oMyDettaglio.tDataVariazione)
                                , ctx.GetParam("IDFLUSSO_RUOLO", nIdFlusso));
                            foreach (DataRowView myRow in dvMyView)
                            {
                                oMyDettaglio.IdDettaglio = int.Parse(myRow[0].ToString());
                            }
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                    return int.Parse(oMyDettaglio.IdDettaglio.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetDetVoci::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myRata"></param>
        /// <param name="nIdFlusso"></param>
        /// <param name="DBOperation"></param>
        /// <returns></returns>
        public int SetRata(ObjRata myRata, int nIdFlusso, int DBOperation)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DBOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLCARTELLERATE_IU", "ID"
                                , "IDAVVISO"
                                , "IDENTE"
                                , "CODICE_CARTELLA"
                                , "DATA_EMISSIONE"
                                , "COD_CONTRIBUENTE"
                                , "NUMERO_RATA"
                                , "DESCRIZIONE_RATA"
                                , "IMPORTO_RATA"
                                , "DATA_SCADENZA"
                                , "CODICE_BOLLETTINO"
                                , "CODELINE"
                                , "IDFLUSSO_RIOLO"
                                , "NUMERO_CONTO_CORRENTE"
                                , "CODICE_BARCODE"
                                , "DATA_INSERIMENTO"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                                , "OPERATORE"
                            );
                            dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myRata.Id)
                                , ctx.GetParam("IDAVVISO", myRata.IdAvviso)
                                , ctx.GetParam("IDENTE", myRata.IdEnte)
                                , ctx.GetParam("CODICE_CARTELLA", myRata.sCodiceCartella)
                                , ctx.GetParam("DATA_EMISSIONE", myRata.tDataEmissione)
                                , ctx.GetParam("COD_CONTRIBUENTE", myRata.IdContribuente)
                                , ctx.GetParam("NUMERO_RATA", myRata.sNRata)
                                , ctx.GetParam("DESCRIZIONE_RATA", myRata.sDescrRata)
                                , ctx.GetParam("IMPORTO_RATA", myRata.impRata)
                                , ctx.GetParam("DATA_SCADENZA", myRata.tDataScadenza)
                                , ctx.GetParam("CODICE_BOLLETTINO", myRata.sCodBollettino)
                                , ctx.GetParam("CODELINE", myRata.sCodeline)
                                , ctx.GetParam("IDFLUSSO_RIOLO", myRata.IdFlussoRuolo)
                                , ctx.GetParam("NUMERO_CONTO_CORRENTE", myRata.sContoCorrente)
                                , ctx.GetParam("CODICE_BARCODE", myRata.sCodiceBarcode)
                                , ctx.GetParam("DATA_INSERIMENTO", DateTime.Now)
                                , ctx.GetParam("DATA_VARIAZIONE", (myRata.tDataVariazione == DateTime.MinValue) ? System.DBNull.Value : (object)myRata.tDataVariazione)
                                , ctx.GetParam("DATA_CESSAZIONE", (myRata.tDataCessazione == DateTime.MinValue) ? System.DBNull.Value : (object)myRata.tDataCessazione)
                                , ctx.GetParam("OPERATORE", myRata.sOperatore)
                            );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myRata.Id = int.Parse(myRow[0].ToString());
                            }
                            break;
                        case Costanti.AZIONE_DELETE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLCARTELLERATE_D", "ID"
                                , "DATA_VARIAZIONE"
                                , "IDFLUSSO_RUOLO"
                            );
                            dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myRata.Id)
                                , ctx.GetParam("DATA_VARIAZIONE", DateTime.Now)
                                , ctx.GetParam("IDFLUSSO_RUOLO", nIdFlusso)
                            );
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myRata.Id = int.Parse(myRow[0].ToString());
                            }
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                }
                return myRata.Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetRata::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myRataDet"></param>
        /// <param name="myRata"></param>
        /// <param name="nIdFlusso"></param>
        /// <param name="DBOperation"></param>
        /// <returns></returns>
        public int SetRataDettaglio(RemotingInterfaceMotoreTarsu.MotoreTarsuVARIABILE.Oggetti.ObjRataDettaglio myRataDet, RemotingInterfaceMotoreTarsu.MotoreTarsuVARIABILE.Oggetti.ObjRata myRata, int nIdFlusso, int DBOperation)
        {
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    switch (DBOperation)
                    {
                        case Costanti.AZIONE_NEW:
                        case Costanti.AZIONE_UPDATE:
                            sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLCARTELLERATEDETTAGLIO_IU", "ID"
                                , "IDAVVISO"
                                , "NRATA"
                                , "TRIBUTO"
                                , "IMPORTO");

                            DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myRataDet.Id)
                                , ctx.GetParam("IDAVVISO", myRataDet.IdAvviso)
                                , ctx.GetParam("NRATA", myRataDet.NRata)
                                , ctx.GetParam("TRIBUTO", myRataDet.Tributo)
                                , ctx.GetParam("IMPORTO", myRataDet.Importo));
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myRataDet.Id = int.Parse(myRow[0].ToString());
                            }

                            /*sSQL = ctx.GetSQL("prc_TBLCARTELLERATE_IU", "ID"
                                , "IDAVVISO"
                                , "IDENTE"
                                , "CODICE_CARTELLA"
                                , "DATA_EMISSIONE"
                                , "COD_CONTRIBUENTE"
                                , "NUMERO_RATA"
                                , "DESCRIZIONE_RATA"
                                , "IMPORTO_RATA"
                                , "DATA_SCADENZA"
                                , "CODICE_BOLLETTINO"
                                , "CODELINE"
                                , "IDFLUSSO_RIOLO"
                                , "NUMERO_CONTO_CORRENTE"
                                , "CODICE_BARCODE"
                                , "DATA_INSERIMENTO"
                                , "DATA_VARIAZIONE"
                                , "DATA_CESSAZIONE"
                                , "OPERATORE");

                            dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myRata.Id)
                                , ctx.GetParam("IDAVVISO", myRata.IdAvviso)
                                , ctx.GetParam("IDENTE", myRata.IdEnte)
                                , ctx.GetParam("CODICE_CARTELLA", myRata.sCodiceCartella)
                                , ctx.GetParam("DATA_EMISSIONE", myRata.tDataEmissione)
                                , ctx.GetParam("COD_CONTRIBUENTE", myRata.IdContribuente)
                                , ctx.GetParam("NUMERO_RATA", myRata.sNRata)
                                , ctx.GetParam("DESCRIZIONE_RATA", myRata.sDescrRata)
                                , ctx.GetParam("IMPORTO_RATA", myRata.impRata)
                                , ctx.GetParam("DATA_SCADENZA", myRata.tDataScadenza)
                                , ctx.GetParam("CODICE_BOLLETTINO", myRata.sCodBollettino)
                                , ctx.GetParam("CODELINE", myRata.sCodeline)
                                , ctx.GetParam("IDFLUSSO_RIOLO", myRata.IdFlussoRuolo)
                                , ctx.GetParam("NUMERO_CONTO_CORRENTE", myRata.sContoCorrente)
                                , ctx.GetParam("CODICE_BARCODE", myRata.sCodiceBarcode)
                                , ctx.GetParam("DATA_INSERIMENTO", DateTime.Now)
                                , ctx.GetParam("DATA_VARIAZIONE", (myRata.tDataVariazione == DateTime.MinValue) ? System.DBNull.Value : (object)myRata.tDataVariazione)
                                , ctx.GetParam("DATA_CESSAZIONE", (myRata.tDataCessazione == DateTime.MinValue) ? System.DBNull.Value : (object)myRata.tDataCessazione)
                                , ctx.GetParam("OPERATORE", myRata.sOperatore));
                            foreach (DataRowView myRow in dvMyView)
                            {
                                myRata.Id = int.Parse(myRow[0].ToString());
                            }*/
                            break;
                        case Costanti.AZIONE_DELETE:
                            break;
                        default:
                            break;
                    }
                    ctx.Dispose();
                }
                return myRataDet.Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetRataDettaglio::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myItem"></param>
        /// <param name="nDBOperation"></param>
        /// <returns>{0= non a buon fine, >0= id tabella}</returns>
        public int SetPagamento(RemotingInterfaceMotoreTarsu.MotoreTarsu.Oggetti.OggettoPagamenti myItem, int nDBOperation)
        {
            int nMyReturn = 0;

            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLPAGAMENTI_IU", "TYPEOPERATION"
                            , "IDPAGAMENTO"
                            , "IDIMPORTAZIONE"
                            , "IDFLUSSO"
                            , "IDENTE"
                            , "IDCONTRIBUENTE"
                            , "PROVENIENZA"
                            , "ANNO"
                            , "CODICE_CARTELLA"
                            , "CFPIVA"
                            , "CODICE_BOLLETTINO"
                            , "DATA_PAGAMENTO"
                            , "DATA_ACCREDITO"
                            , "DIVISA"
                            , "IMPORTO_PAGAMENTO"
                            , "TIPO_BOLLETTINO"
                            , "TIPO_PAGAMENTO"
                            , "PROGRESSIVO_CARICAMENTO"
                            , "PROGRESSIVO_SELEZIONE"
                            , "CCBENEFICIARIO"
                            , "UFFICIOSPORTELLO"
                            , "NOTE"
                            , "FLAGDETTAGLIO"
                            , "OPERATORE"
                            , "DATA_INSERIMENTO"
                            , "NUMERO_RATA"
                            , "IMPORTO_RATA"
                            , "DATA_SCADENZA"
                            , "CHIAVE"
                        );

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("TYPEOPERATION", nDBOperation)
                        , ctx.GetParam("IDPAGAMENTO", myItem.ID)
                        , ctx.GetParam("IDIMPORTAZIONE", myItem.IDImportazione)
                        , ctx.GetParam("IDFLUSSO", myItem.IDFlusso)
                        , ctx.GetParam("IDENTE", myItem.IdEnte)
                        , ctx.GetParam("IDCONTRIBUENTE", myItem.IdContribuente)
                        , ctx.GetParam("PROVENIENZA", myItem.sProvenienza)
                        , ctx.GetParam("ANNO", myItem.sAnno)
                        , ctx.GetParam("CODICE_CARTELLA", myItem.sNumeroAvviso)
                        , ctx.GetParam("CFPIVA", myItem.sCFPIVA)
                        , ctx.GetParam("CODICE_BOLLETTINO", myItem.sCodBollettino)
                        , ctx.GetParam("DATA_PAGAMENTO", (myItem.tDataPagamento == DateTime.MinValue || myItem.tDataPagamento.ToShortDateString() == DateTime.MinValue.ToShortDateString()) ? DBNull.Value : (object)myItem.tDataPagamento)
                        , ctx.GetParam("DATA_ACCREDITO", (myItem.tDataAccredito == DateTime.MinValue || myItem.tDataAccredito.ToShortDateString() == DateTime.MinValue.ToShortDateString()) ?
                            (object)((myItem.tDataPagamento == DateTime.MinValue || myItem.tDataPagamento.ToShortDateString() == DateTime.MinValue.ToShortDateString()) ? myItem.tDataPagamento : myItem.tDataAccredito)
                            : DBNull.Value)
                        , ctx.GetParam("DIVISA", myItem.sDivisa)
                        , ctx.GetParam("IMPORTO_PAGAMENTO", myItem.dImportoPagamento)
                        , ctx.GetParam("TIPO_BOLLETTINO", myItem.sTipoBollettino)
                        , ctx.GetParam("TIPO_PAGAMENTO", myItem.sTipoPagamento)
                        , ctx.GetParam("PROGRESSIVO_CARICAMENTO", myItem.sProgCaricamento)
                        , ctx.GetParam("PROGRESSIVO_SELEZIONE", myItem.sProgSelezione)
                        , ctx.GetParam("CCBENEFICIARIO", myItem.sCCBeneficiario)
                        , ctx.GetParam("UFFICIOSPORTELLO", myItem.sUfficioSportello)
                        , ctx.GetParam("NOTE", myItem.sNote)
                        , ctx.GetParam("FLAGDETTAGLIO", myItem.bFlagDettaglio)
                        , ctx.GetParam("OPERATORE", myItem.sOperatore)
                        , ctx.GetParam("DATA_INSERIMENTO", DateTime.Now)
                        , ctx.GetParam("NUMERO_RATA", myItem.sNumeroRata)
                        , ctx.GetParam("IMPORTO_RATA", myItem.dImportoRata)
                        , ctx.GetParam("DATA_SCADENZA", myItem.tDataScadenza)
                        , ctx.GetParam("CHIAVE", string.Empty)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        nMyReturn = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetPagamento::", ex);
                nMyReturn = 0;
            }
            return nMyReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oMyArticolo"></param>
        /// <param name="nIdFlusso"></param>
        /// <param name="DBOperation"></param>
        /// <returns></returns>
        public int SetScaglione(ObjScaglione oMyArticolo, int nIdFlusso, int DBOperation)
        {
            try
            {
                return 1;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetScaglione::", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public int ClearBancaDatiCatTARES(string IdEnte)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBCatTARES", "IDENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::ClearBancaDatiCatTARES::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public int ClearBancaDatiRidTARSU(string IdEnte)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBRidTARSU", "IDENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::ClearBancaDatiRidTARSU::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="IdContribuente"></param>
        /// <returns></returns>
        public int ClearBancaDatiDich(string IdEnte, int IdContribuente)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBDich", "IDENTE", "IDCONTRIBUENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte), ctx.GetParam("IDCONTRIBUENTE", IdContribuente));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::ClearBancaDatiDich::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="IdContribuente"></param>
        /// <returns></returns>
        public int ClearBancaDatiDichRidEse(string IdEnte, int IdContribuente)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBDichRidEse", "IDENTE", "IDCONTRIBUENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte), ctx.GetParam("IDCONTRIBUENTE", IdContribuente));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::ClearBancaDatiDichRidEse::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="IdContribuente"></param>
        /// <returns></returns>
        public int ClearBancaDatiAvvisi(string IdEnte, int IdContribuente)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBAvvisi", "IDENTE", "IDCONTRIBUENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte), ctx.GetParam("IDCONTRIBUENTE", IdContribuente));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::ClearBancaDatiAvvisi::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="IdContribuente"></param>
        /// <returns></returns>
        public int ClearBancaDatiRate(string IdEnte, int IdContribuente)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBRate", "IDENTE", "IDCONTRIBUENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte), ctx.GetParam("IDCONTRIBUENTE", IdContribuente));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::ClearBancaDatiRate::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="IdContribuente"></param>
        /// <returns></returns>
        public int ClearBancaDatiPagamenti(string IdEnte, int IdContribuente)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDBPagamenti", "IDENTE", "IDCONTRIBUENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte), ctx.GetParam("IDCONTRIBUENTE", IdContribuente));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::ClearBancaDatiPagamenti::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="Cod"></param>
        /// <param name="Descr"></param>
        /// <param name="IsDomestica"></param>
        /// <returns></returns>
        public int SetCatTARES(string IdEnte, string Cod, string Descr, int IsDomestica)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "CATEGORIE_ATECO_ENTE_IU", "IDCATEGORIAATECO"
                        , "CODICECATEGORIA"
                        , "ENTE"
                        , "FK_IDTYPEATECO"
                        , "DEFINIZIONE"
                        , "DOMESTICA"
                        , "ISFROMVARIABILE"
                    );
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDCATEGORIAATECO", -1)
                        , ctx.GetParam("CODICECATEGORIA", Cod)
                        , ctx.GetParam("ENTE", IdEnte)
                        , ctx.GetParam("FK_IDTYPEATECO", -1)
                        , ctx.GetParam("DEFINIZIONE", Descr)
                        , ctx.GetParam("DOMESTICA", IsDomestica)
                        , ctx.GetParam("ISFROMVARIABILE", 0)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetCatTARES::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="Cod"></param>
        /// <param name="Descr"></param>
        /// <returns></returns>
        public int SetRidTARSU(string IdEnte, string Cod, string Descr)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_TBLTIPORIDUZIONI_IU", "IDENTE"
                        , "CODICE"
                        , "DESCRIZIONE"
                    );
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte)
                        , ctx.GetParam("CODICE", Cod)
                        , ctx.GetParam("DESCRIZIONE", Descr)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::SetRidTARSU::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NDichiarazione"></param>
        /// <param name="CodImmobile"></param>
        /// <param name="ProgImmobile"></param>
        /// <param name="DataInizio"></param>
        /// <returns></returns>
        public int GetIdDettaglioTestata(string NDichiarazione, string CodImmobile, int ProgImmobile, DateTime DataInizio)
        {
            int Id = -1;

            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetIdDettaglioTestata", "NDICHIARAZIONE"
                        , "CODIMMOBILE"
                        , "PROGIMMOBILE"
                        , "DATAINIZIO"
                    );

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("NDICHIARAZIONE", NDichiarazione)
                        , ctx.GetParam("CODIMMOBILE", CodImmobile)
                        , ctx.GetParam("PROGIMMOBILE", ProgImmobile)
                        , ctx.GetParam("DATAINIZIO", DataInizio == DateTime.MinValue ? System.DBNull.Value : (object)DataInizio)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception Err)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::GetIdDettaglioTestata::", Err);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CodCategoria"></param>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public int GetIdCat(string CodCategoria, string IdEnte)
        {
            int Id = -1;

            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetCategorieEnte", "IDENTE"
                        , "TIPOTASSAZIONE"
                        , "CODCATEGORIA"
                    );

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte)
                        , ctx.GetParam("TIPOTASSAZIONE", "TARES")
                        , ctx.GetParam("CODCATEGORIA", CodCategoria)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow["IDCATEGORIA"].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception Err)
            {
                Log.Debug("Si è verificato un errore in DichManagerTARSU::GetIdCat::", Err);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public List<IdAvviso> GetAvvisi(string IdEnte)
        {
            List<IdAvviso> ListAvvisi = new List<IdAvviso>();
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetIdAvvisi", "IDENTE");

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        IdAvviso myItem = new IdAvviso();
                        myItem.Id = int.Parse(myRow["id"].ToString());
                        myItem.IdContribuente = int.Parse(myRow["idcontribuente"].ToString());
                        myItem.NAvviso = myRow["codice_cartella"].ToString();
                        myItem.DataEmissione = DateTime.Parse(myRow["data_emissione"].ToString());
                        ListAvvisi.Add(myItem);
                    }
                    ctx.Dispose();
                }
                return ListAvvisi;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerTARSU::GetAvvisi::si è verificato il seguente errore::", ex);
                return new List<IdAvviso>();
            }
        }
    }
    /// <summary>
    /// Classe di utilità generale per la manipolazione dei dati su database OSAP
    /// </summary>
    public class DichManagerOSAP
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DichManagerOSAP));
        private string DBType;
        private string ConnectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeDB"></param>
        /// <param name="myConnectionString"></param>
        public DichManagerOSAP(string TypeDB, string myConnectionString)
        {
            DBType = TypeDB;
            ConnectionString = myConnectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dichiarazione"></param>
        /// <returns></returns>
        public int SetDichiarazione(ref IRemInterfaceOSAP.DichiarazioneTosapCosap dichiarazione)
        {
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "sp_InsertDichiarazione", "ID"
                        , "DataDichiarazione"
                        , "NDichiarazione"
                        , "TipoAtto"
                        , "IdUfficio"
                        , "IdTitoloRichiedente"
                        , "CodContribuente"
                        , "IdEnte"
                        , "Note"
                        , "Operatore"
                        , "DataInserimento"
                        , "IdTributo"
                        , "IdDichiarazione");

                    dichiarazione.IdDichiarazione = ctx.ExecuteNonQuery(sSQL, ctx.GetParam("DataDichiarazione", dichiarazione.TestataDichiarazione.DataDichiarazione)
                        , ctx.GetParam("NDichiarazione", dichiarazione.TestataDichiarazione.NDichiarazione)
                        , ctx.GetParam("TipoAtto", dichiarazione.TestataDichiarazione.IdTipoAtto)
                        , ctx.GetParam("IdUfficio", dichiarazione.TestataDichiarazione.Ufficio.IdUfficio)
                        , ctx.GetParam("IdTitoloRichiedente", dichiarazione.TestataDichiarazione.TitoloRichiedente.IdTitoloRichiedente)
                        , ctx.GetParam("CodContribuente", dichiarazione.AnagraficaContribuente.COD_CONTRIBUENTE)
                        , ctx.GetParam("IdEnte", dichiarazione.IdEnte)
                        , ctx.GetParam("Note", dichiarazione.TestataDichiarazione.NoteDichiarazione)
                        , ctx.GetParam("Operatore", dichiarazione.TestataDichiarazione.Operatore)
                        , ctx.GetParam("DataInserimento", dichiarazione.TestataDichiarazione.DataInserimento)
                        , ctx.GetParam("IdTributo", dichiarazione.CodTributo)
                        , ctx.GetParam("IdDichiarazione", dichiarazione.IdDichiarazione));

                    foreach (IRemInterfaceOSAP.Articolo myItem in dichiarazione.ArticoliDichiarazione)
                    {
                        myItem.IdDichiarazione = dichiarazione.IdDichiarazione;
                        myItem.IdArticolo = SetArticolo(myItem);
                        if (myItem.IdArticolo <= 0)
                            throw new Exception("Errore in inserimento articolo");
                    }
                    ctx.Dispose();
                }
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerOSAP::SetTestata::", Err);
            }
            return dichiarazione.IdDichiarazione;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oMyArticolo"></param>
        /// <returns></returns>
        public int SetArticolo(IRemInterfaceOSAP.Articolo oMyArticolo)
        {
            try
            {
                string sSQL = string.Empty;
                int nIdArticolo = -1;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "sp_InsertArticolo", "IdDichiarazione"
                        , "IdTributo"
                        , "CodVia"
                        , "Via"
                        , "Civico"
                        , "Esponente"
                        , "Interno"
                        , "Scala"
                        , "IdCategoria"
                        , "IdTipologiaOccupazione"
                        , "Consistenza"
                        , "IdTipoConsistenza"
                        , "DataInizioOccupazione"
                        , "DataFineOccupazione"
                        , "IdDurata"
                        , "DurataOccupazione"
                        , "MaggiorazioneImporto"
                        , "MaggiorazionePerc"
                        , "Note"
                        , "DetrazioneImporto"
                        , "Attrazione"
                        , "Operatore"
                        , "DataInserimento"
                        , "DataVariazione"
                        , "IdArticolo"
                        , "IdArticoloPadre");

                    nIdArticolo = ctx.ExecuteNonQuery(sSQL, ctx.GetParam("IdDichiarazione", oMyArticolo.IdDichiarazione)
                        , ctx.GetParam("IdTributo", oMyArticolo.IdTributo)
                        , ctx.GetParam("CodVia", oMyArticolo.CodVia)
                        , ctx.GetParam("Via", oMyArticolo.SVia)
                        , ctx.GetParam("Civico", oMyArticolo.Civico)
                        , ctx.GetParam("Esponente", oMyArticolo.Esponente)
                        , ctx.GetParam("Interno", oMyArticolo.Interno)
                        , ctx.GetParam("Scala", oMyArticolo.Scala)
                        , ctx.GetParam("IdCategoria", oMyArticolo.Categoria.IdCategoria)
                        , ctx.GetParam("IdTipologiaOccupazione", oMyArticolo.TipologiaOccupazione.IdTipologiaOccupazione)
                        , ctx.GetParam("Consistenza", oMyArticolo.Consistenza)
                        , ctx.GetParam("IdTipoConsistenza", oMyArticolo.TipoConsistenzaTOCO.IdTipoConsistenza)
                        , ctx.GetParam("DataInizioOccupazione", oMyArticolo.DataInizioOccupazione)
                        , ctx.GetParam("DataFineOccupazione", oMyArticolo.DataFineOccupazione)
                        , ctx.GetParam("IdDurata", oMyArticolo.TipoDurata.IdDurata)
                        , ctx.GetParam("DurataOccupazione", oMyArticolo.DurataOccupazione)
                        , ctx.GetParam("MaggiorazioneImporto", oMyArticolo.MaggiorazioneImporto)
                        , ctx.GetParam("MaggiorazionePerc", oMyArticolo.MaggiorazionePerc)
                        , ctx.GetParam("Note", oMyArticolo.Note)
                        , ctx.GetParam("DetrazioneImporto", oMyArticolo.DetrazioneImporto)
                        , ctx.GetParam("Attrazione", oMyArticolo.Attrazione)
                        , ctx.GetParam("Operatore", oMyArticolo.Operatore)
                        , ctx.GetParam("DataInserimento", oMyArticolo.DataInserimento)
                        , ctx.GetParam("DataVariazione", (oMyArticolo.DataVariazione != DateTime.MinValue) ? (object)oMyArticolo.DataVariazione : System.DBNull.Value)
                        , ctx.GetParam("IdArticolo", oMyArticolo.IdArticolo)
                        , ctx.GetParam("IdArticoloPadre", oMyArticolo.IdArticoloPadre));
                    if (oMyArticolo.ListAgevolazioni != null)
                    {
                        for (int x = 0; x <= oMyArticolo.ListAgevolazioni.GetUpperBound(0); x++)
                        {
                            if (SetAgevolazione(nIdArticolo, oMyArticolo.ListAgevolazioni[x].IdAgevolazione) <= 0)
                            {
                                Log.Debug("DichManagerOSAP::SetArticolo::errore in inserimento agevolazione");
                                return -1;
                            }
                        }
                    }
                    ctx.Dispose();
                }
                return nIdArticolo;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerOSAP::SetArticolo::si è verificato il seguente errore::" + ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdArticolo"></param>
        /// <param name="IdAgevolazione"></param>
        /// <returns></returns>
        public int SetAgevolazione(int IdArticolo, int IdAgevolazione)
        {
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "sp_InsertArticoloVSAgevolazione", "IdArticolo"
                        , "IdAgevolazione");
                    ctx.ExecuteNonQuery(sSQL, ctx.GetParam("IdArticolo", IdArticolo)
                       , ctx.GetParam("IdAgevolazione", IdAgevolazione));

                    ctx.Dispose();
                }
                return 1;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerOSAP::SetAgevolazione::si è verificato il seguente errore::" + ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NDichiarazione"></param>
        /// <param name="DataDich"></param>
        /// <returns></returns>
        public int GetIdArticolo(string NDichiarazione, DateTime DataDich)
        {
            int Id = -1;

            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetIdDettaglioTestata", "NDICHIARAZIONE"
                        , "DATADICHIARAZIONE"
                    );

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("NDICHIARAZIONE", NDichiarazione)
                        , ctx.GetParam("DATADICHIARAZIONE", DataDich == DateTime.MinValue ? System.DBNull.Value : (object)DataDich)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception Err)
            {
                Log.Debug("Si è verificato un errore in DichManageOSAP::GetIdArticolo::", Err);
                return -1;
            }
        }
    }
    /// <summary>
    /// Classe di utilità generale per la manipolazione dei dati su database STRADE
    /// </summary>
    public class DichManagerSTRADE //: DBManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DichManagerSTRADE));
        private string DBType;
        private string ConnectionString;
        /// <summary>
        /// 
        /// </summary>
        public struct Stradario
        {
            /// <summary>
            /// 
            /// </summary>
            public int IDVia;
            /// <summary>
            /// 
            /// </summary>
            public string Descrizione;
            /// <summary>
            /// 
            /// </summary>
            public string IdDemografico;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeDB"></param>
        /// <param name="myConnectionString"></param>
        public DichManagerSTRADE(string TypeDB, string myConnectionString)
        {
            DBType = TypeDB;
            ConnectionString = myConnectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public List<Stradario> GetStrade(string IdEnte)
        {
            List<Stradario> ListStrade = new List<Stradario>();
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetVie", "IDENTE");

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Stradario myItem = new Stradario();
                        myItem.IDVia = int.Parse(myRow["idvia"].ToString());
                        myItem.Descrizione = myRow["via"].ToString();
                        myItem.IdDemografico = myRow["idviademografico"].ToString();
                        ListStrade.Add(myItem);
                    }
                    ctx.Dispose();
                }
                return ListStrade;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerSTRADE::GetStrade::si è verificato il seguente errore::", ex);
                return new List<Stradario>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdDemografico"></param>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public int GetIdViaDemografico(long IdDemografico, string IdEnte)
        {
            int Id = -1;
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetIdViaDemografico", "IDDEMOGRAFICO"
                        , "IDENTE"
                    );

                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDDEMOGRAFICO", IdDemografico)
                        , ctx.GetParam("IDENTE", IdEnte)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerSTRADE::GetDescrizioneVia::si è verificato il seguente errore::" + ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="IdVia"></param>
        /// <param name="IdToponimo"></param>
        /// <param name="IdFrazione"></param>
        /// <param name="Descrizione"></param>
        /// <param name="IdDemografico"></param>
        /// <returns></returns>
        public int SetVia(string IdEnte, int IdVia, int IdToponimo, int IdFrazione, string Descrizione, string IdDemografico)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_T_STRADE_UI", "ID"
                        , "IDENTE"
                        , "DESCRIZIONE"
                        , "IDTOPONIMO"
                        , "IDFRAZIONE"
                        , "IDVIADEMOGRAFICO"
                    );
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", IdVia)
                        , ctx.GetParam("IDENTE", IdEnte)
                        , ctx.GetParam("DESCRIZIONE", Descrizione)
                        , ctx.GetParam("IDTOPONIMO", IdToponimo)
                        , ctx.GetParam("IDFRAZIONE", IdFrazione)
                        , ctx.GetParam("IDVIADEMOGRAFICO", IdDemografico)
                    );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerSTRADE::SetVia::", ex);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <returns></returns>
        public int ClearBancaDati(string IdEnte)
        {
            try
            {
                string sSQL = string.Empty;
                DataView dvMyView = new DataView();
                int Id = 0;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ClearDB", "IDENTE");
                    dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Id = int.Parse(myRow[0].ToString());
                    }
                    ctx.Dispose();
                }
                return Id;
            }
            catch (Exception ex)
            {
                Log.Debug("Si è verificato un errore in DichManagerSTRADE::ClearBancaDati::", ex);
                return -1;
            }
        }
    }
    /// <summary>
    /// Classe di utilità generale per la manipolazione dei dati su database SPORTELLO
    /// </summary>
    public class DichManagerSPORTELLO
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DichManagerSPORTELLO));
        private string DBType;
        private string ConnectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeDB"></param>
        /// <param name="myConnectionString"></param>
        public DichManagerSPORTELLO(string TypeDB, string myConnectionString)
        {
            DBType = TypeDB;
            ConnectionString = myConnectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CFPIVA"></param>
        /// <returns></returns>
        public string GetUserMail(string CFPIVA)
        {
            string myRet = string.Empty;
            try
            {
                string sSQL = string.Empty;
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetAutorizzazioni", "OPERATORE", "FILTRO", "CFPIVA", "IDENTE");
                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("OPERATORE", string.Empty)
                         , ctx.GetParam("FILTRO", 0)
                        , ctx.GetParam("CFPIVA", CFPIVA)
                        , ctx.GetParam("IDENTE", string.Empty)
                       );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        myRet = myRow["NameUser"].ToString();
                    }
                    ctx.Dispose();
                }
            }
            catch (Exception Err)
            {
                Log.Debug("Si é verificato un errore in DichManagerSPORTELLO.GetUserMail::", Err);
                myRet = string.Empty;
            }
            return myRet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEnte"></param>
        /// <param name="nDichiarazione"></param>
        /// <returns></returns>
        public bool SetStatoAccettata(string IdEnte, int nDichiarazione)
        {
            int myRet = 0;
            try
            {
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_SetStatoAccettata", "IDENTE", "IDDICHIARAZIONE", "DATA_ACCETTATA");
                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", IdEnte)
                        , ctx.GetParam("IDDICHIARAZIONE", nDichiarazione)
                         , ctx.GetParam("DATA_ACCETTATA", DateTime.Now)
                       );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        myRet = int.Parse(myRow["ID"].ToString());
                    }
                    if (myRet <= 0)
                    {
                        Log.Debug("DichManagerSPORTELLO.SetStatoAccettata::errore in salvataggio istanza");
                        return false;
                    }
                    ctx.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerSPORTELLO.SetStatoAccettata::errore::", ex);
                return false;
            }
        }
    }
    /// <summary>
    /// Classe di utilità generale per la manipolazione dei dati su database CATASTO
    /// </summary>
    public class DichManagerCatasto
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DichManagerCatasto));
        private string DBType;
        private string ConnectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeDB"></param>
        /// <param name="myConnectionString"></param>
        public DichManagerCatasto(string TypeDB, string myConnectionString)
        {
            DBType = TypeDB;
            ConnectionString = myConnectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEnte"></param>
        /// <returns></returns>
        public List<Elaborazione> LoadLastElaborazione(string myEnte)
        {
            List<Elaborazione> ListMyData = new List<Elaborazione>();
            try
            {
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetElaborazione", "IDCATASTALE", "ISLAST");
                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDCATASTALE", myEnte)
                            , ctx.GetParam("ISLAST", 1)
                        );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        Elaborazione myElab = new Elaborazione();
                        myElab.ID = int.Parse(myRow["ID"].ToString());
                        myElab.IDCatastale = myRow["IDCatastale"].ToString();
                        myElab.IDEnte = myRow["IDEnte"].ToString();
                        myElab.EsitoConvert = myRow["EsitoConvert"].ToString();
                        myElab.EsitoEstrazioneComunioneMancante = myRow["EsitoEstrazioneComunioneMancante"].ToString();
                        myElab.EsitoEstrazioneDichWork = myRow["EsitoEstrazioneDichWork"].ToString();
                        myElab.EsitoEstrazioneFabVSTit = myRow["EsitoEstrazioneFabVSTit"].ToString();
                        myElab.EsitoEstrazioneDirittoMancante = myRow["EsitoEstrazioneDirittoMancante"].ToString();
                        myElab.EsitoEstrazioneDirittoMancante = myRow["EsitoEstrazioneDirittoMancante"].ToString();
                        myElab.EsitoEstrazionePossMancante = myRow["EsitoEstrazionePossMancante"].ToString();
                        myElab.EsitoEstrazioneSogVSTit = myRow["EsitoEstrazioneSogVSTit"].ToString();
                        myElab.EsitoEstrazioneTitVSFab = myRow["EsitoEstrazioneTitVSFab"].ToString();
                        myElab.EsitoEstrazioneTitVSTer = myRow["EsitoEstrazioneTitVSTer"].ToString();
                        myElab.EsitoEstrazioneTitVSSog = myRow["EsitoEstrazioneTitVSSog"].ToString();
                        myElab.EsitoImport = myRow["EsitoImport"].ToString();
                        myElab.EsitoIncrocio = myRow["EsitoIncrocio"].ToString();
                        myElab.EsitoUpload = myRow["EsitoUpload"].ToString();
                        myElab.FineConvert = DateTime.Parse(myRow["FineConvert"].ToString());
                        myElab.FineEstrazioneComunioneMancante = DateTime.Parse(myRow["FineEstrazioneComunioneMancante"].ToString());
                        myElab.FineEstrazioneDichWork = DateTime.Parse(myRow["FineEstrazioneDichWork"].ToString());
                        myElab.FineEstrazioneFabVSTit = DateTime.Parse(myRow["FineEstrazioneFabVSTit"].ToString());
                        myElab.FineEstrazioneTerVSTit = DateTime.Parse(myRow["FineEstrazioneTerVSTit"].ToString());
                        myElab.FineEstrazioneDirittoMancante = DateTime.Parse(myRow["FineEstrazioneDirittoMancante"].ToString());
                        myElab.FineEstrazioneDirittoMancante = DateTime.Parse(myRow["FineEstrazioneDirittoMancante"].ToString());
                        myElab.FineEstrazionePossMancante = DateTime.Parse(myRow["FineEstrazionePossMancante"].ToString());
                        myElab.FineEstrazioneSogVSTit = DateTime.Parse(myRow["FineEstrazioneSogVSTit"].ToString());
                        myElab.FineEstrazioneTitVSFab = DateTime.Parse(myRow["FineEstrazioneTitVSFab"].ToString());
                        myElab.FineEstrazioneTitVSTer = DateTime.Parse(myRow["FineEstrazioneTitVSTer"].ToString());
                        myElab.FineEstrazioneTitVSSog = DateTime.Parse(myRow["FineEstrazioneTitVSSog"].ToString());
                        myElab.FineImport = DateTime.Parse(myRow["FineImport"].ToString());
                        myElab.FineIncrocio = DateTime.Parse(myRow["FineIncrocio"].ToString());
                        myElab.FineUpload = DateTime.Parse(myRow["FineUpload"].ToString());
                        myElab.InizioConvert = DateTime.Parse(myRow["InizioConvert"].ToString());
                        myElab.InizioEstrazioneComunioneMancante = DateTime.Parse(myRow["InizioEstrazioneComunioneMancante"].ToString());
                        myElab.InizioEstrazioneDichWork = DateTime.Parse(myRow["InizioEstrazioneDichWork"].ToString());
                        myElab.InizioEstrazioneFabVSTit = DateTime.Parse(myRow["InizioEstrazioneFabVSTit"].ToString());
                        myElab.InizioEstrazioneTerVSTit = DateTime.Parse(myRow["InizioEstrazioneTerVSTit"].ToString());
                        myElab.InizioEstrazioneDirittoMancante = DateTime.Parse(myRow["InizioEstrazioneDirittoMancante"].ToString());
                        myElab.InizioEstrazioneDirittoMancante = DateTime.Parse(myRow["InizioEstrazioneDirittoMancante"].ToString());
                        myElab.InizioEstrazionePossMancante = DateTime.Parse(myRow["InizioEstrazionePossMancante"].ToString());
                        myElab.InizioEstrazioneSogVSTit = DateTime.Parse(myRow["InizioEstrazioneSogVSTit"].ToString());
                        myElab.InizioEstrazioneTitVSFab = DateTime.Parse(myRow["InizioEstrazioneTitVSFab"].ToString());
                        myElab.InizioEstrazioneTitVSTer = DateTime.Parse(myRow["InizioEstrazioneTitVSTer"].ToString());
                        myElab.InizioEstrazioneTitVSSog = DateTime.Parse(myRow["InizioEstrazioneTitVSSog"].ToString());
                        myElab.InizioImport = DateTime.Parse(myRow["InizioImport"].ToString());
                        myElab.InizioIncrocio = DateTime.Parse(myRow["InizioIncrocio"].ToString());
                        myElab.InizioUpload = DateTime.Parse(myRow["InizioUpload"].ToString());
                        ListMyData.Add(myElab);
                    }
                    ctx.Dispose();
                }

                return ListMyData;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerCatasto.LoadLastElaborazione.errore::", ex);
                return new List<Elaborazione>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myItem"></param>
        /// <returns></returns>
        public bool SetElaborazione(Elaborazione myItem)
        {
            try
            {
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ELABORAZIONI_IU", "ID"
                            , "IDENTE"
                            , "IDCATASTALE"
                            , "INIZIOUPLOAD"
                            , "FINEUPLOAD"
                            , "ESITOUPLOAD"
                            , "INIZIOIMPORT"
                            , "FINEIMPORT"
                            , "ESITOIMPORT"
                            , "INIZIOCONVERT"
                            , "FINECONVERT"
                            , "ESITOCONVERT"
                            , "INIZIOINCROCIO"
                            , "FINEINCROCIO"
                            , "ESITOINCROCIO"
                            , "INIZIOESTRAZIONEDICHWORK"
                            , "FINEESTRAZIONEDICHWORK"
                            , "ESITOESTRAZIONEDICHWORK"
                            , "INIZIOESTRAZIONETITVSSOG"
                            , "FINEESTRAZIONETITVSSOG"
                            , "ESITOESTRAZIONETITVSSOG"
                            , "INIZIOESTRAZIONESOGVSTIT"
                            , "FINEESTRAZIONESOGVSTIT"
                            , "ESITOESTRAZIONESOGVSTIT"
                            , "INIZIOESTRAZIONETITVSFAB"
                            , "FINEESTRAZIONETITVSFAB"
                            , "ESITOESTRAZIONETITVSFAB"
                            , "INIZIOESTRAZIONEFABVSTIT"
                            , "FINEESTRAZIONEFABVSTIT"
                            , "ESITOESTRAZIONEFABVSTIT"
                            , "INIZIOESTRAZIONETITVSTER"
                            , "FINEESTRAZIONETITVSTER"
                            , "ESITOESTRAZIONETITVSTER"
                            , "INIZIOESTRAZIONETERVSTIT"
                            , "FINEESTRAZIONETERVSTIT"
                            , "ESITOESTRAZIONETERVSTIT"
                            , "INIZIOESTRAZIONEDIRITTOMANCANTE"
                            , "FINEESTRAZIONEDIRITTOMANCANTE"
                            , "ESITOESTRAZIONEDIRITTOMANCANTE"
                            , "INIZIOESTRAZIONEPOSSMANCANTE"
                            , "FINEESTRAZIONEPOSSMANCANTE"
                            , "ESITOESTRAZIONEPOSSMANCANTE"
                            , "INIZIOESTRAZIONECOMUNIONEMANCANTE"
                            , "FINEESTRAZIONECOMUNIONEMANCANTE"
                            , "ESITOESTRAZIONECOMUNIONEMANCANTE"
                        );
                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myItem.ID)
                            , ctx.GetParam("IDENTE", myItem.IDEnte)
                            , ctx.GetParam("IDCATASTALE", myItem.IDCatastale)
                            , ctx.GetParam("INIZIOUPLOAD", myItem.InizioUpload)
                            , ctx.GetParam("FINEUPLOAD", myItem.FineUpload)
                            , ctx.GetParam("ESITOUPLOAD", myItem.EsitoUpload)
                            , ctx.GetParam("INIZIOIMPORT", myItem.InizioImport)
                            , ctx.GetParam("FINEIMPORT", myItem.FineImport)
                            , ctx.GetParam("ESITOIMPORT", myItem.EsitoImport)
                            , ctx.GetParam("INIZIOCONVERT", myItem.InizioConvert)
                            , ctx.GetParam("FINECONVERT", myItem.FineConvert)
                            , ctx.GetParam("ESITOCONVERT", myItem.EsitoConvert)
                            , ctx.GetParam("INIZIOINCROCIO", myItem.InizioIncrocio)
                            , ctx.GetParam("FINEINCROCIO", myItem.FineIncrocio)
                            , ctx.GetParam("ESITOINCROCIO", myItem.EsitoIncrocio)
                            , ctx.GetParam("INIZIOESTRAZIONEDICHWORK", myItem.InizioEstrazioneDichWork)
                            , ctx.GetParam("FINEESTRAZIONEDICHWORK", myItem.FineEstrazioneDichWork)
                            , ctx.GetParam("ESITOESTRAZIONEDICHWORK", myItem.EsitoEstrazioneDichWork)
                            , ctx.GetParam("INIZIOESTRAZIONETITVSSOG", myItem.InizioEstrazioneTitVSSog)
                            , ctx.GetParam("FINEESTRAZIONETITVSSOG", myItem.FineEstrazioneTitVSSog)
                            , ctx.GetParam("ESITOESTRAZIONETITVSSOG", myItem.EsitoEstrazioneTitVSSog)
                            , ctx.GetParam("INIZIOESTRAZIONESOGVSTIT", myItem.InizioEstrazioneSogVSTit)
                            , ctx.GetParam("FINEESTRAZIONESOGVSTIT", myItem.FineEstrazioneSogVSTit)
                            , ctx.GetParam("ESITOESTRAZIONESOGVSTIT", myItem.EsitoEstrazioneSogVSTit)
                            , ctx.GetParam("INIZIOESTRAZIONETITVSFAB", myItem.InizioEstrazioneTitVSFab)
                            , ctx.GetParam("FINEESTRAZIONETITVSFAB", myItem.FineEstrazioneTitVSFab)
                            , ctx.GetParam("ESITOESTRAZIONETITVSFAB", myItem.EsitoEstrazioneTitVSFab)
                            , ctx.GetParam("INIZIOESTRAZIONEFABVSTIT", myItem.InizioEstrazioneFabVSTit)
                            , ctx.GetParam("FINEESTRAZIONEFABVSTIT", myItem.FineEstrazioneFabVSTit)
                            , ctx.GetParam("ESITOESTRAZIONEFABVSTIT", myItem.EsitoEstrazioneFabVSTit)
                            , ctx.GetParam("INIZIOESTRAZIONETITVSTER", myItem.InizioEstrazioneTitVSTer)
                            , ctx.GetParam("FINEESTRAZIONETITVSTER", myItem.FineEstrazioneTitVSTer)
                            , ctx.GetParam("ESITOESTRAZIONETITVSTER", myItem.EsitoEstrazioneTitVSTer)
                            , ctx.GetParam("INIZIOESTRAZIONETERVSTIT", myItem.InizioEstrazioneTerVSTit)
                            , ctx.GetParam("FINEESTRAZIONETERVSTIT", myItem.FineEstrazioneTerVSTit)
                            , ctx.GetParam("ESITOESTRAZIONETERVSTIT", myItem.EsitoEstrazioneTerVSTit)
                            , ctx.GetParam("INIZIOESTRAZIONEDIRITTOMANCANTE", myItem.InizioEstrazioneDirittoMancante)
                            , ctx.GetParam("FINEESTRAZIONEDIRITTOMANCANTE", myItem.FineEstrazioneDirittoMancante)
                            , ctx.GetParam("ESITOESTRAZIONEDIRITTOMANCANTE", myItem.EsitoEstrazioneDirittoMancante)
                            , ctx.GetParam("INIZIOESTRAZIONEPOSSMANCANTE", myItem.InizioEstrazionePossMancante)
                            , ctx.GetParam("FINEESTRAZIONEPOSSMANCANTE", myItem.FineEstrazionePossMancante)
                            , ctx.GetParam("ESITOESTRAZIONEPOSSMANCANTE", myItem.EsitoEstrazionePossMancante)
                            , ctx.GetParam("INIZIOESTRAZIONECOMUNIONEMANCANTE", myItem.InizioEstrazioneComunioneMancante)
                            , ctx.GetParam("FINEESTRAZIONECOMUNIONEMANCANTE", myItem.FineEstrazioneComunioneMancante)
                            , ctx.GetParam("ESITOESTRAZIONECOMUNIONEMANCANTE", myItem.EsitoEstrazioneComunioneMancante)
                        );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        myItem.ID = int.Parse(myRow["ID"].ToString());
                    }
                    if (myItem.ID <= 0)
                    {
                        Log.Debug("DichManagerCatasto.SetElaborazione::errore in inserimento");
                        return false;
                    }
                    else
                    {
                        foreach (ElaborazioneFile myFile in myItem.ListFiles)
                        {
                            if (!SetElaborazioneFile(myFile))
                            {
                                return false;
                            }
                        }
                    }
                    ctx.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerCatasto.SetElaborazione.errore::", ex);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myItem"></param>
        /// <returns></returns>
        public bool SetElaborazioneFile(ElaborazioneFile myItem)
        {
            try
            {
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ELABORAZIONI_FILES_IU", "ID"
                            , "IDELABORAZIONE"
                            , "NAMEFILE"
                            , "INIZIOIMPORT"
                            , "FINEIMPORT"
                            , "ESITOIMPORT"
                        );
                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", myItem.ID)
                            , ctx.GetParam("IDELABORAZIONE", myItem.IDElaborazione)
                            , ctx.GetParam("NAMEFILE", myItem.NameFile)
                            , ctx.GetParam("INIZIOIMPORT", myItem.InizioImport)
                            , ctx.GetParam("FINEIMPORT", myItem.FineImport)
                            , ctx.GetParam("ESITOIMPORT", myItem.EsitoImport)
                        );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        myItem.ID = int.Parse(myRow["ID"].ToString());
                    }
                    if (myItem.ID <= 0)
                    {
                        Log.Debug("DichManagerCatasto.SetElaborazioneFile::errore in inserimento " + myItem.NameFile);
                        return false;
                    }
                    ctx.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerCatasto.SetElaborazioneFile.errore::", ex);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEnte"></param>
        /// <returns></returns>
        public bool DeleteElaborazionePrec(string myEnte)
        {
            int myID = 0;
            try
            {
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_SvuotaElaborazione", "IDENTE");
                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("IDENTE", myEnte));
                    foreach (DataRowView myRow in dvMyView)
                    {
                        myID = int.Parse(myRow["ID"].ToString());
                    }
                    if (myID <= 0)
                    {
                        Log.Debug("DichManagerCatasto.DeleteElaborazionePrec::errore in inserimento");
                        return false;
                    }
                    ctx.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerCatasto.DeleteElaborazionePrec.errore::", ex);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDElab"></param>
        /// <param name="IDElabCat"></param>
        /// <returns></returns>
        public bool SetElaborazioneCat(int IDElab, int IDElabCat)
        {
            try
            {
                using (DBModel ctx = new DBModel(DBType, ConnectionString))
                {
                    string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_ELABORAZIONI_U", "ID"
                            , "IDELABORAZIONE"
                        );
                    DataView dvMyView = ctx.GetDataView(sSQL, "TBL", ctx.GetParam("ID", IDElab)
                            , ctx.GetParam("IDELABORAZIONE", IDElabCat)
                        );
                    foreach (DataRowView myRow in dvMyView)
                    {
                        IDElab = int.Parse(myRow["ID"].ToString());
                    }
                    if (IDElab <= 0)
                    {
                        Log.Debug("DichManagerCatasto.SetElaborazioneCat::errore in inserimento");
                        return false;
                    }
                    ctx.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Debug("DichManagerCatasto.SetElaborazioneCat.errore::", ex);
                return false;
            }
        }
    }
}