using Microsoft.EntityFrameworkCore;

namespace SeminarAPI.Mapping
{
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
