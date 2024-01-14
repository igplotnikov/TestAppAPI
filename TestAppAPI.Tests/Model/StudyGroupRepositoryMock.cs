using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAPI
{

    public class StudyGroupRepositoryMock : IStudyGroupRepository
    {
        internal List<StudyGroup> groups;

        public StudyGroupRepositoryMock()
        {
            groups = new List<StudyGroup>();
        }
        public Task CreateStudyGroup(StudyGroup studyGroup)
        {
            if (groups.Any(g => g.Subject == studyGroup.Subject) || groups.Any(g => g.StudyGroupId == studyGroup.StudyGroupId))
            {
                throw new ArgumentException();
            }
            groups.Add(studyGroup);
            return Task.CompletedTask;
        }
        public Task<List<StudyGroup>> GetStudyGroups()
        {
            return Task.FromResult(groups);
        }
        public Task JoinStudyGroup(int studyGroupId, int userId)
        {
            groups.Find(g => g.StudyGroupId == studyGroupId).AddUser(new User(userId));
            return Task.CompletedTask;
        }

        public Task LeaveStudyGroup(int studyGroupId, int userId)
        {

            groups.Find(g => g.StudyGroupId == studyGroupId).RemoveUser(new User(userId));
            return Task.CompletedTask;
        }
        public Task<List<StudyGroup>> SearchStudyGroups(string subject)
        {
            return Task.FromResult(groups.FindAll(g => g.Subject.ToString() == subject));
        }
    }

}
