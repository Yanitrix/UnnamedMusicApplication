using Domain.DataAccess.Abstract;
using Domain.Entities;
using LiteDB;
using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly LiteDatabase connection;
        protected readonly ILiteCollection<T> repo;

        protected BaseRepository(LiteDatabase connection)
        {
            this.connection = connection;
            this.repo = connection.GetCollection<T>();
            repo.EnsureIndex(e => e.Name);
        }

        public virtual void Add(T entity)
        {
            repo.Insert(entity);
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            repo.InsertBulk(entities);
        }

        public virtual IEnumerable<T> All()
        {
            return repo.FindAll();
        }

        public virtual T GetById(long id)
        {
            return repo.FindById(id);
        }

        public virtual IEnumerable<T> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Array.Empty<T>();
            return repo.Find(Query.Contains("Name", name));
        }
    }
}
