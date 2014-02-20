using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;

namespace Bernoulli
{
	public static class Client
	{
		private const string URL = "http://localhost:5000/client/api/experiments/";

		public static List<Experiment> GetExperiments(string clientId, List<string> experimentIds, string userId, Dictionary<string, string> segmentData) 
		{
			if (String.IsNullOrEmpty(clientId)) 
			{
				throw new ArgumentException("no clientId", "clientId");
			}

			Dictionary<string, string> query = new Dictionary<string, string> ();
			query.Add("clientId", clientId);
			query.Add("experimentIds", String.Join(",", experimentIds));
			query.Add("userId", userId);

			if (segmentData != null) 
			{
				foreach (KeyValuePair<string, string> pair in segmentData) 
				{
					query.Add (pair.Key, pair.Value);
				}
			}

			string fullUrl = URL + "?" + CreateQueryString(query);
			var res = MakeRequest<List<Experiment>>(fullUrl);
			return res.Value;
		}

		public static bool GoalAttained(string clientId, string experimentId, string userId) 
		{
			if (String.IsNullOrEmpty(clientId))
			{
				throw new ArgumentException("clientId");
			}

			if (String.IsNullOrEmpty(experimentId)) 
			{
				throw new ArgumentException("experimentId");
			}

			if (String.IsNullOrEmpty(userId)) 
			{
				throw new ArgumentException("userId");
			}

			var res = MakeRequest<PostValue>(URL, new Dictionary<string, string> {
				{ "clientId", clientId },
				{ "experimentId", experimentId },
				{ "userId", userId },
			});
			return res.Value.Value;
		}

		private static string CreateQueryString(Dictionary<string, string> query) 
		{
			List<string> queryList = new List<string> ();

			foreach (KeyValuePair<string, string> pair in query) 
			{
				queryList.Add(pair.Key + "=" + Uri.EscapeDataString(pair.Value));
			}

			return String.Join("&", queryList);
		}

		private static Response<T> MakeRequest<T>(string url, Dictionary<string, string> postParams = null)
		{
			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
			if (postParams != null) 
			{
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";

				List<string> strs = new List<string>();
				foreach (KeyValuePair<string, string> pair in postParams) 
				{
					strs.Add(pair.Key + "=" + pair.Value);
				}

				var data = Encoding.ASCII.GetBytes(String.Join("&", strs));

				using (var stream = request.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}
			}

			using (HttpWebResponse response = request.GetResponse () as HttpWebResponse) 
			{
				StreamReader reader = new StreamReader (response.GetResponseStream ());
				string responseString = reader.ReadToEnd ();
				reader.Close ();

				JavaScriptSerializer serializer = new JavaScriptSerializer ();
				Response<T> res = serializer.Deserialize<Response<T>> (responseString);

				if (res.Status != "ok") {
					throw new Exception (res.Message);
				}

				return res;
			}
		}
	}
}

