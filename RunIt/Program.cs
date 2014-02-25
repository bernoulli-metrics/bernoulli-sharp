using System;
using Bernoulli;
using System.Collections.Generic;

namespace RunIt
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			List<Experiment> experiments = Client.GetExperiments ("1", new List<string> { "first" }, "user59", null);

			foreach (Experiment experiment in experiments) {
				Console.WriteLine (experiment.Name);

				Console.WriteLine(Client.GoalAttained ("1", "first", "user59"));
			}

		}

		private string GetSignupCopy() 
		{
			List<Experiment> experiments = Client.GetExperiments("1", new List<string> { "first" }, "user59", null);
			return experiments[0].Variant;
		}

		private void GoalAttained()
		{
			string clientId = Environment.GetEnvironmentVariable("BERNOULLI_CLIENT_ID");
			Client.GoalAttained(clientId, "first", "user59");
		}
	}
}
