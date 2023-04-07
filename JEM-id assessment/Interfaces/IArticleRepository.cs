using JEM_id_assessment.Models;

namespace JEM_id_assessment.Interfaces
{
    public interface IArticleRepository
    {
        ICollection<Article> GetArticles();

        ICollection<Article> GetArticlesWhereNameContains(string name);

        ICollection<Article> GetArticlesByPotsize(int potsizeFrom, int potsizeTo);

        ICollection<Article> GetArticlesByColor(string color);

        ICollection<Article> GetArticlesSortedByNameAsc();

        ICollection<Article> GetArticlesSortedByNameDesc();

        ICollection<Article> GetArticlesSortedByPotSizeAsc();

        ICollection<Article> GetArticlesSortedByPotSizeDesc();

        ICollection<Article> GetArticlesSortedByPlantHeightAsc();

        ICollection<Article> GetArticlesSortedByPlantHeightDesc();

        ICollection<Article> GetArticles(int page, int pageSize);

        Article GetArticle(string codeId);

        bool ArticleExists(string codeId);

        bool CreateArticle(Article article);

        bool UpdateArticle(Article article);

        bool DeleteArticle(Article article);

        bool Save();
    }
}
