using System.Threading.Tasks;

namespace ConsoleAppDI
{
    public interface IRepository
    {
        Task DoSomethingAsync();
    }
}