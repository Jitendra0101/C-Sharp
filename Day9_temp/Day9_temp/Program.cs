using Microsoft.Data.SqlClient;
using System.Data;

namespace Databases
{

    internal class Program
    {
        static void Main()
        {
            /*Connect();*/
            /*Insert();*/
            foreach (var item in new int[2] { 1, 2 })
            {
                SelectDeptNameById(item);
            }

            Employee emp = getEmployee(1);

            if (emp != null) { Console.WriteLine("Basic of Employee " + emp.Name + " is: " + emp.Basic); }
            else
            {
                Console.WriteLine("emp is empty !!");
            }



        }
        static void Connect()
        {
            SqlConnection cn = new SqlConnection();

            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            try
            {
                cn.Open();
                Console.WriteLine("success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public static void Insert(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            try
            {
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;  // Set the connection for the SqlCommand
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = $"INSERT INTO employees VALUES ({obj.EmpNo},{obj.Name},{obj.Basic},{obj.DeptNo})";  // Assuming DeptId is the first column

                int rowsAffected = cmdInsert.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Row inserted successfully");
                else
                    Console.WriteLine("Failed to insert row");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public static void Delete(int id)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            try
            {
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;  // Set the connection for the SqlCommand
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = "DELETE FROM employees WHERE EmpNo = @empNo";  // Assuming DeptId is the first column
                cmdInsert.Parameters.AddWithValue("@empNo",id);

                int rowsAffected = cmdInsert.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Row deleted successfully");
                else
                    Console.WriteLine("Failed to delete row");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }


        static void SelectDeptNameById(int deptNo)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            try
            {
                cn.Open();

                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = cn;
                cmdSelect.CommandType = CommandType.Text;
                cmdSelect.CommandText = "SELECT DeptName FROM departments WHERE DeptNo = @DeptNo";
                cmdSelect.Parameters.AddWithValue("@DeptNo", deptNo);

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        /*string deptName = reader["DeptName"].ToString();*/
                        string deptName = reader.GetString(0);
                        Console.WriteLine($"DeptName for DeptId {deptNo}: {deptName}");
                    }
                    else
                    {
                        Console.WriteLine($"No record found for DeptId {deptNo}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public static List<Employee?> getAllEmployees()
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            try
            {
                cn.Open();

                List<Employee> list = List<Employee>();
                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = cn;
                cmdSelect.CommandType = CommandType.Text;
                cmdSelect.CommandText = "SELECT * FROM employees";

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        int empNo = reader.GetInt32("EmpNo");
                        string name = reader.GetString("Name");
                        decimal basic = reader.GetDecimal("Basic");
                        int deptNo = reader.GetInt32("DeptNo");

                        Employee emp = new Employee{empNo, name, basic, deptNo};

                        list.add(emp);

                    }
                    else
                    {
                        return null;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            finally
            {
                cn.Close();
            }

        }

        static Employee? getEmployee(int empNo)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            try
            {
                cn.Open();

                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = cn;
                cmdSelect.CommandType = CommandType.Text;
                cmdSelect.CommandText = "SELECT * FROM employees WHERE EmpNo = @empNo";
                cmdSelect.Parameters.AddWithValue("@empNo", empNo);

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int empNo = reader.GetInt32("EmpNo");
                        string name = reader.GetString("Name");
                        decimal basic = reader.GetDecimal("Basic");
                        int deptNo = reader.GetInt32("DeptNo");



                        Employee emp = new Employee{empNo, name, basic, deptNo};
                        return emp;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            finally
            {
                cn.Close();
            }


        }

        static void multipleQuerySelect()
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            try
            {
                cn.Open();

                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = cn;
                cmdSelect.CommandType = CommandType.Text;
                cmdSelect.CommandText = "SELECT DeptName FROM departments WHERE DeptNo = @DeptNo";
                cmdSelect.Parameters.AddWithValue("@DeptNo", 1);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }

        }

    }

    public class Employee
    {

        public int EmpNo{ get; set;}

        private string name;
        public string Name
        {
            set { name = value; }
            get { return name; }
        }


        private decimal basic;
        public decimal Basic
        {
            set { basic = value; }
            get { return basic; }
        }

        public int DeptNo{get; set;}

        public Employee(string name, decimal basic)
        {
            this.Name = name;
            this.Basic = basic;
        }
    }

}