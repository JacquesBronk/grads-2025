namespace Retro.Payments.Contracts.Request;

public record CreatePaymentRequest(
    Guid Id,
    string OrderId,
    string UserId,
    decimal MoniesPayable,
    decimal MoniesPaid,
    string Currency,
    string PaymentMethod,
    string PaymentStatus,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    DateTimeOffset? PaidAt,
    string? PaymentReference,
    string? PaymentError,
    string? PaymentErrorDescription,
    string? PaymentErrorReason,
    string? PaymentErrorReasonDescription,
    string? PaymentErrorReasonCode
);