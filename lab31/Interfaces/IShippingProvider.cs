using lab31.Models;

namespace lab31.Interfaces;

public interface IShippingProvider
{
    bool Ship(Shipment shipment);
}