using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lunacod.Classes
{
    public static class SMSRU
    {
        public static string Balance{  get; private set; }

        private static string stringFromServer;
        private static string QueryToServerStatus;
        private static string MessageStatus;

        /// <summary>
        /// Проверяет баланс на SMS.RU
        /// </summary>
        public static void CheckBalance()
        {
            Settings.GetSettings();
            using var client = new HttpClient();
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("GET"), "https://sms.ru/my/balance?api_id=" + Settings.SettingSMSRUAPI);
            var response = httpClient.SendAsync(request);

            stringFromServer = response.Result.Content.ReadAsStringAsync().Result;
            Balance = stringFromServer[3..] + "₽";
        }

        /// <summary>
        /// Делает запрос в SMS.RU для получения стоимости сообщения
        /// </summary>
        public static void CheckMessagePrice(string phoneNumber, string Message, ref string priceToSend)
        {
            Settings.GetSettings();
            using HttpClient httpClient = new();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://sms.ru/sms/cost?api_id=" + Settings.SettingSMSRUAPI + "&to=" + phoneNumber);
            request.Content = new StringContent($"msg={Uri.EscapeDataString(Message)}");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            var response = httpClient.SendAsync(request);
            string stringFromServer = response.Result.Content.ReadAsStringAsync().Result;
            
            priceToSend = string.Concat(stringFromServer.AsSpan(3), "₽");
        }
        /// <summary>
        /// Отправляет сообщение через SMS.RU
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="Message"></param>
        public static void SendMessage(string order_ID, string FIO, string device,string phoneNumber, string Message)
        {
            try
            {
                Settings.FillSMSRUcodesXml();
                Settings.GetSettings();
                using HttpClient httpClient = new HttpClient();
                using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), "https://sms.ru/sms/send?api_id=" + Settings.SettingSMSRUAPI + "&to=" + phoneNumber);
                request.Content = new StringContent($"msg={Uri.EscapeDataString(Message)}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
        
                var response = httpClient.SendAsync(request);
        
                string stringFromSMSRU = response.Result.Content.ReadAsStringAsync().Result;

                if (stringFromSMSRU != null)
                {
                    string[] SplittedStringFromSMSRU = stringFromSMSRU.Split('\n');

                    QueryToServerStatus = SplittedStringFromSMSRU[0];
                    MessageStatus = SplittedStringFromSMSRU[1];

                    if (SplittedStringFromSMSRU.Length == 3)
                    {
                        Balance = SplittedStringFromSMSRU[2][8..];
                        if (MessageStatus.Length > 3)
                        {
                            XElement smsrucodes = XElement.Load(Settings.path_smsrucodes);
                            var element = smsrucodes.Elements("code").Where(x => x.Attribute("id").Value == QueryToServerStatus).FirstOrDefault();
                            string Query_CallBackFromSMSRU = element.Value;

                            MessageBox.Show
                                (
                                $"Статус отправки: {Query_CallBackFromSMSRU}\n\n" +
                                $"Номер сообщения: {MessageStatus}\n" +
                                $"Баланс: {Balance}₽\n",
                                "Уведомление",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
                            Statistics.AddStatisticsRow(order_ID,
                                                        FIO,
                                                        device,
                                                        phoneNumber,
                                                        "SMS.RU",
                                                        DateOnly.FromDateTime(DateTime.Now).ToString(),
                                                        TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                                        "Готово к выдаче",
                                                        $"{Query_CallBackFromSMSRU}: {MessageStatus}",
                                                        Message);
                        }
                        else
                        {
                            XElement smsrucodes = XElement.Load(Settings.path_smsrucodes);
                            var element = smsrucodes.Elements("code").Where(x => x.Attribute("id").Value == QueryToServerStatus).FirstOrDefault();
                            string Query_CallBackFromSMSRU = element.Value;
                            element = smsrucodes.Elements("code").Where(x => x.Attribute("id").Value == MessageStatus).FirstOrDefault();
                            string MessageStatus_CallBackFromSMSRU = element.Value;

                            MessageBox.Show($"Статус отправки: {Query_CallBackFromSMSRU}\n\n" +
                                            $"Ответ с сервера: {MessageStatus_CallBackFromSMSRU}\n\n" +
                                            $"Баланс: {Balance}₽\n",
                                            "Уведомление",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information
                                            );
                            Statistics.AddStatisticsRow(order_ID,
                                                        FIO,
                                                        device,
                                                        phoneNumber,
                                                        "SMS.RU",
                                                        DateOnly.FromDateTime(DateTime.Now).ToString(),
                                                        TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                                        "Готово к выдаче",
                                                        $"{Query_CallBackFromSMSRU}: {MessageStatus_CallBackFromSMSRU}",
                                                        Message);
                        }
                    }
                    else
                    {
                        XElement smsrucodes = XElement.Load(Settings.path_smsrucodes);
                        var element = smsrucodes.Elements("code").Where(x => x.Attribute("id").Value == QueryToServerStatus).FirstOrDefault();
                        string Query_CallBackFromSMSRU = element.Value;

                        MessageBox.Show
                            (
                            $"Статус запроса: {Query_CallBackFromSMSRU}\n\n",
                            "Уведомление",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                            );
                        Statistics.AddStatisticsRow(order_ID,
                                                    FIO,
                                                    device,
                                                    phoneNumber,
                                                    "SMS.RU",
                                                    DateOnly.FromDateTime(DateTime.Now).ToString(),
                                                    TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                                    "Готово к выдаче",
                                                    Query_CallBackFromSMSRU,
                                                    Message);
                    }
                }
                else
                {
                    Statistics.AddStatisticsRow(order_ID,
                                                FIO,
                                                device,
                                                phoneNumber,
                                                "SMS.RU",
                                                DateOnly.FromDateTime(DateTime.Now).ToString(),
                                                TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                                "Готово к выдаче",
                                                "Что-то пошло не так, возможно проблемы с SMS.RU",
                                                Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
