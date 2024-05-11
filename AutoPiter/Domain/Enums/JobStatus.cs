namespace AutoPiter.Domain.Statuses
{
    /// <summary>
    /// Статус печати. 0 - Печать с ошибками, 1 - Успешная печать
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// Печать с ошибками
        /// </summary>
        Failed = 0,
        /// <summary>
        /// Успешная печать
        /// </summary>
        Success = 1,    
    }
}
