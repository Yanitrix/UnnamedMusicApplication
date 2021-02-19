namespace Domain.Queue
{
    /// <summary>
    /// Abstraction used to return a sequence of numbers (song indexes in a song queue) between given numbers.
    /// The sequence of the numbers returned is returned according to the implementation.
    /// Every value in the sequence will be in (<see cref="BeginIndex">BeginIndex</see>, <see cref="LastIndex">LastIndex</see>] range.
    /// If any change in a queue happens, <see cref="BeginIndex">BeginIndex</see> should be adjusted accordingly.
    /// </summary>
    public abstract class PlayingMode
    {
        public int BeginIndex { get; protected set; }
        // Can be set externally, so the range of new indices would change.
        public virtual int LastIndex { get; set; }

        /// <summary>
        /// This method is called when the playing mode of a song queue is set or changed.
        /// </summary>
        /// <param name="beginIndex">Index at which the mode is started. Every index after that will be returned according to underlying logic.</param>
        /// <param name="lastIndex">The last index that can be returned. Any value that is greater, won't be returned.</param>
        /// <exception cref="System.ArgumentException">Thrown when <c>lastIndex</c> is less than <c>beginIndex</c></exception>
        public abstract void Initialize(int beginIndex, int lastIndex);

        /// <summary>
        /// Checks if there is anything left to return. If the end was reached, returns false.
        /// </summary>
        public abstract bool HasNext();

        /// <summary>
        /// This method should return the index of the song that is going to be played next.
        /// If the next index cannot be returned (e.g. the end was reached), <c>NoIndexAvailableException</c> is thrown.
        /// </summary>
        /// <returns>The index of the next song</returns>
        /// <exception cref="Domain.Exceptions.NoItemAvailableException">Thrown when there is not next index available.</exception>
        public abstract int Next();
    }
}
