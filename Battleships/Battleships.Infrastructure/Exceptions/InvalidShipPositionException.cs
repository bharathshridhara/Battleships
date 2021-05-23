using System;

namespace Battleships.Infrastructure.Exceptions
{
    public class InvalidShipPositionException : Exception
    {
        public InvalidShipPositionException(string message) : base(message)
        {
        }
    }
}