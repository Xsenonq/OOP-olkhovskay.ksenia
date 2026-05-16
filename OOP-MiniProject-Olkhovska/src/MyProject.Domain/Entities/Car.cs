public class Car
{
    public Guid Id { get; }
    public string Model { get; }
    public bool IsAvailable { get; private set; }

    public Car(string model)
    {
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model is required");

        Id = Guid.NewGuid();
        Model = model;
        IsAvailable = true;
    }

    public void Rent()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("Already rented");

        IsAvailable = false;
    }

    public void Return()
    {
        IsAvailable = true;
    }
}