using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFileDownloader.Core
{
    public interface IDriveExplorer
    {
        void Autheticate();
        IDrive Drive { get;}
    }
}
