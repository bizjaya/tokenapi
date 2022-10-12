using TOKENAPI.Domain;
using TOKENAPI.DTO;
using TOKENAPI.Events;

namespace TOKENAPI.Mapper
{
    public class UsrProfile : Profile
    {
        public UsrProfile()
        {

            CreateMap<Acct, UserDto>()
            ;

            CreateMap<Acct, RefsDto>()
           ;

        }
    }
}
