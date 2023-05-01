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
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        public string evname;
        public EventWindow(string ev)//sa bag string
        {
            evname = ev;
            InitializeComponent(); //?
            var fn = @"D:\FAC_C\ANUL_3\SEM_1\06_Ap. Baze de Date\Proiect\PlanIVent\images\to-do-list.png";
            to_do_brush.ImageSource = new BitmapImage(new Uri(fn, UriKind.RelativeOrAbsolute));
            FillContents();
            Print_To_Do();
        }

        public void FillContents()
        {
            var ctx = new EvPlannerEntities();
            var data = (from e in ctx.Evenimentes
                        where e.Denumire == evname
                        select e).FirstOrDefault();
            int nr = data.NrPersoane.Value;
            EvName.Text += evname;
            EvName.Text += "\n"+nr.ToString()+ " persoane";
            EvName.Text += "\nstart:"+ data.Data_Start.ToString();
            EvName.Text += "\nfinal:" + data.Data_Sfarsit.ToString();

            var locatie = (from l in ctx.Locatiis
                           where l.ID_Locatie == data.ID_Locatie
                           select l).FirstOrDefault();
            EvName.Text += "\n"+ locatie.Denumire + ", " + locatie.Judet;

        }

       

        public void AddTaskWindow()
        {
            Window add_task = new Window();
            add_task.Owner = this;
            add_task.Show();
            //this.Hide();
        }
        public void Print_To_Do()
        {
            var ctx = new EvPlannerEntities();

            int i = 0;
            while(EvName.Text[i]!='\n')
            {
                i++;
            }
            string nume_ev = EvName.Text.Substring(0,i);


            var ev = (from e in ctx.Evenimentes
                      where e.Denumire == nume_ev
                      select e).FirstOrDefault();

            var sarcini = (from t in ctx.Sarcinis
                       // where t.ID_eveniment == ev.ID_Eveniment
                        select t);
            //List<object> data_src = new List<object>(data);
            var data = sarcini.Where(x => x.ID_eveniment == ev.ID_Eveniment);

            foreach (Sarcini d in data)
            {
                var pers = (from p in ctx.Personals
                            where p.ID_persoana == d.ID_Personal
                            select p).FirstOrDefault();

                Border bp = new Border();
                bp.BorderBrush= new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                bp.BorderThickness = new Thickness(1.5);

                Border bs = new Border();
                bs.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                bs.BorderThickness = new Thickness(1.5);

                Border bst = new Border();
                bst.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                bst.BorderThickness = new Thickness(1.5);
                
                TextBlock tb_p = new TextBlock();
                TextBlock tb_s = new TextBlock();
                TextBlock tb_st = new TextBlock();

                bp.Child = tb_p;
                bs.Child = tb_s;
                bst.Child = tb_st;

                tb_p.Background = new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity=0.8};
                tb_s.Background = new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 0.8 };
                tb_st.Background= new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 0.8 };
                //StaticResourceExtension staticResource;

                tb_s.Text = d.Denumire;
                tb_s.TextAlignment = TextAlignment.Center;
                tb_s.Padding = new Thickness(5, 10, 5, 5);

                tb_p.Text = pers.Nume + " " + pers.Prenume;
                tb_p.TextAlignment = TextAlignment.Center;
                tb_p.Padding = new Thickness(5,10, 5, 5);

                
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(40);
                taskGrid.RowDefinitions.Add(r);
                
                Grid.SetColumn(bs, 0);
                Grid.SetRow(bs, taskGrid.RowDefinitions.Count()-1);
                Grid.SetColumn(bp, 1);
                Grid.SetRow(bp, taskGrid.RowDefinitions.Count()-1);

                taskGrid.Children.Add(bs);
                taskGrid.Children.Add(bp);

            }

        }

        private void Refresh_To_Do()
        {
            taskGrid.RowDefinitions.Clear();
            Print_To_Do();
        }
        private void Button_Click_Remove_Task(object sender, RoutedEventArgs e)
        {
            //fereastra noua cu combobox cu item-urile si sterg din baza item ul selectat, apoi din combo box apoi refresh la acest window!!!
        }

        private void Button_Click_Add_Task(object sender, RoutedEventArgs e)
        {
            //task
            //event
            //person delegated
            Add_Task window = new Add_Task();
            window.Owner = this;
            window.ShowDialog();
            //this.Hide();
            Refresh_To_Do();
            
        }
    }

   
}
