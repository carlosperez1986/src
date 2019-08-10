using Microsoft.EntityFrameworkCore;

namespace Modular.Core.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
