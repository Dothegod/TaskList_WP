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

namespace TaskList
{
    public partial class Notes : PhoneApplicationPage
    {
        TaskItem CurrItem;
        public Notes()
        {
            InitializeComponent();
            CurrItem = App.CurrentItem;
            textBoxNotes.Text = CurrItem.Notes;
            textBlockTitle.Text = CurrItem.textboxTaskContent.Text;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            textBoxNotes.IsReadOnly = false;
            textBoxNotes.Focus();
            textBoxNotes.SelectionStart = textBoxNotes.Text.Length;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            CurrItem.Notes = textBoxNotes.Text;
            textBoxNotes.IsReadOnly = true;
        }
        private void textBoxNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.PlatformKeyCode == 13)
            {
                textBoxNotes.Text += "\n";
                textBoxNotes.SelectionStart = textBoxNotes.Text.Length;

            }
        }

    }
}