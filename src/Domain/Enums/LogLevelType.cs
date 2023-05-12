namespace TechOnIt.Domain.Enums;

/// <summary>
/// Represents a log level
/// </summary>
public enum LogLevelType
{
    /// <summary>
    /// Trace
    /// </summary>
    Trace = 0,

    /// <summary>
    /// Debug
    /// </summary>
    Debug = 10,

    /// <summary>
    /// Information
    /// </summary>
    Information = 20,

    /// <summary>
    /// Warning
    /// </summary>
    Warning = 30,

    /// <summary>
    /// Error
    /// </summary>
    Error = 40,

    /// <summary>
    /// Fatal
    /// </summary>
    Fatal = 50
}