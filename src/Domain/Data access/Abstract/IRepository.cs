using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.DataAccess.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        //querying by substring?
        //yup, should be queried by substring but limited in some way
        public IEnumerable<T> GetByName(String name);

        public T GetById(long id);

        public void Add(T entity);

        //TODO update/delete entities
        //is deleting/updating really possible? I think only when rediscovering of files happens but then only adding should be used (we don't know what to delete/update since everything has changed)
        //or should discovering not be run everytime? but that would enforce me to use some watches, etc.
        //removing Update/Delete for now, useful only in IPLaylistRepo
    }
}
