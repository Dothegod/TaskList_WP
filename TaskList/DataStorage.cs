﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.IO;
using System.Collections.Generic;
using Microsoft.Phone.Shell;

namespace TaskList
{
    public class DataStorage
    {
        private DataStorage()
        {
        }

        private static DataStorage _instance = new DataStorage();

        public static DataStorage GetInstance()
        {
            return _instance;
        }

        public void SaveData(string Key, object data)
        {
            IsolatedStorageSettings.ApplicationSettings[Key] = data;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
        public object LoadData(string Key)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(Key))
            {
                return null;
            }
            return IsolatedStorageSettings.ApplicationSettings[Key];
        }

    }
}
