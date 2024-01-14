using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace TestAppAPI.Tests
{
    [TestFixture]
    public class StudyGroupControllerTests
    {
        private StudyGroupController _controller;
        private StudyGroupRepositoryMock _repositoryMock;
        private readonly string GROUPNAME = "test_test";

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new StudyGroupRepositoryMock();
            _controller = new StudyGroupController(_repositoryMock);
        }

        [Test]
        public async Task TestCreateStudyGroup_OnValidInput_ReturnsOkResult()
        {
            var creationDate = DateTime.Now;
            var group = new StudyGroup(1, GROUPNAME, Subject.Chemistry, creationDate, new List<User>());
            var result = await _controller.CreateStudyGroup(group);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkResult>());
            var groups = _repositoryMock.groups;
            Assert.That(groups.Contains(group));
            Assert.That(groups.First(g => g.StudyGroupId == group.StudyGroupId).CreateDate == creationDate);
        }

        [Test]
        public async Task TestCreateStudyGroup_OnDuplicateInput_Throws()
        {
            var creationDate = DateTime.Now;
            var group = new StudyGroup(1, GROUPNAME, Subject.Chemistry, creationDate, new List<User>());
            var result = await _controller.CreateStudyGroup(group);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.ThrowsAsync<ArgumentException>(
                async () => { result = await _controller.CreateStudyGroup(group); }
                );
        }

        [Test]
        public async Task TestGetStudyGroup_ReturnsGroups()
        {
            var creationDate = DateTime.Now;
            _repositoryMock.groups = new List<StudyGroup>()
            {
                new StudyGroup(1,GROUPNAME,Subject.Chemistry,creationDate,new List<User>()),
                new StudyGroup(2,GROUPNAME,Subject.Physics,creationDate,new List<User>())
            };
            var okObjectResult = (OkObjectResult)await _controller.GetStudyGroups();
            Assert.NotNull(okObjectResult);
            var groups = (List<StudyGroup>)okObjectResult.Value;
            Assert.IsNotEmpty(groups);
        }

        [Test]
        public async Task TestGetStudyGroup_ReturnsEmpty()
        {
            var okObjectResult = (OkObjectResult)await _controller.GetStudyGroups();
            Assert.NotNull(okObjectResult);
            var groups = (List<StudyGroup>)okObjectResult.Value;
            Assert.IsEmpty(groups);
        }

        [Test]
        public async Task TestSearchStudyGroups_ReturnsGroups()
        {
            var creationDate = DateTime.Now;
            _repositoryMock.groups = new List<StudyGroup>()
            {
                new StudyGroup(1,GROUPNAME,Subject.Chemistry,creationDate,new List<User>()),
                new StudyGroup(2,GROUPNAME,Subject.Physics,creationDate,new List<User>())
            };
            var okObjectResult = (OkObjectResult)await _controller.SearchStudyGroups(Subject.Physics.ToString());
            Assert.NotNull(okObjectResult);
            var groups = (List<StudyGroup>)okObjectResult.Value;
            Assert.IsNotEmpty(groups);

        }

        [Test]
        public async Task TestSearchStudyGroups_ReturnsEmpty()
        {
            var creationDate = DateTime.Now;
            _repositoryMock.groups = new List<StudyGroup>()
            {
                new StudyGroup(1,GROUPNAME,Subject.Chemistry,creationDate,new List<User>()),
                new StudyGroup(2,GROUPNAME,Subject.Physics,creationDate,new List<User>())
            };
            var okObjectResult = (OkObjectResult)await _controller.SearchStudyGroups(Subject.Math.ToString());
            Assert.NotNull(okObjectResult);
            var groups = (List<StudyGroup>)okObjectResult.Value;
            Assert.IsEmpty(groups);
        }

        [Test]
        public async Task TestJoinStudyGroup_Joins()
        {
            var creationDate = DateTime.Now;
            var user = new User(1);
            var group = new StudyGroup(1, GROUPNAME, Subject.Chemistry, creationDate, new List<User>());
            _repositoryMock.groups = new List<StudyGroup>()
            {
                group
            };
            var result = await _controller.JoinStudyGroup(group.StudyGroupId, user.Id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(
                _repositoryMock.groups.First(g => g.StudyGroupId == group.StudyGroupId)
                .Users.Any(u => u.Id == user.Id));
        }

        //TODO missing implementation
        [Test]
        public async Task TestJoinStudyGroup_OnDuplicateEntry_Throws()
        {
            var creationDate = DateTime.Now;
            var user = new User(1);
            var group = new StudyGroup(1, GROUPNAME, Subject.Chemistry, creationDate, new List<User>() { user });
            _repositoryMock.groups = new List<StudyGroup>()
            {
                group
            };
            var result = await _controller.JoinStudyGroup(group.StudyGroupId, user.Id);
            Assert.ThrowsAsync<ArgumentException>(async () => { await _controller.JoinStudyGroup(group.StudyGroupId, user.Id); });
        }

        [Test]
        public async Task TestLeaveStudyGroup_Leaves()
        {
            var creationDate = DateTime.Now;
            var user = new User(1);
            var group = new StudyGroup(1, GROUPNAME, Subject.Chemistry, creationDate, new List<User>() { user });
            _repositoryMock.groups = new List<StudyGroup>()
            {
                group
            };
            var result = await _controller.LeaveStudyGroup(group.StudyGroupId, user.Id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(
                !_repositoryMock.groups.First(g => g.StudyGroupId == group.StudyGroupId)
                .Users.Any(u => u.Id == user.Id));
        }

        //TODO missing implementation
        [Test]
        public async Task TestLeaveStudyGroup_OnNonExistant_Throws()
        {
            var creationDate = DateTime.Now;
            var user = new User(1);
            var group = new StudyGroup(1, GROUPNAME, Subject.Chemistry, creationDate, new List<User>());
            _repositoryMock.groups = new List<StudyGroup>()
            {
                group
            };
            Assert.ThrowsAsync<ArgumentException>(async () => { await _controller.LeaveStudyGroup(group.StudyGroupId, user.Id); });
        }
    }
}