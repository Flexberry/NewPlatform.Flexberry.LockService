namespace NewPlatform.Flexberry.Services
{
    using System;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Exceptions;

    /// <summary>
    /// Base implementation of <see cref="ILockService"/> using <see cref="IDataService"/> for storing data.
    /// </summary>
    public class LockService : ILockService
    {
        /// <summary>
        /// The data service for storing data.
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        /// The synchronization object.
        /// </summary>
        private readonly object _lockObj = new object();

        /// <summary>
        /// Timeout for locks.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LockService"/> class.
        /// </summary>
        /// <param name="dataService">The data service for storing data.</param>
        public LockService(IDataService dataService)
        {
            if (dataService == null)
            {
                throw new ArgumentNullException(nameof(dataService));
            }

            _dataService = dataService;

            Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Locks the object with specified key.
        /// </summary>
        /// <param name="dataObjectKey">The data object key.</param>
        /// <param name="userName">Name of the user who locks the object.</param>
        /// <returns>Information about lock.</returns>
        public LockData LockObject(object dataObjectKey, string userName)
        {
            lock (_lockObj)
            {
                var lockData = new Lock();
                lockData.SetExistObjectPrimaryKey(dataObjectKey);

                Func<LockData> updateLock = () =>
                {
                    lockData.UserName = userName;
                    lockData.LockDate = DateTime.Now;
                    _dataService.UpdateObject(lockData);

                    return new LockData(dataObjectKey, userName, true);
                };

                try
                {
                    _dataService.LoadObject(lockData);
                }
                catch (CantFindDataObjectException)
                {
                    return updateLock();
                }

                if (lockData.LockDate + Timeout <= DateTime.Now || string.Equals(lockData.UserName, userName))
                    return updateLock();

                return new LockData(lockData.LockKey, lockData.UserName, false);
            }
        }

        /// <summary>
        /// Gets the lock of object with specified key.
        /// </summary>
        /// <param name="dataObjectKey">The data object key.</param>
        /// <returns>Information about lock.</returns>
        public LockData GetLock(object dataObjectKey)
        {
            lock (_lockObj)
            {
                var lockData = new Lock();
                lockData.SetExistObjectPrimaryKey(dataObjectKey);

                try
                {
                    _dataService.LoadObject(lockData);
                }
                catch (CantFindDataObjectException)
                {
                    return new LockData(null, null, false);
                }

                return new LockData(lockData.LockKey, lockData.UserName, lockData.LockDate + Timeout > DateTime.Now);
            }
        }

        /// <summary>
        /// Unlocks the object.
        /// </summary>
        /// <param name="dataObjectKey">The data object key.</param>
        /// <returns>Returns <c>true</c> if object is successfully unlocked or <c>false</c> if it's not exist.</returns>
        public bool UnlockObject(object dataObjectKey)
        {
            lock (_lockObj)
            {
                var lockData = new Lock();
                lockData.SetExistObjectPrimaryKey(dataObjectKey);

                try
                {
                    _dataService.LoadObject(lockData);
                    lockData.SetStatus(ObjectStatus.Deleted);
                    _dataService.UpdateObject(lockData);
                    return true;
                }
                catch (CantFindDataObjectException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Unlocks all objects.
        /// TODO: Feature of deleting all objects in the database is not implemented by <see cref="IDataService"/>.
        /// </summary>
        public void UnlockAllObjects()
        {
            throw new NotImplementedException();
        }
    }
}
