namespace NewPlatform.Flexberry.Services
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Contract class for <see cref="ILockService"/>.
    /// </summary>
    /// <seealso cref="ILockService" />
    [ContractClassFor(typeof(ILockService))]
    internal abstract class LockServiceContract : ILockService
    {
        /// <summary>
        /// Contracts for <see cref="ILockService.LockObject"/>.
        /// </summary>
        /// <param name="dataObjectKey">The data object key.</param>
        /// <param name="userName">Name of the user who locks the object.</param>
        /// <returns>Not used.</returns>
        public LockData LockObject(object dataObjectKey, string userName)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Contracts for <see cref="ILockService.GetLock"/>.
        /// </summary>
        /// <param name="dataObjectKey">The data object key.</param>
        /// <returns>Not used.</returns>
        public LockData GetLock(object dataObjectKey)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Contracts for <see cref="ILockService.UnlockObject"/>.
        /// </summary>
        /// <param name="dataObjectKey">The data object key.</param>
        /// <returns>Not used.</returns>
        public bool UnlockObject(object dataObjectKey)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Contracts for <see cref="ILockService.UnlockAllObjects"/>.
        /// </summary>
        public void UnlockAllObjects()
        {
            throw new InvalidOperationException();
        }
    }
}