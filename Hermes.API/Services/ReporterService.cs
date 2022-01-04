using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Util;
using Hermes.API.Util.Exceptions;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Articles;
using Hermes.Core.Interfaces.Specifications.Reporters;

namespace Hermes.API.Services
{
    public class ReporterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public static IWebHostEnvironment _webHostEnvironment;

        public ReporterService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ReporterToReturnDto> GetReporterByIdAsync(int id)
        {
            var spec = new ReporterWithIncludesByIdSpecification(id);
            var reporter = await _unitOfWork.Reporters.FindByIdAsync(spec);

            if(reporter != null)
            {
                return _mapper.Map<Reporter, ReporterToReturnDto>(reporter);
            }

            throw new NotFoundException("Reporter Not Found");
        }

        public async Task<ReporterToReturnDto> AddReporter(RegisterReporter reporterFromBody)
        {
            var reporterFromDb = await _unitOfWork.Reporters.GetReporterByEmail(reporterFromBody.Email);

            if(reporterFromDb != null)
            {
                throw new ApplicationException("Reporter Already Exists on Database");
            }

            var reporter = _mapper.Map<RegisterReporter, Reporter>(reporterFromBody);
            reporter.Password = Encrypt.GetSha256(reporterFromBody.Password);
            _unitOfWork.Reporters.SaveEntity(reporter);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Reporter ,ReporterToReturnDto>(reporter);
        }

        public async Task<ReporterToReturnDto> AddReporterImageBasedOnId(int reporterId, InputImageRequest file)
        {
            var spec = new ReporterWithIncludesByIdSpecification(reporterId);
            var reporter = await _unitOfWork.Reporters.FindByIdAsync(spec);

            if(reporter != null)
            {
                var fileName = await FileHandler.SaveFileOnDirectory(_webHostEnvironment.WebRootPath, "Reporter", file.Content, file.FilePathOrFileName);
                reporter.Image = fileName;
                _unitOfWork.Reporters.UpdateEntity(reporter);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<Reporter, ReporterToReturnDto>(reporter);
            }

            throw new NotFoundException("Reporter Not Found");
        }

        public async Task<Pagination<ArticleToReturnWithoutReporterDto>> GetReporterArticles(ArticleSpecParams articleParams)
        {
            if (!articleParams.ReporterId.HasValue)
            {
                throw new InvalidParamException("Reporter Id must be Provided");
            }

            var spec = new ArticleSpecification(articleParams);
            var countSpec = new ArticleForCountSpecification(articleParams);
            var articles = await _unitOfWork.Articles.ListAllAsync(spec);
            var count = await _unitOfWork.Articles.GetCountWithSpecifications(countSpec);
            var articlesToReturn = _mapper.Map<List<Article>, List<ArticleToReturnWithoutReporterDto>>(articles.ToList());
            return new Pagination<ArticleToReturnWithoutReporterDto>(count, articleParams.PageSize, articleParams.PageIndex, articlesToReturn);
        }

    }
}
