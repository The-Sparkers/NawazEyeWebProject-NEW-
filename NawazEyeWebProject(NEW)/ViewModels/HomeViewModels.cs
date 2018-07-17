using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NawazEyeWebProject_NEW_.ViewModels
{
    public class HomeViewModel
    {
        public List<HotItemsViewModel> HotItems { get; set; }
        public List<FeaturedSunglassesViewModel> PopSunglasses { get; set; }
        public List<FeaturedPrescriptionGlassesViewModel> PopPrescriptionGlasses { get; set; }
    }
    public class HotItemsViewModel
    {
        string image;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image
        {
            get
            {
                return ConfigurationManager.AppSettings["ItemsImagePath"] + image;
            }
            set
            {
                image = value;
            }
        }
    }
    public class FeaturedSunglassesViewModel
    {
        string image;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image
        {
            get
            {
                return ConfigurationManager.AppSettings["ItemsImagePath"] + image;
            }
            set
            {
                image = value;
            }
        }
        public string Price { get; set; }
    }
    public class FeaturedPrescriptionGlassesViewModel
    {
        string image;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image
        {
            get
            {
                return ConfigurationManager.AppSettings["ItemsImagePath"] + image;
            }
            set
            {
                image = value;
            }
        }
        public string Price { get; set; }
    }
    public class ViewProductViewModel
    {
        [Display(Name ="Id")]
        public string Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Price")]
        public string Price { get; set; }
        [Display(Name = "Discounted Price")]
        public string DiscountedPrice { get; set; }
        public string PrimaryImage { get; set; }
        public List<string> Images { get; set; }
        public bool IsSunglasses { get; set; }
        [Display(Name = "Frame Color")]
        public string FrameColor { get; set; }
        [Display(Name = "Lens Color")]
        public string LensColor { get; set; }
        [Display(Name = "Frame")]
        public string Frame { get; set; }
        [Display(Name = "Lens")]
        public string Lens { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}