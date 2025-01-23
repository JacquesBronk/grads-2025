using Retro.Payments.Contracts.Request;
using Retro.Payments.Contracts.Response;
using Retro.Payments.Domain;

namespace Retro.Payments.Infrastructure;

public interface IPaymentService
{
    Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest createPaymentRequest, CancellationToken cancellationToken);
    Task<PaymentResponse> GetPaymentAsync(Guid paymentId, CancellationToken cancellationToken);
    Task<PaymentResponse> UpdatePaymentStatusAsync(Guid paymentId, PaymentStatus paymentStatus, CancellationToken cancellationToken);
    Task<PaymentResponse> UpdatePaymentErrorAsync(Guid paymentId, string error, string errorDescription, string errorReason, string errorReasonDescription, string errorReasonCode, CancellationToken cancellationToken);
    Task<PaymentResponse> UpdatePaymentReferenceAsync(Guid paymentId, string paymentReference, CancellationToken cancellationToken);
    Task<PaymentResponse> UpdatePaymentPaidAtAsync(Guid paymentId, DateTimeOffset paidAt, CancellationToken cancellationToken);
    Task<PaymentResponse> UpdatePaymentMoniesPaidAsync(Guid paymentId, decimal moniesPaid, CancellationToken cancellationToken);
    Task<PaymentResponse> UpdatePaymentMoniesPayableAsync(Guid paymentId, decimal moniesPayable, CancellationToken cancellationToken);
    Task<PaymentResponse> UpdatePaymentCurrencyAsync(Guid paymentId, string currency, CancellationToken cancellationToken);
    Task<PaymentResponse> UpdatePaymentMethodAsync(Guid paymentId, string paymentMethod, CancellationToken cancellationToken);
}