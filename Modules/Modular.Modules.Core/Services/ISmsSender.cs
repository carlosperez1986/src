using System.Threading.Tasks;

namespace Modular.Modules.Core.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}