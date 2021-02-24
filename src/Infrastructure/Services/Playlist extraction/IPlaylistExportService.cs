using Domain.Entities;
using System;

namespace Infrastructure.Services
{
    //will export csv files to given output directory
    public interface IPlaylistExportService
    {
        public void ExportPlaylists(String outputDirectory, params String[] names);

        public void ExportPlaylists(String outputDirectory, params Playlist[] playlists);
    }
}
