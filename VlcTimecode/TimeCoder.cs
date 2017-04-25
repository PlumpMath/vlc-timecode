using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace VlcTimecode {
	public class TimeCoder {
		public static void GetTimecode()
		{
			using (var client = new WebClient())
			{
				String credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(":pass"));
				client.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";
				var xml = client.DownloadString(new Uri("http://localhost:8080/requests/status.xml"));
				
				var doc = new XmlDocument();
				doc.LoadXml(xml);

				XmlNode node = doc.SelectSingleNode("//time");
				
				var seconds = Int32.Parse(node.InnerText);
				var time = new TimeSpan(0, 0, seconds);
				var result = time.ToString(@"hh\:mm\:ss");

				SendKeys.SendWait(result);
			}
		}
	}
}
