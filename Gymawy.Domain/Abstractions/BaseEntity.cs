using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Abstractions
{
    public abstract class BaseEntity
    { 
        public Guid Id { get; protected set; }

        public DateTime CreatedOnUtc { get; set; } 

        public DateTime? UpdatedAt { get; set; } 

        //public bool IsActive {  get; protected set; }


        protected BaseEntity(  DateTime createdOnUtc , Guid? id = null)
        {
            Id =id ?? Guid.NewGuid();
            CreatedOnUtc = createdOnUtc;
           // IsActive = true;
        }

        protected BaseEntity() { }
    }
}
