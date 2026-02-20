using System;
using System.IO;

// ================= LOGGER (Factory Method) =================

public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[Console] {message}");
    }
}

public class FileLogger : ILogger
{
    private readonly string _filePath = "log.txt";

    public void Log(string message)
    {
        File.AppendAllText(_filePath, $"[File] {message}\n");
    }
}

public abstract class LoggerFactory
{
    public abstract ILogger CreateLogger();
}

public class ConsoleLoggerFactory : LoggerFactory
{
    public override ILogger CreateLogger() => new ConsoleLogger();
}

public class FileLoggerFactory : LoggerFactory
{
    public override ILogger CreateLogger() => new FileLogger();
}

// ================= SINGLETON =================

public sealed class LoggerManager
{
    private static LoggerManager? _instance;
    private LoggerFactory _factory;

    private LoggerManager(LoggerFactory factory)
    {
        _factory = factory;
    }

    public static LoggerManager Initialize(LoggerFactory factory)
    {
        _instance ??= new LoggerManager(factory);
        return _instance;
    }

    public static LoggerManager Instance =>
        _instance ?? throw new InvalidOperationException("LoggerManager not initialized.");

    public void SetFactory(LoggerFactory factory)
    {
        _factory = factory;
    }

    public void Log(string message)
    {
        var logger = _factory.CreateLogger();
        logger.Log(message);
    }
}

// ================= STRATEGY =================

public interface IDataProcessorStrategy
{
    string Process(string data);
    string Name { get; }
}

public class EncryptDataStrategy : IDataProcessorStrategy
{
    public string Name => "Encrypt";

    public string Process(string data)
    {
        char[] arr = data.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
}

public class CompressDataStrategy : IDataProcessorStrategy
{
    public string Name => "Compress";

    public string Process(string data)
    {
        return data.Replace(" ", "");
    }
}

public class DataContext
{
    private IDataProcessorStrategy _strategy;

    public DataContext(IDataProcessorStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IDataProcessorStrategy strategy)
    {
        _strategy = strategy;
    }

    public string Execute(string data)
    {
        return _strategy.Process(data);
    }

    public string CurrentStrategyName => _strategy.Name;
}

// ================= OBSERVER =================

public class DataPublisher
{
    public event Action<string, string>? DataProcessed;

    public void Publish(string result, string strategyName)
    {
        DataProcessed?.Invoke(result, strategyName);
    }
}

public class ProcessingLoggerObserver
{
    public void Subscribe(DataPublisher publisher)
    {
        publisher.DataProcessed += OnDataProcessed;
    }

    private void OnDataProcessed(string result, string strategyName)
    {
        LoggerManager.Instance.Log($"Strategy: {strategyName}, Result: {result}");
    }
}

// ================= MAIN =================

class Program
{
    static void Main()
    {
        Console.WriteLine("===== SCENARIO 1: Full Integration =====");

        LoggerManager.Initialize(new ConsoleLoggerFactory());

        var context = new DataContext(new EncryptDataStrategy());
        var publisher = new DataPublisher();
        var observer = new ProcessingLoggerObserver();
        observer.Subscribe(publisher);

        var result = context.Execute("Hello World");
        publisher.Publish(result, context.CurrentStrategyName);

        Console.WriteLine("\n===== SCENARIO 2: Change Logger =====");

        LoggerManager.Instance.SetFactory(new FileLoggerFactory());

        result = context.Execute("Hello Again");
        publisher.Publish(result, context.CurrentStrategyName);

        Console.WriteLine("Check log.txt for file logging output.");

        Console.WriteLine("\n===== SCENARIO 3: Change Strategy =====");

        context.SetStrategy(new CompressDataStrategy());

        result = context.Execute("Hello World Again");
        publisher.Publish(result, context.CurrentStrategyName);

        Console.WriteLine("\nDone.");
    }
}
