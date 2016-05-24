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
    /// Interaction logic for editWindow.xaml
    /// </summary>
    public partial class editWindow : Window
    {
        private int files = 0;
        private string name = "", requestID = "";
        public editWindow(string user, string buyer, string carrier, string  deliverDate, string department, string description, string realDeliverDate, string requestID, string spentMoney, string status, string trackID)
        {
            InitializeComponent();
            this.Name = user;
            requestText.Text = requestID;
            this.requestID = requestID;
            requestText.IsEnabled = false;
            trackText.Text = trackID;
            carrierBox.Text = carrier;
            descriptionBox.Text = description;
            buyerBox.Text = buyer;
            deparmentBox.Text = department;
            deliverBy.Text = deliverDate;
            totalFact.Text = spentMoney;
            uploadedFilesLabel.Content = "Archivos Cargados: "+files;
        }
        private void requestSend_Click(object sender, RoutedEventArgs e)
        {
            Boolean emptyFields = false;
            if (requestText.Text.Length == 0) { emptyFields = true; }
            if (trackText.Text.Length == 0) { emptyFields = true; }
            if (requestText.Text.Length == 0) { emptyFields = true; }
            if (descriptionBox.Text.Length == 0) { emptyFields = true; }
            if (totalFact.Text.Length == 0) { emptyFields = true; }
            if (carrierBox.Text.Length == 0) { emptyFields = true; }
            if (buyerBox.Text.Length == 0) { emptyFields = true; }
            if (deparmentBox.Text.Length == 0) { emptyFields = true; }
            if (files<=0) { emptyFields = true; }

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
                        cmd.CommandText = "UPDATE `requests` SET `trackID`='" + trackText.Text.ToString() + "',`carrier`='" + carrierBox.Text.ToString() + "',`description`='" + descriptionBox.Text.ToString() + "',`buyer`='" + buyerBox.Text.ToString() + "',`department`='"+deparmentBox.Text.ToString()+"',`spentMoney`='" + int.Parse(totalFact.Text.ToString()) + "',`filesUploaded`='" + files + "',`deliverDate`='" + deliverDate + "',`realDeliverDate`='" + date + "',`status`='Pendiente Importar',`feedBack`='',`userName`='" + name + "' WHERE `requestOrder`='" + requestID + "'";
                        reader = cmd.ExecuteReader();
                        MessageBox.Show("Requerimiento enviado.", "¡Exito!", MessageBoxButton.OK);
                        conn.Close();
                        uploadedFilesLabel.Content = "Archivos Cargados: 0";
                        Close();
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
            if (!string.IsNullOrWhiteSpace(requestText.Text))
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
                    uploadedFilesLabel.Content = "Archivos Cargados: " + files;
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
                    uploadedFilesLabel.Content = "Archivos Cargados: " + files;
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
                    uploadedFilesLabel.Content = "Archivos Cargados: " + files;
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
    }
}
