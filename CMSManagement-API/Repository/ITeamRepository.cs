using CMSManagement_API.Models;
using System;

namespace CMSManagement_API.Repository
{
    public interface ITeamRepository
    {

        List<Team> GetLanguage();

        void SaveTeamLanguage(Team team);

        void SaveTeamAssign(TeamAssign teamAssign);
    }
}
