using ModelMob.Core.Entities;

namespace ModelService.Features.FeaturedModels
{
    public class FeaturedModelApiModel
    {        
        public int FeaturedModelId { get; set; }
        public string Name { get; set; }

        public static FeaturedModelApiModel FromFeaturedModel(FeaturedModel featuredModel)
        {
            var model = new FeaturedModelApiModel();
            model.FeaturedModelId = featuredModel.FeaturedModelId;
            return model;
        }
    }
}
