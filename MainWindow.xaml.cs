using SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopContactsApp.Classes;
using DesktopContactsAppCore;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            this.ReadDatabase();
        }

        private void createNewContact_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();

        }


        private void ReadDatabase()
        {
            

            //string databaseName = "Contacts.db";
            //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string databasePath = System.IO.Path.Combine(folderPath, databaseName);

            using (SQLiteConnection sqliteConnection = new SQLiteConnection(App.databasePath))
            {
                sqliteConnection.CreateTable<Contact>();
                contacts = sqliteConnection.Table<Contact>().ToList<Contact>().OrderBy(C => C.Name).ToList();
            }

            contactsListView.ItemsSource = contacts;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox SearchBox = sender as TextBox;

            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(SearchBox.Text)).ToList<Contact>();

            contactsListView.ItemsSource = filteredList;

        }


        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = contactsListView.SelectedItem as Contact;
            if (selectedContact != null)
            {
                ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);
                contactDetailsWindow.ShowDialog();

                this.ReadDatabase();
            }
        }
    }
}
