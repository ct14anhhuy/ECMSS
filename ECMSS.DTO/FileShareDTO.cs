﻿using System;

namespace ECMSS.DTO
{
    public class FileShareDTO
    {
        public int FileId { get; set; }
        public int EmployeeId { get; set; }
        public int Authority { get; set; }
        public Guid Id { get; set; }

        public virtual EmployeeDTO Employee { get; set; }
        public virtual FileAuthorityDTO FileAuthority { get; set; }
        public virtual FileInfoDTO FileInfo { get; set; }
        public virtual FilePermissionDTO FilePermission { get; set; }
    }
}