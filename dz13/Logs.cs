//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dz13
{
    using System;
    using System.Collections.Generic;
    
    public partial class Logs
    {
        public int idLog { get; set; }
        public string idClient { get; set; }
        public System.DateTime time { get; set; }
        public string idReciever { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<bool> direction { get; set; }
        public string message { get; set; }
    }
}