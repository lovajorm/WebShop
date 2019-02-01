using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Web.Models.Avarda
{
    public class ConnectionDetails
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }

        public override string ToString() => ($"{User}:{Password}");
        public byte[] ToBytes => Encoding.ASCII.GetBytes($"{User}:{Password}");
    }
}

