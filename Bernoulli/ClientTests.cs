using System.Collections.Generic;
using NUnit.Framework;
using System;

namespace Bernoulli
{
	[TestFixture ()]
	public class ClientTests
	{
		[Test ()]
		public void TestNoClientID ()
		{
			bool doesThrow = false;

			try {
				Client.GetExperiments(null, new List<string> { "first" }, "user59", null);
			} catch (ArgumentException ex) {
				doesThrow = true;
			}

			Assert.IsTrue (doesThrow);
		}
	}
}

