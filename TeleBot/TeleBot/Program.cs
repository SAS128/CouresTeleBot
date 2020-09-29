using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
        const string connectionString = "Server=tcp:met.database.windows.net,1433;Initial Catalog=AzureMet_DB;Persist Security Info=False;User ID=pricol;Password=Ujk213l0Il;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        static void Main(string[] args)
        {

            client = new TelegramBotClient("1096899543:AAHGx8455bBmnqZfJwwWgzF28CkprHw9E9U");
            client.OnMessage += getMsg;
            client.StartReceiving();
            Console.Read();
           
        }
        private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
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


            }
        }
        private static void getMsg(object sender, MessageEventArgs e)
        {
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
                    client.SendTextMessageAsync(e.Message.Chat.Id, "Choose lang", replyMarkup: markup);
                    break;
                case "весь курс":
                    getBuy();
                    client.SendTextMessageAsync(e.Message.Chat.Id, "Buy USD : " + kurss[0] + " Sale USD : " + sale[0]);
                    client.SendTextMessageAsync(e.Message.Chat.Id, "Buy EUR : " + kurss[1] + " Sale EUR : " + sale[1]);
                    client.SendTextMessageAsync(e.Message.Chat.Id, "Buy RUB : " + kurss[2] + " Sale RUB : " + sale[2]);
                    break;
                case "настройки":
                    CreateCommand("SELECT * FROM ParentTable",connectionString);
                    break;

                default:
                    break;
            }

        }
    }
}
