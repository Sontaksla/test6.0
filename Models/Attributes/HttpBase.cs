using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test6._0.Models
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpBaseAttribute : Attribute
    {
        public HttpBaseAttribute() : base()
        {
        }
    }
}
