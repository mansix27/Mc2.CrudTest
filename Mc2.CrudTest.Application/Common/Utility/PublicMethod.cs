using PhoneNumbers;
using System.Globalization;
using System.Net;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Application.Common.Utility
{
    public static class PublicMethod
    {
        
        public static string GetServerTime()
        {
            var dt = DateTime.Now;
            return dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00");
        }
        public static string GetPersianDate()
        {
            var dt = DateTime.Now;
            var pcal = new PersianCalendar();
            return pcal.GetYear(dt) + "/" + pcal.GetMonth(dt).ToString("00") + "/" + pcal.GetDayOfMonth(dt).ToString("00");
        }
        public static bool DateValidateFromText(string date)
        {
            if (date.Length == 0)
                return false;

            var checkTime = new Regex(@"\d{2,4}/\d{1,2}/\d{1,2}");
            if (!checkTime.IsMatch(date))
                return false;

            var temp = date.Split('/');

            if (temp[0].Length == 3)
                return false;

            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string Convert_Miladi_Shamsi(DateTime dt)
        {
            var pcal = new PersianCalendar();
            return pcal.GetYear(dt) + "/" + pcal.GetMonth(dt).ToString("00") + "/" + pcal.GetDayOfMonth(dt).ToString("00");
        }

        public static string GetRandomNumber()
        {
            Random rnd = new Random();
            var value = rnd.Next(1000,9999);
            return value.ToString();
        }

        public static DateTime Convert_Shamsi_Miladi(string persianDate, string time)
        {
            if (string.IsNullOrEmpty(persianDate))
                return default;

            var splittedDate = persianDate.Trim().Split('/');
            var splittedTime = time.Split(':');

            if (splittedDate.Length != 3)
                return default;


            var x = new PersianCalendar();

            var dt = time == string.Empty ?
                x.ToDateTime(Convert.ToInt32(splittedDate[0]), Convert.ToInt32(splittedDate[1]), Convert.ToInt32(splittedDate[2]), 1, 1, 1, 1, 1) :
                x.ToDateTime(Convert.ToInt32(splittedDate[0]), Convert.ToInt32(splittedDate[1]), Convert.ToInt32(splittedDate[2]), Convert.ToInt32(splittedTime[0]), Convert.ToInt32(splittedTime[1]), 1, 1, 1);

            return dt;
        }
        public static IQueryable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First()).AsQueryable();
        }
        public static ValidatePhoneNumberModel ValidateMobileNumber( string telephoneNumber,string countryCode)
        {

            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
           
                PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(telephoneNumber, countryCode);

                bool isMobile = false;
                bool isValidNumber = phoneUtil.IsValidNumber(phoneNumber); // returns true for valid number    

                // returns trueor false w.r.t phone number region  
                bool isValidRegion = phoneUtil.IsValidNumberForRegion(phoneNumber, countryCode);

                string region = phoneUtil.GetRegionCodeForNumber(phoneNumber); // GB, US , PK    

                var numberType = phoneUtil.GetNumberType(phoneNumber); // Produces Mobile , FIXED_LINE    

                string phoneNumberType = numberType.ToString();

                if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "MOBILE")
                {
                    isMobile = true;
                }

                var originalNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164); // Produces "+923336323997"    

                var data = new ValidatePhoneNumberModel

                {
                    FormattedNumber = originalNumber,    
                        IsMobile = isMobile,    
                        IsValidNumber = isValidNumber,    
                        IsValidNumberForRegion = isValidRegion,    
                        Region = region
                    };
                return data;

            }
            catch (NumberParseException ex)
            {

                String errorMessage = "NumberParseException was thrown: " + ex.Message.ToString();


                return null;


            }
        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public static bool isValidBankAccountNumber(string inputBankAccNum)
        {
            string strRegex = @"^[A-Z]{2}[0-9]{2}[A-Z0-9]{1,30}$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputBankAccNum))
                return (true);
            else
                return (false);
        }
    }
}
