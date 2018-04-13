using ModelMob.Core.Entities;

namespace ModelMob.ModelService.Features.FeaturedModels
{
    public class FeaturedModelApiModel
    {        
        public int FeaturedModelId { get; set; }
        public string Name { get; set; }

        public static FeaturedModelApiModel FromFeaturedModel(FeaturedModel featuredModel)
        {
            var model = new FeaturedModelApiModel();
            model.FeaturedModelId = featuredModel.FeaturedModelId;
            model.Name = featuredModel.Name;
            return model;
        }
    }
}
