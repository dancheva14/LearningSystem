using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Repository
{
    public class BaseRepository
    {
        //private string connString = @"Server=DESKTOP-DEJHGGV;database=ELearningSystem;Trusted_Connection=True";
        private string connString = @"Server=.\;database=ELearningSystem;User Id=sa;Password=Micr0!nvest;Trusted_Connection=True;";

        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connString);
            }
        }

        public T QueryFirst<T>(string functionName, object parameters)
        {
            using (var conn = Connection)
            {
                conn.Open();
                return conn.Query<T>(functionName, parameters, null, true, null, CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<T> QueryMultiple<T>(string functionName, object parameters = null)
        {
            using (var conn = Connection)
            {
                conn.Open();
                return conn.Query<T>(functionName, parameters, null, true, null, CommandType.StoredProcedure).ToList();
            }
        }
    }
}
