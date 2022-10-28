namespace BankAppCore.Interfaces
{
    public interface IValidators
    {
        bool CheckEmail(string email);
        bool CheckName(string name);
        bool CheckPassword(string password);
    }
}