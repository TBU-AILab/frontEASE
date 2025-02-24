using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Domain.Entities.Shared.Addresses;
using FoP_IMT.Domain.Entities.Shared.Images;
using FoP_IMT.Domain.Entities.Shared.Users;

namespace FoP_IMT.Domain.Entities.Companies
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
