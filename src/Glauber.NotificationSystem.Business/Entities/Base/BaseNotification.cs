namespace Glauber.NotificationSystem.Business.Entities.Base;

public abstract class BaseNotification : BaseEntity
{
    public DateTime SendDate { get; set; }
    public int AppId { get; set; }

    /* EF Relation */
    public App App { get; set; }
}
