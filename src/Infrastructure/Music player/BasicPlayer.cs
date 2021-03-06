﻿using Domain.MusicPlayer;
using NetCoreAudio;
using NetCoreAudio.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure.MusicPlayer
{
    public class BasicPlayer : IMusicPlayer
    {
        private readonly IPlayer player;

        public event EventHandler OnPlayingFinished;

        public BasicPlayer()
        {
            player = new Player();

            player.PlaybackFinished += OnPlaybackFinished;
        }

        private void OnPlaybackFinished(object sender, EventArgs e)
        {
            OnPlayingFinished?.Invoke(this, e);
        }

        public bool Playing => player.Playing;
        public bool Paused => player.Paused;

        public Task Pause()
        {
            return player.Pause();
        }

        public Task Play(string path)
        {
            return player.Play(path);
        }

        public Task Resume()
        {
            return player.Resume();
        }

        public Task Stop()
        {
            return player.Stop();
        }

        public Task Seek(int seconds)
        {
            throw new NotImplementedException();
        }
    }
}
