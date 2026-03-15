using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Domain.Entities.Management.General.Columns;
using FrontEASE.Infrastructure.Data;
using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FrontEASE.DataGenerator.Services.Generators.Resources
{
    [Order(3)]
    public class UserDefaultsGenerator(ApplicationDbContext dataContext) : IGenerator
    {
        private readonly IList<TaskGridColumnsVisibility> defaultVisibleTaskGridColumns =
        [
            TaskGridColumnsVisibility.NAME,
            TaskGridColumnsVisibility.DATE_CREATED,
            TaskGridColumnsVisibility.DATE_UPDATED,
            TaskGridColumnsVisibility.AUTHOR,
            TaskGridColumnsVisibility.STATE
        ];

        public async Task Generate()
        {
            var users = await dataContext.Users
                .Include(x => x.UserPreferences)
                    .ThenInclude(x => x.GeneralOptions)
                        .ThenInclude(x => x.TaskGridColumns)
                .ToListAsync();

            var usersWithoutColumnPrefs = users.Where(x => x.UserPreferences?.GeneralOptions?.TaskGridColumns?.Count < 1);
            foreach (var defaultColumn in defaultVisibleTaskGridColumns)
            {
                foreach (var user in usersWithoutColumnPrefs)
                {
                    if (user.UserPreferences?.GeneralOptions is not null)
                    {
                        var defaultDisplayedColumns = defaultVisibleTaskGridColumns.Select(x => new UserPreferenceGeneralOptionTaskGridColumn()
                        {
                            Column = x
                        });
                        user.UserPreferences.GeneralOptions.TaskGridColumns.AddRange(defaultDisplayedColumns);
                    }
                }
            }

            await dataContext.SaveChangesAsync();
        }
    }
}
