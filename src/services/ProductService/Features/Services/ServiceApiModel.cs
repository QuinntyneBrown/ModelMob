using Core.Entities;

namespace ProductService.Features.Services
{
    public class ServiceApiModel
    {        
        public int ServiceId { get; set; }
        public string Name { get; set; }

        public static ServiceApiModel FromService(Service service)
        {
            var model = new ServiceApiModel();
            model.ServiceId = service.ServiceId;
            model.Name = service.Name;
            return model;
        }
    }
}
