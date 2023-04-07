using JEM_id_assessment.Data;
using JEM_id_assessment.Models;

namespace JEM_id_assessment
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext() 
        { 
            if (!dataContext.Articles.Any())
            {
                var articles = new List<Article>()
                {
                    new Article()
                    {
                        CodeId = "YjGmQGX7GGxxd",
                        Name = "Aconitum napellus",
                        PotSize = 9,
                        PlantHeight = 120,
                        Color = "groen",
                        ProductGroup = new ProductGroup()
                        {
                            Name = "Monnikskap"
                        }
                    },
                    new Article()
                    {
                        CodeId = "s3kKAGRTdn1vn",
                        Name = "Ajuga reptans Atropurpurea",
                        PotSize = 10,
                        PlantHeight = 150,
                        Color = "geel",
                        ProductGroup = new ProductGroup()
                        {
                            Name = "Zenegroen"
                        }
                    },
                    new Article()
                    {
                        CodeId = "kiU394MZkrWqc",
                        Name = "Aquilegia alpina",
                        PotSize = 5,
                        PlantHeight = 50,
                        Color = "blauw",
                        ProductGroup = new ProductGroup()
                        {
                            Name = "Akelei"
                        }
                    },
                    new Article()
                    {
                        CodeId = "glXJuymYWnkkY",
                        Name = "Aquilegia Nora Barlow",
                        PotSize = 6,
                        PlantHeight = 60,
                        Color = "wit",
                        ProductGroup = new ProductGroup()
                        {
                            Name = "Akelei"
                        }
                    },
                    new Article()
                    {
                        CodeId = "WOF8jStbJ9aow",
                        Name = "Helianthemum Elfenbeinglanz",
                        PotSize = 1,
                        PlantHeight = 20,
                        Color = "geel",
                        ProductGroup = new ProductGroup()
                        {
                            Name = "Zonneroosje"
                        }
                    },
                    new Article()
                    {
                        CodeId = "Ptux0KcqiDmA5",
                        Name = "Aconitum carm. Arendsii",
                        PotSize = 9,
                        PlantHeight = 20,
                        Color = "wit",
                        ProductGroup = new ProductGroup()
                        {
                            Name = "Monnikskap"
                        }
                    },
                    new Article()
                    {
                        CodeId = "A1tftAcQL3SJk",
                        Name = "Helianthemum Wisley Primrose",
                        PotSize = 7,
                        PlantHeight = 100,
                        Color = "roze",
                        ProductGroup = new ProductGroup()
                        {
                            Name = "Zonneroosje"
                        }
                    },
                    new Article()
                    {
                        CodeId = "hrI5vHgOSZuWH",
                        Name = "Helianthemum Ben Hope",
                        PotSize = 4,
                        PlantHeight = 90,
                        Color = "groen",
                        ProductGroup = new ProductGroup()
                        {
                            Name = "Zonneroosje"
                        }
                    },
                    new Article()
                    {
                        CodeId = "JOpdTpjr7FJoX",
                        Name = "Helianthemum Sulphureum Plenum",
                        PotSize = 4,
                        PlantHeight = 90,
                        Color = "rood",
                        ProductGroup = new ProductGroup()
                        {
                            Name = "Zonneroosje"
                        }
                    },
                };
                dataContext.Articles.AddRange(articles);
                dataContext.SaveChanges();
            }
        }   
    }
}
