using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace MHElectronicsInventory.Dal
{
    class dalMemo
    {
        SqlCommand cmd = new SqlCommand();
        MyBase mb = new MyBase();
        DateTime date,startdate,enddate;
        Double price;
        int number,m_ID;
        Int32 id;
        String error,st,iMEI;
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
        public DateTime StartDate
        {
            get { return startdate; }
            set { startdate = value; }
        }
        public DateTime EndDate
        {
            get { return enddate; }
            set { enddate = value; }
        }
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
        public Double Price
        {
            get { return price; }
            set { price = value; }
        }
        public String Error
        {
            get { return error; }
            set { error = value; }
        }
        public String ST
        {
            get { return st; }
            set { st = value; }
        }
        public String IMEI
        {
            get { return iMEI; }
            set { iMEI = value; }
        }
        #endregion
        public int insert()
        {
            cmd = mb.Cmd("insert into Memo (Date,Price,M_ID,ST,IMEI) values (@Date,@Price,@M_ID,@ST,@IMEI)");
            cmd.Parameters.AddWithValue("@Date", this.date);
            cmd.Parameters.AddWithValue("@ST", this.st);
            cmd.Parameters.AddWithValue("@IMEI", this.iMEI);
            cmd.Parameters.AddWithValue("@M_ID", this.m_ID);
            cmd.Parameters.AddWithValue("@Price", this.price);
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
            cmd = mb.Cmd("select * from  Memo");
            return mb.DS(cmd);
        }
        public Double SelectPrice()
        {
            cmd = mb.Cmd("select Price from  Memo where ID=@I");
            cmd.Parameters.AddWithValue("@I", this.id);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            Double i = 1;
            while (dr.Read())
            {
                i = Convert.ToDouble(dr["Price"].ToString());
            }
            return i;
        }
        public int SelectMaxID()
        {
            cmd = mb.Cmd("select max(ID) as ID from Memo");
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int i = 1;
            while (dr.Read())
            {
                i = Convert.ToInt32(dr["ID"].ToString());
            }
            return i;
        }
        public int Delete()
        {

            SqlCommand cmd = mb.Cmd("delete from Memo where ST=@ST and IMEI=@IMEI");
            cmd.Parameters.AddWithValue("@IMEI", this.iMEI);
            cmd.Parameters.AddWithValue("@ST", this.st);
            int i = -1;

            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { this.error = ex.Message; }

            return i;
        }
    }
}
