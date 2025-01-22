using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retro.Payments.Domain;

public class PaymentDetail
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    public string OrderId { get; set; }
    public string UserId { get; set; }
    public decimal MoniesPayable { get; set; }
    public decimal MoniesPaid { get; set; }
    public string Currency { get; set; }
    public string PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? PaidAt { get; set; }
    public string? PaymentReference { get; set; }
    public string? PaymentError { get; set; }
    public string? PaymentErrorDescription { get; set; }
    public string? PaymentErrorReason { get; set; }
    public string? PaymentErrorReasonDescription { get; set; }
    public string? PaymentErrorReasonCode { get; set; }
}