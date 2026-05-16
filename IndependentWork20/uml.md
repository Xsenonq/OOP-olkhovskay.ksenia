```mermaid
classDiagram


class IDataProcessorStrategy {
    +string Process(string data)
}

class CelsiusToFahrenheitStrategy {
    +string Process(string data)
}

class FahrenheitToCelsiusStrategy {
    +string Process(string data)
}

class WindSpeedConverterStrategy {
    +string Process(string data)
}

IDataProcessorStrategy <|.. CelsiusToFahrenheitStrategy
IDataProcessorStrategy <|.. FahrenheitToCelsiusStrategy
IDataProcessorStrategy <|.. WindSpeedConverterStrategy

class DataContext {
    -IDataProcessorStrategy strategy
    +SetStrategy(strategy)
    +ExecuteProcessing(data)
}

DataContext --> IDataProcessorStrategy

class DataPublisher {
    +event DataProcessed
    +PublishDataProcessed(data)
}

class ConsoleOutputObserver {
    +Subscribe(publisher)
    +OnDataProcessed(data)
}

class WeatherDatabaseObserver {
    +Subscribe(publisher)
    +OnDataProcessed(data)
}

DataPublisher --> ConsoleOutputObserver
DataPublisher --> WeatherDatabaseObserver

class ProcessingService {
    +Process(data)
    +ChangeStrategy(strategy)
}

ProcessingService --> DataContext
ProcessingService --> DataPublisher