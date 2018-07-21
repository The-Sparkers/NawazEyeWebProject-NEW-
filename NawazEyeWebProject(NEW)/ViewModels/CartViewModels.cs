using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NawazEyeWebProject_NEW_.ViewModels
{
    
    public class BuyersInfoViewModel
    {
        [Required]
        [StringLength(256)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Address", Description = "Your order will be sent to this address.")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "City")]
        public string CityName { get; set; }
    }
    public class OrderPrescriptionGlassesViewModel
    {
        string quantity;
        [Display(Name = "Id")]
        public string Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Price")]
        public string Price { get; set; }
        [Display(Name = "Discounted Price")]
        public string DiscountedPrice { get; set; }
        public List<string> Images { get; set; }
        [Display(Name = "Lens")]
        [Required]
        public string Lens { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name ="Number of Items")]
        [Required]
        public int Quantity
        {
            get
            {
                return Convert.ToInt32(quantity);
            }
            set
            {
                quantity = "" + value; 
            }
        }
        [Display(Name ="Prescription")]
        [Required]
        public HttpPostedFileBase Prescription { get; set; }
        public string DeliveryCharges { get; set; }
    }
    public class OrderSunglassesViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Price")]
        public string Price { get; set; }
        [Display(Name = "Discounted Price")]
        public string DiscountedPrice { get; set; }
        public List<string> Images { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Number of Items")]
        [Required]
        public int Quantity { get; set; }
        public string DeliveryCharges { get; set; }
    }
    public class OrderSuccessViewModel
    {
        [Display(Name ="Order Id")]
        public string OrderId { get; set; }
        [Display(Name ="Discount Availed")]
        public string DiscountAvailed { get; set; }
        [Display(Name ="Total Price")]
        public string TotalPrice { get; set; }
        [Display(Name ="Delivery Charges")]
        public string DeliveryCharges { get; set; }
        [Display(Name ="Order Status")]
        public string Status { get; set; }
        public string BuyersName { get; set; }
        [Display(Name = "Grand Total")]
        public string GTotal
        {
            get
            {
                return decimal.Add(Convert.ToDecimal(TotalPrice), Convert.ToDecimal(DeliveryCharges)).ToString();
            }
        }
    }
}