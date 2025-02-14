using System.Collections.Concurrent;
using IRON_PROGRAMMER_BOT_ConsoleApp.User;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Storage
{
    public class UserStateStorage
    {
        private readonly ConcurrentDictionary<long, UserState> cache = new ConcurrentDictionary<long, UserState>();

        public void AddOrUpdate(long telegramUserId, UserState userState)
        {
            cache.AddOrUpdate(telegramUserId, userState, (x, y) => userState);
        }

        public bool TryGet(long telegramUserId, out UserState? userState)
        {
            return cache.TryGetValue(telegramUserId, out userState);
        }
    }
}
