using Paxi.Enterprise.Common.Domain.Contract.Base;
using System;

namespace Paxi.Enterprise.Common.WebApi.Model
{
    public class User : IEntityBase
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }
    }
}
