using System;

namespace WebApplication1.Services
{
    public interface IFactoryService<TService, TKey>
    {
        public TService Create(TKey key);
    }

    public class FactoryService<TService, TKey> : IFactoryService<TService, TKey> where TService : class
             where TKey : class
    {
        public readonly Func<TKey, TService> func;

        public FactoryService(Func<TKey, TService> func)
        {
            this.func = func;
        }

        public TService Create(TKey key)
        {
            return this.func.Invoke(key);
        }
    }
}
