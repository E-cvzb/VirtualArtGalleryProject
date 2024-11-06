using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Bussiness.DataProtection
{
    public class ServiceMessage
    {
        public bool IsSuccseed { get; set; }
        public string Message { get; set; }

    }
    public class ServiceMessage<T>
    {
        public bool IsSuccseed { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
