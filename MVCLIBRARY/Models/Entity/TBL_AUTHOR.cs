//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCLIBRARY.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_AUTHOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_AUTHOR()
        {
            this.TBL_BOOK = new HashSet<TBL_BOOK>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string DETAIL { get; set; }
        public Nullable<bool> STATUS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_BOOK> TBL_BOOK { get; set; }
    }
}
