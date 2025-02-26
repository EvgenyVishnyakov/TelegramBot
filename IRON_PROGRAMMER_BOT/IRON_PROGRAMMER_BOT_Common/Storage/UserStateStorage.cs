using IRON_PROGRAMMER_BOT_Common.Firebase;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User;

namespace IRON_PROGRAMMER_BOT_Common.Storage
{
    public class UserStateStorage(FirebaseProvider firebaseProvider, PagesFactory pagesFactory)
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
                PageNames = userState.Pages.Select(x => x.GetType().FullName).ToList()
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

        private UserState? ToUserState(UserStateFirebase userStateFirebase)
        {
            try
            {
                var pages = userStateFirebase.PageNames.Select(x => pagesFactory.GetPage(x)).Reverse();
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
