using System;
using System.Collections.Generic;
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
}