using System;

namespace Utility
{
    /// <summary>
    /// Classe di utilità generale per la manipolazione delle stringhe
    /// </summary>
    public class StringOperation
	{
        /*
		private string MyString;
		public StringOperation(string str)
		{
			//
			// TODO: Add constructor logic here
			//

			MyString = str;
		}
		public string Left(int length)
		{
			string tmpstr = MyString.Substring(0, length);
			return tmpstr;
		} 
		public string Right(int length)
		{
			string tmpstr = MyString.Substring(MyString.Length - length, length);
			return tmpstr;
		}

		public string Mid(int startIndex, int length)
		{
			string tmpstr = MyString.Substring(startIndex, length);
			return tmpstr;
		}

		public string Mid(int startIndex)
		{
			string tmpstr = MyString.Substring(startIndex);
			return tmpstr;
		}*/
        #region "controllo e conversione campo DB in formato specifico"
        /// <summary>
        /// Funzione per la gestione del valore null in ingresso trasformandolo in stringa
        /// </summary>
        /// <param name="myObj">object oggetto da testare</param>
        /// <returns>string valore restituito</returns>
        public static string FormatString(object myObj)
        {
            string myRet = string.Empty;
            try
            {
                if (myObj != null)
                {
                    if (myObj!= DBNull.Value)
                    {
                        myRet = myObj.ToString();
                    }
                }
            }
            catch
            {
                myRet = string.Empty;
            }
            return myRet;
        }
        /// <summary>
        /// Funzione per la gestione del valore null in ingresso trasformandolo in intero
        /// </summary>
        /// <param name="myObj">object oggetto da testare</param>
        /// <returns>int valore restituito</returns>
        public static int FormatInt(object myObj)
        {
            int myRet = default(int);
            try
            {
                if (myObj != null)
                {
                    if (myObj != DBNull.Value)
                    {
                        int.TryParse(myObj.ToString(),out myRet);
                    }
                }
            }
            catch
            {
                myRet = default(int);
            }
            return myRet;
        }
        /// <summary>
        /// Funzione per la gestione del valore null in ingresso trasformandolo in double
        /// </summary>
        /// <param name="myObj">object oggetto da testare</param>
        /// <returns>double valore restituito</returns>
        public static double FormatDouble(object myObj)
        {
            double myRet = default(double);
            try
            {
                if (myObj != null)
                {
                    if (myObj != DBNull.Value)
                    {
                        double.TryParse(myObj.ToString(), out myRet);
                    }
                }
            }
            catch
            {
                myRet = default(double);
            }
            return myRet;
        }
        /// <summary>
        /// Funzione per la gestione del valore null in ingresso trasformandolo in DateTime
        /// </summary>
        /// <param name="myObj">object oggetto da testare</param>
        /// <returns>DateTime valore restituito</returns>
        public static DateTime FormatDateTime(object myObj)
        {
            DateTime myRet = DateTime.MaxValue;
            try
            {
                if (myObj != null)
                {
                    if (myObj != DBNull.Value)
                    {
                        if (myObj.ToString() != string.Empty)
                        {
                            DateTime.TryParse(myObj.ToString(), out myRet);
                        }
                    }
                }
            }
            catch
            {
                myRet = DateTime.MaxValue;
            }
            if (myRet==DateTime.MinValue)
                myRet = DateTime.MaxValue;
            return myRet;
        }
        /// <summary>
        /// Funzione per la gestione del valore null in ingresso trasformandolo in boolean
        /// </summary>
        /// <param name="myObj">object oggetto da testare</param>
        /// <returns>bool valore restituito</returns>
        public static bool FormatBool(object myObj)
        {
            bool myRet = default(bool);
            try
            {
                if (myObj != null)
                {
                    if (myObj != DBNull.Value)
                    {
                        bool.TryParse(myObj.ToString(), out myRet);
                    }
                }
            }
            catch
            {
                myRet = default(bool);
            }
            return myRet;
        }
        #endregion
    }
}
