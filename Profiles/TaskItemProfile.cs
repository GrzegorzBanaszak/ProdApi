using System;
using AutoMapper;
using ProdApi.Dtos;
using ProdApi.Models;

namespace ProdApi.Profiles;

public class TaskItemProfile : Profile
{
    public TaskItemProfile()
    {
        CreateMap<TaskItem, TaskItemDto>();
    }
}
