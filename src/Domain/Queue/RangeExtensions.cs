namespace Domain.Queue
{
    public static class RangeExtensions
    {
        /// <summary>
        /// Both inclusive.
        /// </summary>
        public static int CountBetween(this (int beginIndex, int lastIndex) range)
        {
            return range.lastIndex - range.beginIndex + 1;
        }

        /// <summary>
        /// Last inclusive.
        /// </summary>
        public static int LastIndex(this (int beginIndex, int count) range)
        {
            return range.beginIndex + range.count - 1;
        }
    }
}