using Flunt.Notifications;

namespace RichDomainModeling.Shared.Entities
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
