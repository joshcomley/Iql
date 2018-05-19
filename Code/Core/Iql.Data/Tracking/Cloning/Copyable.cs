using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using TypeSharp.Extensions;

namespace Iql.Queryable.Data.Tracking.Cloning
{
    /// <summary>
    ///     This class is an abstract base class that can be used as a really simple way of making an object
    ///     copyable.
    ///     To make an object copyable, simply inherit from this class, and call the base constructor from
    ///     your constructor, with the same arguments as your constructor.
    /// </summary>
    /// <example>
    ///     public class ACopyable : Copyable
    ///     {
    ///     private ACopyable _friend;
    ///     public ACopyable(ACopyable friend)
    ///     : base(friend)
    ///     {
    ///     this._friend = friend;
    ///     }
    ///     }
    /// </example>
    [DoNotConvert]
    public abstract class Copyable
    {
        private readonly ConstructorInfo _constructor;
        private readonly object[] _constructorArgs;

        protected Copyable(params object[] args)
        {
            var frame = new StackFrame(1, true);

            var method = frame.GetMethod();

            if (!method.IsConstructor)
            {
                throw new InvalidOperationException("Copyable cannot be instantiated directly; use a subclass.");
            }

            var parameters = method.GetParameters();

            if (args.Length > parameters.Length)
            {
                throw new InvalidOperationException(
                    "Copyable constructed with more arguments than the constructor of its subclass.");
            }

            var constructorTypeArgs = new List<Type>();
            var i = 0;

            for (; i < args.Length; ++i)
            {
                if (!parameters[i].ParameterType.IsAssignableFrom(args[i].GetType()))
                {
                    throw new InvalidOperationException(
                        string.Format("Copyable constructed with invalid type {0} for argument #{2} (should be {1})",
                            args[i].GetType(), parameters[i].ParameterType, i));
                }
                constructorTypeArgs.Add(parameters[i].ParameterType);
            }
            for (; i < parameters.Length; ++i)
            {
                if (!parameters[i].IsOptional)
                {
                    throw new InvalidOperationException("Copyable constructed with too few arguments.");
                }
                constructorTypeArgs.Add(parameters[i].ParameterType);
            }

            _constructor = GetType().GetConstructor(constructorTypeArgs.ToArray());
            _constructorArgs = args;
        }

        public object CreateInstanceForCopy()
        {
            return _constructor.Invoke(_constructorArgs);
        }
    }
}