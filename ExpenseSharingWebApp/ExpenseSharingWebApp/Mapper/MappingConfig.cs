using AutoMapper;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;

namespace ExpenseSharingWebApp.Mapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //Mapping GroupDto to Group
                config.CreateMap<CreateGroupRequestDto, Group>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString())); 
                //Mapping Group to GroupDto
                config.CreateMap<Group, GroupResponseDto>()
                .ForMember(dest => dest.UserGroups, opt => opt.MapFrom(src => src.UserGroups.Select(ug => new UserGroupDto
                {
                    UserId = ug.UserId,
                    User = new UserDto
                    {
                        Id = ug.User.Id,
                        Name = ug.User.Name,
                        Email = ug.User.Email,      
                    }
                })))
                .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.Expenses.Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Description = e.Description,
                    Date = e.Date
                })))
                .ForMember(dest => dest.Admins, opt => opt.MapFrom(src => src.Admins.Select(a => new UserDto
                {
                    Id = a.User.Id,
                    Name = a.User.Name,
                    Email = a.User.Email
                })));


                config.CreateMap<User, UserDto>();
               
                config.CreateMap<CreateExpenseRequestDto, Expense>();
                config.CreateMap<UpdateExpenseRequestDto, Expense>();
                config.CreateMap<Expense, ExpenseResponseDto>()
                    .ForMember(dest => dest.PaidByUser, opt => opt.MapFrom(src => src.PaidByUser))
                    .ForMember(dest => dest.ExpenseSplits, opt => opt.MapFrom(src => src.ExpenseSplits));


                // Mapping for ExpenseSplit
                config.CreateMap<ExpenseSplit, ExpenseSplitDto>()
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

                //Mapping for expenseSplit response dto
                config.CreateMap<ExpenseSplit, ExpenseSplitResponeDto>()
                .ForMember(dest => dest.ExpenseId, opt => opt.MapFrom(src => src.ExpenseId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Expense.Description))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Expense.Amount))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Expense.Date))
                .ForMember(dest => dest.PaidByUserId, opt => opt.MapFrom(src => src.Expense.PaidByUserId))
                .ForMember(dest => dest.UserShare, opt => opt.MapFrom(src => src.AmountOwed))
                .ForMember(dest => dest.AmountPaid, opt => opt.MapFrom(src => src.AmountPaid))
                .ForMember(dest => dest.AmountOwed, opt => opt.MapFrom(src => src.AmountOwed));


                config.CreateMap<UserGroup, UserGroupDto>()
             .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
             .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));


            });
            return mappingConfig;
        }
    }
}
