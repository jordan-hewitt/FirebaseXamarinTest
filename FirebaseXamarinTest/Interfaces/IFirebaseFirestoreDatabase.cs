using System.Threading.Tasks;
using FirebaseXamarinTest.Models;

namespace FirebaseXamarinTest.Interfaces
{
    public interface IFirebaseFirestoreDatabase
    {
        Task<string> SetProfile(Profile profile);
    }
}
