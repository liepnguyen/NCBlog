using Planru.Domain.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Planru.Domain.Core.Models;

namespace Planru.NCBlog.Persistence.EFCore
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : Entity
    {
        protected IContentDbContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(IContentDbContext dbContext)
        {
            Db = dbContext;
            DbSet = Db.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
