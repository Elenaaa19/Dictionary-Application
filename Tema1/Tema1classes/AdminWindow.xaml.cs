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

namespace Tema1.Tema1classes
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>


    //Getter Setter Proprities

    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = textBoxUsername.Text; 
            string password = passwordBoxPassword.Password;
            Login log = new Login();
            bool ok = log.LoginUser(username,password);
            if (ok)
            {
                // Crează și afișează fereastra administrativă
                Afterlogin afterlogin = new Afterlogin();
                afterlogin.Show();
                this.Close(); // Închide fereastra de autentificare sau fereastra actuală, în funcție de necesități
            }
            else
            {
                MessageBox.Show("Nume de utilizator sau parolă incorecte");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
