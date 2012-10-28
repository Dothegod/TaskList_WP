using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace TaskList
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
            textBlockVersion.Text = LanguageContent.GetInstance().Version + App.version;
            textBlockAuthor.Text = LanguageContent.GetInstance().Author;
        }

        private void hyperlinkButton1_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask Email = new EmailComposeTask();
            Email.To = "highfunstudio@hotmail.com";
            Email.Subject = "Task List Suggestion";
            Email.Show();
        }

        private void hyperlinkButton2_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask Rt = new MarketplaceReviewTask();
            Rt.Show();
        }


    }
}