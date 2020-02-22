using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnnamedMusicApplication.MusicPlayer
{
    interface IMusicPlayer
    {
        Task Play(string path);

        Task Stop();

        Task Resume();

        Task Pause();

        bool Playing { get; }

        bool Paused { get; }

        event EventHandler OnPlayingFinished;

        Task SkipForward(int seconds);

        Task SkipBackward(int seconds);
    }
}
