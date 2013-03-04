using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SmartMad.Ads.WindowsPhone7.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
//using ScheduledTaskAgent2;

namespace TaskList
{
    public struct TaskIteminfo
    {
        public string Content;
        public bool IsFinish;
        public string Note;
        public bool IsPriority;
        public DateTime date;
    }
    public struct TaskListInfo
    {
        public string Header;
        public List<TaskIteminfo> Items;
    }

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor

        private const string Key = "TaskList";
        private string DelTaskListInfo;
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            Init();
            string IsShowAD = (string)(DataStorage.GetInstance().LoadData("AdTip"));
            if (IsShowAD == null || IsShowAD != App.version)
            {
                string Tip = App.version + LanguageContent.GetInstance().ADTip;
                if (MessageBoxResult.OK == MessageBox.Show(Tip, "提示", MessageBoxButton.OKCancel))
                {
                    DataStorage.GetInstance().SaveData("AdTip", App.version);
                }
            }

        }

        private void Init()
        {
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            textBlockListName.Text = LanguageContent.GetInstance().PivotHeader;
            DelTaskListInfo = LanguageContent.GetInstance().DelTaskListInfo;
            initAppBar();
            App.mainPage = this;
            LoadData();
        }

        private void AddPivot(string Header)
        {
            ListBox lb = new ListBox();
            TaskItem ti = new TaskItem();
            ti.textboxTaskContent.Text = LanguageContent.GetInstance().TextItemHit;
            ti.textboxTaskContent.Foreground = new SolidColorBrush(Colors.DarkGray);
            lb.Items.Add(ti);

            PivotItem p = CreatePivot(Header);
            p.Content = lb;
            try
            {
                PivotTask.Items.Add(p);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private PivotItem CreatePivot(string Header)
        {
            PivotItem p = new PivotItem();
            TextBlock tb = new TextBlock();
            tb.Text = Header;

            tb.FontFamily = new System.Windows.Media.FontFamily("/Monotype Corsiva.ttf#Monotype Corsiva");
            p.Header = Header;
            return p;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

//         protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)   //当页面成为非活动的时候 的事件
//         {
//             SaveData();
//             base.OnNavigatedFrom(e);
//         }
//         protected override void OnNavigatedTo(NavigationEventArgs e)
//         {
//             LoadData();
//             base.OnNavigatedTo(e);
//         }

        private void ApplicationBarAddTask(object sender, EventArgs e)
        {
            HideTaskListHeader();
            PivotItem Item = (PivotItem)PivotTask.SelectedItem;
            if (Item == null)
            {
                ApplicationBarMenuItem_Click_AddNewList(sender, e);
                return;
            }
            ListBox listbox = (ListBox)Item.Content;
            listbox.Items.Add(new TaskItem());
               
        }

        private void ApplicationBarMenuItem_Click_AddNewList(object sender, EventArgs e)
        {
            canvasTaskListHeader.Visibility = Visibility.Visible;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            canvasTaskListHeader.Visibility = Visibility.Collapsed;
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            canvasTaskListHeader.Visibility = Visibility.Collapsed;
            AddPivot(textBoxListname.Text);
        }

        private void ApplicationBarMenuItem_Click_DelList(object sender, EventArgs e)
        {
            HideTaskListHeader();
            if (MessageBoxResult.OK != MessageBox.Show(DelTaskListInfo,"", MessageBoxButton.OKCancel))
            {
                return;
            }
            PivotItem Item = (PivotItem)PivotTask.SelectedItem;
            PivotTask.Items.Remove(Item);
        }

        private void ApplicationBarMenuItem_Click_3(object sender, EventArgs e)
        {

        }

        private void ApplicationBarMenuItem_Click_About(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void initAppBar()
        {
//             (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = LanguageContent.GetInstance().New;
            (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).Text = LanguageContent.GetInstance().NewList;
            (ApplicationBar.MenuItems[1] as ApplicationBarMenuItem).Text = LanguageContent.GetInstance().DelList;
            (ApplicationBar.MenuItems[2] as ApplicationBarMenuItem).Text = LanguageContent.GetInstance().About;
        }
        public void LoadData()
        {
            PivotTask.Items.Clear();
            List<TaskListInfo> Lists = (List<TaskListInfo>)DataStorage.GetInstance().LoadData(Key);
            if (Lists == null)
            {
                foreach (string header in LanguageContent.GetInstance().Headers)
                {
                    AddPivot(header);
                }
                return;
            }
            foreach (TaskListInfo ListInfo in Lists)
            {
                ListBox ItemList = new ListBox();
                foreach (TaskIteminfo ItemInfo in ListInfo.Items)
                {
                    TaskItem Item = new TaskItem();
                    Item.textboxTaskContent.Text = ItemInfo.Content;
                    Item.IsFinished = ItemInfo.IsFinish;
                    Item.Notes = ItemInfo.Note;
                    Item.IsPriority = ItemInfo.IsPriority;
                    Item.date = ItemInfo.date;
                    Item.UpdateTextColor();
                    ItemList.Items.Add(Item);
                }
                PivotItem Pi = CreatePivot(ListInfo.Header);
                Pi.Content = ItemList;
                PivotTask.Items.Add(Pi);
            }

        }
        public void SaveData()
        {
            List<TaskListInfo> Lists = new List<TaskListInfo>();
            foreach (PivotItem it in PivotTask.Items)
            {
                TaskListInfo ListInfo;
                ListInfo.Header = (string)it.Header;

                ListBox Itemlist = it.Content as ListBox;
                List<TaskIteminfo> Items = new List<TaskIteminfo>();
                ListInfo.Items = Items;
                foreach (TaskItem Item in Itemlist.Items)
                {
                    TaskIteminfo ItemInfo;
                    ItemInfo.Content = Item.textboxTaskContent.Text;
                    ItemInfo.IsFinish = Item.IsFinished;
                    ItemInfo.Note = Item.Notes;
                    ItemInfo.IsPriority = Item.IsPriority;
                    ItemInfo.date = Item.date;
                    Items.Add(ItemInfo);
                }
                Lists.Add(ListInfo);
            }
            DataStorage.GetInstance().SaveData(Key, Lists);
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (canvasTaskListHeader.Visibility == Visibility.Visible)
            {
                HideTaskListHeader();
                e.Cancel = true;
            } 
        }

        private void HideTaskListHeader()
        {
            canvasTaskListHeader.Visibility = Visibility.Collapsed;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxTaskInfo.Text == "")
            {
                return;
            }
            TaskItem item = new TaskItem();
            item.textboxTaskContent.Text = TextBoxTaskInfo.Text;
            TextBoxTaskInfo.Text = "";
            PivotItem Item = (PivotItem)PivotTask.SelectedItem;
            if (Item == null)
            {
                ApplicationBarMenuItem_Click_AddNewList(sender, e);
                return;
            }
            ListBox listbox = (ListBox)Item.Content;
            listbox.Items.Add(item);
        }

        private void TextBoxTaskInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.PlatformKeyCode == 13)
            {
                HyperlinkButton_Click(null,null);
            }

        }
        #region  AD_Control
        private void adView1_AdViewEvent(object sender, SmartMad.Ads.WindowsPhone7.WPF.AdViewEventArgs args)
        {
        }
        private void adView1_AdViewFullscreenEvent(object sender, AdViewFullscreenEventArgs args)
        {
            if (!args.fullscreen)
            {
                adView1.IsEnabled = false;
                this.LayoutRoot.Children.Remove(adView1);
            }
        }
        #endregion

        private void TextBoxTaskInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

    }
}