using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ModelBinding.Models
{
    public class Employee
    {
        public int EmpNo { get; set; }
        public string Name { get; set; }
        public decimal Basic { get; set; }
        public int DeptNo { get; set; }

        public Employee(int empNo, string name, decimal basic, int deptNo)
        {
            this.EmpNo = empNo;
            this.Name = name;
            this.Basic = basic;
            this.DeptNo = deptNo;
        }

        public Employee()
        {
        }

        public static Employee? getEmployee(int empId)
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
                cmdSelect.Parameters.AddWithValue("@empNo", empId);

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int empNo = reader.GetInt32("EmpNo");
                        string name = reader.GetString("Name");
                        decimal basic = reader.GetDecimal("Basic");
                        int deptNo = reader.GetInt32("DeptNo");



                        Employee emp = new Employee(empNo, name, basic, deptNo);
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
                cmdInsert.CommandText = $"INSERT INTO employees VALUES ({obj.EmpNo},'{obj.Name}',{obj.Basic},{obj.DeptNo})";  // Assuming DeptId is the first column

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
                cmdInsert.Parameters.AddWithValue("@empNo", id);

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

        public static List<Employee?>? getAllEmployees()
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            try
            {
                cn.Open();

                List<Employee?> list = new List<Employee?>();
                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = cn;
                cmdSelect.CommandType = CommandType.Text;
                cmdSelect.CommandText = "SELECT * FROM employees";

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int empNo = reader.GetInt32("EmpNo");
                        string name = reader.GetString("Name");
                        decimal basic = reader.GetDecimal("Basic");
                        int deptNo = reader.GetInt32("DeptNo");

                        Employee emp = new Employee(empNo, name, basic, deptNo);

                        list.Add(emp);

                    }
                    return list;
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

    }


}
