//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SE_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class userJob
    {
         public int UserJobID { get; set; }
        public int UserID { get; set; }
        public int JobID { get; set; }
    
        public virtual job job { get; set; }
        public virtual user user { get; set; }
    }
}
