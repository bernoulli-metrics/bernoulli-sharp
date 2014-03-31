using System;

namespace Bernoulli
{
	public class Experiment
	{
		public Experiment ()
		{

		}

		public string ID 
		{
			get;
			set;
		}

		public string Name 
		{
			get;
			set;
		}

		public string User_ID 
		{
			get;
			set;
		}

		public string Variant 
		{
			get;
			set;
		}

		public Status Status 
		{
			get;
			set;
		}

		public string SegmentName 
		{
			get;
			set;
		}

		public int? Segment 
		{
			get;
			set;
		}
	}

	public enum Status {
		Inactive = 0,
		Active = 1,
		Paused = 2,
		Complete = 3,
	}
}

