using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface BouquetEventService
{
    public dynamic findByEventId(int eventId);

    public bool Created(BouquetEvent bouquetEvent);
    public bool DeleteByBouquetId(int bouquetId);
}
