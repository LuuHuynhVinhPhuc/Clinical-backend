using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicalBackend.Persistence.Interceptors
{
    public class CommandInterceptor : DbCommandInterceptor
    {
        private readonly ILogger<CommandInterceptor> _logger;

        public CommandInterceptor(ILogger<CommandInterceptor> logger)
        {
            _logger = logger;
        }

        public override async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Executing command: {command.CommandText}");
            return await base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override async ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Executing command: {command.CommandText}");
            return await base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override async ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Executing command: {command.CommandText}");
            return await base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
        }
    }
}
