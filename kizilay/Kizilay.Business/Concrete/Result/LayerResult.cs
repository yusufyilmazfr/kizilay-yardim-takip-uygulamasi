using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Concrete.Result
{
    public class LayerResult<T> where T : class, new()
    {
        public T Result { get; set; }
        public List<string> Errors { get; set; }

        public LayerResult()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public bool HasError()
        {
            return Errors.Count > 0;
        }

        public void ClearResult()
        {
            Result = default(T);
            Errors.Clear();
        }
    }
}
