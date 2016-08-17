using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace MHElectronicsInventory.Dal
{
    class dalModel
    {
        SqlCommand cmd = new SqlCommand();
        MyBase mb = new MyBase();
        Int32 id;
        int number;
        String model_name, error;
        #region prpoerties
        public Int32 ID
        {
            get { return id; }
            set { id = value; }
        }
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public String Model_name
        {
            get { return model_name; }
            set { model_name = value; }
        }
        public String Error
        {
            get { return error; }
            set { error = value; }
        }
        #endregion
        public int insert()
        {
            cmd = mb.Cmd("insert into Model (name) values (@Model_name)");
            cmd.Parameters.AddWithValue("@Model_name", this.model_name);
            int i = -1;

            try
            {
                i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex) { this.error = ex.Message; return i; }

        }
        public String getname()
        {

            cmd = mb.Cmd("select name from Model where ID=@ID");
            cmd.Parameters.AddWithValue("@ID", this.id);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            String i = null;
            while (dr.Read())
            {
                i = dr["name"].ToString();
            }
            return i;
        }
        public int getID()
        {

            cmd = mb.Cmd("select ID from Model where name=@name");
            cmd.Parameters.AddWithValue("@name", this.model_name);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                i = Convert.ToInt32(dr["ID"].ToString());
            }
            return i;
        }
        
        public int updatename()
        {
            cmd = mb.Cmd("update Model set name=@name where ID = @Id");
            cmd.Parameters.AddWithValue("@Id", this.id);
            cmd.Parameters.AddWithValue("@name", this.model_name);
            int i = -1;

            try
            {
                i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex) { this.error = ex.Message; return i; }
        }
    }
}
