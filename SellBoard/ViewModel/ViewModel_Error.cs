using System;

namespace SellBoard.ViewModel
{
    public class ViewModel_Error
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string  ErrorMassage { get; set; }
        public string ErrorTitle { get; set; }
    }
}