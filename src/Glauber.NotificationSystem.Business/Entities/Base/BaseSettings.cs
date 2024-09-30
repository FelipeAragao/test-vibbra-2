namespace Glauber.NotificationSystem.Business.Entities.Base;

public abstract class BaseSettings : BaseEntity
{
    public int AppId { get; set; }

    /* EF Relation */
    public App App { get; set; }
}
