//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECMSS.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class FileHistory
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int StatusId { get; set; }
        public int Size { get; set; }
        public string Version { get; set; }
        public int Modifier { get; set; }
        public Nullable<System.Guid> FileId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual FileInfo FileInfo { get; set; }
        public virtual FileStatus FileStatus { get; set; }
    }
}
