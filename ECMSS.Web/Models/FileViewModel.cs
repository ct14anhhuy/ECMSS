﻿namespace ECMSS.Web.Models
{
    public class FileViewModel
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string FileName { get; set; }
        public string Version { get; set; }
        public byte[] FileData { get; set; }
    }
}