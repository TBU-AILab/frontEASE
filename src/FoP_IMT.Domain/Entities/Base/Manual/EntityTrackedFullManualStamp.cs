namespace FoP_IMT.Domain.Entities.Base.Manual
{
    public class EntityTrackedFullManualStamp : EntityTrackedBaseManualStamp
    {
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
