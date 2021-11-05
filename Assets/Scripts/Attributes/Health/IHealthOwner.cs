
namespace Attributes.Health
{
    public interface IHealthOwner
    {
        void OnDied();

        void OnDamageTaken();
    }
}