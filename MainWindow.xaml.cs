using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;

namespace PlanIVent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation=  WindowStartupLocation.CenterScreen;

        }

        private void textUsername_MouseDown(object sender,MouseButtonEventArgs e)
        {
            txtUsername.Focus();
        }

        private void txtUsername_TextChanged(object sender,TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(textUsername.Text)&& textUsername.Text.Length>0)
            {
                textUsername.Visibility = Visibility.Collapsed;
            }
            else
            {
                textUsername.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)  ///////aici la logare va fi de completat
        {
            if(!string.IsNullOrEmpty(txtUsername.Text)&&!string.IsNullOrEmpty(txtPassword.Password))
            {
                if(login()==true)
                {
                    MessageBox.Show("Successfully Signed in");
                    switch(checkAccountType().ToString()[0])
                    {
                        case 'a':
                            {
                                admin_home window = new admin_home();
                                window.Owner = this;
                                window.Show();
                            }
                            break;
                        case 'p':
                            {
                                //fereastra pt personal
                            }
                            break;
                        case 'c':
                            {
                                //fereastra pentru client
                            }break;
                        default:
                            {
                                //eror
                            }break;
                    }
                    
                    
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect Password. Maybe you should try again :)");
                }
               
            }
        }

        private object checkAccountType()
        {
            var ctx = new EvPlannerEntities();
            var exists = (from l in ctx.Logins
                          where l.Nume == txtUsername.Text
                          select l).FirstOrDefault<Login>();
            return exists.Privilegiu;
        }
        private bool login()
        {
            var ctx = new EvPlannerEntities();
            var exists = (from l in ctx.Logins
                          where l.Nume == txtUsername.Text
                          select l).FirstOrDefault<Login>();
            if(exists==null)
            {
                return false;
            }
            if(exists.Parola==txtPassword.Password)
            {
                return true;
            }
            return false;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton==MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            Register window = new Register();
            window.Owner = this;
            //window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }
    }
}
