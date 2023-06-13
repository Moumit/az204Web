using az204Web.Models;
using System.Data.SqlClient;

namespace az204Web.Services
{
    public class ProductService : IProductService
    {

        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private SqlConnection GetConnection()
        {

            return new SqlConnection(_configuration.GetConnectionString("az204WebConnString"));
        }

        public List<Product> GetProducts()
        {
            var con = GetConnection();
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
