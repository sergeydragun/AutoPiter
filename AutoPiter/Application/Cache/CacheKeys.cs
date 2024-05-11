namespace AutoPiter.Application.Cache
{
    public static class CacheKeys
    {
        /// <summary>
        /// Ключ кэша для объекта инсталляции устройства
        /// </summary>
        /// <param name="instId">Id инсталляции</param>
        /// <returns></returns>
        public static string InstallationKey(Guid instId) => $"CACHE_KEY_INSTALLATION_{instId}";
        /// <summary>
        /// Ключ кэша для списка инсталляций в филиале
        /// </summary>
        /// <param name="branchId">Id филлиала</param>
        /// <returns></returns>
        public static string InstallationsListByBranchKey(Guid branchId) => $"CACHE_KEY_INSTALLATION_LIST_{branchId}";
        /// <summary>
        /// Ключ кэша для списка всех инсталляций
        /// </summary>
        /// <returns></returns>
        public static string InstallationsListBKey() => $"CACHE_KEY_INSTALLATION_LIST";
    }
}
