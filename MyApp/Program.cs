using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace MyApp
{
    internal static class Program
    {
        static void AddWorker(string f,DateTime b,string g, string c)
        {
            using (SqlConnection connection = new SqlConnection(c))
            {
                connection.Open();
                string request = "INSERT INTO PersonalData (Full_Name, Birthday, Gender) VALUES(@Full_Name, @Birthday, @Gender)";
                using (SqlCommand command = new SqlCommand(request, connection))
                {
                    command.Parameters.AddWithValue("@Full_Name", f);
                    command.Parameters.AddWithValue("@Birthday", b);
                    command.Parameters.AddWithValue("@Gender", g);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Random random = new Random();
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"\\MyApp\\MyApp\\DB.mdf\";Integrated Security=True;Connect Timeout=30";
            string fullName, gender, surname;
            DateTime birthday;
            int randomLastName, randomFirstName, randomSecondNames, day, month, year;
            string[] lastNames = new string[]{"Ivanov", "Petrov", "Sidorov", "Ferguson", "Johnson", "Williams", "Foster", "Jones", "Miller", "Franklin"};
            string[] firstNames = new string[]{"Ivan", "Maria", "Sergei", "Olga", "Pavel", "Elena", "Alexei", "Natalia", "Dmitri", "Anna" };
            string[] secondNames = new string[] { "Ivanovich", "Sergeevna", "Petrovich", "Nikolaevna", "Dmitrievich", "Andreevna", "Alexandrovich", "Vladimirovna", "Mikhailovich", "Ivanovna" };
            if (args.Length > 0)
            {
                string mode = args[0];

                switch (mode)
                {
                    case "Create":
                        string tableName = "PersonalData";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                int tableCount = (int)command.ExecuteScalar();

                                if (tableCount > 0)
                                {
                                    MessageBox.Show($"Таблица уже существует.");
                                }
                                else
                                {
                                    query = "CREATE TABLE [dbo].[PersonalData] ([ID] INT IDENTITY (1, 1) NOT NULL, [Full_Name] VARCHAR (255) NOT NULL, [Birthday]  DATE  NOT NULL, [Gender] VARCHAR (255) NOT NULL);";
                                    using (SqlCommand com = new SqlCommand(query, connection))
                                    {
                                        com.ExecuteNonQuery();
                                    }
                                    MessageBox.Show($"Таблица создана.");
                                }
                            }
                            connection.Close();
                        }
                        break;
                    case "AddWorker":
                        fullName = args[1];
                        birthday = DateTime.Parse(args[2]);
                        gender = args[3];
                        AddWorker(fullName, birthday, gender, connectionString);
                        MessageBox.Show("Сотрудник добавлен.");
                        break;
                    case "FullAge":
                        birthday = DateTime.Parse(args[2]);
                        DateTime currentDate = DateTime.Now;
                        int age = currentDate.Year - birthday.Year;
                        if (currentDate < birthday.AddYears(age))
                        {
                            age--;
                        }
                        MessageBox.Show("Сотруднику " + age + " лет");
                        break;
                    case "DBOutput":
                        Application.Run(new DBOutput());
                        break;
                    case "DBOutputAll":
                        Application.Run(new DBOutputAll());
                        break;
                    case "AutoAdd":
                        for (int i = 0; i < 1000000; i++)
                        {
                            randomLastName = random.Next(0, 10);
                            randomFirstName = random.Next(0, 10);
                            if (randomFirstName % 2 == 1)
                            {
                                randomSecondNames = new int[] { 1, 3, 5, 7, 9 }[random.Next(5)];
                                gender = "Female";
                            }
                            else
                            {
                                randomSecondNames = new int[] { 0, 2, 4, 6, 8 }[random.Next(5)];
                                gender = "Male";
                            }
                            fullName = lastNames[randomLastName] + ' ' + firstNames[randomFirstName] + ' ' + secondNames[randomSecondNames];
                            year = random.Next(1943, 2006);
                            month = random.Next(1, 13);
                            if (month == 2)
                            {
                                if (year % 4 == 0)
                                {
                                    day = random.Next(1, 30);
                                }
                                else
                                {
                                    day = random.Next(1, 29);
                                }
                            }
                            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                            {
                                day = random.Next(1, 32);
                            }
                            else
                            {
                                day = random.Next(1, 31);
                            }
                            birthday = new DateTime(year, month, day);
                            AddWorker(fullName, birthday, gender, connectionString);
                        }
                        MessageBox.Show("Сотрудники добавлены.");
                        break;
                    case "AutoAddF":
                        for (int i = 0; i < 100; i++)
                        {
                            var filteredLastNames = lastNames.Where(lastName => lastName.StartsWith("F")).ToArray();
                            int l = random.Next(0, filteredLastNames.Length);
                            surname = filteredLastNames[l];
                            randomFirstName = new int[] { 0, 2, 4, 6, 8 }[random.Next(5)];
                            randomSecondNames = new int[] { 0, 2, 4, 6, 8 }[random.Next(5)];
                            fullName = surname + ' ' + firstNames[randomFirstName] + ' ' + secondNames[randomSecondNames];
                            year = random.Next(1943, 2006);
                            month = random.Next(1, 13);
                            if (month == 2)
                            {
                                if (year % 4 == 0)
                                {
                                    day = random.Next(1, 30);
                                }
                                else
                                {
                                    day = random.Next(1, 29);
                                }
                            }
                            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                            {
                                day = random.Next(1, 32);
                            }
                            else
                            {
                                day = random.Next(1, 31);
                            }
                            birthday = new DateTime(year, month, day);
                            AddWorker(fullName, birthday, "Male", connectionString);
                        }
                        MessageBox.Show("Сотрудники добавлены.");
                        break;
                    case "DBOutputF":
                        DBOutputF.start = DateTime.Now;
                        Application.Run(new DBOutputF());
                        break;
                    default:
                        MessageBox.Show("Такого действия нет.");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Укажите действие которое хотите выполнить.");
            }
        }
    }
}
