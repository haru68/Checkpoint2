using NUnit.Framework;
using System;
using WCS;
using System.Globalization;

namespace WCSTest
{
	[TestFixture]
	public class TestEvent
	{
		[Test]
		public void TestEventPostponed()
		{
			Event newEvent = new Event("TestPresent", DateTime.Now, DateTime.Now);
			newEvent.StartTime = DateTime.Now - TimeSpan.FromMinutes(1);
			newEvent.EndTime = newEvent.StartTime + TimeSpan.FromHours(1);
			DateTime startDateBeforePostpone = newEvent.StartTime;
			DateTime endDateBeforePostpone = newEvent.EndTime;

			newEvent.Postpone(TimeSpan.FromDays(1));

			Assert.AreEqual(startDateBeforePostpone, newEvent.StartTime - TimeSpan.FromDays(1));
			Assert.AreEqual(endDateBeforePostpone, newEvent.EndTime - TimeSpan.FromDays(1));
		}
	}

	[TestFixture]
	public class TestDatabase
	{
		[Test]
		public void TestGetCursusFromName()
		{
			string cursusName = "Cursus 1";
			Cursus cursus = Database.GetInstance().GetCursusFromName(cursusName);

			Assert.AreEqual(cursus.StartDate, DateTime.ParseExact("20191208", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None));
		}
	}
}
