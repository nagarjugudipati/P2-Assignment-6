using System;
using System.Data.SqlClient;

namespace ConAppAssignment6
{
    internal class Program
    {
        static SqlDataReader reader;
                static SqlCommand cmd;
                static SqlConnection con;
                static string conStr = "server=DESKTOP-7UGE70I;database=ProductInventoryDb;trusted_connection=true;";
        static void Main(string[] args)
        {
            try
            {
                con = new SqlConnection(conStr);
                cmd = new SqlCommand();
            start:
                Console.WriteLine("1.View Products 2.Add New Product 3.Update Product Quantity 4.Remove Product \nEnter your choice: ");
                int op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        {
                            ViewProductInventory();
                            break;
                        }
                    case 2:
                        {
                            AddNewProduct();
                            break;
                        }
                    case 3:
                        {
                            UpdateProductQuantity();
                            break;
                        }
                    case 4:
                        {
                            RemoveProduct();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Choice");
                            return;
                        }
                }
                Console.WriteLine("Do you want to continue? Y/N");
                string ch = Console.ReadLine().ToLower();
                if (ch == "y")
                {
                    goto start;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
        static void ViewProductInventory()
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Products";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("ID: " + reader["ProductId"]);
                Console.WriteLine("Product Name: " + reader["ProductName"]);
                Console.WriteLine("Price: " + reader["Price"]);
                Console.WriteLine("Quantity: " + reader["Quantity"]);
                Console.WriteLine("Manufactured Date: " + reader["MfgDate"]);
                Console.WriteLine("Expiry Date: " + reader["ExpDate"]);
                Console.WriteLine("----------------------------------");
            }
            con.Close();
        }
        static void AddNewProduct()
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into Products values (@aid,@aproductname,@aprice,@aquantity,@amfdate,@aexpdate)";
            Console.WriteLine("Enter Product Id: ");
            cmd.Parameters.AddWithValue("@aid", int.Parse(Console.ReadLine()));
            Console.WriteLine("Enter Product Name: ");
            cmd.Parameters.AddWithValue("@aproductname", Console.ReadLine());
            Console.WriteLine("Enter Product Price: ");
            cmd.Parameters.AddWithValue("@aprice", double.Parse(Console.ReadLine()));
            Console.WriteLine("Enter Product Quantity: ");
            cmd.Parameters.AddWithValue("@aquantity", int.Parse(Console.ReadLine()));
            Console.WriteLine("Enter Product Manufacturing Date: ");
            cmd.Parameters.AddWithValue("@amfdate", Console.ReadLine());
            Console.WriteLine("Enter Product Expiry Date: ");
            cmd.Parameters.AddWithValue("@aexpdate", Console.ReadLine());
            int num = cmd.ExecuteNonQuery();
            if (num >= 1)
            {
                Console.WriteLine("Product Added!!");
            }
            con.Close();
        }
        static void UpdateProductQuantity()
        {
            int uid;
            Console.WriteLine("Enter Product ID to update quantity: ");
            uid = int.Parse(Console.ReadLine());
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Products where ProductId=@uid";
            cmd.Parameters.AddWithValue("@uid", uid);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                con.Close();
                con.Open();
                cmd.CommandText = "update Products set Quantity = @quantity where ProductId = @uid";
                Console.WriteLine("Enter new Quantity: ");
                cmd.Parameters.AddWithValue("@quantity", int.Parse(Console.ReadLine()));
                cmd.ExecuteNonQuery();
                Console.WriteLine("Product Updated!!");
            }
            else
            {
                Console.WriteLine($"No such id {uid} exist in database");
            }
            con.Close();
        }
        static void RemoveProduct()
        {
            int rid;
            Console.WriteLine("Enter Product ID to Remove Product: ");
            rid = int.Parse(Console.ReadLine());
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Products where ProductId=@rid";
            cmd.Parameters.AddWithValue("@rid", rid);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                con.Close();
                con.Open();
                cmd.CommandText = "delete from Products where ProductId = @rid";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Product Removed!!");
            }
            else
            {
                Console.WriteLine($"No such id {rid} exist in database");
            }
            con.Close();


        }
    }
}
