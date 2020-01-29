using System;
using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Interface
{
    public interface IRepository
    {
        Result Log(LogLevel level, string message, DateTime dateTime);
    }
}
