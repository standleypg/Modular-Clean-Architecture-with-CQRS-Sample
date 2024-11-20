using MediatR;

namespace RetailPortal.Core.Entities.Common.Base;

public class EntityBase
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<INotification> _domainEvents = [];
    public IReadOnlyCollection<INotification> DomainEvents => this._domainEvents.AsReadOnly();

    protected EntityBase()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.UtcNow;
    }

    public void SetUpdatedDate()
    {
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void AddDomainEvent(INotification eventItem)
    {
        this._domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        this._domainEvents.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        this._domainEvents.Clear();
    }
}