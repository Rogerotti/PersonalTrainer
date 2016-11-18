using System;
using System.Linq;
using Framework.DataBaseContext;
using Framework.Models.Database;
using Framework.Models.Dto;

namespace Framework.Services
{
    public class UserGoalsManagement : IUserGoalsManagement
    {
        private readonly IUserManagement userManagement;
        private readonly DefaultContext context;

        public UserGoalsManagement(
            DefaultContext context,
            IUserManagement userManagement)
        {
            this.context = context;
            this.userManagement = userManagement;
        }

        public void SetGoals(UserGoalsDto userGoals)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                var id = userManagement.GetCurrentUserId();

                var goal = context.UserGoal.FirstOrDefault(x => x.UserId.Equals(id));

                if (goal == null)
                {
                    context.UserGoal.Add(new UserGoal()
                    {
                        UserId = id,
                        Calories = userGoals.Calories,
                        Carbohydrates = userGoals.Carbohydrates,
                        Fat = userGoals.Fat,
                        Proteins = userGoals.Proteins,
                    });

                    context.SaveChanges();
                }
                else
                {
                    goal.Calories = userGoals.Calories;
                    goal.Carbohydrates = userGoals.Carbohydrates;
                    goal.Fat = userGoals.Fat;
                    goal.Proteins = userGoals.Proteins;
                    context.UserGoal.Update(goal);
                    context.SaveChanges();
                }

                trans.Commit();
            }
        }

        public UserGoalsDto GetCurrentUserGoals()
        {
            var id = userManagement.GetCurrentUserId();
            var goals =  context.UserGoal.FirstOrDefault(x => x.UserId.Equals(id));
            if (goals == null)
            {
                return new UserGoalsDto()
                {
                    Calories = 0,
                    Carbohydrates = 0,
                    Fat = 0,
                    Proteins = 0,
                    UserId = id
                };
            }
        

            return new UserGoalsDto()
            {
                Calories = goals.Calories,
                Carbohydrates = goals.Carbohydrates,
                Fat = goals.Fat,
                Proteins = goals.Proteins,
                UserId = id
            };
        }

        public UserGoalsDto GetUserGoals(Guid userId)
        {
            var goals = context.UserGoal.FirstOrDefault(x => x.UserId.Equals(userId));
            if (goals == null)
                return null;

            return new UserGoalsDto()
            {
                Calories = goals.Calories,
                Carbohydrates = goals.Carbohydrates,
                Fat = goals.Fat,
                Proteins = goals.Proteins,
                UserId = userId
            };
        }
    }
}
