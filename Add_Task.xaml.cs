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
    /// Interaction logic for Add_Task.xaml
    /// </summary>
    public partial class Add_Task : Window
    {
        public Add_Task()
        {
            InitializeComponent();
            //DataContext = new Data();
            DataContext = this;
            FillEvents();
            FillPersonal();
            
        }

        //public class Data
        //{
        //    public List<string> IList { get; set; }
        //    public Data()
        //    {
        //        IList = new List<string>();
        //    }
        //}

        private List<int> selectedEvID=new List<int>();
        private List<int> selectedPersID=new List<int>();

        public List<string> Eventeslist=new List<string>();
        public List<string> Names= new List<string>();
        public void FillEvents()
        {
            var ctx = new EvPlannerEntities();
            var events = (from e in ctx.Evenimentes
                          select new
                          {
                              e.ID_Eveniment,
                              e.Denumire
                          });

            
            foreach(var e in events)
            {
                Eventeslist.Add(e.Denumire);
                selectedEvID.Add(e.ID_Eveniment);
                cmbEvent.Items.Add(Eventeslist[Eventeslist.Count()-1]);
                //evID.Add(e.ID_Eveniment);
            }
            //cmbEvent.ItemsSource = Eventeslist;
           // DataContext = this;
           // Data items = new Data(eventeslist);
            //DataBind();
            //cmbEvent.
        }

        public void FillPersonal()
        {
            var ctx = new EvPlannerEntities();
            var personal = (from p in ctx.Personals
                            select new{ 
                p.ID_persoana,
                p.Nume,
                p.Prenume
                    });
            int no = personal.Count();

            

            foreach (var p in personal)
            {
                Names.Add (p.Nume + " " + p.Prenume);
                selectedPersID.Add(p.ID_persoana);
                cmbPerson.Items.Add(Names[Names.Count()-1]);
            }
            //cmbPerson.ItemsSource =Names;
            
        }
        private void Add_Task_DB()
        {
            var eventdb = cmbEvent.SelectedItem.ToString();
            var persondb = cmbPerson.SelectedItem.ToString();
            var taskdb = txtTask.Text;

            var ctx = new EvPlannerEntities();

            var ide = selectedEvID[cmbEvent.SelectedIndex];
            var idp = selectedPersID[cmbPerson.SelectedIndex];
            //var idp = (from p in ctx.Personals
            //           where );


            //TO DO: event handler pt cand se schimba valoarea din combobox


            var newTask = new Sarcini()
            {
                Denumire = taskdb,
                //pers
                ID_Personal = idp,
                ID_eveniment=ide
            };

            ctx.Sarcinis.Add(newTask);
            ctx.SaveChanges();
           // this.Owner.Show();
        }

        private void cmbEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBoxItem evitem = (ComboBoxItem)cmbEvent.SelectedItem;
            //string 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Task_DB();
            MessageBox.Show($"Successfully added task for {0}", cmbPerson.SelectedItem.ToString());
            this.Hide();
            
            
            
        }

        public void SetPerson(string person)
        {
            foreach(var i in cmbPerson.Items)
            {
                if (i.ToString()==person)
                {
                    cmbPerson.SelectedItem = i;
                    break;
                }
            }
        
        }
        //private void txtTask_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    txtTask.Focus();
        //}

        private void txtTask_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTask.Text) && txtTask.Text.Length > 0)
            {
                txtTask.Visibility = Visibility.Visible;
            }
        }
    }
}
