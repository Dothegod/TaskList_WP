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
using System.Threading;
using System.Collections.Generic;

namespace TaskList
{
    public class LanguageContent
    {
        public string New;
        public string ADTip;
        public string PivotHeader;
        public string NewList;
        public string DelList;
        public string Setting;
        public string Version;
        public string Author;
        public string About;
        public string TextItemHit;
        public List<string> Headers = new List<string>();
        public string BuyMsg;
        public string BuyMsgTitle;
        public string DelTaskListInfo;
        public string Priority;
        public string Reminder;
        public string Notes;
        public string Edit;
        public string Complete;


        private static LanguageContent _instance = new LanguageContent();

        private LanguageContent()
        {
            string CultureName = Thread.CurrentThread.CurrentCulture.Name;
            if ("zh-CN" == CultureName || "zh-SG" == CultureName || "zh-TW" == CultureName || "zh-HK" == CultureName)
            {
                Headers.Add("日常");
                Headers.Add("工作");
                Headers.Add("购物");
                Headers.Add("约会");
                Headers.Add("琐事");

                New = "新建";
                PivotHeader = "列表名称";
                NewList = "新建列表";
                DelList = "删除当前列表";
                TextItemHit = "我想要……";
                BuyMsgTitle = "购买软件";
                BuyMsg = "您使用的是免费版本，最多只能使用两个列表，需要购买正式版吗？";
                DelTaskListInfo = "你确信要删除任务列表吗？";
                Priority = "重要";
                Reminder = "提醒";
                Notes = "详情";
                Edit = "编辑";
                Complete = "完成";
                ADTip = "\n广告点击一次就会消失。\n“确定”——不再提示\n“取消”——下次继续提示";

                Version = "版本";
                Setting = "设置";
                Author = "Created by HighFunStudio";
                About = "关于";

            }
            else
            {
                Headers.Add("Daily");
                Headers.Add("Work");
                Headers.Add("Shopping");
                Headers.Add("Date");
                Headers.Add("Other");

                New = "New";
                PivotHeader = "List Name";
                NewList = "New List";
                DelList = "Remove Current List";
                TextItemHit = "I want ...";
                BuyMsgTitle = "BUY IT";
                BuyMsg = "Free version supports two lists at most, buy a full version?";
                DelTaskListInfo = "Are you sure to delete task list?";
                Priority = "Priority";
                Reminder = "Reminder";
                Notes = "Notes";
                Edit = "Edit";
                Complete = "Complete";
                ADTip = "\nAD will disapper after you click it.\n“OK”——Do not show this anymore.\n“Cancel”——Show this next time.";


                Version = "Version";
                Author = "Created by HighFunStudio";
                About = "About";
                Setting = "Setting";
            }
        }

        public static LanguageContent GetInstance()
        {
            return _instance;
        }
    }
}
