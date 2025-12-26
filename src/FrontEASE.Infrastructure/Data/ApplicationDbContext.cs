using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Entities.Jobs;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.General;
using FrontEASE.Domain.Entities.Management.Tokens;
using FrontEASE.Domain.Entities.Management.Tokens.Connectors;
using FrontEASE.Domain.Entities.Shared.Addresses;
using FrontEASE.Domain.Entities.Shared.CountryCodes;
using FrontEASE.Domain.Entities.Shared.Images;
using FrontEASE.Domain.Entities.Shared.Resources;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.Enum;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.RepeatedMessage;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Infrastructure.Data.Configuration.Companies;
using FrontEASE.Infrastructure.Data.Configuration.Jobs;
using FrontEASE.Infrastructure.Data.Configuration.Management;
using FrontEASE.Infrastructure.Data.Configuration.Management.Modules;
using FrontEASE.Infrastructure.Data.Configuration.Management.Modules.Connectors;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Addresses;
using FrontEASE.Infrastructure.Data.Configuration.Shared.CountryCodes;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Images;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Resources;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Users;
using FrontEASE.Infrastructure.Data.Configuration.Tasks;
using FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs;
using FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules;
using FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params;
using FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values;
using FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values.Enums;
using FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values.List;
using FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.RepeatedMessage;
using FrontEASE.Infrastructure.Data.Configuration.Tasks.Messages;
using FrontEASE.Infrastructure.Data.Configuration.Tasks.Solutions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontEASE.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {

        /* App Control */
        public DbSet<JobLog> JobExecutions { get; set; }

        /* App Data */
        public DbSet<CountryCode> CountryCodes { get; set; }
        public DbSet<Resource> Resources { get; set; }

        /* Shared Data */
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Company> Companies { get; set; }

        /* Management Data */
        public DbSet<UserPreferences> UserPreferences { get; set; }
        public DbSet<UserPreferenceTokenOption> UserPreferenceTokenOptions { get; set; }
        public DbSet<UserPreferenceTokenOptionConnectorType> UserPreferenceTokenOptionConnectorTypes { get; set; }
        public DbSet<UserPreferenceGeneralOptions> UserPreferenceGeneralOptions { get; set; }

        /* Tasks */
        public DbSet<Domain.Entities.Tasks.Task> Tasks { get; set; }
        public DbSet<TaskConfig> TaskConfigurations { get; set; }
        public DbSet<TaskMessage> TaskMessages { get; set; }
        public DbSet<TaskSolution> TaskSolutions { get; set; }
        public DbSet<TaskConfigRepeatedMessage> TaskRepeatedMessages { get; set; }
        public DbSet<TaskConfigRepeatedMessageItem> TaskRepeatedMessageItems { get; set; }

        /* Task Modules */
        public DbSet<TaskModuleEntity> TaskModules { get; set; }
        public DbSet<TaskModuleParameterEntity> TaskModuleParameters { get; set; }
        public DbSet<TaskModuleParameterValueEntity> TaskModuleParameterValues { get; set; }
        public DbSet<TaskModuleParameterEnumValueEntity> TaskModuleParameterValueEnumValues { get; set; }
        public DbSet<TaskModuleParameterListValueEntity> TaskModuleParameterValueListValues { get; set; }
        public DbSet<TaskModuleParameterListValueItemEntity> TaskModuleParameterValueListValueItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ApplyCustomConfigurations(builder);
        }

        private static void ApplyCustomConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new JobLogConfiguration());

            builder.ApplyConfiguration(new CountryCodeConfiguration());
            builder.ApplyConfiguration(new ResourceConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());

            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());

            builder.ApplyConfiguration(new UserPreferencesConfiguration());
            builder.ApplyConfiguration(new UserPreferenceTokenOptionsConfiguration());
            builder.ApplyConfiguration(new UserPreferenceGeneralOptionsConfiguration());
            builder.ApplyConfiguration(new UserPreferenceTokenOptionConnectorTypeConfiguration());

            builder.ApplyConfiguration(new TaskConfiguration());
            builder.ApplyConfiguration(new TaskSolutionConfiguration());
            builder.ApplyConfiguration(new TaskMessageConfiguration());
            builder.ApplyConfiguration(new TaskConfigConfiguration());
            builder.ApplyConfiguration(new TaskConfigRepeatedMessageConfiguration());
            builder.ApplyConfiguration(new TaskConfigRepeatedMessageItemConfiguration());

            builder.ApplyConfiguration(new TaskModuleConfiguration());
            builder.ApplyConfiguration(new TaskModuleParameterConfiguration());
            builder.ApplyConfiguration(new TaskModuleParameterValueConfiguration());
            builder.ApplyConfiguration(new TaskModuleParameterEnumValueConfiguration());
            builder.ApplyConfiguration(new TaskModuleParameterListValueConfiguration());
            builder.ApplyConfiguration(new TaskModuleParameterListValueItemConfiguration());
        }

        private void HandleTrackedEntitiesBase()
        {
            var trackedStates = new EntityState[] { EntityState.Added, EntityState.Deleted };
            var changedEntitiesTrackedBase = ChangeTracker.Entries().Where(e => e.Entity is EntityTrackedBase && trackedStates.Contains(e.State));

            foreach (var item in changedEntitiesTrackedBase)
            {
                var entity = item.Entity as EntityTrackedBase;
                if (entity is not null)
                {
                    if (item.State == EntityState.Added)
                    {
                        entity.DateCreated = DateTime.UtcNow;
                    }
                }
            }
        }

        private void HandleTrackedEntitiesFull()
        {
            var trackedStates = new EntityState[] { EntityState.Modified, EntityState.Added, EntityState.Deleted };
            var changedEntitiesTrackedFull = ChangeTracker.Entries().Where(e => e.Entity is EntityTrackedFull && trackedStates.Contains(e.State));

            foreach (var item in changedEntitiesTrackedFull)
            {
                var entity = item.Entity as EntityTrackedFull;
                if (entity is not null)
                {
                    if (item.State == EntityState.Added)
                    {
                        entity.DateCreated = DateTime.UtcNow;
                    }
                    entity.DateUpdated = DateTime.UtcNow;
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            HandleTrackedEntitiesBase();
            HandleTrackedEntitiesFull();

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
