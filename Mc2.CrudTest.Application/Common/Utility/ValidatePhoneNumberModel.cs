namespace Mc2.CrudTest.Application.Common.Utility
{
    public  class ValidatePhoneNumberModel
    {
        public string FormattedNumber { get; set; }
        public bool IsMobile { get; set; }
        public bool IsValidNumber { get; set; }
        public bool IsValidNumberForRegion { get; set; }
        public string Region { get; set; }
    }
}