using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lunacod.Classes
{
    public static class Messagio
    {
        /// <summary>
        /// Отправляет сообщение по заданному номеру телефона
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <param name="settingsSenderCode"></param>
        /// <param name="settingsProjectLogin"></param>
        /// <param name="ShowMessageBox"></param>
        //public static string callback;
        public static void SendMessage(string MessageType, string order_ID, string FIO, string device, string phoneNumber, string message, bool ShowMessageBox, string comboBox_MessageTemplates_text)
        {
            if (phoneNumber.Length != null)
            {
                using var client = new HttpClient();
                var endpoint = new Uri("https://msg.messaggio.com/api/v1/send");
                var payload = new StringContent(PostJS(phoneNumber, Settings.SettingMessagio_SenderCode, message), Encoding.UTF8, "application/json");
                payload.Headers.Add("Messaggio-Login", Settings.SettingMessagio_Projectlogin);
                var json = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                var jsonObject = JsonConvert.DeserializeObject<CallBacker>(json);
                try
                {
                    if (json != null && json.Length > 1)
                    {
                        if (jsonObject.Messages != null)
                        {
                            foreach (var JsonMessageList in jsonObject.Messages)
                            {
                                //Если есть ошибки, и включен messageBox
                                if (JsonMessageList.Error != null && ShowMessageBox)
                                {
                                    MessageBox.Show(
                                    $"Отправлено в: {jsonObject.Accepted_at}\n" +
                                    $"Телефон: {JsonMessageList.Recipient.Phone}\n" +
                                    $"Ошибка: {JsonMessageList.Error.Title}\n" +
                                    $"Описание ошибки: {JsonMessageList.Error.Detail}",
                                    
                                    "Messagio", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Statistics.AddStatisticsRow(order_ID,
                                        FIO,
                                        device,
                                        phoneNumber,
                                        MessageType,
                                        DateOnly.FromDateTime(DateTime.Now).ToString(),
                                        TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                        comboBox_MessageTemplates_text,
                                        $"{JsonMessageList.Error.Title}: {JsonMessageList.Error.Detail}",
                                        message);
                                }
                                //если нет ошибок и включен mbox
                                else if (ShowMessageBox)
                                {
                                    MessageBox.Show(
                                    $"Отправлено в: {jsonObject.Accepted_at}\n" +
                                    $"Телефон: {JsonMessageList.Recipient.Phone}\n" +
                                    $"ID Сообщения: {JsonMessageList.Message_id}\n",
                                    "Messagio",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                                    Statistics.AddStatisticsRow(order_ID,
                                        FIO,
                                        device,
                                        phoneNumber,
                                        MessageType,
                                        DateOnly.FromDateTime(DateTime.Now).ToString(),
                                        TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                        comboBox_MessageTemplates_text,
                                        $"Запрос выполнен: {JsonMessageList.Message_id}",
                                        message);
                                }
                                else
                                {
                                    Statistics.AddStatisticsRow(order_ID,
                                        FIO,
                                        device,
                                        phoneNumber,
                                        MessageType,
                                        DateOnly.FromDateTime(DateTime.Now).ToString(),
                                        TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                        comboBox_MessageTemplates_text,
                                        $"Запрос выполнен: {JsonMessageList.Message_id}",
                                        message);
                                }
                            }
                        }
                        else if (jsonObject.Messages == null)
                        {
                            Statistics.AddStatisticsRow(order_ID,
                                    FIO,
                                    device,
                                    phoneNumber,
                                    MessageType,
                                    DateOnly.FromDateTime(DateTime.Now).ToString(),
                                    TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                    comboBox_MessageTemplates_text,
                                    $"{jsonObject.Title}: {jsonObject.Detail}",
                                    message);
                        }
                    } 
                    else
                    {
                        Statistics.AddStatisticsRow(order_ID,
                                    FIO,
                                    device,
                                    phoneNumber,
                                    MessageType,
                                    DateOnly.FromDateTime(DateTime.Now).ToString(),
                                    TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                    comboBox_MessageTemplates_text,
                                    $"Что-то пошло не так, возможно проблемы с Messagio",
                                    message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show(json);
                }
            }
            else
            {
                Statistics.AddStatisticsRow(order_ID,
                                FIO,
                                device,
                                phoneNumber,
                                MessageType,
                                DateOnly.FromDateTime(DateTime.Now).ToString(),
                                TimeOnly.FromDateTime(DateTime.Now).ToString(),
                                comboBox_MessageTemplates_text,
                                $"Ошибка: null в ячейке номера телефона",
                                message);
            }
        }
        private static string PostJS(string messagio_phoneNumber, string messagio_APIKey, string messagio_Message)
        {
            string json = $@"{{
                          ""recipients"": [
                            {{
                              ""phone"": ""{messagio_phoneNumber}""
                            }}
                          ],
                          ""options"": {{
                            ""ttl"": 60,
                            ""dlr_callback_url"": ""https://example.com/dlr"",
                            ""external_id"": ""messaggio-acc-external-id-0""
                          }},
                          ""channels"": [
                            ""vk""
                          ],
                          ""vk"": {{
                            ""from"": ""{messagio_APIKey}"",
                            ""content"": [
                              {{
                                ""type"": ""text"",
                                ""text"": ""{messagio_Message}""
                              }}
                            ]
                          }}
                        }}";
            return json;
        }
        public static void FormirateMessage(ref ComboBox CurrentSelectComboBox,ref TextBox textBox,string FIO, string OrderID, string Device)
        {
            if (CurrentSelectComboBox.SelectedIndex == 0)
            {
                textBox.Text = $"{NameClient(FIO)}, ваш заказ {OrderID} готов к выдаче. Центр Компьютерной Поддержки +73517349949 ckp74.ru";
            }
            else if (CurrentSelectComboBox.SelectedIndex == 1)
            {
                textBox.Text = $"{NameClient(FIO)}, ваш {Device} принят в работу. Номер заказа {OrderID}.Благодарим за обращение! Центр Компьютерной Поддержки +73517349949 ckp74.ru";
            }
            else if (CurrentSelectComboBox.SelectedIndex == 2)
            {
                textBox.Text = $"{NameClient(FIO)}, уведомляем, что прошел год с момента чистки вашего компьютера. Для записи на повторную чистку запишитесь по телефону +73517349949";
            }
        }
        /// <summary>
        /// Метод отделяющий имя от ФИО
        /// </summary>
        /// <param name="FIO"></param>
        /// <returns></returns>
        private static string NameClient(string FIO)
        {
            //массив с Ф.И.О.
            string[] _f_i_o_ = FIO.Split(' ');

            if (_f_i_o_.Length == 3)
            {
                return _f_i_o_[1];
            }
            else if (_f_i_o_.Length == 2)
            {
                return _f_i_o_[1];
            }
            else
            {
                return _f_i_o_[0];
            }
        }
        private class CallBacker
        {
            [JsonPropertyName("accepted_at")]
            public string Accepted_at { get; set; }

            [JsonPropertyName("messages")]
            public List<Message> Messages { get; set; }


            [JsonPropertyName("title")]
            public string Title { get; set; }
            [JsonPropertyName("detail")]
            public string Detail { get; set; }
            
            
            
            
            public class Message
            {
                [JsonPropertyName("recipient")]
                public Recipient Recipient { get; set; }

                [JsonPropertyName("message_id")]
                public string Message_id { get; set; }
                [JsonPropertyName("error")]
                public Error Error { get; set; }
            }

            public class Recipient
            {
                [JsonPropertyName("phone")]
                public string Phone { get; set; }
            }
            public class Error
            {
                [JsonPropertyName("title")]
                public string Title { get; set; }
                [JsonPropertyName("detail")]
                public string Detail { get; set; }
            }
        }
    }
    
}
