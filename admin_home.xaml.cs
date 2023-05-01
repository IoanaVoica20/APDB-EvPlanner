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
    /// Interaction logic for admin_home.xaml
    /// </summary>
    public partial class admin_home : Window
    {
        public admin_home()
        {
            InitializeComponent();
            var ctx = new EvPlannerEntities();
            var latest_ev = (from e in ctx.Evenimentes
                             orderby e.Data_Start descending
                             select e.Denumire).Take(4);
            Latest_Ev = new List<object>(latest_ev);
            setLatestEventsRows();

        }

        private List<object> Latest_Ev;
        private void setLatestEventsRows()
        {
            if(Latest_Ev.Count()==5)
            {
                FirstRow.Text = Latest_Ev[0].ToString();
                SecondRow.Text = Latest_Ev[1].ToString();
                ThirdRow.Text = Latest_Ev[2].ToString();
                FourthRow.Text = Latest_Ev[3].ToString();
                
                //FifthRow.Text = Latest_Ev[4].ToString();
            }else
            {
                FirstRow.Text = Latest_Ev[0].ToString();
                SecondRow.Text = Latest_Ev[1].ToString();
                ThirdRow.Text = Latest_Ev[2].ToString();
                FourthRow.Text = "...";
               // FifthRow.Text = "...";
            }
            home_choose_event.ItemsSource = Latest_Ev;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.MainOption.Text == "[Human Resources]")
            {
                this.MainOption.Text = "[Event Calendar]";
                this.MainImage.Source = new BitmapImage(new Uri(@"/images/calendar.png", UriKind.Relative));
            }
            else if (this.MainOption.Text == "[Event Calendar]")
            {
                this.MainOption.Text = "[Human Resources]";
                this.MainImage.Source = new BitmapImage(new Uri(@"/images/deal.png", UriKind.Relative));
            }

        }

        private void Button_Click_Go(object sender, RoutedEventArgs e)
        {
            if (this.MainOption.Text == "[Human Resources]")
            {
                //fereastra resurse umane
                human_resources window = new human_resources();
                window.Owner = this;
                window.Show();
            }
            else if (this.MainOption.Text == "[Event Calendar]")
            {
                //fereastra calendar 
                calendar_admin window = new calendar_admin();
                window.Owner = this;
                window.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource evenimenteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("evenimenteViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // evenimenteViewSource.Source = [generic data source]
        }

        private void Button_Click_See_Ev_Page(object sender, RoutedEventArgs e)
        {
            EventWindow window = new EventWindow(home_choose_event.SelectedItem.ToString());
           //indow.evname= ;
            window.Owner = this;
            window.Show();
            //window.EvName.Text = 
            //window.FillContents();
            //window.Print_To_Do();
        }
    }
}
