using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TaskList
{
    public class CalendarItem: Button
    {
        public DateTime Date;
        public Brush SelectBackGround{  get; set; }
        public CalendarItem()
        {

        }
        public CalendarItem(string Content)
        {
            if (Content == "")
            {
                BorderThickness = new System.Windows.Thickness(0);
                return;
            }
            this.Content = Content;
            this.Click += button1_Click;
            this.LostFocus += button1_LostFocus;
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_LostFocus(object sender, RoutedEventArgs e)
        {

        }

    }
}
