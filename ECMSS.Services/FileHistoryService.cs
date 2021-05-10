﻿using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using System;

namespace ECMSS.Services
{
    public class FileHistoryService : IFileHistoryService
    {
        private readonly IGenericRepository<FileHistory> _fileHistoryRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IDirectoryService _directoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileHistoryService(IUnitOfWork unitOfWork, IMapper mapper, IDirectoryService directoryService)
        {
            _unitOfWork = unitOfWork;
            _fileHistoryRepository = _unitOfWork.FileHistoryRepository;
            _employeeRepository = _unitOfWork.EmployeeRepository;
            _directoryService = directoryService;
            _mapper = mapper;
        }

        public void UploadFile(FileHistoryDTO fileHistory)
        {
            try
            {
                fileHistory.Modifier = _employeeRepository.GetSingle(x => x.EpLiteId == fileHistory.ModifierUser).Id;
                string filePath = ConfigHelper.Read("FileUploadPath");
                filePath += $"{_directoryService.GetDirFromFileId(fileHistory.FileId).Name}/{fileHistory.FileName}";
                fileHistory.Size = fileHistory.FileData.Length / 1024;
                _fileHistoryRepository.Add(_mapper.Map<FileHistory>(fileHistory));
                FileHelper.SaveFile(filePath, fileHistory.FileData, true);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}