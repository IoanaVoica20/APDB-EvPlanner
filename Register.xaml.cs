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

namespace PlanIVent
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        string UNume;
        string Parola;
        string Nume, Prenume, Tlf, Adresa;






        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.Text = string.Empty;
            t.GotFocus -= TextBox_GotFocus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UNume = txtUsername.Text;
            Parola = txtPassword.Text;
            Nume = txtName.Text;
            Prenume = txtSurame.Text;
            Tlf = txtPhone.Text;
            Adresa = txtAddress.Text;

            int ID=InsertAccount(UNume, Parola);
            InsertClient(Nume, Prenume, Tlf, Adresa, ID);
            MessageBox.Show("Successfully Signed up");
            MainWindow window = new MainWindow();
            window.Owner = this;
            window.Show();
        }

        static int InsertAccount(string nume,string parola)
        {
            var ctx = new EvPlannerEntities();
            var newAccount = new Login()
            {
                Nume = nume,
                Parola = parola,
                Conectat = 0,
                Privilegiu="c"
            };
            ctx.Logins.Add(newAccount);
            ctx.SaveChanges();

            return newAccount.ID_cont;
        }

        static void InsertClient(string nume,string prenume,string tlf, string adr,int ID)
        {
            var ctx = new EvPlannerEntities();
            var newClient = new Clienti()
            {
                Nume = nume,
                Prenume = prenume,
                Telefon = tlf,
                Adresa = adr,
                ID_Login=ID
            };

            ctx.Clientis.Add(newClient);
            ctx.SaveChanges();
        }
    }
}
