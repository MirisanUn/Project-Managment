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

namespace AppDip
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        AppContext db;

        public AuthWindow()
        {
            InitializeComponent();

            db = new AppContext();
        }

        private void Button_SignIn_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPassword.Password.Trim();

            if(login.Length < 5)
            {
                textBoxLogin.ToolTip = "Слишком короткий Логин";
                textBoxLogin.Background = Brushes.Red;
            } else if(pass.Length <= 8 || pass.Length >= 16)
            {
                textBoxPassword.ToolTip = "Пароль должен быть от 8 до 16 сиволов";
                textBoxPassword.Background = Brushes.Red;
            } else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;
                textBoxPassword.ToolTip = "";
                textBoxPassword.Background = Brushes.Transparent;

                MessageBox.Show("Всё корректно!");

                User authUser = null;
                using (AppContext db = new AppContext())
                {
                    authUser = db.Users.Where(b => b.Login == login && b.Pass == pass).FirstOrDefault();
                }

                if (authUser != null)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Hide();
                }                   
                else
                    MessageBox.Show("Данного пользователя не существует!");
            }
        }

        private void Button_OnReg_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            Hide();
        }
    }
}
