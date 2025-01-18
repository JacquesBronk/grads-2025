using Retro.Payments.Domain;

namespace Retro.Payments.Infrastructure;

public interface IPaymentRepository
{
    Task<PaymentDetail> CreatePaymentAsync(PaymentDetail paymentDetail, CancellationToken cancellationToken);
    Task<PaymentDetail> GetPaymentAsync(Guid paymentId, CancellationToken cancellationToken);
    Task<PaymentDetail> UpdatePaymentAsync(PaymentDetail paymentDetail, CancellationToken cancellationToken);
    Task<PaymentDetail> UpdatePaymentStatusAsync(Guid paymentId, PaymentStatus paymentStatus, CancellationToken cancellationToken);
    Task<PaymentDetail> UpdatePaymentErrorAsync(Guid paymentId, string error, string errorDescription, string errorReason, string errorReasonDescription, string errorReasonCode, CancellationToken cancellationToken);
    Task<PaymentDetail> UpdatePaymentReferenceAsync(Guid paymentId, string paymentReference, CancellationToken cancellationToken);
    Task<PaymentDetail> UpdatePaymentPaidAtAsync(Guid paymentId, DateTimeOffset paidAt, CancellationToken cancellationToken);
    Task<PaymentDetail> UpdatePaymentMoniesPaidAsync(Guid paymentId, decimal moniesPaid, CancellationToken cancellationToken);
    Task<PaymentDetail> UpdatePaymentMoniesPayableAsync(Guid paymentId, decimal moniesPayable, CancellationToken cancellationToken);
    Task<PaymentDetail> UpdatePaymentCurrencyAsync(Guid paymentId, string currency, CancellationToken cancellationToken);
    Task<PaymentDetail> UpdatePaymentMethodAsync(Guid paymentId, string paymentMethod, CancellationToken cancellationToken);
}