﻿using System;

namespace ECMSS.Web.Models
{
    public class FileInfoViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Modifier { get; set; }
        public int Size { get; set; }
        public int DirectoryId { get; set; }
        public string SecurityLevel { get; set; }
        public string Version { get; set; }
        public string Tag { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsImportant { get; set; }
    }
}