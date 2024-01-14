
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestAppAPI.Tests
{
    [TestFixture]
    public class StudyGroupTests
    {
        private readonly string GROUPNAME = "test_test";
        [Test]
        public void TestStudyGroup_Creates([Values] Subject subject)
        {
            var studyGroup = new StudyGroup(1, GROUPNAME, subject, DateTime.Now, new List<User>());
            Assert.NotNull(studyGroup);
        }

        //TODO missing implementation
        [Test]
        public void TestStudyGroup_LessThan5InName_Throws()
        {
            Assert.Throws<ArgumentException>(() => { new StudyGroup(1, new string('1', 3), Subject.Chemistry, DateTime.Now, new List<User>()); });
        }

        //TODO missing implementation
        [Test]
        public void TestStudyGroup_MoreThan20InName_Throws()
        {
            Assert.Throws<ArgumentException>(() => { new StudyGroup(1, new string('1', 20), Subject.Chemistry, DateTime.Now, new List<User>()); });
        }


        [Test]
        public void TestAddUser_Adds()
        {
            var user = new User(1);
            var studyGroup = new StudyGroup(1, GROUPNAME, Subject.Chemistry, DateTime.Now, new List<User>());
            studyGroup.AddUser(user);
            Assert.That(studyGroup.Users.Contains(user));
        }

        //TODO missing implementation
        [Test]
        public void TestAddUser_Duplicated_Throws()
        {
            var user = new User(1);
            var studyGroup = new StudyGroup(1, GROUPNAME, Subject.Chemistry, DateTime.Now, new List<User>() { user });
            Assert.Throws<ArgumentException>(() => { studyGroup.AddUser(user); });
        }


        [Test]
        public void TestRemoveUser_Removes()
        {
            var user = new User(1);
            var studyGroup = new StudyGroup(1, GROUPNAME, Subject.Chemistry, DateTime.Now, new List<User>() { user });
            studyGroup.RemoveUser(user);
            Assert.That(!studyGroup.Users.Contains(user));
        }

        //TODO missing implementation
        [Test]
        public void TestRemoveUser_NonExistant_Throws()
        {
            var user = new User(1);
            var studyGroup = new StudyGroup(1, GROUPNAME, Subject.Chemistry, DateTime.Now, new List<User>());
            studyGroup.RemoveUser(user);
            Assert.Throws<ArgumentException>(() => { studyGroup.RemoveUser(user); });
        }
    }

}
