namespace KP.Synchronization.ESAS.Synchronizations
{
    /// <summary>
    /// A delivery-mechanism is something that knows how to take the loaded entites from an IEsasEntitiesLoaderStrategy 
    /// and send it somewhere - to rabbitMq, to a database, what have you.
    /// </summary>
    public interface IEsasDeliveryMechanism
    {
        void Deliver(object o);
    }
}

