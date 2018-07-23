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
                if (Convert.ToInt32(discount) > 0)
                {
                    return "-" + discount + "%";
                }
                else
                {
                    return "";
                }
            }
            set
            {
                discount = value;
            }
        }
    }
}