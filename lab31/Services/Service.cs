using lab31.Interfaces;
using lab31.Models;

namespace lab31.Services;

public class ShippingService
{
    private readonly IShippingProvider _shippingProvider;
    private readonly ITrackingService _trackingService;

    public ShippingService(IShippingProvider shippingProvider, ITrackingService trackingService)
    {
        _shippingProvider = shippingProvider;
        _trackingService = trackingService;
    }

    public bool ShipOrder(Shipment shipment)
    {
        if (shipment.Weight <= 0)
            throw new ArgumentException("Invalid weight");

        var result = _shippingProvider.Ship(shipment);

        if (result)
        {
            _trackingService.StartTracking(shipment.Id);
        }

        return result;
    }
}