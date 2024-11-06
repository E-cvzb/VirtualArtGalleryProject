using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Bussiness.DataProtection
{
    public class DataProtection : IDataProdection
    {
        private readonly IDataProtector _protector;
        public DataProtection(IDataProtectionProvider dataProtection)
        {

            _protector = dataProtection.CreateProtector("Gallery-Security");
        }
        public string Protect(string text)
        {
           return _protector.Protect(text);
        }

        public string UnProtect(string protectText)
        {
            return _protector.Unprotect(protectText);
        }
    }
}
