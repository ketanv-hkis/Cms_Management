using CMSManagement_API.Models;
using CMSManagement_API.Repository;
using System;

namespace CMSManagement_API.Services
{
    public class TeamService : ITeamService
    {

        public readonly ITeamRepository _teamRepository;
        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }


        public List<Team> GetLanguage()
        {
            List<Team> TeamAssign = _teamRepository.GetLanguage();
            return TeamAssign;
        }

        public void SaveTeamLanguage(Team team)
        {
            _teamRepository.SaveTeamLanguage(team);
        }

        public void SaveTeamAssign(TeamAssign teamAssign)
        {
            _teamRepository.SaveTeamAssign(teamAssign);
        }

        public List<TeamAssign> GetTeamAssign(int TeamAssignId)
        {
            List<TeamAssign> teamAssigns = _teamRepository.GetTeamAssign(TeamAssignId);
            return teamAssigns;
        }

        public void UpdateTeamAssign(TeamAssign teamAssign)
        {
            _teamRepository.SaveTeamAssign(teamAssign);
        }


    }
}
