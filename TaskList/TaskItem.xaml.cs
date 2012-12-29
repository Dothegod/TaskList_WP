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
    public partial class TaskItem : UserControl
    {
        private bool m_IsFinished = false;
        public bool IsFinished
        {
            get
            {
                return m_IsFinished;
            }
            set
            {
                m_IsFinished = value;
                date = date;
            }
        }
        private DateTime m_date = new DateTime();
        public DateTime date
        {
            get
            {
                return m_date;
            }
            set
            {
                m_date = value;

                if ( IsFinished == true )
                {
                    GridTaskInfo.Background = null;
                    return;
                }
                if (value < DateTime.Now && value.Year != 1)  //超时变红
                {
                    GridTaskInfo.Background = (Brush)this.Resources["RedBrushKey"];
                }
                else
                {
                    TimeSpan dif = value - DateTime.Now;
                    if (dif.TotalMinutes <= 5 && dif.TotalMinutes > 0)  //小于五分钟提示
                    {
                        GridTaskInfo.Background = (Brush)this.Resources["PrinkBrushKey"];

                    }
                    else
                    {
                        GridTaskInfo.Background = null;
                    }
                }
            }
        }
        public string Notes
        {
            get
            {
                return textBlockNotes.Text;
            }

            set
            {
                textBlockNotes.Text = value;
                if (textBlockNotes.Text != "")
                {
                    textBlockNotes.Visibility = Visibility.Visible;
                }
                else
                {
                    textBlockNotes.Visibility = Visibility.Collapsed;
                }

            }
        }
        public bool IsPriority
        {
            get
            {
                if (GridPriority.Visibility == Visibility.Visible)
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }

            set
            {
                if (true == value)
                {
                    GridPriority.Visibility = Visibility.Visible;
                } 
                else
                {
                    GridPriority.Visibility = Visibility.Collapsed;
                }

            }
        }
        public TaskItem()
        {
            InitializeComponent();
            GridConfig.Visibility = Visibility.Collapsed;
            buttonDel.Visibility = Visibility.Collapsed;
            textBlockNotes.Text = "";
            textBlockNotes.Visibility = Visibility.Collapsed;
            textboxTaskContent.Text = "";
            IsPriority = false;

            buttonPrio.Content = LanguageContent.GetInstance().Priority;
            buttonEdit.Content = LanguageContent.GetInstance().Edit;
            buttonReminder.Content = LanguageContent.GetInstance().Reminder;
            buttonNotes.Content = LanguageContent.GetInstance().Notes;
            buttonComplete.Content = LanguageContent.GetInstance().Complete;
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as ListBox).Items.Remove(this);
        }

        private void EditTask()
        {
            if (!IsFinished)
            {
                textboxTaskContent.IsReadOnly = !textboxTaskContent.IsReadOnly;
                textboxTaskContent.Focus();
                textboxTaskContent.SelectionStart = textboxTaskContent.Text.Length;

            }
            UpdateTextColor();
        }

        public void UpdateTextColor()
        {
            if (IsFinished)
            {
                textboxTaskContent.Foreground = new SolidColorBrush(Colors.DarkGray);
                buttonDel.Visibility = Visibility.Visible;
                GridPriority.Visibility = Visibility.Collapsed;
            }
            else
            {
                textboxTaskContent.Foreground = new SolidColorBrush(Colors.Black);
                buttonDel.Visibility = Visibility.Collapsed;
            }
            checkboxComplete.IsChecked = IsFinished;

        }

        private void textBoxTaskInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.PlatformKeyCode == 13)
            {
                textboxTaskContent.IsReadOnly = !textboxTaskContent.IsReadOnly;
            }
        }

        private void textBoxTaskInfo_Tap(object sender, GestureEventArgs e)
        {
            if (GridConfig.Visibility != Visibility.Visible)
            {
                this.ConfigPanelExp.Begin();
                GridConfig.Visibility = Visibility.Visible;
            } 
            else
            {
                GridConfig.Visibility = Visibility.Collapsed;
            }
        }

        private void buttonNotes_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentItem = this;
        }

        private void buttonComplete_Click(object sender, RoutedEventArgs e)
        {
            GridConfig.Visibility = Visibility.Collapsed;
            IsFinished = !IsFinished;
            checkboxComplete.IsChecked = IsFinished;
            UpdateTextColor();
        }

        private void buttonPrio_Click(object sender, RoutedEventArgs e)
        {
            IsPriority = !IsPriority;
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            EditTask();

        }



    }
}
