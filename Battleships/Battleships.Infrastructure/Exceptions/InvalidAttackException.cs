using System;

namespace Battleships.Infrastructure.Exceptions
{
    public class InvalidAttackException : Exception
    {
        public InvalidAttackException(string message) : base(message)
        {
        }
    }
}