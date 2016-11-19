using System.Threading.Tasks;

namespace OnBackButtonPressed
{
    public interface IConfirmGoBack
    {
        Task<bool> CanGoBackAsync();
    }
}