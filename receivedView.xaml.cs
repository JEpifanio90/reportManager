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
    /// Interaction logic for receivedView.xaml
    /// </summary>
    public partial class receivedView : Window
    {
        public receivedView(string name , string department)
        {
            InitializeComponent();
            headerLabel.Content = "Usuario: " + name + "/ Departamento: " + department;
            fillTheTable();
        }

        private void fillTheTable()
        {
            string connectionString = "server=localhost; database=receiptManagerDB; user=root; password=; Allow Zero Datetime=True; Convert Zero Datetime=True";
            MySqlCommand cmd;
            MySqlDataReader reader;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection status OK");
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM `requests` WHERE `status`='En espera de llegada'";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        requestList.Items.Add(new data() { requestID = reader["requestOrder"].ToString(), trackID = reader["trackID"].ToString(), user = reader["userName"].ToString(), carrier = reader["carrier"].ToString(), description = reader["description"].ToString(), buyer = reader["buyer"].ToString(), department = reader["department"].ToString(), spentMoney = reader["spentMoney"].ToString(), filesUploaded = reader["filesUploaded"].ToString(), deliverDate = reader["deliverDate"].ToString(), realDeliverDate = reader["realDeliverDate"].ToString(), status = reader["status"].ToString() });
                    }
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Conection error with the DB " + ex.Message.ToString(), "Warning", MessageBoxButton.OK);
                }

            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void updateTable_Click(object sender, RoutedEventArgs e)
        {
            requestList.Items.Clear();
            fillTheTable();
        }

        private void notifyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (requestList.Items.Count > 0)
            {
                for (int i = 0; i < requestList.Items.Count; i++)
                {
                    data dataObject = (data)requestList.Items[i];
                    if(dataObject.requestChecked)
                    {
                        string connectionString = "server=localhost; database=receiptManagerDB; user=root; password=; Allow Zero Datetime=True; Convert Zero Datetime=True";
                        MySqlCommand cmd;
                        MySqlDataReader reader;
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            try
                            {
                                conn.Open();
                                Console.WriteLine("Connection status OK");
                                cmd = conn.CreateCommand();
                                cmd.CommandText = "UPDATE `requests` SET `status`='En planta' WHERE `requestOrder`= '" + dataObject.requestID.ToString() + "';";
                                //UPDATE `requestsinfo` SET `status`='something' WHERE id 
                                reader = cmd.ExecuteReader();
                                conn.Close();
                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show("Conection error with the DB " + ex.Message.ToString(), "Warning", MessageBoxButton.OK);
                            }

                        }
                    }
                }
                MessageBox.Show("¡Actualización exitosa!", "¡Éxito!", MessageBoxButton.OK);
            }
            requestList.Items.Clear();
            fillTheTable();
        }
    }
}
