using Microsoft.Extensions.DependencyInjection;
using System.Transactions;
using LabDBEntity.DAL.Settings;
using LabDBEntity.DAL;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace LabDBEntity.Services
{
    public class UpdateService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UpdateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using (var scope = _serviceScopeFactory.CreateAsyncScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<LabDbContext>())
            {
                var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();  

                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var students = dbContext.Students.ToList();
                        foreach (var student in students)
                        {
                            await studentService.CreateAsync(student); 
                        }
                        transaction.Complete();
                    }
                    catch (Exception ex)
                    {
                        transaction.Dispose();
                        throw;
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }


}
