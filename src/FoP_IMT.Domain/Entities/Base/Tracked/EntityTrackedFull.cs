namespace FoP_IMT.Domain.Entities.Base.Tracked
{
    public abstract class EntityTrackedFull : EntityTrackedBase
    {
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
