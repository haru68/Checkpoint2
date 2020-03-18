using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System;
using WCS;
using System.Globalization;

namespace TestCheckpoint2
{
    class FunctionnalTest
    {
		[TestFixture]
		public class TestDatabase
		{
			[SetUp]
			public void SetUpCursus()
			{
				Database.GetInstance().GetCursusFromId(3);
			}

			[Test]
			public void TestGetCursusFromName()
			{
				int cursusId = 1;
				Cursus cursus = Database.GetInstance().GetCursusFromId(cursusId);

				Assert.AreEqual(cursus.StartDate, DateTime.ParseExact("20191208", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None));
			}
		}
	}
}
