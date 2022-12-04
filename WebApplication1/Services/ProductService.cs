using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;

namespace WebApplication1.Services
{

    // This service will interact with our Product data in the SQL database
    public class ProductService
    {
        private static string db_source = "appserver2012.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Ninad$112233";
        private static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var _builer = new SqlConnectionStringBuilder();
            _builer.DataSource = db_source;
            _builer.UserID = db_user;
            _builer.Password = db_password;
            _builer.InitialCatalog = db_database;
            return new SqlConnection(_builer.ConnectionString);
    
        }
        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> _product_lst = new List<Product>();
            string _statement = "SELECT ProductID,ProductName,Quantity from Products";

            conn.Open();

            SqlCommand cmd = new SqlCommand(_statement, conn);
            using (SqlDataReader _reader = cmd.ExecuteReader())
            {
                while(_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };
                    _product_lst.Add(_product);
                }
            }
            conn.Close();
            return _product_lst;
        }

    }
}

