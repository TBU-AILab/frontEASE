using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Shared.Addresses;
using FrontEASE.Domain.Entities.Shared.Images;
using FrontEASE.Domain.Entities.Shared.Users;

namespace FrontEASE.Domain.Entities.Companies
{
    public class Company : EntityTrackedFull
    {
        public Company()
        {
            Name = string.Empty;
            Users = [];
            Tasks = [];
        }

        #region Navigation

        public Guid? AddressID { get; set; }
        public Address? Address { get; set; }

        public Guid? ImageID { get; set; }
        public Image? Image { get; set; }

        public IList<ApplicationUser> Users { get; set; }
        public IList<Tasks.Task> Tasks { get; set; }

        #endregion

        #region Data

        public string Name { get; set; }
        public string? NameAbbreviation { get; set; }

        #endregion
    }
}
