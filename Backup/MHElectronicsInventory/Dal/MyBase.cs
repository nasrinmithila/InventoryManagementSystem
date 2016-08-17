using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MHElectronicsInventory.Dal
{
    class MyBase
    {
        SqlConnection cn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=MHElectronics;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void Connection()
        { 
            if(cn.State == ConnectionState.Open )
            {
                cn.Close();
            }
            cn.Open();
        }
        public SqlCommand Cmd(string sql)
        {
            Connection();
            
            cmd.CommandText = sql;
            cmd.Connection = cn;
            return cmd;
        }
        public DataSet DS(SqlCommand cmd)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        

        
    }
}
