using System;

namespace BOS.Webclient.Infrastructure.Services
{
    public interface IUnitOfWork
    {
        void Worker(Action work);
        void TransactionWorker(Action work);
        void SaveChanges();
    }
}