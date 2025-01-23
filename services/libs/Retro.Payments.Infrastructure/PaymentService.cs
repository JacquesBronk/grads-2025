using Retro.Payments.Contracts.Request;
using Retro.Payments.Contracts.Response;
using Retro.Payments.Domain;

namespace Retro.Payments.Infrastructure;

public class PaymentService(IPaymentRepository repository): IPaymentService
{
    public async Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest createPaymentRequest, CancellationToken cancellationToken)
    {
        var paymentDetail = new PaymentDetail
        {
            Id = Guid.NewGuid(),
            PaymentStatus = PaymentStatus.Pending,
            MoniesPayable = createPaymentRequest.MoniesPayable,
            Currency = createPaymentRequest.Currency,
            PaymentMethod = createPaymentRequest.PaymentMethod
        };
        await repository.CreatePaymentAsync(paymentDetail, cancellationToken);
  
        return new PaymentResponse(
            paymentDetail.Id,
            createPaymentRequest.OrderId,
            createPaymentRequest.UserId,
            createPaymentRequest.MoniesPayable,
            createPaymentRequest.MoniesPaid,
            createPaymentRequest.Currency,
            createPaymentRequest.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            createPaymentRequest.CreatedAt,
            createPaymentRequest.UpdatedAt,
            createPaymentRequest.PaidAt,
            createPaymentRequest.PaymentReference,
            createPaymentRequest.PaymentError,
            createPaymentRequest.PaymentErrorDescription,
            createPaymentRequest.PaymentErrorReason,
            createPaymentRequest.PaymentErrorReasonDescription,
            createPaymentRequest.PaymentErrorReasonCode
        );
    }

    public async Task<PaymentResponse> GetPaymentAsync(Guid paymentId, CancellationToken cancellationToken)
    {
        var paymentDetail = await repository.GetPaymentAsync(paymentId, cancellationToken);
        return new PaymentResponse(
            paymentDetail.Id,
            paymentDetail.OrderId,
            paymentDetail.UserId,
            paymentDetail.MoniesPayable,
            paymentDetail.MoniesPaid,
            paymentDetail.Currency,
            paymentDetail.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            paymentDetail.CreatedAt,
            paymentDetail.UpdatedAt,
            paymentDetail.PaidAt,
            paymentDetail.PaymentReference,
            paymentDetail.PaymentError,
            paymentDetail.PaymentErrorDescription,
            paymentDetail.PaymentErrorReason,
            paymentDetail.PaymentErrorReasonDescription,
            paymentDetail.PaymentErrorReasonCode
        );
    }

    public async Task<PaymentResponse> UpdatePaymentStatusAsync(Guid paymentId, PaymentStatus paymentStatus, CancellationToken cancellationToken)
    {
        var paymentDetail = await repository.UpdatePaymentStatusAsync(paymentId, paymentStatus, cancellationToken);
        return new PaymentResponse(
            paymentDetail.Id,
            paymentDetail.OrderId,
            paymentDetail.UserId,
            paymentDetail.MoniesPayable,
            paymentDetail.MoniesPaid,
            paymentDetail.Currency,
            paymentDetail.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            paymentDetail.CreatedAt,
            paymentDetail.UpdatedAt,
            paymentDetail.PaidAt,
            paymentDetail.PaymentReference,
            paymentDetail.PaymentError,
            paymentDetail.PaymentErrorDescription,
            paymentDetail.PaymentErrorReason,
            paymentDetail.PaymentErrorReasonDescription,
            paymentDetail.PaymentErrorReasonCode
        );
    }

    public async Task<PaymentResponse> UpdatePaymentErrorAsync(Guid paymentId, string error, string errorDescription, string errorReason, string errorReasonDescription, string errorReasonCode, CancellationToken cancellationToken)
    {
        var paymentDetail = await repository.UpdatePaymentErrorAsync(paymentId, error, errorDescription, errorReason, errorReasonDescription, errorReasonCode, cancellationToken);
        return new PaymentResponse(
            paymentDetail.Id,
            paymentDetail.OrderId,
            paymentDetail.UserId,
            paymentDetail.MoniesPayable,
            paymentDetail.MoniesPaid,
            paymentDetail.Currency,
            paymentDetail.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            paymentDetail.CreatedAt,
            paymentDetail.UpdatedAt,
            paymentDetail.PaidAt,
            paymentDetail.PaymentReference,
            paymentDetail.PaymentError,
            paymentDetail.PaymentErrorDescription,
            paymentDetail.PaymentErrorReason,
            paymentDetail.PaymentErrorReasonDescription,
            paymentDetail.PaymentErrorReasonCode
        );
    }

    public async Task<PaymentResponse> UpdatePaymentReferenceAsync(Guid paymentId, string paymentReference, CancellationToken cancellationToken)
    {
        var paymentDetail = await repository.UpdatePaymentReferenceAsync(paymentId, paymentReference, cancellationToken);
        return new PaymentResponse(
            paymentDetail.Id,
            paymentDetail.OrderId,
            paymentDetail.UserId,
            paymentDetail.MoniesPayable,
            paymentDetail.MoniesPaid,
            paymentDetail.Currency,
            paymentDetail.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            paymentDetail.CreatedAt,
            paymentDetail.UpdatedAt,
            paymentDetail.PaidAt,
            paymentDetail.PaymentReference,
            paymentDetail.PaymentError,
            paymentDetail.PaymentErrorDescription,
            paymentDetail.PaymentErrorReason,
            paymentDetail.PaymentErrorReasonDescription,
            paymentDetail.PaymentErrorReasonCode
        );
    }

    public async Task<PaymentResponse> UpdatePaymentPaidAtAsync(Guid paymentId, DateTimeOffset paidAt, CancellationToken cancellationToken)
    {
        var paymentDetail = await repository.UpdatePaymentPaidAtAsync(paymentId, paidAt, cancellationToken);
        return new PaymentResponse(
            paymentDetail.Id,
            paymentDetail.OrderId,
            paymentDetail.UserId,
            paymentDetail.MoniesPayable,
            paymentDetail.MoniesPaid,
            paymentDetail.Currency,
            paymentDetail.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            paymentDetail.CreatedAt,
            paymentDetail.UpdatedAt,
            paymentDetail.PaidAt,
            paymentDetail.PaymentReference,
            paymentDetail.PaymentError,
            paymentDetail.PaymentErrorDescription,
            paymentDetail.PaymentErrorReason,
            paymentDetail.PaymentErrorReasonDescription,
            paymentDetail.PaymentErrorReasonCode
        );
    }

    public async Task<PaymentResponse> UpdatePaymentMoniesPaidAsync(Guid paymentId, decimal moniesPaid, CancellationToken cancellationToken)
    {
        var paymentDetail = await repository.UpdatePaymentMoniesPaidAsync(paymentId, moniesPaid, cancellationToken);
        return new PaymentResponse(
            paymentDetail.Id,
            paymentDetail.OrderId,
            paymentDetail.UserId,
            paymentDetail.MoniesPayable,
            paymentDetail.MoniesPaid,
            paymentDetail.Currency,
            paymentDetail.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            paymentDetail.CreatedAt,
            paymentDetail.UpdatedAt,
            paymentDetail.PaidAt,
            paymentDetail.PaymentReference,
            paymentDetail.PaymentError,
            paymentDetail.PaymentErrorDescription,
            paymentDetail.PaymentErrorReason,
            paymentDetail.PaymentErrorReasonDescription,
            paymentDetail.PaymentErrorReasonCode
        );
    }

    public async Task<PaymentResponse> UpdatePaymentMoniesPayableAsync(Guid paymentId, decimal moniesPayable, CancellationToken cancellationToken)
    {
        var paymentDetail = await repository.UpdatePaymentMoniesPayableAsync(paymentId, moniesPayable, cancellationToken);
        return new PaymentResponse(
            paymentDetail.Id,
            paymentDetail.OrderId,
            paymentDetail.UserId,
            paymentDetail.MoniesPayable,
            paymentDetail.MoniesPaid,
            paymentDetail.Currency,
            paymentDetail.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            paymentDetail.CreatedAt,
            paymentDetail.UpdatedAt,
            paymentDetail.PaidAt,
            paymentDetail.PaymentReference,
            paymentDetail.PaymentError,
            paymentDetail.PaymentErrorDescription,
            paymentDetail.PaymentErrorReason,
            paymentDetail.PaymentErrorReasonDescription,
            paymentDetail.PaymentErrorReasonCode
        );
    }

    public async Task<PaymentResponse> UpdatePaymentCurrencyAsync(Guid paymentId, string currency, CancellationToken cancellationToken)
    {
        var paymentDetail = await repository.UpdatePaymentCurrencyAsync(paymentId, currency, cancellationToken);
        return new PaymentResponse(
            paymentDetail.Id,
            paymentDetail.OrderId,
            paymentDetail.UserId,
            paymentDetail.MoniesPayable,
            paymentDetail.MoniesPaid,
            paymentDetail.Currency,
            paymentDetail.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            paymentDetail.CreatedAt,
            paymentDetail.UpdatedAt,
            paymentDetail.PaidAt,
            paymentDetail.PaymentReference,
            paymentDetail.PaymentError,
            paymentDetail.PaymentErrorDescription,
            paymentDetail.PaymentErrorReason,
            paymentDetail.PaymentErrorReasonDescription,
            paymentDetail.PaymentErrorReasonCode
        );
    }

    public async Task<PaymentResponse> UpdatePaymentMethodAsync(Guid paymentId, string paymentMethod, CancellationToken cancellationToken)
    {
        var paymentDetail = await repository.UpdatePaymentMethodAsync(paymentId, paymentMethod, cancellationToken);
        return new PaymentResponse(
            paymentDetail.Id,
            paymentDetail.OrderId,
            paymentDetail.UserId,
            paymentDetail.MoniesPayable,
            paymentDetail.MoniesPaid,
            paymentDetail.Currency,
            paymentDetail.PaymentMethod,
            paymentDetail.PaymentStatus.ToString(),
            paymentDetail.CreatedAt,
            paymentDetail.UpdatedAt,
            paymentDetail.PaidAt,
            paymentDetail.PaymentReference,
            paymentDetail.PaymentError,
            paymentDetail.PaymentErrorDescription,
            paymentDetail.PaymentErrorReason,
            paymentDetail.PaymentErrorReasonDescription,
            paymentDetail.PaymentErrorReasonCode
        );
    }
}