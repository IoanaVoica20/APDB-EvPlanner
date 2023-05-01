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
    /// Interaction logic for calendar_admin.xaml
    /// </summary>
    public partial class calendar_admin : Window
    {
        private string _selectedEvent;
        private int _idEvent;
        public calendar_admin()
        {
            InitializeComponent();
            //BlackoutEventsDates();
           
        }

        private void BlackoutEventsDates()
        {
            var ctx = new EvPlannerEntities();
            var events = (from e in ctx.Evenimentes
                          select new
                          {
                              e.Data_Start,
                              e.Data_Sfarsit,
                              e.Denumire
                          });

            foreach(var e in events)
            {
                if(e.Data_Sfarsit.Subtract(e.Data_Start).TotalDays==0)
                {
                    Calendar.BlackoutDates.Add(new CalendarDateRange(e.Data_Start));
                }else
                {
                    for (int i= 0; i < e.Data_Sfarsit.Subtract(e.Data_Start).TotalDays; i++)
                    {
                        Calendar.BlackoutDates.Add(new CalendarDateRange(e.Data_Start, e.Data_Sfarsit));

                    }
                    
                }
                
            }


        }

        private void Get_Selected_Event()
        {
            var ctx = new EvPlannerEntities();
            var events = (from e in ctx.Evenimentes
                          where (e.Data_Start >= Calendar.SelectedDate )&&( Calendar.SelectedDate<= e.Data_Sfarsit) // ((Calendar.SelectedDate && e.Data_Start)<e.Data_Sfarsit)
                          select new
                          {
                              e.Denumire,
                              e.ID_Eveniment
                          }).FirstOrDefault();

            _selectedEvent = events.Denumire;
            _idEvent = events.ID_Eveniment;
                
        }

        private void Print_Events()
        {
            var ctx = new EvPlannerEntities();


            var events = (from e in ctx.Evenimentes
                               // where t.ID_eveniment == ev.ID_Eveniment
                           select e);
            DateTime selected = Calendar.SelectedDate.Value;
            //List<object> data_src = new List<object>(data);
            var data = events.Where(x => x.Data_Start <= selected && x.Data_Sfarsit>=selected) ;
            if(data.Count()==0)
            {
                return;
            }
            foreach (Evenimente e in data)
            {
                //Border box = new Border();
                //box.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                //box.BorderThickness = new Thickness(1.5);
                

                Border bev = new Border();
                bev.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                bev.BorderThickness = new Thickness(1.5);

                Border bloc = new Border();
                bloc.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                bloc.BorderThickness = new Thickness(1.5);

                Border badd = new Border();
                badd.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                badd.BorderThickness = new Thickness(1.5);

                Border bst = new Border();
                bst.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                bst.BorderThickness = new Thickness(1.5);

                //TextBlock tb_p = new TextBlock();
                CheckBox chk = new CheckBox();
                //chk.BorderBrush= new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                //chk.BorderThickness = new Thickness(1.5);

                TextBlock tb_ev = new TextBlock();
                TextBlock tb_loc = new TextBlock();
                TextBlock tb_add = new TextBlock();
                TextBlock tb_bst = new TextBlock();

                //bp.Child = tb_p;
                // box.Child = chk;
                
                
                bev.Child = tb_ev;
                bloc.Child = tb_loc;
                badd.Child = tb_add;
                bst.Child = tb_bst;

                //tb_p.Background = new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 0.8 };
                tb_ev.Background = new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 1 };
                tb_loc.Background = new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 1 };
                tb_add.Background = new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 1 };
                tb_bst.Background= new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 1 };
                //StaticResourceExtension staticResource;

                chk.Margin = new Thickness(15);
                chk.VerticalAlignment = VerticalAlignment.Center;
                //chk.Padding = new Thickness(5, 10, 5, 5);

                tb_ev.Text = e.Denumire;
                tb_ev.TextAlignment = TextAlignment.Center;
                tb_ev.Padding = new Thickness(5, 10, 5, 5);

                tb_loc.Text = e.Locatii.Denumire;
                tb_loc.TextAlignment = TextAlignment.Center;
                tb_loc.Padding = new Thickness(5, 10, 5, 5);

                tb_add.Text = e.Locatii.Adresa;
                tb_add.TextAlignment = TextAlignment.Center;
                tb_add.Padding = new Thickness(5, 10, 5, 5);

                tb_bst.Text = e.Status_Ev.Denumire;
                tb_bst.TextAlignment = TextAlignment.Center;
                tb_bst.Padding = new Thickness(5, 10, 5, 5);

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(40);
                calendarEvGrid.RowDefinitions.Add(r);

               // Grid.SetColumn(box, 1);
               // Grid.SetRow(box, calendarEvGrid.RowDefinitions.Count() - 1);
                Grid.SetColumn(bev, 1);
                Grid.SetRow(bev, calendarEvGrid.RowDefinitions.Count() - 1);
                Grid.SetColumn(bloc, 2);
                Grid.SetRow(bloc, calendarEvGrid.RowDefinitions.Count() - 1);
                Grid.SetColumn(badd, 3);
                Grid.SetRow(badd, calendarEvGrid.RowDefinitions.Count() - 1);
                Grid.SetColumn(bst, 4);
                Grid.SetRow(bst, calendarEvGrid.RowDefinitions.Count() - 1);

                stackBox.Children.Add(chk);
                calendarEvGrid.Children.Add(bev);
                calendarEvGrid.Children.Add(bloc);
                calendarEvGrid.Children.Add(badd);
                calendarEvGrid.Children.Add(bst);
                //taskGrid.Children.Add(bp);

            }
        }
        private void Button_Click_See_Event(object sender, RoutedEventArgs e)
        {
            Get_Selected_Event();
            EventWindow window = new EventWindow(_selectedEvent);
            window.Owner = this;
            window.ShowDialog();
        }
        private void Fill_About()
        {
            txtEventsOnData.Text = "You have these events on "+ Calendar.SelectedDate.ToString()+":";
        }
        private void Button_Click_Cancel_Event(object sender, RoutedEventArgs e)
        {

        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            calendarEvGrid.RowDefinitions.Clear();
            Print_Events();
        }
    }
}
