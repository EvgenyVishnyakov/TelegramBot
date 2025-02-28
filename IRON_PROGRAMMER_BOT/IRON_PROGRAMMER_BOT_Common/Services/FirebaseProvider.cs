using Firebase.Database;
using Firebase.Database.Query;

namespace IRON_PROGRAMMER_BOT_Common.Services
{
    public class FirebaseProvider(FirebaseClient client)
    {
        public async Task<T?> TryGetAsync<T>(string key)
        {
            try
            {
                return await client.Child(key).OnceSingleAsync<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение в методе TryGetAsync в FirebaseProvider: {ex.ToString()}");
                return default;
            }
        }

        public async Task AddOrUpdateAsync<T>(string key, T item)
        {
            try
            {
                await client.Child(key).PutAsync(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
