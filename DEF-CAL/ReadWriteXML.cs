using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace GIIS_SHL_API
{

	class ReadWriteXML
	{
		protected internal string ServerName = null;
		protected internal string UserName = null;
		protected internal string Password = null;
		protected internal string DBName = null;
		protected internal string SageUserName = null;
		protected internal string SagePassword = null;
		protected internal string SageDBName = null;


		//  private static string configPassword = "SecretKey";
		// private static string configPassword = "AcrossDomain";
		private static byte[] _salt = Encoding.ASCII.GetBytes("0123456789abcdef");
		XmlDocument xmldoc = new XmlDocument();

		protected internal bool ReadXML()
		{
			bool result = true;
			try
			{
				bool checkRes = File.Exists(@"DefConfig.xml");
				//bool checkRes = File.Exists(@"C:\Users\Administrator\Desktop\Ajay\GIIS\GIIS-SHL-API\GIIS-SHL-API\bin\GiiSConfig.xml");
				//bool checkRes = File.Exists(@"I:\domains\mygiis.org\wwwroot\AccPac_Across\GiiSConfig.xml");
				//string dff=File.
				if (checkRes == true)
				{
					string xmlFile = File.ReadAllText("DefConfig.xml");
					//string xmlFile = File.ReadAllText(@"C:\Users\Administrator\Desktop\Ajay\GIIS\GIIS-SHL-API\GIIS-SHL-API\bin\GiiSConfig.xml");
					//string xmlFile = File.ReadAllText(@"I:\domains\mygiis.org\wwwroot\AccPac_Across\GiiSConfig.xml");
					xmldoc.LoadXml(xmlFile);
					XmlNodeList dblist = xmldoc.SelectNodes("dbconfig");
					foreach (XmlNode xn in dblist)
					{
						
						ServerName = xn["ServerName"].InnerText;
						Password = xn["Password"].InnerText;
						UserName = xn["UserName"].InnerText;
						DBName = xn["DBName"].InnerText;
						SagePassword = xn["SagePassword"].InnerText;
						SageUserName = xn["SageUserName"].InnerText;
						SageDBName = xn["SageDBName"].InnerText;
					}
				}
				else
				{
					result = checkRes;
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public string EncryptString(string plainText, string sharedSecret)
		{
			string result = null;
			RijndaelManaged aesAlg = null;

			try
			{
				Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);
				aesAlg = new RijndaelManaged();
				aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msEncrypt = new MemoryStream())
				{
					msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
					msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
					}
					result = Convert.ToBase64String(msEncrypt.ToArray());
				}
			}
			finally
			{
				if (aesAlg != null)
					aesAlg.Clear();
			}

			return result;
		}

		public string DecryptString(string cipherText, string sharedSecret)
		{
			RijndaelManaged aesAlg = null;
			string result = null;

			try
			{
				Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);
				byte[] bytes = Convert.FromBase64String(cipherText);
				using (MemoryStream msDecrypt = new MemoryStream(bytes))
				{
					aesAlg = new RijndaelManaged();
					aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
					aesAlg.IV = ReadByteArray(msDecrypt);
					ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							result = srDecrypt.ReadToEnd();
						}
					}
				}
			}
			finally
			{
				if (aesAlg != null)
					aesAlg.Clear();
			}

			return result;
		}

		private static byte[] ReadByteArray(Stream s)
		{
			byte[] rawLength = new byte[sizeof(int)];
			if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
			{
				throw new SystemException("Stream did not contain properly formatted byte array");
			}

			byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
			if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
			{
				throw new SystemException("Did not read byte array properly");
			}

			return buffer;
		}

	}
}