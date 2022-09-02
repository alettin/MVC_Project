using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_UnitOfWork_Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

    }
}
