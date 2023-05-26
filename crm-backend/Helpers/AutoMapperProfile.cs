using AutoMapper;
using crm_backend.Entities;
using crm_backend.Models;

namespace crm_backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateRequest -> Customer
            CreateMap<CreateRequest, Customer>();

            // UpdateRequest -> Customer
            CreateMap<UpdateRequest, Customer>().ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignorar nulos y vacios en strings
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}
