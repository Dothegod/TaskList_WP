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

namespace TaskList
{
    public partial class Calendar : UserControl
    {
        public DateTime Date;
        CalendarItem m_CurrItem;
        public Calendar()
        {
            InitializeComponent();
//             textBlockdate.Text = DateTime.Now.ToString();
            wpPool.Children.Clear();

        }
        public void init()
        {
            DateTime date = Date;
            textBlockdate.Text = string.Format("{0:Y}",date);

            wpPool.Children.Clear();
            date = date.AddDays(1 - date.Day);
            int week = (int)date.DayOfWeek;

            double ItemHeight = (this.ActualHeight - wpPool.Margin.Top - wpPool.Margin.Bottom) / 6 - 1;
            double ItemWidth = (this.ActualWidth - wpPool.Margin.Left - wpPool.Margin.Right) / 7 - 1;
            for (int i = 0; i < week; i++)
            {
                Grid item = new Grid();
                item.Height = ItemHeight;
                item.Width = ItemWidth;
                wpPool.Children.Add(item);
            }
            int DayNum = DateTime.DaysInMonth(date.Year, date.Month);
            for (int i = 0; i < DayNum; i++)
            {
                CalendarItem item = new CalendarItem(date.Day.ToString());
                item.Height = ItemHeight;
                item.Width = ItemWidth;
                item.Date = date;
                item.Click += SelectDate;
                wpPool.Children.Add(item);
                TimeSpan dif = date - DateTime.Today;
                if (dif.TotalDays >= 0 && dif.TotalDays <= 1)
                {
                    item.Foreground = calendarItem1.Background;
                    item.Background = calendarItem1.Foreground;
                }
                else
                {
                    item.Foreground = calendarItem1.Foreground;
                    item.Background = calendarItem1.Background;
                }
                item.BorderBrush = calendarItem1.BorderBrush;
                date = date.AddDays(1);
            }
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            init();

        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            Date = Date.AddMonths(1);
            init();

        }

        private void buttonPre_Click(object sender, RoutedEventArgs e)
        {
            Date = Date.AddMonths(-1);
            init();
        }

        private void SelectDate(object sender, RoutedEventArgs e)
        {
            CalendarItem item = sender as CalendarItem;
//             item.Foreground = App.Current.Resources["WhiteBrushKey"] as Brush;
//             item.Background = App.Current.Resources["BlueFontKey"] as Brush;
            item.Foreground = calendarItem1.Background;
            item.Background = calendarItem1.Foreground;
            if (m_CurrItem != null)
            {
                m_CurrItem.Foreground = calendarItem1.Foreground;
                m_CurrItem.Background = calendarItem1.Background;
            }
            m_CurrItem = item;
            Date = m_CurrItem.Date;
        }

        private void wpPool_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (e.TotalManipulation.Translation.X > 80)
            {
                buttonPre_Click(null, null);

            }
            else if (e.TotalManipulation.Translation.X < -80)
            {
                buttonNext_Click(null, null);
            }

        }

        private void wpPool_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            

        }
    }
}
