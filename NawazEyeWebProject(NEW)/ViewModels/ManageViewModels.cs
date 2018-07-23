using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Configuration;

namespace NawazEyeWebProject_NEW_.ViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public string Address { get; set; }
        public int OrdersCount { get; set; }
    }
    public class ViewOrdersViewModel
    {
        DateTime dispatchDate;
        [Display(Name ="Order Date")]
        public string OrderDate { get; set; }
        [Display(Name ="Dispatch Date")]
        public string DispatchDate
        {
            get
            {
                if (dispatchDate == DateTime.Parse("1/1/9999"))
                {
                    return "";
                }
                else
                {
                    return dispatchDate.ToShortDateString();
                }
            }
            set
            {
                dispatchDate = DateTime.Parse(value);
            }
        }
        [Display(Name ="Order Status")]
        public string Status { get; set; }
        [Display(Name ="Total Price")]
        public string TotalPrice { get; set; }
        [Display(Name ="Delivery Charges")]
        public string DeliveryCharges { get; set; }
        public int Id { get; set; }
    }
    public class ViewOrderItemsViewModel
    {
        string image;
        [Display(Name ="Item Name")]
        public string Name { get; set; }
        [Display(Name ="Price")]
        public string Price { get; set; }
        [Display(Name ="Quantity")]
        public string Quantity { get; set; }
        [Display(Name ="Image")]
        public string Image
        {
            get
            {
                string path = ConfigurationManager.AppSettings["ItemsImagePath"] + image;
                return path;
            }
            set
            {
                image = value;
            }
        }
        [Display(Name ="Total Price")]
        public string TotalPrice
        {
            get
            {
                return (Convert.ToUInt32(Quantity) * Convert.ToUInt32(Price)).ToString();
            }
        }
    }
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
    public class ChangeAddressViewModel
    {
        [Required]
        [Display(Name ="New Address")]
        public string NewAddress { get; set; }
        [Required]
        [Display(Name ="City")]
        public int CityId { get; set; }
    }
}