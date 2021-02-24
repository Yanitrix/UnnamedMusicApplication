using Domain.DataAccess.Abstract;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.DataAccess
{
    public interface IArtistRepository : IRepository<Artist>
    {
        //everything with songs and albums included? sorted
        //maybe just names and IDs for tree view
        public IEnumerable<Artist> All();
    }
}
