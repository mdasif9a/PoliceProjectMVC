using System.Collections.Generic;

namespace PoliceProjectMVC.Models
{
    public class HomeViewModel
    {
        public DistrictDetail DistrictDetail { get; set; }
        public List<Banner> Banners { get; set; }
        public List<PressRelease> PressReleases { get; set; }
        public List<MWCriminal> MWCriminals { get; set; }
        public List<IAcheveMent> AcheveMents { get; set; }
        public List<BestEmployee> BestEmployees { get; set; }
        public List<ImageGallery> Galleries { get; set; }
    }


}