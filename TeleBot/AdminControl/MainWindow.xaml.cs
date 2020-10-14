using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Xml;

namespace AdminControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string connectionString = "Server=tcp:met.database.windows.net,1433;Initial Catalog=AzureMet_DB;Persist Security Info=False;User ID=pricol;Password=Ujk213l0Il;";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            // устанавливаем соединение с БД
            con.Open();
            string sql = "SELECT tele_id FROM Users";
            SqlCommand cmd = new SqlCommand(sql, con);
            try { 
            
               
                
                SqlDataReader reader = cmd.ExecuteReader();
                // Загружаем данные
                while (reader.Read())
                {
                    string sLastName = reader[0].ToString();

                    list.Items.Add(sLastName);
                }
              
                // читаем результат
             
                reader.Close();
                // Закрываем соединение
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
        }
        //public static void GenerateExcelByDate(int daycount)
        //{



        //    //try
        //    //{
        //    Application ex;
        //    Worksheet sheet;
        //    ex = new Application();
        //    ex.Visible = true;
        //    //Количество листов в рабочей книге
        //    //Добавить рабочую книгу
        //    Workbook workBook;
        //    string[,] nums2 = new string[daycount * 4, 5];
        //    if (System.IO.File.Exists("C:\\Users\\admnz\\source\\repos\\BotValuts\\BotVal\\BotVal\\bin\\Debug\\test.xlsx"))
        //    {
        //        workBook = ex.Workbooks.Open("C:\\Users\\admnz\\source\\repos\\BotValuts\\BotVal\\BotVal\\bin\\Debug\\test.xlsx");
        //        sheet = ex.Worksheets.get_Item(1);
        //        int tmpi = 0;
        //        int filed = 0;

        //        for (int i = 1; i < sheet.Cells.Rows.Count; i++)
        //        {

        //            if (sheet.Cells[i, 1].Text == String.Format(""))
        //            {
        //                filed = i;
        //                break;
        //            }
        //        }
        //        int tmpk = filed - daycount * 4;
        //        if (filed < daycount * 4)
        //        {
        //            tmpk = 2;

        //        }


        //        for (int i = tmpk; i < filed; i++)
        //        {

        //            for (int j = 1; j <= 5; j++)
        //            {
        //                nums2[tmpi, j - 1] = sheet.Cells[i, j].Text;

        //            }
        //            tmpi++;


        //        }


        //        workBook.Close();
        //        ex.Quit();







        //        ex = new Application();
        //        workBook = ex.Workbooks.Add(Type.Missing);
        //        sheet = ex.Worksheets.get_Item(1);



        //        ex.SheetsInNewWorkbook = 1;
        //        workBook = ex.Workbooks.Add(Type.Missing);
        //        sheet = ex.Worksheets.get_Item(1);
        //        sheet.Activate();
        //        ex.DisplayAlerts = false;
        //        //Получаем первый лист документа (счет начинается с 1)
        //        //Название листа (вкладки снизу)
        //        sheet.Name = "Info";
        //        sheet.Cells[1, 1] = String.Format("First Valut");
        //        sheet.Cells[1, 2] = String.Format("Second Valut");
        //        sheet.Cells[1, 3] = String.Format("SELL");
        //        sheet.Cells[1, 4] = String.Format("BUY");
        //        sheet.Cells[1, 5] = String.Format("Date");
        //        tmpk = daycount * 4 + 1;
        //        if (filed < daycount * 4)
        //        {
        //            tmpk = filed;
        //        }
        //        for (int i = 2; i < tmpk; i++)
        //        {
        //            for (int j = 1; j <= 5; j++)
        //            {

        //                sheet.Cells[i, j] = String.Format(nums2[i - 2, j - 1]);
        //            }
        //        }

        //        workBook.SaveAs("C:\\Users\\admnz\\source\\repos\\BotValuts\\BotVal\\BotVal\\bin\\Debug\\tmp.xlsx");
        //        workBook.Close();
        //        ex.Quit();


        //    }
        //    else
        //    {
        //        ExcelEz();
        //        GenerateExcelByDate(daycount);
        //    }

        //}
        //static public void ExcelEz()
        //{
        //    Application ex;
        //    Worksheet sheet;
        //    ex = new Application();

        //    ex.Visible = true;

        //    //Количество листов в рабочей книге
        //    //Добавить рабочую книгу
        //    Workbook workBook;




        //    string URL = "https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5";
        //    XmlTextReader xmlread = new XmlTextReader(URL);

        //    string[,] nums2 = { { "", "", "", "", DateTime.Now.ToString() }, { "", "", "", "", DateTime.Now.ToString() }, { "", "", "", "", DateTime.Now.ToString() }, { "", "", "", "", DateTime.Now.ToString() } };
        //    int k = 0;
        //    while (xmlread.Read())
        //    {

        //        if (xmlread.AttributeCount > 3)
        //        {
        //            nums2[k, 0] = xmlread.GetAttribute("ccy");
        //            nums2[k, 1] = xmlread.GetAttribute("base_ccy");
        //            nums2[k, 2] = xmlread.GetAttribute("buy");
        //            nums2[k, 3] = xmlread.GetAttribute("sale");
        //            k++;
        //        }
        //    }


        //    if (System.IO.File.Exists("C:\\Users\\admnz\\source\\repos\\BotValuts\\BotVal\\BotVal\\bin\\Debug\\test.xlsx"))
        //    {
        //        workBook = ex.Workbooks.Open("C:\\Users\\admnz\\source\\repos\\BotValuts\\BotVal\\BotVal\\bin\\Debug\\test.xlsx");
        //        sheet = ex.Worksheets.get_Item(1);
        //        int tmpi = 0;
        //        int filed = 0;

        //        for (int i = 1; i < sheet.Cells.Rows.Count; i++)
        //        {

        //            if (sheet.Cells[i, 1].Text == String.Format(""))
        //            {
        //                filed = i;
        //                break;
        //            }
        //        }


        //        for (int i = filed; i <= filed + 3; i++)
        //        {

        //            for (int j = 1; j <= 5; j++)
        //            {

        //                string tm = String.Format(nums2[tmpi, j - 1]);
        //                Console.WriteLine();
        //                sheet.Cells[i, j] = tm;
        //            }
        //            tmpi++;

        //        }

        //        workBook.Save();
        //        workBook.Close();
        //        ex.Quit();

        //    }
        //    else
        //    {

        //        Console.WriteLine("This is NEW");

        //        ex.SheetsInNewWorkbook = 1;
        //        workBook = ex.Workbooks.Add(Type.Missing);
        //        sheet = ex.Worksheets.get_Item(1);
        //        sheet.Activate();
        //        ex.DisplayAlerts = false;
        //        //Получаем первый лист документа (счет начинается с 1)
        //        //Название листа (вкладки снизу)
        //        sheet.Name = "Отчет за 13.12.2017";
        //        sheet.Cells[1, 1] = String.Format("First Valut");
        //        sheet.Cells[1, 2] = String.Format("Second Valut");
        //        sheet.Cells[1, 3] = String.Format("SELL");
        //        sheet.Cells[1, 4] = String.Format("BUY");
        //        sheet.Cells[1, 5] = String.Format("Date");

        //        for (int i = 2; i <= 5; i++)
        //        {
        //            for (int j = 1; j <= 5; j++)
        //            {

        //                sheet.Cells[i, j] = String.Format(nums2[i - 2, j - 1]);
        //            }
        //        }

        //        workBook.SaveAs("C:\\Users\\admnz\\source\\repos\\BotValuts\\BotVal\\BotVal\\bin\\Debug\\test.xlsx");
        //        workBook.Close();
        //        ex.Quit();
        //    }


        //}
    }
}
