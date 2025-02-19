using System;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Firebase
{
    public class FirebaseProvider
    {
        private readonly FirebaseClient _client;

        public FirebaseProvider()
        {
            var _basePath = Environment.GetEnvironmentVariable("basePath");
            var _secret = Environment.GetEnvironmentVariable("secretFirebase");

            _client = new FirebaseClient(_basePath, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(_secret)
            });
        }

        public async Task<T> TryGetAsync<T>(string key)
        {
            return await _client.Child(key).OnceSingleAsync<T>();
        }

        public async Task AddOrUpdateAsync<T>(string key, T item)
        {
            await _client.Child(key).PutAsync(item);
        }

    }
}
