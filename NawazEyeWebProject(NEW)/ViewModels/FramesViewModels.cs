using System.Configuration;

namespace NawazEyeWebProject_NEW_.ViewModels
{
    public class ViewFramesViewModel
    {
        string image, discount;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = ConfigurationManager.AppSettings["ItemsImagePath"] + value;
            }
        }
        public string Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = "-" + value + "%";
            }
        }
    }
}