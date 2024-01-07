using System.Threading.Tasks;

namespace Start.Utils
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);
    }
}