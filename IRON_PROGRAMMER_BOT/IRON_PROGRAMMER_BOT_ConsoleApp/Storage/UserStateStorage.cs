using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRON_PROGRAMMER_BOT_ConsoleApp.Firebase;
using IRON_PROGRAMMER_BOT_ConsoleApp.User;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Storage
{
    public class UserStateStorage(FirebaseProvider firebaseProvider)
    {
        public async Task AddOrUpdateAsync(long telegramUserId, UserState userState)
        {
            try
            {
                var userStateFirebase = ToUserStateFirebase(userState);
                await firebaseProvider.AddOrUpdateAsync($"userstate/{telegramUserId}", userStateFirebase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
            try
            {
                var userStateFirebase = await firebaseProvider.TryGetAsync<UserStateFirebase>($"userstate/{telegramUserId}");
                if (userStateFirebase == null)
                    return null;
                return ToUserState(userStateFirebase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private static UserState? ToUserState(UserStateFirebase userStateFirebase)
        {
            try
            {
                var pages = userStateFirebase.PageNames.Select(PagesFactory.GetPage).Reverse();
                return new UserState(new Stack<IPage>(pages), userStateFirebase.UserData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
