using JEM_id_assessment.Models;

namespace JEM_id_assessment.Interfaces
{
    public interface IProductGroupRepository
    {
        ICollection<ProductGroup> GetProductGroups();
        ProductGroup GetProductGroup(int productGroupId);
        ICollection<Article> GetArticlesByProductGroup(int productGroupId);
        bool ProductGroupExists(int productGroupId);
        bool CreateProductGroup(ProductGroup productGroup);
        bool UpdateProductGroup(ProductGroup productGroup);
        bool DeleteProductGroup(int productGroup);
        bool Save();
    }
}
