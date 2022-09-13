using AutoMapper;
using ChatSystem.Filter;
using ChatSystem.Model.Chats;
using ChatSystem.Model.Images;
using ChatSystem.Model.Messages;
using ChatSystem.Model.Users;
using ChatSystem.ViewModel.Chats;
using ChatSystem.ViewModel.Images;
using ChatSystem.ViewModel.Messages;
using ChatSystem.ViewModel.Users;

namespace ChatSystem.Api
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<Chat, ChatViewModel>();
            CreateMap<ChatFilterViewModel, Filters>();

            CreateMap<Image, ImageViewModel>();


            CreateMap<Message, MessageViewModel>();
            CreateMap<MessageFilterViewModel, MessageFilter>();


            CreateMap<User, UserViewModel>();
            CreateMap<UserFilterViewModel, Filters>();
        }
    }
}
