using System;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;

namespace Lunacod.Classes
{
    static public class Settings
    {
        static public readonly string path_Settings = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Lunacod\settings.xml";
        static public readonly string path_smsrucodes = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Lunacod\smsrucodes.xml";
        static public readonly string path_Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Lunacod";

        static public bool SettingsChange;

        static public string SettingDataSource;
        static public string SettingInitialCatalog;
        static public string SettingUserID;
        static public string SettingPassword;
        static public string SettingSMSRUAPI;
        static public string SettingMessagio_SenderCode;
        static public string SettingMessagio_Projectlogin;
        

        /// <summary>
        /// Создает файл с настройками: settings.xml
        /// </summary>
        static public void CreateSettingsXml()
        {
            XDocument xdoc = new(
                new XElement("settings",
                    new XElement("DataSource"),
                    new XElement("InitialCatalog"),
                    new XElement("UserID"),
                    new XElement("Password"),
                    new XElement("SMSRUAPI"),
                    new XElement("Messagio_SenderCode"),
                    new XElement("Messagio_Projectlogin")
                ));
            xdoc.Save(path_Settings);
        }
        /// <summary>
        /// Создает xml-файл с отладочными кодами для сайта SMS.RU
        /// </summary>
        static public void FillSMSRUcodesXml()
        {
            if (!File.Exists(path_smsrucodes))
            {
                XDocument xdoc = new(
                    new XElement("codes",
                        new XElement("code", new XAttribute("id", "-1"), "Сообщение не найдено"),
                        new XElement("code", new XAttribute("id", "100"), "Запрос выполнен или сообщение находится в нашей очереди"),
                        new XElement("code", new XAttribute("id", "101"), "Сообщение передается оператору"),
                        new XElement("code", new XAttribute("id", "102"), "Сообщение отправлено (в пути)"),
                        new XElement("code", new XAttribute("id", "103"), "Сообщение доставлено"),
                        new XElement("code", new XAttribute("id", "104"), "Не может быть доставлено: время жизни истекло"),
                        new XElement("code", new XAttribute("id", "105"), "Не может быть доставлено: удалено оператором"),
                        new XElement("code", new XAttribute("id", "106"), "Не может быть доставлено: сбой в телефоне"),
                        new XElement("code", new XAttribute("id", "107"), "Не может быть доставлено: неизвестная причина"),
                        new XElement("code", new XAttribute("id", "108"), "Не может быть доставлено: отклонено"),
                        new XElement("code", new XAttribute("id", "110"), "Сообщение прочитано (для Viber, временно не работает)"),
                        new XElement("code", new XAttribute("id", "150"), "Не может быть доставлено: не найден маршрут на данный номер"),
                        new XElement("code", new XAttribute("id", "200"), "Неправильный api_id"),
                        new XElement("code", new XAttribute("id", "201"), "Не хватает средств на лицевом счету"),
                        new XElement("code", new XAttribute("id", "202"), "Неправильно указан номер телефона получателя, либо на него нет маршрута"),
                        new XElement("code", new XAttribute("id", "203"), "Нет текста сообщения"),
                        new XElement("code", new XAttribute("id", "204"), "Имя отправителя не зарегистрировано у оператора получателя данного сообщения. Подайте заявку через раздел Отправители на сайте SMS.RU"),
                        new XElement("code", new XAttribute("id", "205"), "Сообщение слишком длинное (превышает 8 СМС)"),
                        new XElement("code", new XAttribute("id", "206"), "Будет превышен или уже превышен дневной лимит на отправку сообщений"),
                        new XElement("code", new XAttribute("id", "207"), "На этот номер нет маршрута для доставки сообщений"),
                        new XElement("code", new XAttribute("id", "208"), "Параметр time указан неправильно"),
                        new XElement("code", new XAttribute("id", "209"), "Вы добавили этот номер (или один из номеров) в стоп-лист"),
                        new XElement("code", new XAttribute("id", "215"), "Этот номер находится в стоп-листе SMS.RU (от получателя поступала жалоба на спам)"),
                        new XElement("code", new XAttribute("id", "210"), "Используется GET, где необходимо использовать POST"),
                        new XElement("code", new XAttribute("id", "211"), "Метод не найден"),
                        new XElement("code", new XAttribute("id", "212"), "Текст сообщения необходимо передать в кодировке UTF-8 (вы передали в другой кодировке)"),
                        new XElement("code", new XAttribute("id", "213"), "Указано более 5000 номеров в списке получателей"),
                        new XElement("code", new XAttribute("id", "214"), "Номер находится зарубежом (включена настройка 'Отправлять только на номера РФ')"),
                        new XElement("code", new XAttribute("id", "220"), "Сервис временно недоступен, попробуйте чуть позже"),
                        new XElement("code", new XAttribute("id", "230"), "Превышен общий лимит количества сообщений на этот номер в день"),
                        new XElement("code", new XAttribute("id", "231"), "Превышен лимит одинаковых сообщений на этот номер в минуту"),
                        new XElement("code", new XAttribute("id", "232"), "Превышен лимит одинаковых сообщений на этот номер в день"),
                        new XElement("code", new XAttribute("id", "233"), "Превышен лимит отправки повторных сообщений с кодом на этот номер за короткий промежуток времени ('защита от мошенников', можно отключить в разделе 'Настройки')"),
                        new XElement("code", new XAttribute("id", "300"), "Неправильный token (возможно истек срок действия, либо ваш IP изменился)"),
                        new XElement("code", new XAttribute("id", "301"), "Неправильный api_id, либо логин/пароль"),
                        new XElement("code", new XAttribute("id", "302"), "Пользователь авторизован, но аккаунт не подтвержден (пользователь не ввел код, присланный в регистрационной смс)"),
                        new XElement("code", new XAttribute("id", "303"), "Код подтверждения неверен"),
                        new XElement("code", new XAttribute("id", "304"), "Отправлено слишком много кодов подтверждения. Пожалуйста, повторите запрос позднее"),
                        new XElement("code", new XAttribute("id", "305"), "Слишком много неверных вводов кода, повторите попытку позднее"),
                        new XElement("code", new XAttribute("id", "500"), "Ошибка на сервере. Повторите запрос."),
                        new XElement("code", new XAttribute("id", "501"), "Превышен лимит: IP пользователя из сети TOR, слишком много таких сообщений за короткий промежуток времени (можно настроить в ЛК)."),
                        new XElement("code", new XAttribute("id", "502"), "Превышен лимит: IP пользователя не совпадает с его страной, слишком много таких сообщений за короткий промежуток времени (можно настроить в ЛК)."),
                        new XElement("code", new XAttribute("id", "503"), "Превышен лимит: Слишком много сообщений в эту страну за короткий промежуток времени (можно настроить в ЛК)."),
                        new XElement("code", new XAttribute("id", "504"), "Превышен лимит: Слишком много кодов авторизаций зарубеж за короткий промежуток времени (можно настроить в ЛК)."),
                        new XElement("code", new XAttribute("id", "505"), "Превышен лимит: Слишком много сообщений на один IP адрес (можно настроить в ЛК)."),
                        new XElement("code", new XAttribute("id", "506"), "Подсеть (ASN %s) IP адреса пользователя (%s) заблокирована, так как используется для атак."),
                        new XElement("code", new XAttribute("id", "507"), "IP адрес пользователя указан неверно, либо идет из частный подсети (192.*, 10.*, итд)."),
                        new XElement("code", new XAttribute("id", "901"), "Callback: URL неверный (не начинается на http://)"),
                        new XElement("code", new XAttribute("id", "902"), "Callback: Обработчик не найден (возможно был удален ранее)"))
                );
                xdoc.Save(path_smsrucodes);
            }
        }

        //Сохрани настройки
        static public void SaveSettings(string DataSource, string InitialCatalog, string UserID, string Password, string SMSRUAPI, string Messagio_SenderCode, string Messagio_Projectlogin)
        {
            if (!Directory.Exists(path_Directory))
            {
                Directory.CreateDirectory(path_Directory);
            }
            else
            {
                if (!File.Exists(path_Settings))
                {
                    CreateSettingsXml();
                }
                else
                {
                    XDocument xdoc = new(
                        new XElement("settings",
                            new XElement("DataSource", DataSource),
                            new XElement("InitialCatalog", InitialCatalog),
                            new XElement("UserID", UserID),
                            new XElement("Password", Password),
                            new XElement("SMSRUAPI", SMSRUAPI),
                            new XElement("Messagio_SenderCode", Messagio_SenderCode),
                            new XElement("Messagio_Projectlogin", Messagio_Projectlogin)
                        ));
                    xdoc.Save(path_Settings);
                }
            }
        }
        /// <summary>
        /// Заполняет поля класса записями из файла settings.xml
        /// </summary>
        static public void GetSettings()
        {
            try
            {
                if (!Directory.Exists(path_Directory))
                {
                    Directory.CreateDirectory(path_Directory);
                }
                else
                {
                    if (!File.Exists(path_Settings))
                    {
                        CreateSettingsXml();
                    }
                    else
                    {
                        XDocument xdoc = XDocument.Load(path_Settings);

                        SettingDataSource = xdoc.Element("settings").Element("DataSource")?.Value;
                        SettingInitialCatalog = xdoc.Element("settings").Element("InitialCatalog")?.Value;
                        SettingUserID = xdoc.Element("settings").Element("UserID")?.Value;
                        SettingPassword = xdoc.Element("settings").Element("Password")?.Value;
                        SettingSMSRUAPI = xdoc.Element("settings").Element("SMSRUAPI")?.Value;
                        SettingMessagio_SenderCode = xdoc.Element("settings").Element("Messagio_SenderCode")?.Value;
                        SettingMessagio_Projectlogin = xdoc.Element("settings").Element("Messagio_Projectlogin")?.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                                $"{ex.Message}",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
            }
        }
        
        /// <summary>
        /// Возвращает строку подключения к БД с клиентами
        /// </summary>
        /// <returns></returns>
        static public string GetConnString()
        {
            GetSettings();
            string queryString = $@"Data Source={SettingDataSource};Initial Catalog={SettingInitialCatalog};User ID={SettingUserID};Password={SettingPassword}";
            return queryString;
        }
        /// <summary>
        /// Возвращает строку подключения к БД с логами
        /// </summary>
        /// <returns></returns>
        static public string GetLogTableConnString()
        {
            string queryString = $@"Data Source=0.ckp74.ru,14333;Initial Catalog=sms;User ID=sms;Password=FyIFcsikm3DN";
            return queryString;
        }
    }
}