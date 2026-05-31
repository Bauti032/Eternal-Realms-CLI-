using EternalRealms.Core.Entities;

namespace EternalRealms.Core.Interfaces
{
    public interface ILevelService
    {
        bool ShouldLevelUp(Character character);
        void ApplyLevelUp(Character character);
    }
}

