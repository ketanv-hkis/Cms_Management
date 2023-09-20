﻿using CMSManagement_API.Models;

namespace CMSManagement_API.Services
{
    public interface ITeamService
    {
        List<Team> GetLanguage();

        void SaveTeamLanguage(Team team);
        void SaveTeamAssign(TeamAssign teamAssign);
    }
}
