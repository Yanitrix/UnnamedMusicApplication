namespace Domain.Queue
{
    public static class PlayingModes
    {
        public static readonly PlayingMode Normal = new LinearPlayingMode();

        public static readonly PlayingMode Shuffle = new ShufflePlayingMode();
    }
}
