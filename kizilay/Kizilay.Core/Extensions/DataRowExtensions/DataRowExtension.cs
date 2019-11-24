using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Core.Extensions.DataRowExtensions
{
    public static class DataRowExtension
    {
        public static TModel Map<TModel>(this DataRow dataRow)
        {
            if (dataRow == null)
                return default(TModel);

            Type typeOfModel = typeof(TModel);

            TModel model = (TModel)Activator.CreateInstance(typeOfModel);

            var listOfPropertyName = typeOfModel.GetProperties().Select(i => new
            {
                i.Name
            }).ToList();

            int countOfProperties = listOfPropertyName.Count;


            for (int i = 0; i < countOfProperties; i++)
            {
                var propertyName = listOfPropertyName[i].Name;

                if (dataRow.Table.Columns.Contains(propertyName))
                {
                    var value = dataRow[propertyName];

                    PropertyInfo propertyInfo = typeOfModel.GetProperty(propertyName);
                    propertyInfo.SetValue(model, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                }
            }

            return model;
        }
    }
}
