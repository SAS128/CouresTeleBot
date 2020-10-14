using MySqlConnector;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;


namespace ConsoleApp1
{
    class Program
    {
        static TelegramBotClient client;
        static List<string> kurss = new List<string>();
        static List<string> sale = new List<string>();
        static string Rub;
        static string activ;
        const string connectionString = "Server=tcp:met.database.windows.net,1433;Initial Catalog=AzureMet_DB;Persist Security Info=False;User ID=pricol;Password=Ujk213l0Il;";
       
        static void Main(string[] args)
        {

            client = new TelegramBotClient("1096899543:AAHGx8455bBmnqZfJwwWgzF28CkprHw9E9U");
            client.OnMessage += getMsg;
            client.StartReceiving();
            Console.Read();
           
        }
        private static void CreateCommand()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            // устанавливаем соединение с БД
            conn.Open();
            
            
            // запрос
            string sql = "SELECT name FROM Menu WHERE parent_id = 2 AND ID =4";
            // объект для выполнения SQL-запроса
            SqlCommand command = new SqlCommand(sql, conn);
            // объект для чтения ответа сервера
            SqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                Console.WriteLine(reader[0].ToString());
                Rub = reader[0].ToString();
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }
        private static void getBuy()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.privatbank.ua/p24api/pubinfo?exchange&json&coursid=11");
            JArray array = new JArray();
            using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var objText = reader.ReadToEnd();
                Console.WriteLine(objText);
                JArray joResponse = JArray.Parse(objText);
                // JObject result = (JObject)joResponse;
                // array = (JArray)result["buy"];
                foreach (var item in joResponse)
                {
                    kurss.Add(item["buy"].ToString());
                    sale.Add(item["sale"].ToString());
                }
                objText.ToString();
                //Console.WriteLine(objText);
            }
        }
        private static void CreateCommandNewID(long id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            
            conn.Open();
           
            string sql = $"INSERT INTO Users (tele_id,active) VALUES ({id},1)";
           
            SqlCommand command = new SqlCommand(sql, conn);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка");
               
            }
            
            conn.Close();
        }
        private static void CommandZero(long id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            // устанавливаем соединение с БД
            conn.Open();


            // запрос
            string sql = $"SELECT active FROM Users WHERE tele_id = {id}";
            // объект для выполнения SQL-запроса
            SqlCommand command = new SqlCommand(sql, conn);
            // объект для чтения ответа сервера
            SqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
               
                Console.WriteLine(reader[0].ToString());

                activ = reader[0].ToString();
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }
        private static void getMsg(object sender, MessageEventArgs e)
        {   long UserID=0 ;
           
            UserID = e.Message.Chat.Id;
            CommandZero(UserID);
            if (activ == "True")
            {
                Console.WriteLine(UserID);
                CreateCommandNewID(UserID);
                //client.SendTextMessageAsync(392600173, "kek");
                if (e.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                    return;
                Console.WriteLine($"Msg from {e.Message.Chat.Id}");
                switch (e.Message.Text.ToLower())
                {
                    case "/start":

                        var markup = new ReplyKeyboardMarkup(new[]
                        {
                        new KeyboardButton("весь курс"),
                        new KeyboardButton("настройки"),
                    });
                        markup.OneTimeKeyboard = true;
                        client.SendTextMessageAsync(e.Message.Chat.Id, "Выберете пункт меню", replyMarkup: markup);
                        break;
                    case "весь курс":
                        getBuy();
                        client.SendTextMessageAsync(e.Message.Chat.Id,
                              "Buy USD : " + kurss[0] + " Sale USD : " + sale[0] + "\n" +
                              "Buy EUR : " + kurss[1] + " Sale EUR : " + sale[1] + "\n" +
                              "Buy RUB : " + kurss[2] + " Sale RUB : " + sale[2]);
                        //Thread.Sleep(500);
                        //client.SendTextMessageAsync(e.Message.Chat.Id, "Buy EUR : " + kurss[1] + " Sale EUR : " + sale[1]);
                        //Thread.Sleep(500);
                        // client.SendTextMessageAsync(e.Message.Chat.Id, "Buy RUB : " + kurss[2] + " Sale RUB : " + sale[2]);
                        //Thread.Sleep(500);
                        break;
                    //case "настройки":
                    //    var options = new ReplyKeyboardMarkup(new[]
                    //     {
                    //        new KeyboardButton("задать интервал"),
                    //        new KeyboardButton("выбрать валюты"),
                    //    });
                    //    options.OneTimeKeyboard = true;
                    //    client.SendTextMessageAsync(e.Message.Chat.Id, "Выберете нужную настройку", replyMarkup: options);
                    //    break;
                    //case "задать интервал":
                    //    var timerr = new ReplyKeyboardMarkup(new[]
                    //    {
                    //        new KeyboardButton("1 минута"),
                    //        new KeyboardButton("2 минуты"),
                    //        new KeyboardButton("5 минут"),
                    //        new KeyboardButton("10 минут"),
                    //    });
                    //    timerr.OneTimeKeyboard = true;
                    //    client.SendTextMessageAsync(e.Message.Chat.Id, "Выберете интервал отправки сообщения", replyMarkup: timerr);
                    //    break;
                    //getBuy();
                    //CreateCommand();
                    //client.SendTextMessageAsync(e.Message.Chat.Id, "Buy" + Rub + ": " + kurss[2] + " Sale " + Rub + " : " + sale[2]);
                    default:
                        break;
                }

            }
            else
            {
                Console.WriteLine($"{UserID} этот пользователь забанен");
            }
           

        }
    }
}
