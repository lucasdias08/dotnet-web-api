using FireSharp.Config;
using FireSharp.Interfaces;

namespace FirebaseMedium
{
    class ConnectionDB
    {
        //firebase connection Settings
        public IFirebaseConfig fc = new FirebaseConfig()
        {
            AuthSecret = "ceKeAyqEEgNBwHIHcxWVBlkiAx19JrjWnKMJNdQR",
            BasePath = "https://a3sd-5ecbb-default-rtdb.firebaseio.com",
        };

        public IFirebaseClient client;
        //Code to warn console if class cannot connect when called.
        public ConnectionDB()
        {
            try
            {
                client = new FireSharp.FirebaseClient(fc);
            }
            catch (Exception)
            {
                Console.WriteLine("sunucuya bağlanılamadı");
            }
        }
    }
}