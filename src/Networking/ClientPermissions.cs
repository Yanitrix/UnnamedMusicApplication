using System;

namespace Networking
{
    [Flags]
    public enum ClientPermissions : short
    {
        //only receive updates and see what songs are played
        Display = 0,

        //add songs/playlists to some suggestion queue, set cap in order not to overflow
        Suggest = 1,

        //change queue, play different songs, etc.
        Play = 2,

        //rearrange playlists
        Modify = 4,

        //view all songs stored in the db
        SeeAll = 8
    }
}