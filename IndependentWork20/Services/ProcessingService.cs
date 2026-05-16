using System;
using IndependentWork20.Strategy;
using IndependentWork20.Observer;

namespace IndependentWork20.Services;

public class ProcessingService
{
    private readonly DataContext _context;
    private readonly DataPublisher _publisher;

    public ProcessingService(DataContext context, DataPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public void Process(string data)
    {
        var result = _context.ExecuteProcessing(data);

        Console.WriteLine(result);

        _publisher.PublishDataProcessed(result);
    }

    public void ChangeStrategy(IDataProcessorStrategy strategy)
    {
        _context.SetStrategy(strategy);
    }
}