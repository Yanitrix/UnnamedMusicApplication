using System;
using System.Threading.Tasks;

namespace Domain.MusicPlayer
{
    public interface IMusicPlayer
    {
        Task Play(string path);

        Task Stop();

        Task Resume();

        Task Pause();

        Task Seek(int seconds);

        bool Playing { get; }

        bool Paused { get; }

        event EventHandler OnPlayingFinished;
    }
}
