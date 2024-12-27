using DesktopContactsApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlClient;
using SQLite;
using DesktopContactsAppCore;


namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            // Save New Contact here
            // After Save
            Contact contact = new Contact()
            {
                Name = nameTextBlock.Text,
                Email = emailTextBlock.Text,
                Phone = phoneTextBlock.Text,
            };

            // Connection to DataBase
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(App.databasePath))
            {
                sqliteConnection.CreateTable<Contact>();
                sqliteConnection.Insert(contact);
            };
            



            this.Close();
        }
    }
}
