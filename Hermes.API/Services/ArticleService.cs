using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Util;
using Hermes.API.Util.Exceptions;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Articles;

namespace Hermes.API.Services
{
    public class ArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public static IWebHostEnvironment _webHostEnvironment;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Pagination<ArticleToReturnDto>> GetArticles(ArticleSpecParams articleParams)
        {
            var spec = new ArticleSpecification(articleParams);
            var countSpec = new ArticleForCountSpecification(articleParams);
            var articles = await _unitOfWork.Articles.ListAllAsync(spec);
            var count = await _unitOfWork.Articles.GetCountWithSpecifications(countSpec);
            if(count == 0)
            {
                throw new NotFoundException("Articles Not Found");
            }
            var articlesToReturn = _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleToReturnDto>>(articles);
            return new Pagination<ArticleToReturnDto>(count, articleParams.PageSize, articleParams.PageIndex, articlesToReturn);
        }

        public async Task<ArticleToReturnDto> AddArticle(RegisterArticle articleFromBody)
        {
            var article = _mapper.Map<RegisterArticle, Article>(articleFromBody);
            _unitOfWork.Articles.SaveEntity(article);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Article, ArticleToReturnDto>(article);
        }

        //public async Task<string> UploadArticleImagesBasedOnArticleId(int id, List<IFormFile> files)
        //{
        //    var spec = new ArticleWithIncludesByIdSpecification(id);
        //    var article = await _unitOfWork.Articles.FindByIdAsync(spec);
         
        //    if(article != null)
        //    {
        //        article.Images = new List<Image>();
        //        foreach(var file in files)
        //        {
        //            var image = new Image();
        //            image.Name =  FileHandler.SaveFileOnDirectory(_webHostEnvironment.WebRootPath, "Articles", file);
        //            image.ArticleId = article.Id;
        //            article.Images.Add(image);
        //            _unitOfWork.Images.SaveEntity(image);
        //            _unitOfWork.Articles.UpdateEntity(article);
        //        }
        //        await _unitOfWork.SaveChangesAsync();
        //        return "Image Saved on DB";
        //    }
        //    throw new NotFoundException("Article Not Found");
        //}

        public async Task<ArticleToReturnDto> GetArticleBasedOnId(int id)
        {
            var spec = new ArticleWithIncludesByIdSpecification(id);
            var article = await _unitOfWork.Articles.FindByIdAsync(spec);
            
            if(article != null)
            {
                return _mapper.Map<Article, ArticleToReturnDto>(article);
            }

            throw new NotFoundException("Article Not Found");
        }

        public async Task RemoveArticle(int id)
        {
            var article = await _unitOfWork.Articles.FindArticleByIdAsync(id);

            if(article == null)
            {
                throw new NotFoundException("Article Not Found");
            }

            _unitOfWork.Articles.RemoveEntity(article);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
