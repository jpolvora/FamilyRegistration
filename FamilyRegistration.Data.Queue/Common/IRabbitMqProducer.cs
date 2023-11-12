namespace FamilyRegistration.Data.Queue.Common;

public interface IRabbitMqProducer<in T>
{
    void Publish(T @event);
}
