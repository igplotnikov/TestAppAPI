using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestAppAPI
{
    public interface IStudyGroupRepository
    {
        Task CreateStudyGroup(StudyGroup studyGroup);
        Task<List<StudyGroup>> GetStudyGroups();
        Task JoinStudyGroup(int studyGroupId, int userId);
        Task LeaveStudyGroup(int studyGroupId, int userId);
        Task<List<StudyGroup>> SearchStudyGroups(string subject);
    }
}