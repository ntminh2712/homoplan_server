using System;

namespace SeminarAPI.Helpers
{
    public class EnumDisplayNameAttribute : Attribute
    {
        public string DisplayName { get; }

        public EnumDisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }

    public enum TypeTemplateMail
    {
        [EnumDisplayName("THÔNG BÁO ĐÃ ĐĂNG KÝ THAM GIA HỘI NGHỊ(Acknowledgement of Registration)")]
        MailRegisterAcknow = 1,

        [EnumDisplayName("THÔNG BÁO ĐÃ NHẬN ABSTRACT (Acknowledgement of Abstract Submission)")]
        MailAbstractSubmit = 2, 

        [EnumDisplayName("NHẮC NỘP LỆ PHÍ ĐĂNG KÝ THAM GIA HỘI NGHỊ (Notification about registration fee)")]
        MailRegistrationFee = 3,

        [EnumDisplayName("THÔNG BÁO ĐÃ NHẬN ĐƯỢC TIỀN LỆ PHÍ VÀ HOÀN TẤT ĐĂNG KÝ (Acknowledgement of registration fee completion)")]
        MailRegistrationFeeComplete = 4,

        [EnumDisplayName("THÔNG BÁO THỜI GIAN, ĐỊA ĐIỂM ĐÓN (Notication about shuttle bus)")]
        MailNotifyLocation = 5,

        [EnumDisplayName("GỬI CHƯƠNG TRÌNH HỘI NGHỊ")]
        MailConferenceProgram = 6,

        [EnumDisplayName("HỎI THÔNG TIN LƯU TRÚ")]
        MailStayInfo = 7, 

        [EnumDisplayName("HỎI THÔNG TIN YÊU CẦU CHĂM SÓC Y TẾ ĐẶC BIỆT")]
        MailSpecialRequiment = 8, 

        [EnumDisplayName("HỎI THÔNG TIN ĐỊA ĐIỂM NHẬP CẢNH")]
        MailEntryLocation = 9
    }

    public enum StatusPayment
    {
        [EnumDisplayName("Chưa thanh toán")]
        Unconfirmed = 0,
        [EnumDisplayName("Chờ xác nhận")]
        Waiting = 1,
        [EnumDisplayName("Đã thanh toán")]
        Confirmed = 2,
        [EnumDisplayName("Đã hủy")]
        Cancelled = 3,
    }


    public static class EnumHelper
    {
        public static string GetDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (EnumDisplayNameAttribute)Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute));

            return attribute?.DisplayName ?? value.ToString();
        }
    }
}
