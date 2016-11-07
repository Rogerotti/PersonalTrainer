using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Models;
using Framework.DataBaseContext;
using Framework.Models.Database;

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

                var goal = context.UserGoal.FirstOrDefault(x => x.UserId.Equals(userGoals));

                if (goal == null)
                {
                    context.UserGoal.Add(new UserGoal()
                    {
                        UserId = id,
                        BodyFat = userGoals.BodyFat,
                        Calories = userGoals.Calories,
                        Carbohydrates = userGoals.Carbohydrates,
                        Fat = userGoals.Fat,
                        Fibre = userGoals.Fibre,
                        Proteins = userGoals.Proteins,
                        Weight = userGoals.Weight
                    });

                    context.SaveChanges();
                }
                else
                {
                    goal.BodyFat = userGoals.BodyFat;
                    goal.Calories = userGoals.Calories;
                    goal.Carbohydrates = userGoals.Carbohydrates;
                    goal.Fat = userGoals.Fat;
                    goal.Fibre = userGoals.Fibre;
                    goal.Proteins = userGoals.Proteins;
                    goal.Weight = userGoals.Weight;
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
                    BodyFat = 0,
                    Calories = 0,
                    Carbohydrates = 0,
                    Fat = 0,
                    Fibre = 0,
                    Proteins = 0,
                    Weight = 50,
                    UserId = id
                };
            }
        

            return new UserGoalsDto()
            {
                BodyFat = goals.BodyFat,
                Calories = goals.Calories,
                Carbohydrates = goals.Carbohydrates,
                Fat = goals.Fat,
                Fibre = goals.Fibre,
                Proteins = goals.Proteins,
                Weight = goals.Weight,
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
                BodyFat = goals.BodyFat,
                Calories = goals.Calories,
                Carbohydrates = goals.Carbohydrates,
                Fat = goals.Fat,
                Fibre = goals.Fibre,
                Proteins = goals.Proteins,
                Weight = goals.Weight,
                UserId = userId
            };
        }
    }
}
