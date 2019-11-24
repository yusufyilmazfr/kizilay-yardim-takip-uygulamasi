using Kizilay.Core.Aspects.Ninject.UnitOfWork;
using Kizilay.Core.Extensions.DataRowExtensions;
using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.DataAccess.Concrete.AdoNet.SqlHelper;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Concrete.AdoNet.Repository
{
    public abstract class AdoNetRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        // This base will be refactoring!
        public AdoNetDbHelper _adoNetDbHelper { get; set; }
        public abstract string TableName { get; set; }

        protected AdoNetRepository(AdoNetDbHelper adoNetDbHelper)
        {
            _adoNetDbHelper = adoNetDbHelper;
        }

        public List<TEntity> GetAll()
        {
            _adoNetDbHelper.command = _adoNetDbHelper.connection.CreateCommand();

            _adoNetDbHelper.command.CommandText = $"SELECT * FROM {TableName}";
            var dataTable = _adoNetDbHelper.GetDataTable();

            List<TEntity> entities = new List<TEntity>();

            foreach (DataRow row in dataTable.Rows)
            {
                var entity = row.Map<TEntity>();

                entities.Add(entity);
            }

            dataTable.Dispose();

            return entities;
        }

        public TEntity GetById(int Id)
        {
            _adoNetDbHelper.command.CommandText = $"SELECT * FROM {TableName} WHERE Id = {Id}";
            var dataTable = _adoNetDbHelper.GetDataTable();

            TEntity entity = dataTable.Rows[0].Map<TEntity>();
            
            return entity;
        }

        [UnitOfWorkAspect]
        public int RemoveById(int Id)
        {
            int count = 0;

            try
            {
                _adoNetDbHelper.command.CommandText = $"DELETE FROM {TableName} WHERE Id = {Id}";

                _adoNetDbHelper.OpenSafeConnection();

                count = _adoNetDbHelper.command.ExecuteNonQuery();

                _adoNetDbHelper.CloseSafeConnection();
            }
            catch
            {
            }

            return count;
        }
    }
}
