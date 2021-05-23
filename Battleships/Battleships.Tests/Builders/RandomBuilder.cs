using System;

namespace Battleships.Test.Builders
{
    public class RandomBuilder
    {
        private static readonly Random Random = new Random();

        public static int NextInt()
        {
            return Random.Next(1, 10);
        }

        public static T NextEnum<T>()
        {
            var values = Enum.GetValues(typeof(T));

            return (T) values.GetValue(Random.Next(values.Length));
        }
    }
}