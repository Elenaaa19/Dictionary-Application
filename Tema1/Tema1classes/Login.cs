using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tema1.Tema1classes
{
    internal class Login
    {
        public bool LoginUser(string username, string password)
        {
            bool ok = false;
            StreamReader streamReader = new StreamReader("../../utilizatori.txt");
            string line = "";
            while ((line = streamReader.ReadLine()) != null)
            {
                string u = line.Split(',')[0];
                string p = line.Split(',')[1];
                if (u.Equals(username) && p.Equals(password))
                {
                    ok = true;
                }
            }
            streamReader.Close();
            return ok;
        }
    }
}
