using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Core
{
    public class Result
    {
        protected Result(bool success, string error)
        {
            // TODO
            ////Contracts.Require(success || !string.IsNullOrEmpty(error));
            ////Contracts.Require(!success || string.IsNullOrEmpty(error));

            Success = success;
            Error = error;
        }

        public bool Success { get; private set; }

        public string Error { get; private set; }

        public bool Failure => !Success;

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (var result in results)
            {
                if (result.Failure)
                {
                    return result;
                }
            }

            return Ok();
        }
    }

    public class Result<T> : Result
    {
        private T _value;

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            // TODO Contracts.Require(value != null || !success);
            Value = value;
        }

        public T Value
        {
            get
            {
                // TODO Contracts.Require(Success);
                return _value;
            }

            private set
            {
                _value = value;
            }
        }
    }
}
