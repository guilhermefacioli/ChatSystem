using ChatSystem.DatabaseAccess.Repositories;
using ChatSystem.DatabaseAccess.Repositories.ChatUser;
using ChatSystem.Model.Chats;
using ChatSystem.Model.Images;
using ChatSystem.Model.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ChatSystem.DatabaseAccess
{
    public static class DatabaseAccessRegistry
    {
        public static IServiceCollection AddChatSystemDatabaseAccess(this IServiceCollection services, string connectionString, bool inMemory = false)
        {
            if (inMemory)
            {
                services.AddDbContext<ChatContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
            }
            else
            {
                services.AddDbContext<ChatContext>(options =>
                options.UseSqlServer(connectionString));
            }

            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IChatsUsersRepository, ChatsUsersRepository>();

            return services;
        }

    }
}
