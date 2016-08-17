using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MHElectronicsInventory.Dal
{
    class dalDaily
    {
        SqlCommand cmd = new SqlCommand();
        MyBase mb = new MyBase();
        DateTime date;
        Double price, profit;
        int number,m_ID;
        Int32 id;
        String error,iMEI, st;
        #region prpoerties
        public Int32 ID
        {
            get { return id; }
            set { id = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public int M_ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        public String IMEI
        {
            get { return iMEI; }
            set { iMEI = value; }
        }
        public String ST
        {
            get { return st; }
            set { st = value; }
        }
        public Double Price
        {
            get { return price; }
            set { price = value; }
        }
        public Double Profit
        {
            get { return profit; }
            set { profit = value; }
        }
        public String Error
        {
            get { return error; }
            set { error = value; }
        }
        #endregion
        public int insert()
        {
            cmd = mb.Cmd("insert into Stock (Date,ST,IMEI,Price,Profit,M_ID) values (@Date,@ST,@IMEI,@Price,@Profit,@M_ID)");
            cmd.Parameters.AddWithValue("@Date", this.date);
            cmd.Parameters.AddWithValue("@ST", this.st);
            cmd.Parameters.AddWithValue("@IMEI", this.iMEI);
            cmd.Parameters.AddWithValue("@Price", this.price);
            cmd.Parameters.AddWithValue("@Profit", this.profit);
            cmd.Parameters.AddWithValue("@M_ID", this.m_ID);
            int i = -1;

            try
            {
                i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex) { this.error = ex.Message; return i; }

        }
        public DataSet SelectIMEI()
        {
            cmd = mb.Cmd("select IMEI from Stock where M_ID=@ID");
            cmd.Parameters.AddWithValue("@ID", this.m_ID);
            return mb.DS(cmd);
        }
        public DataSet SelectST()
        {
            cmd = mb.Cmd("select ST from Stock where M_ID=@I");
            cmd.Parameters.AddWithValue("@I", this.m_ID);
            return mb.DS(cmd);
        }
        public Double selectprice()
        {

            cmd = mb.Cmd("select Price from Stock where ST=@ST");
            cmd.Parameters.AddWithValue("@ST", this.st);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            Double i = 1;
            while (dr.Read())
            {
                i = Convert.ToDouble(dr["Price"].ToString());
            }
            return i;
        }
        public int Update()
        {

            SqlCommand cmd = mb.Cmd("update Stock set Price=@Price,Profit=@Profit where M_ID = @I and ST=@ST and IMEI=@IMEI");
            cmd.Parameters.AddWithValue("@ST", this.st);
            cmd.Parameters.AddWithValue("@IMEI", this.iMEI);
            cmd.Parameters.AddWithValue("@Price", this.price);
            cmd.Parameters.AddWithValue("@Profit", this.profit);
            cmd.Parameters.AddWithValue("@I", this.m_ID);
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
