namespace NewPlatform.Flexberry.Services
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Base interface of service for locking objects.
    /// </summary>
    [ContractClass(typeof(LockServiceContract))]
    public interface ILockService
    {
        /// <summary>
        /// Locks the object with specified key.
        /// </summary>
        /// <param name="dataObjectKey">The data object key.</param>
        /// <param name="userName">Name of the user who locks the object.</param>
        /// <returns>Information about lock.</returns>
        LockData LockObject(object dataObjectKey, string userName);

        /// <summary>
        /// Gets the lock of object with specified key.
        /// </summary>
        /// <param name="dataObjectKey">The data object key.</param>
        /// <returns>Information about lock.</returns>
        LockData GetLock(object dataObjectKey);

        /// <summary>
        /// Unlocks the object.
        /// </summary>
        /// <param name="dataObjectKey">The data object key.</param>
        /// <returns>Returns <c>true</c> if object is successfully unlocked or <c>false</c> if it's not exist.</returns>
        bool UnlockObject(object dataObjectKey);

        /// <summary>
        /// Unlocks all objects.
        /// </summary>
        void UnlockAllObjects();
    }
}