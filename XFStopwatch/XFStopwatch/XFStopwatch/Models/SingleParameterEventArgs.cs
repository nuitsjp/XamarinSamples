using System;

namespace XFStopwatch.Models
{
    public class SingleParameterEventArgs<T> : EventArgs
    {
        public T Parameter { get; }

        public SingleParameterEventArgs(T parameter)
        {
            Parameter = parameter;
        }
    }
}