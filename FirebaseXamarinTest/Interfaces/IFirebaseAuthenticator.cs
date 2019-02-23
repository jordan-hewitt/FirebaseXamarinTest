using System.Threading.Tasks;

namespace FirebaseXamarinTest.Interfaces
{
    public interface IFirebaseAuthenticator 
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<string> RegsiterWithEmailPassword(string email, string password);
        Task<string> RegisterWithPhone(string phoneNumber);
        Task<string> VerifyAccount(string verificationCode);
        Task<string> DeleteAccount();
        Task<string> DeleteAccount(string email, string password);
    }
}
