using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Helper
{
    public static class ExceptionHelper
    {
        public static void WriteLogFile(Exception ex)
        {
            FileInfo file = new FileInfo(@"C:\Users\ADMIN\source\repos\BankApp\Bank.Exception\Exception Log\Exceptions Log.txt");
            FileStream fileStream = file.Open(FileMode.Append, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(fileStream))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.GetType());
                sw.WriteLine();
            }
        }
    }
}
