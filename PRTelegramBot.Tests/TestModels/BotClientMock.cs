using Moq;
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.TestModels
{
    /// <summary>
    /// Captures the requests a service sends to Telegram.
    /// </summary>
    /// <remarks>
    /// Every extension method of <see cref="ITelegramBotClient"/> funnels into
    /// <c>SendRequest</c>, so intercepting that one method is enough to see the request
    /// object a service built — and to assert how the options were mapped onto it.
    /// </remarks>
    public class BotClientMock
    {
        private readonly List<object> requests = new();

        /// <summary>
        /// The mocked client to hand to the service under test.
        /// </summary>
        public Mock<ITelegramBotClient> Client { get; } = new();

        /// <summary>
        /// A bot context whose <see cref="IBotContext.BotClient"/> is the mocked client.
        /// </summary>
        public Mock<IBotContext> Context { get; } = new();

        /// <summary>
        /// Every request that was sent, in order.
        /// </summary>
        public IReadOnlyList<object> Requests => requests;

        /// <summary>
        /// Number of requests that were sent.
        /// </summary>
        public int SentCount => requests.Count;

        public BotClientMock(Update? update = null)
        {
            Client
                .Setup(x => x.SendRequest(It.IsAny<IRequest<Message>>(), It.IsAny<CancellationToken>()))
                .Callback<IRequest<Message>, CancellationToken>((request, _) => requests.Add(request))
                .ReturnsAsync(new Message { Id = 1, Chat = new Chat { Id = 1 } });

            Client
                .Setup(x => x.SendRequest(It.IsAny<IRequest<Message[]>>(), It.IsAny<CancellationToken>()))
                .Callback<IRequest<Message[]>, CancellationToken>((request, _) => requests.Add(request))
                .ReturnsAsync(new[] { new Message { Id = 1, Chat = new Chat { Id = 1 } } });

            Client
                .Setup(x => x.SendRequest(It.IsAny<IRequest<bool>>(), It.IsAny<CancellationToken>()))
                .Callback<IRequest<bool>, CancellationToken>((request, _) => requests.Add(request))
                .ReturnsAsync(true);

            Client
                .Setup(x => x.SendRequest(It.IsAny<IRequest<MessageId>>(), It.IsAny<CancellationToken>()))
                .Callback<IRequest<MessageId>, CancellationToken>((request, _) => requests.Add(request))
                .ReturnsAsync(new MessageId { Id = 1 });

            Context.SetupGet(x => x.BotClient).Returns(Client.Object);

            if (update is not null)
                Context.SetupGet(x => x.Update).Returns(update);
        }

        /// <summary>
        /// The single request that was sent, cast to the expected request type.
        /// </summary>
        /// <typeparam name="TRequest">Expected request type.</typeparam>
        /// <returns>The request that was sent.</returns>
        public TRequest Single<TRequest>() where TRequest : class
        {
            if (requests.Count != 1)
                throw new InvalidOperationException($"Expected exactly one request, but {requests.Count} were sent.");

            return requests[0] as TRequest
                ?? throw new InvalidOperationException($"The request was {requests[0].GetType().Name}, not {typeof(TRequest).Name}.");
        }

        /// <summary>
        /// The request at the given position, cast to the expected request type.
        /// </summary>
        /// <typeparam name="TRequest">Expected request type.</typeparam>
        /// <param name="index">Position of the request.</param>
        /// <returns>The request that was sent.</returns>
        public TRequest At<TRequest>(int index) where TRequest : class
        {
            return requests[index] as TRequest
                ?? throw new InvalidOperationException($"Request {index} was {requests[index].GetType().Name}, not {typeof(TRequest).Name}.");
        }
    }
}
