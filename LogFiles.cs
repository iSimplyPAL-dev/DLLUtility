using System;
using System.IO;
using System.Text;

namespace Utility
{
    /// <summary>
    /// Classe di utilità generale per la gestione file di log
    /// </summary>
    public class LogFile
	{
		private string sLogFormat;
		private string sErrorTime;
		private string sMetodo;

		public LogFile()
		{
			//sLogFormat used to create log files format :
			// dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
			sLogFormat = DateTime.Now.ToShortDateString().ToString()+" "+DateTime.Now.ToLongTimeString().ToString()+ " Metodo Non specificato " + " ==> ";
			
			//this variable used to create log filename format "
			//for example filename : ErrorLogYYYYMMDD
			string sYear	= DateTime.Now.Year.ToString();
			string sMonth	= DateTime.Now.Month.ToString();
			string sDay	= DateTime.Now.Day.ToString();
			sErrorTime = sYear+sMonth+sDay;
		}


		public LogFile(string metodo)
		{

			sLogFormat = DateTime.Now.ToShortDateString().ToString()+" "+DateTime.Now.ToLongTimeString().ToString();
			sMetodo = metodo;
			sLogFormat = sLogFormat + " " + sMetodo + " ==> ";

			string sYear	= DateTime.Now.Year.ToString();
			string sMonth	= DateTime.Now.Month.ToString();
			string sDay	= DateTime.Now.Day.ToString();
			sErrorTime = sYear+sMonth+sDay;
			
		}



		public void ErrorLog(string sPathName, string sErrMsg)
		{
			StreamWriter sw = new StreamWriter(sPathName+sErrorTime,true);
			sw.WriteLine(sLogFormat + "" + sErrMsg);
			sw.Flush();
			sw.Close();
		}
	}
}
