using AutoMapper;
using JEM_id_assessment.Interfaces;
using JEM_id_assessment.Models;
using JEM_id_assessment.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JEM_id_assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductGroupController : Controller
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;

        public ProductGroupController(IProductGroupRepository productGroupRepository, IMapper mapper)
        {
            _productGroupRepository = productGroupRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductGroup>))]
        public IActionResult GetProductGroups()
        {
            var productGroups = _mapper.Map<List<ProductGroupDto>>(_productGroupRepository.GetProductGroups());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productGroups);
        }

        [HttpGet("{productGroupId}")]
        [ProducesResponseType(200, Type = typeof(ProductGroup))]
        [ProducesResponseType(400)]
        public IActionResult GetProductGroup(int productGroupId)
        {
            if (!_productGroupRepository.ProductGroupExists(productGroupId))
                return NotFound();

            var productGroup = _mapper.Map<ProductGroupDto>(_productGroupRepository.GetProductGroup(productGroupId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productGroup);
        }

        [HttpGet("{productGroupId}/articles")]
        public IActionResult GetArticlesByProductGroup(int productGroupId)
        {
            if (!_productGroupRepository.ProductGroupExists(productGroupId))
                return NotFound();

            var articles = _mapper.Map<List<ArticleDto>>(
                _productGroupRepository.GetArticlesByProductGroup(productGroupId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(articles);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductGroup([FromBody] ProductGroupDto productGroupCreate)
        {
            if (productGroupCreate == null)
                return BadRequest(ModelState);

            var productGroup = _productGroupRepository.GetProductGroups()
                .Where(c => c.Name.Trim().ToUpper() == productGroupCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (productGroup != null)
            {
                ModelState.AddModelError("", "Product Group already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productGroupMap = _mapper.Map<ProductGroup>(productGroupCreate);

            if (!_productGroupRepository.CreateProductGroup(productGroupMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{productGroupId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProductGroup(int productGroupId, [FromBody] ProductGroupDto updatedProductGroup)
        {
            if (updatedProductGroup == null)
                return BadRequest(ModelState);

            if (productGroupId != updatedProductGroup.Id)
                return BadRequest(ModelState);

            if (!_productGroupRepository.ProductGroupExists(productGroupId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var productGroupMap = _mapper.Map<ProductGroup>(updatedProductGroup);

            if (!_productGroupRepository.ProductGroupExists(productGroupId))
            {
                ModelState.AddModelError("", "Something went wrong updating product group");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{productGroupId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProductGroup(int productGroupId)
        {
            if (!_productGroupRepository.ProductGroupExists(productGroupId))
            {
                return NotFound();
            }

            var productGroupToDelete = _productGroupRepository.ProductGroupExists(productGroupId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productGroupRepository.ProductGroupExists(productGroupId))
            {
                ModelState.AddModelError("", "Something went wrong deleting product group");
            }

            return NoContent();
        }
    }
}
