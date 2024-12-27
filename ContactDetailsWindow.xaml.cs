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
using DesktopContactsApp.Classes;
using SQLite;


namespace DesktopContactsAppCore
{
    /// <summary>
    /// Interaction logic for ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        Contact contact;
        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();
            this.contact = contact;

            this.nameTextBlock.Text = contact.Name;
            this.emailTextBlock.Text = contact.Email;
            this.phoneTextBlock.Text = contact.Phone;   
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = this.nameTextBlock.Text;
            contact.Email = this.emailTextBlock.Text;
            contact.Phone = this.phoneTextBlock.Text;

            using (SQLiteConnection sqliteConnection = new SQLiteConnection(App.databasePath))
            {
                sqliteConnection.CreateTable<Contact>();
                sqliteConnection.Update(contact);
            };

            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(App.databasePath))
            {
                sqliteConnection.CreateTable<Contact>();
                sqliteConnection.Delete(contact);
            };
            Close();
        }
    }
}
