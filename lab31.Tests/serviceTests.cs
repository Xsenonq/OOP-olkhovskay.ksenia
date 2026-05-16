using Xunit;
using Moq;
using lab31.Models;
using lab31.Interfaces;
using lab31.Services;

namespace lab31.Tests;

public class ShippingServiceTests
{
    private readonly Mock<IShippingProvider> _shippingMock;
    private readonly Mock<ITrackingService> _trackingMock;
    private readonly ShippingService _service;

    public ShippingServiceTests()
    {
        _shippingMock = new Mock<IShippingProvider>();
        _trackingMock = new Mock<ITrackingService>();

        _service = new ShippingService(_shippingMock.Object, _trackingMock.Object);
    }

    [Fact]
    public void ShipOrder_Should_CallShippingProvider()
    {
        var shipment = new Shipment { Id = 1, Address = "Kyiv", Weight = 5 };

        _shippingMock.Setup(s => s.Ship(shipment)).Returns(true);

        _service.ShipOrder(shipment);

        _shippingMock.Verify(s => s.Ship(shipment), Times.Once);
    }

    [Fact]
    public void ShipOrder_Should_StartTracking_WhenShippingSuccess()
    {
        var shipment = new Shipment { Id = 2, Address = "Lviv", Weight = 3 };

        _shippingMock.Setup(s => s.Ship(shipment)).Returns(true);

        _service.ShipOrder(shipment);

        _trackingMock.Verify(t => t.StartTracking(2), Times.Once);
    }

    [Fact]
    public void ShipOrder_Should_NotStartTracking_WhenShippingFails()
    {
        var shipment = new Shipment { Id = 3, Address = "Odesa", Weight = 2 };

        _shippingMock.Setup(s => s.Ship(shipment)).Returns(false);

        _service.ShipOrder(shipment);

        _trackingMock.Verify(t => t.StartTracking(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void ShipOrder_Should_ThrowException_WhenWeightInvalid()
    {
        var shipment = new Shipment { Id = 4, Address = "Dnipro", Weight = 0 };

        Assert.Throws<ArgumentException>(() => _service.ShipOrder(shipment));
    }

    [Fact]
    public void ShipOrder_Should_ReturnTrue_WhenShippingSuccess()
    {
        var shipment = new Shipment { Id = 5, Address = "Kharkiv", Weight = 4 };

        _shippingMock.Setup(s => s.Ship(shipment)).Returns(true);

        var result = _service.ShipOrder(shipment);

        Assert.True(result);
    }

    [Fact]
    public void ShipOrder_Should_ReturnFalse_WhenShippingFails()
    {
        var shipment = new Shipment { Id = 6, Address = "Poltava", Weight = 4 };

        _shippingMock.Setup(s => s.Ship(shipment)).Returns(false);

        var result = _service.ShipOrder(shipment);

        Assert.False(result);
    }

    [Fact]
    public void ShipOrder_Should_CallTrackingOnce()
    {
        var shipment = new Shipment { Id = 7, Address = "Sumy", Weight = 3 };

        _shippingMock.Setup(s => s.Ship(shipment)).Returns(true);

        _service.ShipOrder(shipment);

        _trackingMock.Verify(t => t.StartTracking(7), Times.Once);
    }

    [Fact]
    public void ShipOrder_Should_NotCallTracking_WhenFailed()
    {
        var shipment = new Shipment { Id = 8, Address = "Chernihiv", Weight = 2 };

        _shippingMock.Setup(s => s.Ship(shipment)).Returns(false);

        _service.ShipOrder(shipment);

        _trackingMock.Verify(t => t.StartTracking(It.IsAny<int>()), Times.Never);
    }
}