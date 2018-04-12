using ModelMob.Core.Entities;

namespace ModelMob.ModelService.Features.Models
{
    public class ModelApiModel
    {        
        public int ModelId { get; set; }
        public string Name { get; set; }

        public static ModelApiModel FromModel(Model model)
        {
            var model = new ModelApiModel();
            model.ModelId = model.ModelId;
            model.Name = model.Name;
            return model;
        }
    }
}
