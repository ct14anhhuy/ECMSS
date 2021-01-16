﻿using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECMSS.Services
{
    public class FileInfoService : IFileInfoService
    {
        private readonly IGenericRepository<FileInfo> _fileInfoRepository;
        private readonly IGenericRepository<FileHistory> _fileHistoryRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDirectoryService _directoryService;
        private readonly IMapper _mapper;

        public FileInfoService(IUnitOfWork unitOfWork, IMapper mapper, IDirectoryService directoryService)
        {
            _unitOfWork = unitOfWork;
            _fileInfoRepository = _unitOfWork.FileInfoRepository;
            _fileHistoryRepository = _unitOfWork.FileHistoryRepository;
            _employeeRepository = _unitOfWork.EmployeeRepository;
            _directoryService = directoryService;
            _mapper = mapper;
        }

        public IEnumerable<FileInfoDTO> GetFileInfos()
        {
            var result = _fileInfoRepository.GetAll(x => x.Employee, x => x.FileHistories, x => x.FileHistories.Select(h => h.Employee));
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetFileInfosByDirId(int dirId)
        {
            var result = _fileInfoRepository.GetMany(x => x.DirectoryId == dirId, x => x.Employee, x => x.FileHistories, x => x.FileHistories.Select(h => h.Employee));
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public string[] GetFileUrl(int id)
        {
            string[] result = new string[2];
            var fileInfo = _fileInfoRepository.GetSingle(x => x.Id == id, x => x.FileHistories);
            string filePath = ConfigHelper.Read("FileUploadPath");
            filePath += $"{_directoryService.GetPathFromFileId(id)}/{fileInfo.Name}";
            string version = fileInfo.FileHistories.OrderByDescending(x => x.Id).FirstOrDefault().Version;
            result[0] = $"ECMProtocol: <Download>[{fileInfo.Id}][{filePath}][{fileInfo.Owner}][{version}]";
            result[1] = fileInfo.Name;
            return result;
        }

        public void UploadNewFile(FileInfoDTO fileInfo)
        {
            try
            {
                fileInfo.Owner = _employeeRepository.GetSingle(x => x.EpLiteId == fileInfo.OwnerUser).Id;
                string filePath = ConfigHelper.Read("FileUploadPath");
                filePath += $"{_directoryService.GetPathFromDirId(fileInfo.DirectoryId)}/{fileInfo.Name}";
                FileHelper.SaveFile(filePath, fileInfo.FileData);
                _fileInfoRepository.Add(_mapper.Map<FileInfo>(fileInfo));

                FileHistoryDTO fileHistory = new FileHistoryDTO
                {
                    FileId = fileInfo.Id,
                    Modifier = fileInfo.Owner,
                    Size = fileInfo.FileData.Length / 1024,
                    StatusId = 1,
                    Version = "0.1"
                };
                _fileHistoryRepository.Add(_mapper.Map<FileHistory>(fileHistory));

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}