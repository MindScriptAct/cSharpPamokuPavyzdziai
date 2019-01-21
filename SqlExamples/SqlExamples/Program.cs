using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExamples
{
    class Program
    {
        /**
         * Instaliuoti NuGet : "MySql.Data", "System.Data.SQLite"
         */
        static void Main(string[] args)
        {
            SqlLiteApp app = new SqlLiteApp();

            /* Local SqlLite testavimas */
            app.StartSqlLite();

            /* Remote MySql testavimas */
            //app.StartMySql();

            /* Local Sql Server Compact testavimas */
            //app.StartSqlCompact();
        }
    }
}
