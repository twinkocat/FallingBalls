
namespace twinkocat
{
    /// <summary>
    /// Interface for working with object pool.
    /// </summary>
    public interface IPoolObject
    {
        /// <summary>
        /// Invokes when object is created.
        /// </summary>
        void OnCreate();

        /// <summary>
        /// Invokes when object is get from pool.
        /// </summary>
        void OnGet();

        /// <summary>
        /// Invokes when object is return to pool.
        /// </summary>
        void OnReturn();
    }
}
