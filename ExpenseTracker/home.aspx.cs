using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;

namespace ExpenseTracker
{
    public partial class home : System.Web.UI.Page
    {
        string conn = "Data Source=DROGON;Initial Catalog=Test;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowGrid();
            LabelText();
            HighCat();
        }

        protected void AddTRX_Click(object sender, EventArgs e)
        {
            if (PurchaseTXT.Text == "" || AmountTRX.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please fill all the spaces.');", true);
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(conn);
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into TRX (Date_TRX, Amount, Category, Purchase) values (cast(GETDATE() as date), @Amount, @Category, @Purchase)";
                        cmd.Parameters.AddWithValue("@Amount", AmountTRX.Text);
                        cmd.Parameters.AddWithValue("@Category", CategoryDDL.Text);
                        cmd.Parameters.AddWithValue("@Purchase", PurchaseTXT.Text);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 1)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Succes", "alert('Purchase submitted.');", true);
                            PurchaseTXT.Text = "";
                            AmountTRX.Text = "";
                            ShowGrid();
                            LabelText();
                            HighCat();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Something went wrong. Please try again');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding date: '{ex}'");
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Something went wrong. Error: {ex}');", true);
                }
            }
        }

        public void ShowGrid ()
        {
            SqlConnection con = new SqlConnection(conn);
            string com = "select Date_TRX as 'Date', Purchase, Amount, Category from TRX";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        public void LabelText()
        {
            SqlConnection con = new SqlConnection(conn);
            string com = "select '$'+cast(sum(amount) as varchar) Total from TRX where DATEPART(month, Date_TRX) = DATEPART(month, getdate()) and DATEPART(year, Date_TRX) = DATEPART(year, getdate())";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //TotalLbl.Text = dt.Rows[0]("Total").ToString();
                TotalLbl.Text = dt.Rows[0][0].ToString();
            }
        }

        public void HighCat()
        {
            SqlConnection con = new SqlConnection(conn);
            string com = "select Category, Sum(amount) Total into #TEMP from TRX group by Category select Category, ' $'+cast(Total as varchar) from #TEMP order by Total DESC drop table #TEMP";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //TotalLbl.Text = dt.Rows[0]("Total").ToString();
                HighestCat.Text = dt.Rows[0][0].ToString();
                HighAmount.Text = dt.Rows[0][1].ToString();
            }
        }
    }
}