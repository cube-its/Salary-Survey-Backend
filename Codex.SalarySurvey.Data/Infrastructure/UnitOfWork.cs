using Codex.SalarySurvey.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public AppDbContext DbContext
        {
            get { return dbContext; }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
