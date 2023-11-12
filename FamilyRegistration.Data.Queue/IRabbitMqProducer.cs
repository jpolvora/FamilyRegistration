namespace FamilyRegistration.Data.Queue;

public interface IRabbitMqProducer<in T>
{
    void Publish(T @event);
}
