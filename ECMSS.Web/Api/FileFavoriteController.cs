﻿using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class FileFavoriteController : ApiController
    {
        private readonly IFileFavoriteService _fileFavoriteService;

        public FileFavoriteController(IFileFavoriteService fileFavoriteService)
        {
            _fileFavoriteService = fileFavoriteService;
        }

        [HttpPost]
        public HttpResponseMessage AddOrRemoveFavoriteFile(int fileId)
        {
            try
            {
                var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
                _fileFavoriteService.AddOrRemoveFavoriteFile(fileId, empId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}