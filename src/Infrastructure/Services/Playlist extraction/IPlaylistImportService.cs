using System;

namespace Infrastructure.Services
{
    public interface IPlaylistImportService
    {
        public void ImportPlaylists(params String[] filePaths);
    }
}
