using PoliceProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace PoliceProjectMVC
{
    public class MessageSchedulerService
    {
        private readonly PDDBContext db = new PDDBContext();

        public void CheckAndSendScheduledMessages()
        {
            DateTime today = DateTime.Now.Date;

            List<SRNSRCase> cases = db.SRNSRCases
                .Where(x => DbFunctions.TruncateTime(x.LastChargeSheetdate) >= DbFunctions.TruncateTime(DateTime.Now)
                            && x.Status == "Pending")
                .ToList();

            foreach (var item in cases)
            {
                var result = db.Accuseds.Where(x => x.SRNSRCaseId == item.Id).ToList();
                int chdays = db.MHeads.Where(x => x.Id == item.MHeadId).Select(x => x.CSM_Days).FirstOrDefault();
                DateTime secondReminderDate = item.CreatedDate.AddDays(chdays / 3).Date;
                DateTime thirdReminderDate = item.CreatedDate.AddDays((chdays / 3) * 2).Date;
                DateTime fourthReminderDate = item.CreatedDate.AddDays((chdays / 3) * 3).Date;

                int sdpo = db.SubDivisions.Where(x => x.Id == item.SubDivisionId).Select(x => x.SdpoId).FirstOrDefault();
                string spnumber = db.DistrictDetails.Select(x => x.ContactNo).FirstOrDefault();
                string sdponumber = db.SDPOs.Where(x => x.Id == sdpo).Select(x => x.MobileNo).FirstOrDefault();
                var policestation = db.PoliceStations.Where(x => x.Id == item.PoliceStationId).FirstOrDefault();
                string shonumber = policestation.MobileNo;
                var validNumbers = new List<string> { spnumber, sdponumber, shonumber, item.IoMobile, item.CIMobile }
                    .Where(num => !string.IsNullOrEmpty(num));
                string allnumbers = string.Join(",", validNumbers);

                int leftdays = (item.LastChargeSheetdate.Date - DateTime.Today).Days;
                string accusedsname = string.Join(", ", result.Select(s => s.Name));
                string accusedsaddress = string.Join(", ", result.Select(s => s.Address));

                string message = $"Section : {item.Section} Case-No : {item.SrNo} \nPolice Station : {policestation.Name_En}\nAccused Name : {accusedsname}\nAddress : {accusedsaddress}\nDate of Arrest : {item.JailDate.ToShortDateString()}\nLast Date of ChargeSheet : {item.LastChargeSheetdate.ToShortDateString()}\nDays Left : {leftdays} days\nIO Name : {item.IoName}";

                SRNSRCase rNSRCase = db.SRNSRCases.Find(item.Id);

                if (!item.SecondWPMessageSent && today == secondReminderDate)
                {
                    foreach (var number in validNumbers)
                    {
                        SendWPMessage(number, item.Section, item.SrNo, policestation.Name_En, item.AccusedName, item.AccusedAddress, item.JailDate.ToShortDateString(),
                            item.LastChargeSheetdate.ToShortDateString(), leftdays.ToString(), item.IoName);
                        //SendSMSMessage(number, message);
                    }
                    rNSRCase.SecondWPMessageSent = true;
                }

                if (!item.ThirdWPMessageSent && today == thirdReminderDate)
                {
                    foreach (var number in validNumbers)
                    {
                        SendWPMessage(number, item.Section, item.SrNo, policestation.Name_En, item.AccusedName, item.AccusedAddress, item.JailDate.ToShortDateString(),
                            item.LastChargeSheetdate.ToShortDateString(), leftdays.ToString(), item.IoName);
                        //SendSMSMessage(number, message);
                    }
                    rNSRCase.ThirdWPMessageSent = true;
                }

                if (!item.FourthWPMessageSent && today == fourthReminderDate)
                {
                    foreach (var number in validNumbers)
                    {
                        SendWPMessage(number, item.Section, item.SrNo, policestation.Name_En, item.AccusedName, item.AccusedAddress, item.JailDate.ToShortDateString(),
                            item.LastChargeSheetdate.ToShortDateString(), leftdays.ToString(), item.IoName);
                        //SendSMSMessage(number, message);
                    }
                    rNSRCase.FourthWPMessageSent = true;
                }
                db.Entry(rNSRCase).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void SendWPMessage(string number, string section, string CaseNo, string PoliceStation,
            string AccusedName, string AccusedAddress, string ArrestDate, string LastDate, string LeftDays, string ioname)
        {
            ApiAndWebContent apidetails = db.ApiAndWebContents.FirstOrDefault();
            if (string.IsNullOrEmpty(number))
            {
                return;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    //var request = new HttpRequestMessage(HttpMethod.Post, apidetails.ApiUrl);
                    //request.Headers.Add("Authorization", apidetails.ApiHeader);
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://cloudapi.wafortius.com/api/v1.0/messages/send-template/919450057501");
                    request.Headers.Add("Authorization", "Bearer aJTcVeudaUaWamvQbfenLg");

                    var jsonPayload = new
                    {
                        messaging_product = "whatsapp",
                        recipient_type = "individual",
                        to = number,
                        type = "template",
                        template = new
                        {
                            name = "bettiah_police123",
                            language = new { code = "en" },
                            components = new[]
                            {
                                new
                                {
                                    type = "body",
                                    parameters = new[]
                                    {
                                        new { type = "text", text = section },
                                        new { type = "text", text = CaseNo },
                                        new { type = "text", text = PoliceStation },
                                        new { type = "text", text = AccusedName },
                                        new { type = "text", text = AccusedAddress },
                                        new { type = "text", text = ArrestDate },
                                        new { type = "text", text = LastDate },
                                        new { type = "text", text = LeftDays },
                                        new { type = "text", text = ioname }
                                    }
                                }
                            }
                        }
                    };


                    string jsonContent = JsonSerializer.Serialize(jsonPayload);
                    request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    var response = client.SendAsync(request).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();

                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Console.WriteLine(responseContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending WhatsApp message: {ex.Message}");
            }
        }

        public void SendSMSMessage(string number, string message1)
        {
            ApiAndWebContent apidetails = db.ApiAndWebContents.FirstOrDefault();
            if (string.IsNullOrEmpty(number))
            {
                return;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://smsfortius.in/api/mt/SendSMS?user=fipldlfb2&password=123321&senderid=MCTSER&channel=2&DCS=0&flashsms=0&route=14&peid=1601336160567548927&DLTTemplateId=1707169477410107685&number=";
                    url += number;
                    url += "&text=";

                    url += message1;
                    //var request = new HttpRequestMessage(HttpMethod.Post, apidetails.ApiUrl);
                    //request.Headers.Add("Authorization", apidetails.ApiHeader);
                    var request = new HttpRequestMessage(HttpMethod.Post, url);

                    var response = client.SendAsync(request).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();

                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Console.WriteLine(responseContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending WhatsApp message: {ex.Message}");
            }
        }
    }
}