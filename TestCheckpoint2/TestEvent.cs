using NUnit.Framework;
using System;
using WCS;

namespace WCSTest
{
	[TestFixture]
	public class TestEvent
	{
		[Test]
		public void TestEventPostponed()
		{
			Event newEvent = new Event("TestPresent");
			newEvent.StartTime = DateTime.Now - TimeSpan.FromMinutes(1);
			newEvent.EndTime = newEvent.StartTime + TimeSpan.FromHours(1);
			DateTime startDateBeforePostpone = newEvent.StartTime;
			DateTime endDateBeforePostpone = newEvent.EndTime;

			newEvent.Postpone(TimeSpan.FromDays(1));

			Assert.AreEqual(startDateBeforePostpone, newEvent.StartTime - TimeSpan.FromDays(1));
			Assert.AreEqual(endDateBeforePostpone, newEvent.EndTime - TimeSpan.FromDays(1));
		}
}
}
