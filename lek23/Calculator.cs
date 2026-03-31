namespace lek23;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new ArgumentException("Division by zero");

        return a / b;
    }

    public bool IsPositive(int number)
    {
        return number > 0;
    }
}
