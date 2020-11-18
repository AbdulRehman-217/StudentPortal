using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace StudentPortal.Utilities
{
    public static class NotificationsHandler
    {
        public static void SendPushNotification(string deviceId, string message,string title, string type)
        {
            try
            {
                string applicationID = "AAAAHJ4jZ94:APA91bFfp2HLEbJafFC-saFzSSEZ0teIwlChM9XPr_JDA36vInX98m6QrNXLt6fUfSRFw9RnTJfGs8CTLcbqiWoBptg0nyjjwgsdsnnPKjMdI1zs933_aqvJjNO5NhSy44d5k-V8VUyZ";
                string senderId = "122912204766";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var payload = new
                {
                    to = deviceId,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = message,
                        title = title,
                        badge = 1,
                        type = type
                    },
                    data = new
                    {
                        msg = message,
                        title = title,
                        type = type
                    }
                };
                var json = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;

            }
        }
        public static void SendPushNotificationToMultiUser(List<string> deviceIds, string message, string title, string type)
        {
            try
            {
                string applicationID = "AAAAHJ4jZ94:APA91bFfp2HLEbJafFC-saFzSSEZ0teIwlChM9XPr_JDA36vInX98m6QrNXLt6fUfSRFw9RnTJfGs8CTLcbqiWoBptg0nyjjwgsdsnnPKjMdI1zs933_aqvJjNO5NhSy44d5k-V8VUyZ";
                string senderId = "122912204766";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var payload = new
                {
                    registration_ids = deviceIds,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = message,
                        title = title,
                        badge = 1,
                        type = type
                    },
                    data = new
                    {
                        msg = message,
                        title = title,
                        type = type
                    }
                };
                var json = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;

            }
        }


    }
}
