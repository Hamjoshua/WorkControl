//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkControl
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeProfiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeProfiles()
        {
            this.WorkWeeks = new HashSet<WorkWeeks>();
            this.WorkWeeks1 = new HashSet<WorkWeeks>();
            this.WorkWeeks2 = new HashSet<WorkWeeks>();
            this.WorkWeeks3 = new HashSet<WorkWeeks>();
            this.WorkWeeks4 = new HashSet<WorkWeeks>();
            this.WorkWeeks5 = new HashSet<WorkWeeks>();
            this.WorkWeeks6 = new HashSet<WorkWeeks>();
        }
    
        public int TimeProfileId { get; set; }
        public string NameOfProfile { get; set; }
        public Nullable<System.TimeSpan> LanchTime { get; set; }
        public Nullable<System.TimeSpan> StartWorkTime { get; set; }
        public Nullable<System.TimeSpan> EndWorkTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkWeeks> WorkWeeks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkWeeks> WorkWeeks1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkWeeks> WorkWeeks2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkWeeks> WorkWeeks3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkWeeks> WorkWeeks4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkWeeks> WorkWeeks5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkWeeks> WorkWeeks6 { get; set; }
    }
}
