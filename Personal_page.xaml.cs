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
    /// Interaction logic for Personal_page.xaml
    /// </summary>
    public partial class Personal_page : Window
    {
        private string _person;
        private int _id;
        public Personal_page(string person)
        {
            _person = person;
            InitializeComponent();
            Fill_About();
            Print_Personal_Tasks();
        }

        private void Fill_About()
        {
            var ctx = new EvPlannerEntities();
            var pers = (from p in ctx.Personals
                        where p.Nume+" "+p.Prenume == _person
                        select new
                        {
                            p.Nume,
                            p.Prenume,
                            p.Adresa,
                            p.Categorii_Personal.Denumire,
                            p.Telefon,
                            p.ID_departament,
                            p.ID_persoana
                        }).FirstOrDefault();
            //
            _id = pers.ID_persoana;
            var dep = (from d in ctx.Departaments
                       where d.ID_departament == pers.ID_departament
                       select d).FirstOrDefault();

            PInfo.Text = _person + "\nCategorie: " + pers.Denumire +"\nDepartament: "+ dep.Denumire + "\nAdresa: " + pers.Adresa + "\nTelefon: " + pers.Telefon;
        }

        private void Print_Personal_Tasks()
        {
            var ctx = new EvPlannerEntities();


            var sarcini = (from t in ctx.Sarcinis
                               // where t.ID_eveniment == ev.ID_Eveniment
                           select t);
            //List<object> data_src = new List<object>(data);
            var data = sarcini.Where(x => x.ID_Personal == _id);

            foreach (Sarcini d in data)
            {

                //Border bp = new Border();
                //bp.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                //bp.BorderThickness = new Thickness(1.5);

                Border be = new Border();
                be.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                be.BorderThickness = new Thickness(1.5);

                Border bs = new Border();
                bs.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                bs.BorderThickness = new Thickness(1.5);

                Border bst = new Border();
                bst.BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                bst.BorderThickness = new Thickness(1.5);

                //TextBlock tb_p = new TextBlock();
                CheckBox chk = new CheckBox();
                TextBlock tb_e = new TextBlock();
                TextBlock tb_s = new TextBlock();
                TextBlock tb_st = new TextBlock();

                //bp.Child = tb_p;
                be.Child = tb_e;
                bs.Child = tb_s;
                bst.Child = chk;


                chk.Margin = new Thickness(15);
                chk.VerticalAlignment = VerticalAlignment.Center;
                chk.BorderBrush= new SolidColorBrush(Color.FromRgb(0x23, 0x11, 0x23)) { Opacity = 0.8 };
                chk.BorderThickness = new Thickness(1.5);

                //tb_p.Background = new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 0.8 };
                tb_e.Background= new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 0.8 };
                tb_s.Background = new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 0.8 };
                tb_st.Background = new SolidColorBrush(Color.FromRgb(0xFB, 0xD1, 0xA2)) { Opacity = 0.8 };
                //StaticResourceExtension staticResource;

                tb_s.Text = d.Denumire;
                tb_s.TextAlignment = TextAlignment.Center;
                tb_s.Padding = new Thickness(5, 10, 5, 5);

                tb_e.Text = d.Evenimente.Denumire;
                tb_e.TextAlignment = TextAlignment.Center;
                tb_e.Padding = new Thickness(5, 10, 5, 5);
                //tb_p.Text = pers.Nume + " " + pers.Prenume;
                //tb_p.TextAlignment = TextAlignment.Center;
                //tb_p.Padding = new Thickness(5, 10, 5, 5);

                //tb_st.Text=

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(40);
                personTaskGrid.RowDefinitions.Add(r);

                Grid.SetColumn(bs, 0);
                Grid.SetRow(bs, personTaskGrid.RowDefinitions.Count() - 1);
                Grid.SetColumn(be, 1);
                Grid.SetRow(be, personTaskGrid.RowDefinitions.Count() - 1);
                Grid.SetColumn(bst, 2);
                Grid.SetRow(bst, personTaskGrid.RowDefinitions.Count() - 1);

                
                personTaskGrid.Children.Add(bs);
                personTaskGrid.Children.Add(be);
                stackPersBox.Children.Add(bst);
                //taskGrid.Children.Add(bp);

            }
        }
        
        private void Refresh_Personal_Tasks()
        {
            personTaskGrid.RowDefinitions.Clear();
            Print_Personal_Tasks();
        }
        private void Button_Click_Add_Task(object sender, RoutedEventArgs e)
        {
            Add_Task window = new Add_Task();
            window.Owner = this;
            window.SetPerson(_person);
            window.ShowDialog();
            Refresh_Personal_Tasks();

        }

        private void Button_Click_Remove_Task(object sender, RoutedEventArgs e)
        {

        }
    }
}
