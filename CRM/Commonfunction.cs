using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using log4net;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using CRM.SendBoxSMSService;
using System.Net.Mail;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CRM
{
    internal class Commonfunction
    {
        internal static readonly ILog logger = LogManager.GetLogger(typeof(BaseController));
        internal static string SMSURL = Convert.ToString(ConfigurationManager.AppSettings["UnifonicSMSURL"]);
        internal static string SMSUserName = Convert.ToString(ConfigurationManager.AppSettings["SMSUserName"]);
        internal static string SMSPass = Convert.ToString(ConfigurationManager.AppSettings["AppSid"]);
        internal static string SMSFrom = Convert.ToString(ConfigurationManager.AppSettings["SMSFrom"]);

        internal static string MobilySMSURL = Convert.ToString(ConfigurationManager.AppSettings["MobilySMSURL"]);
        internal static string MobilySMSUserName = Convert.ToString(ConfigurationManager.AppSettings["MobilySMSUserName"]);
        internal static string MobilySMSPass = Convert.ToString(ConfigurationManager.AppSettings["MobilySMSPass"]);
        internal static string MobilySMSFrom = Convert.ToString(ConfigurationManager.AppSettings["MobilySMSFrom"]);

        internal static string SandBoxSMSUserName = Convert.ToString(ConfigurationManager.AppSettings["SandBoxSMSUserName"]);
        internal static string SandBoxSMSPassword = Convert.ToString(ConfigurationManager.AppSettings["SandBoxSMSPassword"]);
        internal static string SandBoxSMSFrom = Convert.ToString(ConfigurationManager.AppSettings["SandBoxSMSFrom"]);
        internal static string SandBoxCustomerId = Convert.ToString(ConfigurationManager.AppSettings["SandBoxCustomerId"]);

        internal static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        /// <summary>
        /// Function is used to Decrypt the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        internal static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        /// <summary>
        ///  code to generate 8 digit alpha numeric string
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static string GeneratePassword(int length)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;

            characters += alphabets + small_alphabets + numbers;

            string finalPassword = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (finalPassword.IndexOf(character) != -1);
                finalPassword += character;
            }
            return finalPassword;
        }

        /*printingRequest=1 means reprint; 0 means order allocation; 2 means Shipment Label*/
        //public MessageResponse GetHtmlToPrint(string data, int printingRequest, string BoxId)
        //{
        //    logger.Error("GetHtmlToPrint() started with parameter:data=" + data + " ; printingRequest=" + printingRequest);
        //    MessageResponse msg = new MessageResponse();
        //    try
        //    {
        //        string sfileName = "";

        //        switch (printingRequest)
        //        {
        //            case 0:
        //                {
        //                    sfileName = "OrderAllocation_" + DateTime.Now.Ticks + ".pdf";
        //                    break;
        //                }
        //            case 1:
        //                {
        //                    sfileName = "OrderAllocationReprint_" + DateTime.Now.Ticks + ".pdf";
        //                    break;
        //                }
        //            case 2:
        //                {
        //                    sfileName = "ShipmentLabel_" + BoxId + "_" + DateTime.Now.Ticks + ".pdf";
        //                    break;
        //                }
        //        }
        //        //sfileName = (printingRequest == 1) ? "OrderAllocationReprint_" + DateTime.Now.Ticks + ".pdf" : "OrderAllocation_" + DateTime.Now.Ticks + ".pdf";
        //        string filePath = ConfigurationManager.AppSettings["OrderAllocationPdfPath"] + sfileName;

        //        logger.Error("GetHtmlToPrint() filePath=" + filePath);

        //        logger.Error("ConvertingHtmlToPdf() started");

        //        ConvertingHtmlToPdf(data, filePath);
        //        logger.Error("ConvertingHtmlToPdf() ended");
        //        msg.ResponseCode = (int)ResponseCode.Success;
        //        msg.ResponseMessage = sfileName;
        //    }
        //    catch (Exception ex)
        //    {
        //        msg.ResponseCode = (int)ResponseCode.Exception;
        //        msg.ResponseMessage = ex.Message;
        //        logger.Error("Exception in GetHtmlToPrint()=" + ex.ToString());
        //    }
        //    //response = JsonConvert.SerializeObject(msg);
        //    logger.Error("GetHtmlToPrint() ended");

        //    return msg;
        //}
        private void ConvertingHtmlToPdf(string htmlString, string fileName)
        {
            logger.Error("ConvertingHtmlToPdf() Open with fileName=" + fileName);
            logger.Error("htmlStr=" + htmlString);

            //var pdfDoc = new Document(PageSize.A4, 4f, 2f, 2f, 0f);
            var pdfDoc = new Document(new iTextSharp.text.Rectangle(600f, 600f));
            pdfDoc.SetMargins(20f, 2f, 10f, 6f);
            var htmlparser = new HTMLWorker(pdfDoc);
            using (var memoryStream = new MemoryStream())
            {
                logger.Error("memoryStream loop started");

                var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                htmlparser.Parse(new StringReader(htmlString));
                pdfDoc.Close();
                byte[] bytes = memoryStream.ToArray();
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
                System.IO.File.WriteAllBytes(fileName, bytes);
                memoryStream.Close();
                logger.Error("memoryStream loop closed");
            }
            logger.Error("ConvertingHtmlToPdf() close");
        }

        public HttpResponseMessage PostWebApi(string webApi, object model)
        {
            var endPointAddress = ConfigurationManager.AppSettings["WebAPIPort"];
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using (var handler = new HttpClientHandler { UseDefaultCredentials = true })
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "true");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync(endPointAddress + webApi, byteContent);
                response.Wait();
                return response.Result;
            }
        }
        public HttpResponseMessage GetWebApi(string webApi, object model)
        {
            var endPointAddress = ConfigurationManager.AppSettings["WebAPIPort"];
            using (var handler = new HttpClientHandler { UseDefaultCredentials = true })
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "true");
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.GetAsync(endPointAddress + webApi);
                response.Wait();
                return response.Result;
            }
        }

        public static string PostHttpWebRequestAPICall(string url, object data)
        {
            var endPointAddress = ConfigurationManager.AppSettings["WebAPIPort"];
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(endPointAddress + url);
            string response = string.Empty;
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    response = (new StreamReader(stream)).ReadToEnd();
                }
            }
            return response;

        }
        public static string PutHttpWebRequestAPICall(string url, object data)
        {
            var endPointAddress = ConfigurationManager.AppSettings["WebAPIPort"];
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(endPointAddress + url);
            string response = string.Empty;
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "Put";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    response = (new StreamReader(stream)).ReadToEnd();
                }
            }
            return response;

        }
        public static string DeleteHttpWebRequestAPICall(string url, object data)
        {
            var endPointAddress = ConfigurationManager.AppSettings["WebAPIPort"];
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(endPointAddress + url);
            string response = string.Empty;
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "Delete";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    response = (new StreamReader(stream)).ReadToEnd();
                }
            }
            return response;

        }
        // For Arabic Address
        public static string PostHttpWebRequestAPICallArabic(string url, string data, string token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            string response = string.Empty;
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Headers["Authorization"] = "Bearer " + token;
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    response = (new StreamReader(stream)).ReadToEnd();
                }
            }
            return response;

        }
        public static string GetHttpWebRequestAPICall(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string response = string.Empty;

            using (HttpWebResponse httpresponse = (HttpWebResponse)request.GetResponse())
            using (Stream stream = httpresponse.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                response = reader.ReadToEnd();
            }
            return response;
        }

        public static string AsyncPostHttpWebRequestAPICall(string url, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            string response = string.Empty;
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    response = (new StreamReader(stream)).ReadToEnd();
                }
            }
            return response;

        }
        public static string AsyncGetHttpWebRequestAPICall(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string response = string.Empty;

            using (WebResponse httpresponse = request.GetResponseAsync().Result)
            using (Stream stream = httpresponse.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                response = reader.ReadToEnd();
            }
            return response;
        }

        public static string SendMessage(string msg, string numbers)
        {
            HttpWebRequest req = (HttpWebRequest)
            WebRequest.Create(SMSURL);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string postData = "mobile=" + SMSUserName + "&password=" + SMSPass + "&numbers=" + numbers + "&sender=" + SMSFrom + "&msg=" + msg + "&applicationType=24&msgId=0";
            logger.Info("Data For SMS Sending " + postData);
            req.ContentLength = postData.Length;
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(postData);
            stOut.Close();
            // Do the request to get the response
            string strResponse;
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
            logger.Info("Response For SMS Sending " + strResponse);
            return strResponse;
        }

        private static string ConvertToUnicode(string val)
        {
            string msg2 = string.Empty;
            for (int i = 0; i < val.Length; i++)
            {
                msg2 += convertToUnicode(System.Convert.ToChar(val.Substring(i, 1)));
            }
            return msg2;
        }

        private static string convertToUnicode(char ch)
        {
            System.Text.UnicodeEncoding class1 = new System.Text.UnicodeEncoding();
            byte[] msg = class1.GetBytes(System.Convert.ToString(ch));
            return fourDigits(msg[1] + msg[0].ToString("X"));
        }

        private static string fourDigits(string val)
        {
            string result = string.Empty;
            switch (val.Length)
            {
                case 1: result = "000" + val; break;
                case 2: result = "00" + val; break;
                case 3: result = "0" + val; break;
                case 4: result = val; break;
            }
            return result;
        }



        public static string SMSBoxKuwait(string msg, string numbers)
        {
            SendingSMSResult obj = new SendingSMSResult();
            MessagingSoapClient messagingClient = new MessagingSoapClient();

            var senderText = SandBoxSMSFrom;
            var messageBody = msg;
            DateTime? defDate = null;
            SendingSMSResult result = null;
            SoapUser user = new SoapUser()
            {
                CustomerId = Convert.ToInt32(SandBoxCustomerId),
                Password = SandBoxSMSPassword,
                Username = SandBoxSMSUserName
            };

            var sendMessageRequest = new SendingSMSRequest()
            {
                User = user,
                IsBlink = false,
                IsFlash = false,
                MessageBody = messageBody,
                SenderText = senderText,
                RecipientNumbers = numbers,
                DefDate = defDate
            };

            result = messagingClient.SendSMS(sendMessageRequest);
            decimal bal = result.NetPoints;

            if (result.Result)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        //public static string SendEmailTOUser(string email, string message, string messageDetails, string NotificationFor)
        //{
        //    MailMessage mail = new MailMessage();
        //    mail.To.Add("parth.sourot@gmail.com");
        //    mail.From = new MailAddress("notification@lajexportsblr.com");
        //    mail.Subject = "NOTIFICATION";
        //    string body = "test";
        //    mail.Body = body;
        //    mail.IsBodyHtml = true;
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.lajexportsblr.com"; //Or Your SMTP Server Address
        //    smtp.Port = 25;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = new System.Net.NetworkCredential
        //    ("notification@lajexportsblr.com", "lajblr@123");

        //    smtp.EnableSsl = false;
        //    smtp.Send(mail);
        //    return "1";

        //}
        public static string SendEmailTOUser(string email, string message, string messageDetails, string NotificationFor)
        {
            MailMessage mail = new MailMessage();
            var FromEmailAddress = ConfigurationManager.AppSettings["FromMailAddress"].ToString();
            var HostAddress = ConfigurationManager.AppSettings["HostAddress"].ToString();
            var FromMailAddressPwd = ConfigurationManager.AppSettings["FromMailAddressPwd"].ToString();
            var HostAddressPort = ConfigurationManager.AppSettings["HostAddressPort"].ToString();
            mail.To.Add(email);
            mail.From = new MailAddress(FromEmailAddress);
            mail.Subject = NotificationFor;
            string body = "";
            body += "<h3>" + message + "</h3>";
            body += "</br>";
            body += messageDetails;
            body += "</br>";
            body += "</br>";
            body += "</br>";
            body += "From ERP";
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = HostAddress; //Or Your SMTP Server Address
            smtp.Port = Convert.ToInt32(HostAddressPort);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmailAddress, FromMailAddressPwd);

            smtp.EnableSsl = true;
            smtp.Send(mail);
            return "1";

        }

    }
}