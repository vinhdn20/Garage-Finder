using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.StorageApi
{
    public interface IStorageCloud
    {
        string CreateSASContainerUri();
    }
}
