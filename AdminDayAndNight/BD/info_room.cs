//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminDayAndNight.BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class info_room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public info_room()
        {
            this.borrow_room = new HashSet<borrow_room>();
        }
    
        public int num_room { get; set; }
        public int count_room { get; set; }
        public int capacity { get; set; }
        public int type_room { get; set; }
        public string chort_description { get; set; }
        public decimal price { get; set; }
        public int status_room { get; set; }
    
        public virtual status_room status_room1 { get; set; }
        public virtual type_room type_room1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<borrow_room> borrow_room { get; set; }
    }
}
