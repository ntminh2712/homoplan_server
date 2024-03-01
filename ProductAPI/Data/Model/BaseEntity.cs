using System;

namespace SeminarAPI.Data.Model
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Created_At = DateTime.Now;
        }

        public DateTime Created_At { get; set; }
    }
}
