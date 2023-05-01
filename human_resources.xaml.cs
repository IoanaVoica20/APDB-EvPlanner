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
    /// Interaction logic for human_resources.xaml
    /// </summary>
    public partial class human_resources : Window
    {
        public human_resources()
        {
            InitializeComponent();
            Fill_Departments();
            //Fill_Persons();
        }

        private void Fill_Departments()
        {
            var ctx = new EvPlannerEntities();
            var dep = (from d in ctx.Departaments
                        select new
                        {
                            d.Denumire
                        });

            List<string> departaments = new List<string>();
            //departaments = dep.ToList();

            foreach (var d in dep)
            {
                departaments.Add(d.Denumire);
                cmbDepartment.Items.Add(departaments[departaments.Count()-1]);
            }
            //int nr = data.NrPersoane.Value;


        }

        private void Fill_Persons()
        {
            var ctx = new EvPlannerEntities();
            var pers = (from p in ctx.Personals
                        where p.ID_departament == (cmbDepartment.SelectedIndex+1)
                        select new
                        {
                            p.Nume,
                            p.Prenume
                        }); 
            List<string> persons = new List<string>();
            foreach (var p in pers)
            {
                persons.Add(p.Nume+" "+p.Prenume);
                cmbPerson.Items.Add(persons[persons.Count()-1]);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Personal_page window = new Personal_page(cmbPerson.SelectedItem.ToString());
            window.Owner = this;
            window.ShowDialog();


        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbPerson.Items.Clear();
            Fill_Persons();
        }
    }
}
