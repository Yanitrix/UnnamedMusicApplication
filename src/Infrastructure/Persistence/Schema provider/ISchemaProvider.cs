namespace Infrastructure.Persistence
{
    public interface ISchemaProvider
    {
        public string ArtistCollectionName { get; set; }

        public string AlbumCollectionName { get; set; }

        public string SongCollectionName { get; set; }

        public string PlaylistCollectionName { get; set; }

        public string DatabaseName { get; set; }
    }
}
