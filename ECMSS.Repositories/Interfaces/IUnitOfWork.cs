﻿using ECMSS.Data;

namespace ECMSS.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Department> DepartmentRepository { get; }
        IGenericRepository<Directory> DirectoryRepository { get; }
        IGenericRepository<Employee> EmployeeRepository { get; }
        IGenericRepository<FilePermission> FilePermissionRepository { get; }
        IGenericRepository<FileFavorite> FileFavoriteRepository { get; }
        IGenericRepository<FileImportant> FileImportantRepository { get; }
        IGenericRepository<FileHistory> FileHistoryRepository { get; }
        IGenericRepository<FileInfo> FileInfoRepository { get; }
        IGenericRepository<FileShare> FileShareRepository { get; }
        IGenericRepository<FileStatus> FileStatusRepository { get; }
        IGenericRepository<Trash> TrashRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }

        void Commit();
    }
}