using ModelMob.Core.Entities;

namespace ModelService.Features.Models
{
    public class ModelApiModel
    {        
        public int ModelId { get; set; }
        public string Firstname { get; set; }

        public static ModelApiModel FromModel(Model model)
        {
            var apiModel = new ModelApiModel();
            apiModel.ModelId = apiModel.ModelId;
            apiModel.Firstname = apiModel.Firstname;
            return apiModel;
        }
    }
}
