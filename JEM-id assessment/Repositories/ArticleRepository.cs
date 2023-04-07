using AutoMapper;
using JEM_id_assessment.Data;
using JEM_id_assessment.Interfaces;
using JEM_id_assessment.Models;

namespace JEM_id_assessment.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ArticleRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateArticle(Article article)
        {
            _context.Add(article);
            return Save();
        }

        public bool DeleteArticle(Article article)
        {
            _context.Remove(article);
            return Save();
        }

        public Article GetArticle(string codeId)
        {
            return _context.Articles.Where(a => a.CodeId == codeId).FirstOrDefault();
        }

        public ICollection<Article> GetArticles()
        {
            return _context.Articles.ToList();
        }

        public bool ArticleExists(string codeId)
        {
            return _context.Articles.Any(a => a.CodeId == codeId);
        }

        public bool UpdateArticle(Article article)
        {
            _context.Update(article);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<Article> GetArticlesWhereNameContains(string name)
        {
            return _context.Articles.Where(a => a.Name.Contains(name)).ToList();
        }

        public ICollection<Article> GetArticlesByPotsize(int potsizeFrom, int potsizeTo)
        {
            return _context.Articles.Where(a => a.PotSize >= potsizeFrom && a.PotSize <= potsizeTo).ToList();
        }

        public ICollection<Article> GetArticlesSortedByNameAsc()
        {
            return _context.Articles.OrderBy(a => a.Name).ToList();
        }

        public ICollection<Article> GetArticlesSortedByNameDesc()
        {
            return _context.Articles.OrderByDescending(a => a.Name).ToList();
        }

        public ICollection<Article> GetArticlesSortedByPotSizeAsc()
        {
            return _context.Articles.OrderBy(a =>a.PotSize).ToList();
        }

        public ICollection<Article> GetArticlesSortedByPotSizeDesc()
        {
            return _context.Articles.OrderByDescending(a => a.PotSize).ToList();
        }

        public ICollection<Article> GetArticlesSortedByPlantHeightAsc()
        {
            return _context.Articles.OrderBy(a => a.PlantHeight).ToList();
        }

        public ICollection<Article> GetArticlesSortedByPlantHeightDesc()
        {
            return _context.Articles.OrderByDescending(a => a.PlantHeight).ToList();
        }

        public ICollection<Article> GetArticles(int page, int pageSize)
        {
            if (page < 1)
            {
                page = 0;
            }
            int totalNumber = page * pageSize;
            return _context.Articles.Skip(totalNumber).Take(pageSize).ToList();
        }

        public ICollection<Article> GetArticlesByColor(string color)
        {
            return _context.Articles.Where(a => a.Color == color).ToList();
        }
    }
}
