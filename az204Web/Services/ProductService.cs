using az204Web.Models;
using System.Data.SqlClient;

namespace az204Web.Services
{
    public class ProductService
    {
        private static readonly string db_source = "mkm.database.windows.net";
        private static readonly string db_Name = "az204Test";
        private static readonly string db_UserName = "az204vmadmin";
        private static readonly string db_Password = "Az204vm@admin";
        

        private static SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.DataSource = db_source;
            con.InitialCatalog = db_Name;
            con.UserID = db_UserName;
            con.Password = db_Password;

            return new SqlConnection(con.ConnectionString);
        }

        public   List<Product> GetProducts()
        {
            var con= GetConnection();
            List<Product> products = new List<Product>();
            string selectProducts = "Select ProductId,Name,Quantity from Product";
            con.Open();
            SqlCommand cmd = new SqlCommand(selectProducts, con);
            using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Name = Convert.ToString(reader["Name"]),
                        Quantity = Convert.ToInt32(reader["Quantity"])
                    };
                    products.Add(product);
                }
            }
             

            return products;

        }

    }
}
