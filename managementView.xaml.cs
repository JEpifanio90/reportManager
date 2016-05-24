using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
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
using System.IO;
using System.Diagnostics;

namespace Report_Manager
{
    /// <summary>
    /// Interaction logic for managementView.xaml
    /// </summary>
    public partial class managementView : System.Windows.Window
    {
        private string name;
        public managementView(string name)
        {
            InitializeComponent();
            fillTheTable();
            this.name = name;
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
                    cmd.CommandText = "SELECT * FROM `requests`";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        requestList.Items.Add(new data() { requestID = reader["requestOrder"].ToString(), trackID = reader["trackID"].ToString(), carrier = reader["carrier"].ToString(), description = reader["description"].ToString(), buyer = reader["buyer"].ToString(), department = reader["department"].ToString(), spentMoney = reader["spentMoney"].ToString(), filesUploaded = reader["filesUploaded"].ToString(), deliverDate = reader["deliverDate"].ToString(), realDeliverDate = reader["realDeliverDate"].ToString(), status = reader["status"].ToString() });
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
            /*if (requestList.SelectedItems.Count > 0)
            {
                requestList.Items.RemoveAt(requestList.SelectedIndex);
            }*/
        }

        private void sendIt_Click(object sender, RoutedEventArgs e)
        {
            File.Copy(@"C:\Users\Public\ReportManager\templateBackup.docx", @"C:\Users\Public\ReportManager\template.docx", true);
            int totalMaquinado = 0, totalOficina = 0, totalSoldadura = 0, totalAlmacen = 0, total = 0, alm = 0, maq = 0, of = 0, sold = 0, completed=0, pending=0, waiting=0, verify=0, inStock=0;
            string trackID = "", requestName= "OA -WAS-" + DateTime.Today.Month + "-" + DateTime.Today.Year;
            string connectionString = "server=localhost; database=receiptManagerDB; user=root; password=; Allow Zero Datetime=True; Convert Zero Datetime=True";
            if (requestList.Items.Count > 0)
            {
                DateTime init = DateTime.Today, end = DateTime.Today;
                for (int i = 0; i < requestList.Items.Count; i++)
                {
                    data dataObject = (data)requestList.Items[i];
                    DateTime.TryParse(dataObject.realDeliverDate.ToString(), out end);
                    trackID = dataObject.trackID;
                    if (end<init)
                    {
                        init = end;
                    }
                    switch (dataObject.department)
                    {
                        case "Maquinados":
                            totalMaquinado += int.Parse(dataObject.spentMoney);
                            maq += 1;
                        break;

                        case "Oficina":
                            totalOficina += int.Parse(dataObject.spentMoney);
                            of += 1;
                        break;
                        case "Soldadura":
                            totalSoldadura += int.Parse(dataObject.spentMoney);
                            sold += 1;
                        break;

                        case "Almacen":
                            totalAlmacen += int.Parse(dataObject.spentMoney);
                            alm += 1;
                        break;
                    }
                    switch(dataObject.status)
                    {
                        case "Completada":
                            completed += 1;
                            break;

                        case "Pendiente Importar":
                            pending += 1;
                            break;
                        case "En espera de llegada":
                            waiting += 1;
                            break;

                        case "Verificar Informacion":
                            verify += 1;
                            break;

                        case "En planta":
                            inStock += 1;
                            break;
                    }
                    MySqlCommand cmd;
                    MySqlDataReader reader;
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            cmd = conn.CreateCommand();
                            cmd.CommandText = "UPDATE `requests` SET `status`='Completada' WHERE `requestOrder`='" + dataObject.requestID + "';";
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
                total = totalMaquinado + totalOficina + totalSoldadura + totalAlmacen;
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document wordDoc = wordApp.Documents.Open(@"C:\Users\Public\ReportManager\template.docx", Visible: false);
                string classtype = "Excel.Chart.8";
                Bookmark bkm = wordDoc.Bookmarks["trackID"];
                Microsoft.Office.Interop.Word.Range rng = bkm.Range;
                rng.Text = trackID;
                bkm = wordDoc.Bookmarks["managerName"];
                    rng = bkm.Range;
                    rng.Text = name;
                bkm = wordDoc.Bookmarks["requestOrder"];
                    rng = bkm.Range;
                    rng.Text = requestName;
                bkm = wordDoc.Bookmarks["initDate"];
                    rng = bkm.Range;
                    rng.Text = init.Date.Day+"/"+ init.Date.Month+"/"+ init.Date.Year;
                bkm = wordDoc.Bookmarks["endDate"];
                    rng = bkm.Range;
                    rng.Text = end.Date.Day + "/" + end.Date.Month + "/" + end.Date.Year;
                bkm = wordDoc.Bookmarks["creationDate"];
                    rng = bkm.Range;
                    rng.Text = DateTime.Today.Day+"/"+ DateTime.Today.Month+"/"+ DateTime.Today.Year;
                //////CHARTS PIE
                bkm = wordDoc.Bookmarks.get_Item("pieChart");
                Microsoft.Office.Interop.Word.InlineShape wrdInlineShape = wordDoc.InlineShapes.AddOLEObject(classtype);
                object oEndOfDoc = "pieChart";
                if (wrdInlineShape.OLEFormat.ProgID == "Excel.Chart.8")
                {
                    object verb = Microsoft.Office.Interop.Word.WdOLEVerb.wdOLEVerbHide;
                    wrdInlineShape.OLEFormat.DoVerb(ref verb);
                    Microsoft.Office.Interop.Excel.Workbook obook = (Microsoft.Office.Interop.Excel.Workbook)wrdInlineShape.OLEFormat.Object;
                    Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets["Sheet1"];

                    obook.Application.Visible = false;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 1]).Value = "Departamentos";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 1]).Value = "Almacen";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 1]).Value = "Maquinado";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 1]).Value = "Oficinas";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 1]).Value = "Soldadura";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 1]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 1]).ClearContents();

                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 2]).Value = "Costos";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 2]).Value = totalAlmacen;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 2]).Value = totalMaquinado;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 2]).Value = totalOficina;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 2]).Value = totalSoldadura;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 2]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 2]).ClearContents();

                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 3]).ClearContents();

                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 4]).ClearContents();

                    wrdInlineShape.Width = 400;

                    obook.ActiveChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie;
                    Microsoft.Office.Interop.Word.Range wrdRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    object oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    wrdRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    sheet.UsedRange.Copy();
                    wordDoc.SetDefaultTableStyle("Light List - Accent 4", false);
                    wrdRng.PasteExcelTable(true, true, false);
                    wrdInlineShape.ConvertToShape();
                    obook.Close();
                }
                //baaars chart
                bkm = wordDoc.Bookmarks.get_Item("barChart");
                wrdInlineShape = wordDoc.InlineShapes.AddOLEObject(classtype);
                oEndOfDoc = "barChart";
                if (wrdInlineShape.OLEFormat.ProgID == "Excel.Chart.8")
                {
                    object verb = Microsoft.Office.Interop.Word.WdOLEVerb.wdOLEVerbHide;
                    wrdInlineShape.OLEFormat.DoVerb(ref verb);
                    Random rn = new Random();
                    Microsoft.Office.Interop.Excel.Workbook obook = (Microsoft.Office.Interop.Excel.Workbook)wrdInlineShape.OLEFormat.Object;
                    Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets["Sheet1"];

                    obook.Application.Visible = false;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 1]).Value = "Departamentos";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 1]).Value = "Almacen";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 1]).Value = "Maquinado";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 1]).Value = "Oficinas";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 1]).Value = "Soldadura";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 1]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 1]).ClearContents();

                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 2]).Value = "Total de requerimientos";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 2]).Value = alm;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 2]).Value = maq;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 2]).Value = of;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 2]).Value = sold;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 2]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 2]).ClearContents();

                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 3]).ClearContents();

                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 4]).ClearContents();
                    wrdInlineShape.Width = 400;

                    obook.ActiveChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DLine;
                    Microsoft.Office.Interop.Word.Range wrdRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    object oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    wrdRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    sheet.UsedRange.Copy();
                    wordDoc.SetDefaultTableStyle("Light List - Accent 4", false);
                    wrdRng.PasteExcelTable(true, true, false);
                    wrdInlineShape.ConvertToShape();
                    obook.Close();
                }
                //Lines chart
                bkm = wordDoc.Bookmarks.get_Item("linesChart");
                wrdInlineShape = wordDoc.InlineShapes.AddOLEObject(classtype);
                oEndOfDoc = "linesChart";
                if (wrdInlineShape.OLEFormat.ProgID == "Excel.Chart.8")
                {
                    object verb = Microsoft.Office.Interop.Word.WdOLEVerb.wdOLEVerbHide;
                    wrdInlineShape.OLEFormat.DoVerb(ref verb);
                    Microsoft.Office.Interop.Excel.Workbook obook = (Microsoft.Office.Interop.Excel.Workbook)wrdInlineShape.OLEFormat.Object;
                    Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets["Sheet1"];

                    obook.Application.Visible = false;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 1]).Value = "Estatus";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 1]).Value = "Completada";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 1]).Value = "Pendiente Importar";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 1]).Value = "En espera de llegada";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 1]).Value = "Verificar Informacion";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 1]).Value = "En planta";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 1]).ClearContents();
                    
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 2]).Value = "Cantidad de requerimientos";
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 2]).Value = completed;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 2]).Value = pending;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 2]).Value = waiting;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 2]).Value = verify;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 2]).Value = inStock;
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 2]).ClearContents();

                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 3]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 3]).ClearContents();

                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[3, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[5, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[6, 4]).ClearContents();
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[7, 4]).ClearContents();

                    wrdInlineShape.Width = 400;

                    obook.ActiveChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DBarStacked;
                    Microsoft.Office.Interop.Word.Range wrdRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    object oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    wrdRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    sheet.UsedRange.Copy();
                    wordDoc.SetDefaultTableStyle("Light List - Accent 4", false);
                    wrdRng.PasteExcelTable(true, true, false);
                    wrdInlineShape.ConvertToShape();
                    obook.Close();
                }
                object fileName = @"C:\Users\Public\ReportManager\Reportes\Request_Report #" + requestName + ".docx";
                try
                {
                    wordDoc.SaveAs2(fileName);
                    MessageBox.Show("Se creó el archivo satisfactoriamente", "¡Listo!", MessageBoxButton.OK);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Hubo un error con el archivo" + exc.Message.ToString(), "¡ERROR!", MessageBoxButton.OK);
                }
                wordDoc.Close();
                wordApp.Quit();
                Process.Start(fileName.ToString());
                Process.Start(@"C:\Users\Public\ReportManager\Reportes\");
                MainWindow main = new MainWindow();
                main.Show();
                Close();
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void updateTable_Click(object sender, RoutedEventArgs e)
        {
            requestList.Items.Clear();
            fillTheTable();
        }
    }
}
