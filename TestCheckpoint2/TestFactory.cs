using NUnit.Framework;
using System;
using WCS;
using System.Globalization;
using System.Collections.Generic;

namespace WCSTest
{
	[TestFixture]
	class TestFactory
    {
		[Test]
		public void TestStudentCreation()
		{
			AbstractPerson person = PersonFactory.Create();
			Assert.AreEqual(person.GetType(), typeof(Student));
		}

		[Test]
		public void TestTrainerCreation()
		{
			List<AbstractPerson> people = new List<AbstractPerson> { new Student() };

			AbstractPerson person = PersonFactory.Create(people);
			Assert.AreEqual(person.GetType(), typeof(Trainer));
		}

		[Test]
		public void TestLeadingTrainerCreation()
		{
			List<AbstractPerson> people = new List<AbstractPerson> { new Trainer() };

			AbstractPerson person = PersonFactory.Create(people);
			Assert.IsTrue(person.IsLead);
		}
	}
}
