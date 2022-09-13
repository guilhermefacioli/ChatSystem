using ChatSystem.Model.Chats;
using ChatSystem.Model.Images;
using ChatSystem.Model.Messages;
using ChatSystem.Model.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ChatSystem
{
    public static class DomainRegistry
    {
        public static IServiceCollection AddChatSystemDomain(this IServiceCollection services)
        {
            services.AddTransient<IChatService, ChatService>();

            services.AddTransient<IMessageService, MessageService>();

            services.AddTransient<IImageService, ImageService>();

            services.AddTransient<IUserService, UserService>();

            return services;

        }
    }
}
