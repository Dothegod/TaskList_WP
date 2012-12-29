using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Threading;
using System.Windows;

namespace TaskList
{
    public partial class About : PhoneApplicationPage
    {
//         public string AppName { get; set; }
//         public string AppVersion { get; set; }
//         public bool IsChn { get; set; }
//         public string UpdateInfo { set; get; }
        public About()
        {
            InitializeComponent();
            Refresh();
        }
        private void hyperlinkButton1_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask Email = new EmailComposeTask();
            Email.To = "highfunstudio@hotmail.com";
            Email.Subject = "TaskList" + " Suggestion";
            Email.Show();
        }

        private void hyperlinkButton2_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask Rt = new MarketplaceReviewTask();
            Rt.Show();
        }

        public void Refresh()
        {
            string CultureName = Thread.CurrentThread.CurrentCulture.Name;
            if ("zh-CN" == CultureName || "zh-SG" == CultureName || "zh-TW" == CultureName || "zh-HK" == CultureName)
            {
                hyperlinkButtonReview.Content = "给个好评吧，亲！";
                hyperlinkButtonApp.Content = "获取更多精彩应用";
            }
        }

        private void hyperlinkButtonApp_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceSearchTask marketsearch = new MarketplaceSearchTask();
            marketsearch.ContentType = MarketplaceContentType.Applications;
            marketsearch.SearchTerms = "HighFunStudio";
            marketsearch.Show();
        }
    }
}