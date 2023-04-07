using AutoMapper;
using JEM_id_assessment.Dto;
using JEM_id_assessment.Interfaces;
using JEM_id_assessment.Models;
using Microsoft.AspNetCore.Mvc;

namespace JEM_id_assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IProductGroupRepository _productGroupRepository;

        public ArticlesController(IArticleRepository articleRepository, IMapper mapper, IProductGroupRepository productGroupRepository) 
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _productGroupRepository = productGroupRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticles()
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticles());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

        [HttpGet("{codeId}")]
        [ProducesResponseType(200, Type = typeof(Article))]
        [ProducesResponseType(400)]
        public IActionResult GetArticleByCode(string codeId)
        {
            if (!_articleRepository.ArticleExists(codeId))
                return NotFound();

            var article = _mapper.Map<ArticleDto>(_articleRepository.GetArticle(codeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(article);
        }

        [HttpGet("/GetArticlesContaining/{articleName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        [ProducesResponseType(400)]
        public IActionResult GetArticlesContaining(string articleName)
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticlesWhereNameContains(articleName));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

        [HttpGet("/GetArticlesByColor/{articleColor}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        [ProducesResponseType(400)]
        public IActionResult GetArticlesByColor(string articleColor)
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticlesByColor(articleColor));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

        [HttpGet("GetArticlesBetween")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        [ProducesResponseType(400)]
        public IActionResult GetArticlesBetween([FromQuery] int potsizeFrom, [FromQuery] int potsizeTo)
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticlesByPotsize(potsizeFrom, potsizeTo));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateArticle([FromQuery] int productGroupId, [FromBody] ArticleDto articleCreate)
        {
            if (articleCreate == null)
                return BadRequest(ModelState);

            if (!_productGroupRepository.ProductGroupExists(productGroupId))
            {
                ModelState.AddModelError("", "Product Group does not exists");
                return BadRequest(ModelState);
            }

            var articles = _articleRepository.GetArticles()
                .Where(a => a.CodeId.Trim().ToUpper() == articleCreate.CodeId.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (articles != null)
            {
                ModelState.AddModelError("", "Article already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var articleMap = _mapper.Map<Article>(articleCreate);

            articleMap.ProductGroup = _productGroupRepository.GetProductGroup(productGroupId);


            if (!_articleRepository.CreateArticle(articleMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{articleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(string codeId, [FromBody] ArticleDto updatedArticle)
        {
            if (updatedArticle == null)
                return BadRequest(ModelState);

            if (codeId != updatedArticle.CodeId)
                return BadRequest(ModelState);

            if (!_articleRepository.ArticleExists(codeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var articleMap = _mapper.Map<Article>(updatedArticle);

            if (!_articleRepository.UpdateArticle(articleMap))
            {
                ModelState.AddModelError("", "Something went wrong updating article");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{codeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteArticle(string codeId)
        {
            if (!_articleRepository.ArticleExists(codeId))
            {
                return NotFound();
            }

            var articleToDelete = _articleRepository.GetArticle(codeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_articleRepository.DeleteArticle(articleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }

        [HttpGet("GetArticlesPaging")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticlesPaging([FromQuery] int page)
        {
            int pageSize = 3;
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticles(page, pageSize));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }



        [HttpGet("GetArticlesOrderedByNameAsc")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticlesOrderedByNameAsc()
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticlesSortedByNameAsc());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

        [HttpGet("GetArticlesOrderedByNameDesc")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticlesOrderedByNameDesc()
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticlesSortedByNameDesc());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

        [HttpGet("GetArticlesOrderedByPotsizeAsc")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticlesOrderedByPotsizeAsc()
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticlesSortedByPotSizeAsc());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

        [HttpGet("GetArticlesOrderedByPotsizeDesc")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticlesOrderedByPotsizeDesc()
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticlesSortedByPotSizeDesc());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

        [HttpGet("GetArticlesOrderedByPlantHeightAsc")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticlesOrderedByPlantHeightAsc()
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticlesSortedByPlantHeightAsc());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

        [HttpGet("GetArticlesOrderedByPlantHeightDesc")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticlesOrderedByPlantHeightDesc()
        {
            var articles = _mapper.Map<List<ArticleDto>>(_articleRepository.GetArticlesSortedByPlantHeightDesc());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(articles);
        }

    };


}
