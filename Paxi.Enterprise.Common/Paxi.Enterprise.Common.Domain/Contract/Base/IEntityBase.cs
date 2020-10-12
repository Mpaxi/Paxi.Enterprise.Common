using System;

namespace Paxi.Enterprise.Common.Domain.Contract.Base
{
    public interface IEntityBase
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }
    }
}
