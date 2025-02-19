using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRON_PROGRAMMER_BOT_ConsoleApp.Firebase;
using IRON_PROGRAMMER_BOT_ConsoleApp.User;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Storage
{
    public class UserStateStorage
    {
        private readonly FirebaseProvider _firebaseProvider = new();

        public async Task AddOrUpdateAsync(long telegramUserId, UserState userState)
        {
            var userStateFirebase = ToUserStateFirebase(userState);
            await _firebaseProvider.AddOrUpdateAsync($"userstate/{telegramUserId}", userStateFirebase);
        }

        private static UserStateFirebase ToUserStateFirebase(UserState userState)
        {
            return new UserStateFirebase
            {
                UserData = userState.UserData,
                PageNames = userState.Pages.Select(x => x.GetType().Name).ToList()
            };
        }

        public async Task<UserState?> TryGetAsync(long telegramUserId)
        {
            var userStateFirebase = await _firebaseProvider.TryGetAsync<UserStateFirebase>($"userstate/{telegramUserId}");
            if (userStateFirebase == null)
                return null;
            return ToUserState(userStateFirebase);
        }

        private static UserState? ToUserState(UserStateFirebase userStateFirebase)
        {
            var pages = userStateFirebase.PageNames.Select(PagesFactory.GetPage).Reverse();
            return new UserState(new Stack<IPage>(pages), userStateFirebase.UserData);
        }
    }
}
