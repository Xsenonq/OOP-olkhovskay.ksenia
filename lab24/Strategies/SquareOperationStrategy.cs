namespace lab24.Strategies
{
    public class SquareOperationStrategy : INumericOperationStrategy
    {
        public string OperationName => "Square";

        public double Execute(double value)
        {
            return value * value;
        }
    }
}
