using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_UnitOfWork_Repository
{
    public class UnitOfWork<T> : IUnitOfWork where T : class
    {
        private readonly DbContext _dbContext;
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private DbContextTransaction _dbContextTransaction;
        public IRepository<T> _repository;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new Repository<T>(_dbContext);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _dbContext.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void CreateTransaction()
        {
            _dbContextTransaction = _dbContext.Database.BeginTransaction();
        }

        private void Commit()
        {
            _dbContextTransaction.Commit();
        }

        private void RollBack()
        {
            _dbContextTransaction.Rollback();
            _dbContextTransaction.Dispose();
        }

        public void Save()
        {
            CreateTransaction();
            try
            {
                _dbContext.SaveChanges();
                Commit();
            }
            catch (DbEntityValidationException ex)
            {
                RollBack();
                foreach (var validationErrors in ex.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                throw new Exception(_errorMessage, ex);
            }
            Dispose();
        }

    }
}
