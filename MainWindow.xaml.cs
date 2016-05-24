using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Report_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            lockMethod();
        }

        private void lockMethod()
        {
            DateTime date = new DateTime(2016, 5, 25, 12, 30, 0);
            if(date <= DateTime.Now)
            {
                Close();
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=localhost; database=receiptManagerDB; user=root; password=; Allow Zero Datetime=True; Convert Zero Datetime=True";
            MySqlCommand cmd;
            MySqlDataReader reader;
            if (emailBox.Text.Length > 0)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Connection status OK");
                        cmd = conn.CreateCommand();
                        cmd.CommandText = "SELECT id, email, user , pwd, type FROM `users` WHERE email='" + emailBox.Text.ToString() + "';";
                        Console.WriteLine(cmd.CommandText);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            String userPwd = reader["pwd"].ToString();
                            String email = reader["email"].ToString();
                            if (pwd.Password.Equals(userPwd) && emailBox.Text.Equals(email))
                            {
                                switch(reader["type"].ToString())
                                {
                                    case "management":
                                        managementView manager = new managementView(reader["user"].ToString());
                                        manager.Show();
                                        Close();
                                    break;

                                    case "user":
                                        userView client = new userView(reader["user"].ToString());
                                        client.Show();
                                        Close();
                                    break;

                                    case "receiver":
                                        receivedView receiveV = new receivedView(reader["user"].ToString(),"Recepción");
                                        receiveV.Show();
                                        Close();
                                    break;

                                    case "authGuy":
                                        authView auth = new authView(reader["user"].ToString(),"Authentication");
                                        auth.Show();
                                        Close();
                                    break;

                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect user/password", "¡ERROR!", MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect user/password", "¡ERROR!", MessageBoxButton.OK);
                        }
                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Conection error with the DB", "Warning", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Plese write your user and password.", "¡Warning!", MessageBoxButton.OK);
            }
        }

        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            newUser userForm = new newUser();
            userForm.Show();
            this.Close();
        }
    }
}
