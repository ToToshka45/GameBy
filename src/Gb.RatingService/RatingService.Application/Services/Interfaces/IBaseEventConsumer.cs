using Microsoft.Extensions.Hosting;

namespace RatingService.Application.Services.Interfaces
{
    public interface IBaseEventConsumer: IHostedService, IAsyncDisposable
    {
    }
}
