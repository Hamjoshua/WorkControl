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
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Stats = new HashSet<Stats>();
        }
    
        public int UserId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> WorkWeekId { get; set; }
        public string SecondName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string WorkPost { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
    
        public virtual Roles Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stats> Stats { get; set; }
        public virtual WorkWeeks WorkWeeks { get; set; }
    }
}
