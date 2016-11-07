using Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Services
{
    public interface IUserGoalsManagement
    {
        void SetGoals(UserGoalsDto userGoals);

        UserGoalsDto GetCurrentUserGoals();

        UserGoalsDto GetUserGoals(Guid userId);
    }
}
