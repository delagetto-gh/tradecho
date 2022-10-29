using System;
using System.Threading.Tasks;

namespace Framework.Ddd;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync();
}