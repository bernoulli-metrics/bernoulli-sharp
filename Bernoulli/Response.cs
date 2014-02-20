using System;

namespace Bernoulli
{
	public class Response<T>
	{
		public string Status {
			get; set;
		}

		public T Value {
			get;
			set;
		}

		public string Message {
			get;
			set;
		}
	}

	public class PostValue {
		public bool Value {
			get; set;
		}
	}
}

