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
    
    public partial class booking_history
    {
        public int id { get; set; }
        public int borrow_room { get; set; }
        public System.DateTime date_departure { get; set; }
    
        public virtual borrow_room borrow_room1 { get; set; }
    }
}
