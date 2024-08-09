using System;

namespace Iql.Server;

public class Result<T>
{
    public T Value { get; }
    public string Errors { get; }
    public Exception Exception { get; }

    public Result(T value, Exception exception = null)
    {
        Value = value;
        Exception = exception;
        if (Exception != null)
        {
            Errors = Exception.Message;
        }
    }
}