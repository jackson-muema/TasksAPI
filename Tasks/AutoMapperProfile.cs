using AutoMapper;
using Tasks.Models;

namespace Tasks
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        { 
            //used when retrieving data from the db (converts the entity to a dto)
            CreateMap<TasksModel, TasksDTO>();
            //used when posting data to the database
            CreateMap<TasksDTO, TasksModel>();

        }
    }
}
