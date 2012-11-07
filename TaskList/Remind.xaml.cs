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
using Microsoft.Phone.Scheduler;

namespace TaskList
{
    public partial class Remind : PhoneApplicationPage
    {
        public Remind()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            DateTime date = calendar.Date;
            date = date.AddHours(0 - date.Hour);
            date = date.AddMinutes(0 - date.Minute);
            date = date.AddHours((int)(loopSelectHour.selector.DataSource.SelectedItem));
            date = date.AddMinutes((int)(loopSelectMin.selector.DataSource.SelectedItem));
            App.CurrentItem.date = date;
            if (App.CurrentItem.date < DateTime.Now)
            {
                MessageBox.Show("You can not set a reminder with past time.");
                return;
            }

//             StartResourceIntensiveAgent();
            AddReminder();
            NavigationService.GoBack();
        }
        private void AddReminder()
        {
            Reminder reminder = new Reminder(App.CurrentItem.textboxTaskContent.Text);
            reminder.Title = App.CurrentItem.textboxTaskContent.Text;
            reminder.Content = App.CurrentItem.Notes;
            reminder.BeginTime = App.CurrentItem.date;
            reminder.ExpirationTime = App.CurrentItem.date;
            reminder.RecurrenceType = RecurrenceInterval.None;
            reminder.NavigationUri = new Uri("/Mainpage.xaml",UriKind.Relative);

            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);

        }
        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime date = App.CurrentItem.date;
            if (date.Year == 1)
            {
                date = DateTime.Now;
            }
            calendar.Date = date;
            loopSelectHour.selector.DataSource.SelectedItem = date.Hour;
            loopSelectMin.selector.DataSource.SelectedItem = date.Minute;
            calendar.UserControl_Loaded(null, null);
        }
        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
            }
        }

        private void StartResourceIntensiveAgent()
        {
            string resourceIntensiveTaskName = "ResourceIntensiveAgent";

            ResourceIntensiveTask resourceIntensiveTask;

            resourceIntensiveTask = ScheduledActionService.Find(resourceIntensiveTaskName) as ResourceIntensiveTask;

            if (resourceIntensiveTask != null)
            {
                RemoveAgent(resourceIntensiveTaskName);
            }

            resourceIntensiveTask = new ResourceIntensiveTask(resourceIntensiveTaskName);

            resourceIntensiveTask.Description = "This demonstrates a resource-intensive task.";

            try
            {
                ScheduledActionService.Add(resourceIntensiveTask);

#if DEBUG
                ScheduledActionService.LaunchForTest(resourceIntensiveTaskName, TimeSpan.FromSeconds(6));
#else
            ScheduledActionService.LaunchForTest(resourceIntensiveTaskName, TimeSpan.FromSeconds(30));
#endif

            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }
            }
            catch (SchedulerServiceException)
            {
            }


        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            App.CurrentItem.date = new DateTime();
            NavigationService.GoBack();
        }

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
        }

        private void PhoneApplicationPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Orientation == PageOrientation.LandscapeLeft || this.Orientation == PageOrientation.LandscapeRight)
            {
                Grid.SetRow(GridTime, 0);
                Grid.SetColumn(GridTime, 1);
                calendar.Width = 392;
                calendar.init();
            }
            else
            {
                Grid.SetRow(GridTime, 1);
                Grid.SetColumn(GridTime, 0);

                calendar.Width = 440;
                calendar.init();
            }


        }



    }
}