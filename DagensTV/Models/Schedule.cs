//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DagensTV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Schedule()
        {
            this.PopularContent = new HashSet<PopularContent>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<int> Duration { get; set; }
        public string Resume { get; set; }
        public Nullable<int> ChannelId { get; set; }
        public Nullable<int> ShowId { get; set; }
    
        public virtual Channel Channel { get; set; }
        public virtual Show Show { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PopularContent> PopularContent { get; set; }
    }
}
