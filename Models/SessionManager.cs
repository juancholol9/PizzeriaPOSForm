using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosPizza.Models
{
    public static class SessionManager
    {
        public static string Token { get; set; }
        public static UserData CurrentUser { get; set; }
    }
}
