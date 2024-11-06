using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Bussiness.DataProtection
{
    public interface IDataProdection
    {
        string Protect(string text);
        string UnProtect(string protectText);
    }
}
