namespace NewPlatform.Flexberry.Services
{
    /// <summary>
    /// Class that represents information about lock.
    /// </summary>
    public struct LockData
    {
        /// <summary>
        /// Is lock acquired now?
        /// </summary>
        public bool Acquired { get; }

        /// <summary>
        /// User who acquired lock.
        /// </summary>
        public string UseName { get; }

        /// <summary>
        /// The key of the acquired lock.
        /// </summary>
        public object Key { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LockData" /> struct.
        /// </summary>
        /// <param name="lockKey">The key of the acquired lock.</param>
        /// <param name="userName">User who acquired lock.</param>
        /// <param name="acquired">Is lock acquired now?</param>
        public LockData(object lockKey, string userName, bool acquired)
        {
            Acquired = acquired;
            UseName = userName;
            Key = lockKey;
        }
    }
}