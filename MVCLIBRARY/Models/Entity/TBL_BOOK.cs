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
    
    public partial class TBL_BOOK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_BOOK()
        {
            this.TBL_DEPOSIT = new HashSet<TBL_DEPOSIT>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> CATEGORYID { get; set; }
        public int AUTHORID { get; set; }
        public Nullable<int> LIBRARYID { get; set; }
        public Nullable<int> PUBLISHERID { get; set; }
        public Nullable<int> PAGE { get; set; }
        public string PRINTYEAR { get; set; }
        public Nullable<int> LANGUAGEID { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public string PICTURE { get; set; }
    
        public virtual TBL_AUTHOR TBL_AUTHOR { get; set; }
        public virtual TBL_CATEGORY TBL_CATEGORY { get; set; }
        public virtual TBL_LANGUAGE TBL_LANGUAGE { get; set; }
        public virtual TBL_LIBRARY TBL_LIBRARY { get; set; }
        public virtual TBL_PUBLISHER TBL_PUBLISHER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_DEPOSIT> TBL_DEPOSIT { get; set; }
    }
}
