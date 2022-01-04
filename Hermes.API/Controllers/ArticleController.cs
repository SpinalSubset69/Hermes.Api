using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Services;
using Hermes.API.Util;
using Hermes.API.Util.Exceptions;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Articles;
using Hermes.Core.Interfaces.Specifications.Reporters;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Controllers
{
    public class ArticleController : BaseController
    {
        public readonly ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {           
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll([FromQuery] ArticleSpecParams articleParams)
        {
            try
            {
                var articles = await _articleService.GetArticles(articleParams);
                return ResponseWithPagination<ArticleToReturnDto>.HttpResponseWithPagination( articles);
            }catch(Exception ex)
            {
                string message = (ex is NotFoundException) ? "No se encontraron artículos" : "Error on server";
                return HttpHandleErrors(message, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetById(int id)
        {
            try
            {
                //Get Article with reporter, category an images
                var articleToReturn = await _articleService.GetArticleBasedOnId(id);
                return ResponseWithData<ArticleToReturnDto>.HttpResponseWithData("Article", articleToReturn);
            }
            catch (Exception ex)
            {
                return HttpHandleErrors("Server Error", ex);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> PostArticle([FromBody] RegisterArticle articleFromBody)
        {
            try
            {                
                var articleToReturn = await _articleService.AddArticle(articleFromBody);
                return ResponseWithData<ArticleToReturnDto>.HttpResponseWithData("Article Saved", articleToReturn);
            }
            catch(Exception ex)
            {
                return HttpHandleErrors("Error On Server", ex);
            }
        }

        [Authorize]
        [HttpPost("uplodaimages/{id}")]
        public async Task<JsonResult> PostArticleImages(int id, [FromBody] InputImageRequest[] files)
        {
            try
            {
                if (files.Length != 0)
                {
                    var result = await _articleService.UploadArticleImagesBasedOnArticleId(id, files);
                    return new JsonResult(new
                    {
                        message = result
                    });
                }
                return new JsonResult(new { message = "Must Provide Images" });
            }
            catch (Exception ex)
            {
                return HttpHandleErrors("Error On Server", ex);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteArticle(int id)
        {
            try
            {
                await _articleService.RemoveArticle(id);

                return new JsonResult(new { message = "Article Removed"});
            }catch(Exception ex)
            {
                return HttpHandleErrors("Error on server", ex);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<JsonResult> UpdateArticle(int id, [FromBody]RegisterArticle articleToUpdate)
        {
            try
            {
                var articleUpdated = await _articleService.UpdateArticle(id, articleToUpdate);

                return ResponseWithData<ArticleToReturnDto>.HttpResponseWithData("ArticleUpdated", articleUpdated);
            }
            catch (Exception ex)
            {
                return HttpHandleErrors("Error on server", ex);
            }
        }
    }
}
