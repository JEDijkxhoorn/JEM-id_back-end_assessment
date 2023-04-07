using AutoMapper;
using JEM_id_assessment.Data;
using JEM_id_assessment.Interfaces;
using JEM_id_assessment.Models;
using Microsoft.EntityFrameworkCore;

namespace JEM_id_assessment.Repositories
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductGroupRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateProductGroup(ProductGroup productGroup)
        {
            _context.Add(productGroup);
            return Save();
        }

        public bool DeleteProductGroup(int productGroup)
        {
            _context.Remove(productGroup);
            return Save();
        }

        public ICollection<Article> GetArticlesByProductGroup(int productGroupId)
        {
            return _context.Articles.Where(a => a.ProductGroup.Id == productGroupId).ToList();
        }

        public ProductGroup GetProductGroup(int productGroupId)
        {
            return _context.ProductGroups.Where(pg => pg.Id == productGroupId).Include(a => a.Articles).FirstOrDefault();
        }

        public ICollection<ProductGroup> GetProductGroups()
        {
            return _context.ProductGroups.ToList();
        }

        public bool ProductGroupExists(int productGroupId)
        {
            return _context.ProductGroups.Any(pg => pg.Id == productGroupId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProductGroup(ProductGroup productGroup)
        {
            _context.Update(productGroup);
            return Save();
        }
    }
}
