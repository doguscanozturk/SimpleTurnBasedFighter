using Attributes;
using ProgressSystem.Data;

namespace ProgressSystem
{
    public interface IProgressionProvider
    {
        ProgressData CurrentProgress { get; }
        
        HeroAttributes[] GetUpToDateHeroAttributes();
    }
}