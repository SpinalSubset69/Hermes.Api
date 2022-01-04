using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Services;
using Hermes.API.Util;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Articles;
using Hermes.Core.Interfaces.Specifications.Reporters;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Controllers
{
    public class ReporterController : BaseController
    {
        private readonly ReporterService _reporterService;

        public ReporterController(ReporterService reporterService)
        {            
            _reporterService = reporterService;
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetById(int id)
        {
            try
            {
                var reporter = await _reporterService.GetReporterByIdAsync(id);

                return ResponseWithData<ReporterToReturnDto>.HttpResponseWithData("Reporter", reporter);
            }catch(Exception ex)
            {
                return HttpHandleErrors("Error on server", ex);
            }            
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> PostReporter([FromBody] RegisterReporter reporterFromBody)
        {
            try
            {
                var reporterToReturn = await _reporterService.AddReporter(reporterFromBody);
                return ResponseWithData<ReporterToReturnDto>.HttpResponseWithData("ReporterSaved", reporterToReturn);
            }
            catch(Exception ex)
            {
                return HttpHandleErrors("Error", ex);
            }
        }

        [Authorize]
        [HttpPost("fileupload/{id}")]        
        public async Task<JsonResult> PostReporterUpload(int id, [FromBody] InputImageRequest files)
        {
            try
            {
                    var reporterToReturn = await _reporterService.AddReporterImageBasedOnId(id, files);
                    return ResponseWithData<ReporterToReturnDto>.HttpResponseWithData("Image Saved On DB", reporterToReturn);
                

                HttpContext.Response.StatusCode = StatusCodes.Status304NotModified;
                return new JsonResult(new
                {
                    message = "An error has ocurred"                    
                });
            }
            catch(Exception ex)
            {
                return new JsonResult(new
                {
                    message = "An error has ocurred",
                    errorMessage = ex.Message.ToString()
                });
            }
        }

        [HttpGet("articles")]
        public async Task<JsonResult> GetReporterArticles([FromQuery]ArticleSpecParams articleParams)
        {
            try
            {
                var pagination = await _reporterService.GetReporterArticles(articleParams);
                return ResponseWithPagination<ArticleToReturnWithoutReporterDto>.HttpResponseWithPagination(pagination);
            }catch(Exception ex)
            {
                return HttpHandleErrors("Error on server", ex);
            }
        }
    }
}
