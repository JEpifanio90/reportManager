using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for userView.xaml
    /// </summary>
    public partial class userView : Window
    {
        private int files = 0;
        private string name = "";
        public userView(string name)
        {
            InitializeComponent();
            this.name = name;
            fillTheTable();
            fillTheOtherTable();
        }

        private void requestSend_Click(object sender, RoutedEventArgs e)
        {
            Boolean emptyFields = false;
            if(requestText.Text.Length==0) { emptyFields = true; }
            if (trackText.Text.Length == 0) { emptyFields = true; }
            if (requestText.Text.Length == 0) { emptyFields = true; }
            if (descriptionBox.Text.Length == 0) { emptyFields = true; }
            if (totalFact.Text.Length == 0) { emptyFields = true; }
            if (carrierBox.Text.Length == 0) { emptyFields = true; }
            if (buyerBox.Text.Length == 0) { emptyFields = true; }
            if (deparmentBox.Text.Length == 0) { emptyFields = true; }

            if (!emptyFields)
            {
                string connectionString = "server=localhost; database=receiptManagerDB; user=root; password=; Allow Zero Datetime=True; Convert Zero Datetime=True";
                MySqlCommand cmd;
                MySqlDataReader reader;
                string deliverDate = "";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Connection status OK");
                        cmd = conn.CreateCommand();
                        string date = DateTime.Now.Date.Year + "-" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day;
                        try
                        {
                            deliverDate = deliverBy.SelectedDate.Value.Year + "-" + deliverBy.SelectedDate.Value.Month + "-" + deliverBy.SelectedDate.Value.Day;
                        }
                        catch (Exception es)
                        {
                            deliverDate = DateTime.Now.Date.Year + "-" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day; ;
                        }
                        cmd.CommandText = "INSERT INTO `requests`(`requestOrder`, `trackID`, `carrier`, `description`, `buyer`, `department`, `spentMoney`, `filesUploaded`, `deliverDate`, `realDeliverDate`, `status`, `feedBack`,`userName`)   VALUES ('" + requestText.Text.ToString() + "','" + trackText.Text.ToString() + "','" + carrierBox.Text.ToString() + "','" + descriptionBox.Text.ToString() + "','" + buyerBox.Text.ToString() + "','" + deparmentBox.Text.ToString() + "','" + int.Parse(totalFact.Text.ToString()) + "','" + files + "','" + deliverDate + "','" + date + "','Pendiente Importar','','"+name+"')";
                        reader = cmd.ExecuteReader();
                        MessageBox.Show("Request sent.", "¡Success!", MessageBoxButton.OK);
                        requestText.Text = "";
                        trackText.Text = "";
                        carrierBox.Text = "";
                        descriptionBox.Text = "";
                        buyerBox.Text = "";
                        deparmentBox.Text = "";
                        totalFact.Text = "";
                        uploadedFilesLabel.Content = "Archivos Cargados: 0";
                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Conection error with the DB " + ex.Message.ToString(), "Warning", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("¡Revisa tus campos! ", "¡Cuídado!", MessageBoxButton.OK);
            }
        }

        private void attachReceipt_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(requestText.Text))
            {
                OpenFileDialog op = new OpenFileDialog();
                string folderpath = @"C:\Users\Public\ReportManager\Reportes\" + requestText.Text.ToString() + @"\";
                op.Title = "Selecciona una factura";
                op.Filter = "All word and pdf files|*.pdf;*.doc;*.docx| Docx Files (*.docx)|*.docx|Doc Files (*.doc)|*.doc|PDF Files (*.pdf)|*.pdf";
                bool? myResult;
                myResult = op.ShowDialog();
                if (myResult != null && myResult == true)
                {
                    if (!Directory.Exists(folderpath))
                    {
                        Directory.CreateDirectory(folderpath);
                        Directory.CreateDirectory(folderpath+"Archivos");
                    }
                    folderpath = folderpath + @"Archivos \";
                    string filePath = folderpath + System.IO.Path.GetFileName(op.FileName);
                    System.IO.File.Copy(op.FileName, filePath, true);
                    files += 1;
                    uploadedFilesLabel.Content = "Archivos cargados: " + files;
                }
            }
            else
            {
                MessageBox.Show("Llena el campo de 'Orden de compra'.", "¡AVISO!", MessageBoxButton.OK);
            }
        }

        private void attachAnnex_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(requestText.Text))
            {
                OpenFileDialog op = new OpenFileDialog();
                string folderpath = @"C:\Users\Public\ReportManager\Reportes\" + requestText.Text.ToString() + @"\";
                op.Title = "Selecciona un Anexo de importancia";
                op.Filter = "All word and pdf files|*.pdf;*.doc;*.docx| Docx Files (*.docx)|*.docx|Doc Files (*.doc)|*.doc|PDF Files (*.pdf)|*.pdf";
                bool? myResult;
                myResult = op.ShowDialog();
                if (myResult != null && myResult == true)
                {
                    if (!Directory.Exists(folderpath))
                    {
                        Directory.CreateDirectory(folderpath);
                        Directory.CreateDirectory(folderpath + "Archivos");
                    }
                    folderpath = folderpath + @"Archivos \";
                    string filePath = folderpath + System.IO.Path.GetFileName(op.FileName);
                    System.IO.File.Copy(op.FileName, filePath, true);
                    files += 1;
                    uploadedFilesLabel.Content = "Archivos cargados: " + files;
                }
            }
            else
            {
                MessageBox.Show("Llena el campo de 'Orden de compra'.", "¡AVISO!", MessageBoxButton.OK);
            }
        }

        private void attachOrder_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(requestText.Text))
            {
                OpenFileDialog op = new OpenFileDialog();
                string folderpath = @"C:\Users\Public\ReportManager\Reportes\" + requestText.Text.ToString() + @"\";
                op.Title = "Selecciona una Orden de Compra";
                op.Filter = "All word and pdf files|*.pdf;*.doc;*.docx| Docx Files (*.docx)|*.docx|Doc Files (*.doc)|*.doc|PDF Files (*.pdf)|*.pdf";
                bool? myResult;
                myResult = op.ShowDialog();
                if (myResult != null && myResult == true)
                {
                    if (!Directory.Exists(folderpath))
                    {
                        Directory.CreateDirectory(folderpath);
                        Directory.CreateDirectory(folderpath + "Archivos");
                    }
                    folderpath = folderpath + @"Archivos \";
                    string filePath = folderpath + System.IO.Path.GetFileName(op.FileName);
                    System.IO.File.Copy(op.FileName, filePath, true);
                    files += 1;
                    uploadedFilesLabel.Content = "Archivos cargados: " + files;
                }
            }
            else
            {
                MessageBox.Show("Llena el campo de 'Orden de compra'.", "¡AVISO!", MessageBoxButton.OK);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        //SECOND TAB

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
                    cmd.CommandText = "SELECT * FROM `requests`";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        requestList.Items.Add(new data() { requestID = reader["requestOrder"].ToString(), trackID = reader["trackID"].ToString(), user = reader["userName"].ToString() , carrier = reader["carrier"].ToString(), description = reader["description"].ToString(), buyer = reader["buyer"].ToString(), department = reader["department"].ToString(), spentMoney = reader["spentMoney"].ToString(), filesUploaded = reader["filesUploaded"].ToString(), deliverDate = reader["deliverDate"].ToString(), realDeliverDate = reader["realDeliverDate"].ToString(), status = reader["status"].ToString() });
                    }
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Conection error with the DB " + ex.Message.ToString(), "Warning", MessageBoxButton.OK);
                }

            }
        }

        public void fillTheOtherTable()
        {
            string connectionString = "server=localhost; database=receiptManagerDB; user=root; password=; Allow Zero Datetime=True; Convert Zero Datetime=True";
            MySqlCommand cmd;
            MySqlDataReader reader;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM `requests` WHERE `status`= 'Verificar Informacion' AND  `userName`= '" + name+"';";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        errorList.Items.Add(new data() { requestID = reader["requestOrder"].ToString(), trackID = reader["trackID"].ToString(), user = reader["userName"].ToString(),  carrier = reader["carrier"].ToString(), description = reader["description"].ToString(), buyer = reader["buyer"].ToString(), department = reader["department"].ToString(), spentMoney = reader["spentMoney"].ToString(), filesUploaded = reader["filesUploaded"].ToString(), deliverDate = reader["deliverDate"].ToString(), realDeliverDate = reader["realDeliverDate"].ToString(), status = reader["status"].ToString() });
                    }
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Filling the error list: Failed. " + ex.Message.ToString(), "Warning", MessageBoxButton.OK);
                }

            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(errorList.Items.Count>0)
            {
                data dataObject = (data)errorList.Items[errorList.SelectedIndex];
                editWindow edit = new editWindow(dataObject.user, dataObject.buyer, dataObject.carrier, dataObject.deliverDate, dataObject.department, dataObject.description, dataObject.realDeliverDate, dataObject.requestID, dataObject.spentMoney, dataObject.status, dataObject.trackID);
                edit.Show();
                errorList.Items.Clear();
                fillTheOtherTable();
            }
        }

        private void refreshErrorList_Click(object sender, RoutedEventArgs e)
        {
            errorList.Items.Clear();
            fillTheOtherTable();

        }

        private void refreshRequestList_Click(object sender, RoutedEventArgs e)
        {
            requestList.Items.Clear();
            fillTheTable();
        }
    }
}
