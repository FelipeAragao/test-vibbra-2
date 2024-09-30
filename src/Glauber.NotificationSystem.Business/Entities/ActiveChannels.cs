using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Entities;

public class ActiveChannels : BaseEntity
{
    public int AppId { get; set; }
    public bool WebPush { get; set; }
    public bool Email { get; set; }
    public bool SMS { get; set; }

    /* EF Relation */
    public App? App { get; set; }
}