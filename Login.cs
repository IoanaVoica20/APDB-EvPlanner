//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlanIVent
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Login
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Login()
        {
            this.Clientis = new ObservableCollection<Clienti>();
        }
    
        public int ID_cont { get; set; }
        public string Nume { get; set; }
        public string Parola { get; set; }
        public int Conectat { get; set; }
        public string Privilegiu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Clienti> Clientis { get; set; }
    }
}