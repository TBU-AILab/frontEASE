using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Domain.Entities.Companies;
using FoP_IMT.Domain.Entities.Management;
using FoP_IMT.Domain.Entities.Management.General;
using FoP_IMT.Domain.Entities.Management.Tokens;
using FoP_IMT.Domain.Entities.Shared.Addresses;
using FoP_IMT.Domain.Entities.Shared.CountryCodes;
using FoP_IMT.Domain.Entities.Shared.Images;
using FoP_IMT.Domain.Entities.Shared.Resources;
using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Domain.Entities.Tasks.Configs;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.RepeatedMessage;
using FoP_IMT.Domain.Entities.Tasks.Messages;
using FoP_IMT.Domain.Entities.Tasks.Solutions;
using FoP_IMT.Infrastructure.Data.Configuration.Companies;
using FoP_IMT.Infrastructure.Data.Configuration.Management;
using FoP_IMT.Infrastructure.Data.Configuration.Management.Modules;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Addresses;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.CountryCodes;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Images;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Resources;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Users;
using FoP_IMT.Infrastructure.Data.Configuration.Tasks;
using FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs;
using FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs.Modules;
using FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params;
using FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values;
using FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs.Modules.RepeatedMessage;
using FoP_IMT.Infrastructure.Data.Configuration.Tasks.Runs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoP_IMT.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ApplyCustomConfigurations(builder);
        }

        private void ApplyCustomConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CountryCodeConfiguration());
            builder.ApplyConfiguration(new ResourceConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());

            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());

            builder.ApplyConfiguration(new UserPreferencesConfiguration());
            builder.ApplyConfiguration(new UserPreferenceTokenOptionsConfiguration());
            builder.ApplyConfiguration(new UserPreferenceGeneralOptionsConfiguration());

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
