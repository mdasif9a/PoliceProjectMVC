using PoliceProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Web;

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

                string message = $"Section : {item.Section} Case-No : {item.SrNo} \nPolice Station : {policestation.Name_En}\nAccused Name : {accusedsname}\nAddress : {accusedsaddress}\nDate of Arrest : {item.JailDate.ToShortDateString()}\nLast Date of ChargeSheet : {item.LastChargeSheetdate.ToShortDateString()}\nDays Left : {leftdays} days";

                if (!item.SecondWPMessageSent && today == secondReminderDate)
                {
                    foreach (var number in validNumbers)
                    {
                        SendWPMessage(number, message);
                    }
                }

                if (!item.ThirdWPMessageSent && today == thirdReminderDate)
                {
                    foreach (var number in validNumbers)
                    {
                        SendWPMessage(number, message);
                    }
                }

                if (!item.FourthWPMessageSent && today == fourthReminderDate)
                {
                    foreach (var number in validNumbers)
                    {
                        SendWPMessage(number, message);
                    }
                }
            }
        }
        private void SendWPMessage(string numbers, string message)
        {
            ApiAndWebContent apidetails = db.ApiAndWebContents.FirstOrDefault();
            if (string.IsNullOrEmpty(numbers))
            {
                return;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, apidetails.ApiUrl);
                    request.Headers.Add("Authorization", apidetails.ApiHeader);

                    var jsonPayload = new
                    {
                        messaging_product = "whatsapp",
                        recipient_type = "individual",
                        to = numbers,
                        type = "text",
                        text = new { preview_url = false, body = message }
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
    }
}