namespace IndependentWork19.IdGenerators;

public class SequentialIdGenerator : IIdGenerator
{
    private static int _counter = 0;

    public string GenerateId()
    {
        _counter++;
        return $"SEQ-{_counter}";
    }
}