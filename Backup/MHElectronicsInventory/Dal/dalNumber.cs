using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MHElectronicsInventory.Dal
{
    class dalNumber
    {
        SqlCommand cmd = new SqlCommand();
        MyBase mb = new MyBase();
        int number,m_ID;
        String error;
        #region prpoerties
       
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public int M_ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        public String Error
        {
            get { return error; }
            set { error = value; }
        }
        #endregion
        public int insert()
        {
            cmd = mb.Cmd("insert into Number (M_ID,Number) values (@M_ID,@Number)");
            cmd.Parameters.AddWithValue("@M_ID", this.m_ID);
            cmd.Parameters.AddWithValue("@Number", this.number);
            int i = -1;

            try
            {
                i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex) { this.error = ex.Message; return i; }
        }
        public int update()
        {
            cmd = mb.Cmd("update Number set Number=@Number where M_ID=@M_ID");
            cmd.Parameters.AddWithValue("@M_ID", this.m_ID);
            cmd.Parameters.AddWithValue("@Number", this.number);
            int i = -1;

            try
            {
                i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex) { this.error = ex.Message; return i; }
        }
        public DataSet SelectAll()
        {
            cmd = mb.Cmd("select m.[name],n.M_ID as ID from Model m,NUmber n where n.Number!=0 and n.M_ID=m.ID");
            return mb.DS(cmd);
        }
        public int getnumber()
        {

            cmd = mb.Cmd("select Number from Number where M_ID=@MID");
            cmd.Parameters.AddWithValue("@MID", this.m_ID);

            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                i = Convert.ToInt32(dr["Number"].ToString());
            }
            return i;
        }
    }
}
