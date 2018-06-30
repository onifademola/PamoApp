using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repos
{
    public class RepositoryActionResult<T> where T : class
    {
        public T Entity { get; private set; }

        public RepositoryActionStatus Status { get; private set; }

        public string Message { get; private set; }

        public string OtherMessage { get; private set; }

        public Exception Exception { get; private set; }

        public bool _Bool { get; private set; }

        public RepositoryActionResult(RepositoryActionStatus status)
        {
            Status = status;
        }

        public RepositoryActionResult(RepositoryActionStatus status, bool _bool)
        {
            Status = status;
            _Bool = _bool;
        }

        public RepositoryActionResult(T entity, RepositoryActionStatus status)
        {
            Entity = entity;
            Status = status;
        }

        public RepositoryActionResult(T entity, RepositoryActionStatus status, bool _bool)
        {
            Entity = entity;
            Status = status;
            _Bool = _bool;
        }

        //public RepositoryActionResult(T entity, RepositoryActionStatus status, string message) : this(entity, status)
        //{
        //    Message = message;
        //}

        public RepositoryActionResult(T entity, RepositoryActionStatus status, Exception exception) : this(entity, status)
        {
            Exception = exception;
        }

        public RepositoryActionResult(RepositoryActionStatus status, string message, string otherMessage)
        {
            Status = status;
            Message = message;
            OtherMessage = otherMessage;
        }

        public RepositoryActionResult(T entity, RepositoryActionStatus status, string message, string otherMessage)
        {
            Entity = entity;
            Status = status;
            Message = message;
            OtherMessage = otherMessage;
        }
    }
}
