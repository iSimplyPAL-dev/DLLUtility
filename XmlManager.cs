using System;
using System.Xml;
using System.IO;
using System.Configuration;

namespace Utility
{
    /// <summary>
    /// Classe di utilità generale per la creazione XML
    /// </summary>
	public class XmlManager
	{

		private XmlDocument objXmlDoc;
		public string sXmlFileName;
		private LogFile LogError;
		private string sFileLogName;


		public XmlManager(string XmlFile)

			//Constructor

		{
			sFileLogName = ConfigurationSettings.AppSettings["LogFileError"];
			LogError = new LogFile();
		
			try
			{

				sXmlFileName = XmlFile; 
			
				FileStream fs = new FileStream(sXmlFileName,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
				objXmlDoc = new XmlDocument();
				objXmlDoc.Load(fs);
				fs.Close();
			}
			catch (Exception objError)
			{
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
			
		}





		public XmlManager(string root, string path)

			//Constructor

		{
			sFileLogName = ConfigurationSettings.AppSettings["LogFileError"];
			LogError = new LogFile();
		

			try
			{

				objXmlDoc = new XmlDocument();

				DirectoryInfo objDI = new DirectoryInfo(path);


				//Creo il percorso dei file XML se non esiste
				if (!objDI.Exists)
				{
					objDI.Create();
				}

				sXmlFileName = path + @"\" + "Transaction_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hhmmss") + ".xml";
			

				if (!File.Exists (sXmlFileName))
				{
			
					StreamWriter sw = new StreamWriter(sXmlFileName,false);
					sw.WriteLine ("<" + root + "/>");
					sw.Close();
						
				}
				FileStream fs = new FileStream(sXmlFileName,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
				objXmlDoc.Load(fs);
				fs.Close();
			}
			catch (Exception objError)
			{
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
			
		}

		// Method for Displaying the catalog

		

		#region Create New Element

		// Aggiungo un nuovo elemento

		/*
		 * Parametri D'ingresso
		 * 
		 * Xml Element
		 * aElementAttribute: matrice contente la seguente coppia di valore:
		 *										Nome Attributo
		 *										Valore Attributo
		 */
		public XmlElement AddRootElement(string sElementName)
		{
			// New XML Element Created
			XmlElement newElement = objXmlDoc.CreateElement(sElementName);
			try
			{
			
				InsertNewXmlElementAfterLastChild(newElement);
				return newElement;
			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}
	
		public XmlElement AddRootElement(string sElementName, string[,] aElementAttribute)
		{

			try
			{
				// New XML Element Created
				XmlElement newElement = objXmlDoc.CreateElement(sElementName);

				int i = 0;
				
				for (i=0;i<=aElementAttribute.GetUpperBound(0);i++)
				{
	
					// New Attribute Created
					XmlAttribute newAttr = objXmlDoc.CreateAttribute(aElementAttribute[i,0]);

					// Value given for the new attribute
					newAttr.Value = aElementAttribute[i,1];

					// Attach the attribute to the xml element
					newElement.SetAttributeNode(newAttr);
				}
			
				InsertNewXmlElementAfterLastChild(newElement);
				return newElement;
			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}

		#endregion

		#region Add Child


		public XmlElement AddChild(XmlElement objXmlElement, string sElementName)
		{
			try
			{
				// First Element - Book - Created
				XmlElement firstElement = objXmlDoc.CreateElement(sElementName);
				objXmlElement.AppendChild(firstElement);
				return objXmlElement;
			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}
	
		public XmlElement AddChild(XmlElement objXmlElement,string sElementName,string text)
		{
		
			try
			{
		
				// First Element - Book - Created
				XmlElement firstElement = objXmlDoc.CreateElement(sElementName);

				// Value given for the first element
				firstElement.InnerText = text;
				objXmlElement.AppendChild(firstElement);

				return objXmlElement;
			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}

		public XmlElement AddChild(XmlElement objXmlElement,string sElementName, string[,] aElementAttribute)
		{
			try
			{
				// First Element - Book - Created
				XmlElement firstElement = objXmlDoc.CreateElement(sElementName);

				int i = 0;

								for (i=0;i<=aElementAttribute.GetUpperBound(0);i++)
				{
	
					// New Attribute Created
					XmlAttribute newAttr = objXmlDoc.CreateAttribute(aElementAttribute[i,0]);

					// Value given for the new attribute
					newAttr.Value = aElementAttribute[i,1];

					// Attach the attribute to the xml element
					firstElement.SetAttributeNode(newAttr);
				}

				// Append the newly created element as a child element
				objXmlElement.AppendChild(firstElement);

				return objXmlElement;

			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}

		public XmlElement AddChild(XmlElement objXmlElement,string[,] aElementAttribute, string text, string sElementName)
		{
			try
			{
				// First Element - Book - Created
				XmlElement firstElement = objXmlDoc.CreateElement(sElementName);

				// Value given for the first element
				firstElement.InnerText = text;

				int i = 0;

				for (i=0;i<=aElementAttribute.GetUpperBound(0);i++)
				{
	
					// New Attribute Created
					XmlAttribute newAttr = objXmlDoc.CreateAttribute(aElementAttribute[i,0]);

					// Value given for the new attribute
					newAttr.Value = aElementAttribute[i,1];

					// Attach the attribute to the xml element
					firstElement.SetAttributeNode(newAttr);
				}

				// Append the newly created element as a child element
				objXmlElement.AppendChild(firstElement);

				return objXmlElement;
			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}

		#endregion

		public void InsertNewXmlElementBeforeLastChild(XmlElement objXmlElement)
		{
			try
			{
				// New XML element inserted into the document
				objXmlDoc.DocumentElement.InsertBefore(objXmlElement,objXmlDoc.DocumentElement.LastChild);
			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}

		public void InsertNewXmlElementAfterLastChild(XmlElement objXmlElement)
		{
			try
			{
				// New XML element inserted into the document
				objXmlDoc.DocumentElement.InsertAfter(objXmlElement,objXmlDoc.DocumentElement.LastChild);
			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}

		public void InsertNewXmlElementAfter(XmlElement objXmlElement, XmlNode objXmlNode)
		{
			try
			{
				// New XML element inserted into the document
				objXmlDoc.DocumentElement.InsertAfter(objXmlElement,objXmlNode);
			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}



		public XmlNode getNode(string sSelect)
		{

			return objXmlDoc.SelectSingleNode(sSelect) ;
				
		}

		public XmlNodeList getNodes(string sSelect)
		{

			return objXmlDoc.SelectNodes(sSelect);
				
		}

		public XmlNodeList getNodeList(string sElement)
		{

			return objXmlDoc.GetElementsByTagName(sElement);
				
		}

		public void SaveXml()
		{
			// An instance of FileStream class created
			// First parameter is the path to our XML file - Catalog.xml
			try
			{
				FileStream fsxml = new FileStream(sXmlFileName,FileMode.Truncate,FileAccess.Write,FileShare.ReadWrite);
			
				// XML Document Saved
				objXmlDoc.Save(fsxml);
				fsxml.Close();
			}
			catch (Exception objError)
			{
				//return  newElement;
				LogError.ErrorLog(sFileLogName,objError.Message);
				throw objError;
			}
		}



		//end of class

	}
}