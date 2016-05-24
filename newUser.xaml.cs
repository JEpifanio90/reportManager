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
using System.Windows.Shapes;

namespace Report_Manager
{
    /// <summary>
    /// Interaction logic for newUser.xaml
    /// </summary>
    public partial class newUser : Window
    {
        public newUser()
        {
            InitializeComponent();
        }

        private void createUser_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=localhost; database=receiptManagerDB; user=root; password=; Allow Zero Datetime=True; Convert Zero Datetime=True";
            string type = "";
            MySqlCommand cmd;
            MySqlDataReader reader;
            if (newUsrName.Text.ToString().Length == 0 || newUsrPwd.Text.ToString().Length == 0 || newUserEmail.Text.ToString().Length == 0)
            {

                MessageBox.Show("¡Check your fields!", "Warning", MessageBoxButton.OK);
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Connection status OK");
                        if ((bool)userType.IsChecked) { type = "user"; }
                        if ((bool)impType.IsChecked) { type = "authGuy"; }
                        if ((bool)receivedType.IsChecked) { type = "receiver"; }
                        if ((bool)managementType.IsChecked) { type = "management"; }
                        cmd = conn.CreateCommand();
                        cmd.CommandText = "INSERT INTO `users`(`user`, `pwd`, `email`, `type`) VALUES('" + newUsrName.Text.ToString() + "','" + newUsrPwd.Text.ToString() + "','" + newUserEmail.Text.ToString() + "','" + type.ToString() + "');";
                        //Console.WriteLine(cmd.CommandText.ToString());
                        reader = cmd.ExecuteReader();
                        conn.Close();
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Conection error with the DB " + ex.Message.ToString(), "Warning", MessageBoxButton.OK);
                    }

                }
            }
        }

        private void backTo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
