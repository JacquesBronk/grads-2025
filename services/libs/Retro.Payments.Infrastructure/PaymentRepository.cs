using MongoDB.Driver;
using Retro.Payments.Domain;
using Retro.Persistence.Mongo.Infra;

namespace Retro.Payments.Infrastructure;

public class PaymentRepository(IMongoDbContext mongoDbContext): IPaymentRepository
{
    private readonly IMongoCollection<PaymentDetail> _collection = mongoDbContext.GetCollection<PaymentDetail>("payments");
    
    public async Task<PaymentDetail> CreatePaymentAsync(PaymentDetail paymentDetail, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(paymentDetail, cancellationToken: cancellationToken);
        return paymentDetail;
    }

    public async Task<PaymentDetail> GetPaymentAsync(Guid paymentId, CancellationToken cancellationToken)
    {
        return await _collection.Find(p => p.Id == paymentId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PaymentDetail> UpdatePaymentAsync(PaymentDetail paymentDetail, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(p => p.Id == paymentDetail.Id, paymentDetail, cancellationToken: cancellationToken);
        return paymentDetail;
    }

    public async Task<PaymentDetail> UpdatePaymentStatusAsync(Guid paymentId, PaymentStatus paymentStatus, CancellationToken cancellationToken)
    {
        var update = Builders<PaymentDetail>.Update.Set(p => p.PaymentStatus, paymentStatus);
        await _collection.UpdateOneAsync(p => p.Id == paymentId, update, cancellationToken: cancellationToken);
        return await GetPaymentAsync(paymentId, cancellationToken);
    }

    public async Task<PaymentDetail> UpdatePaymentErrorAsync(Guid paymentId, string error, string errorDescription, string errorReason, string errorReasonDescription, string errorReasonCode, CancellationToken cancellationToken)
    {
        var update = Builders<PaymentDetail>.Update
            .Set(p => p.PaymentError, error)
            .Set(p => p.PaymentErrorDescription, errorDescription)
            .Set(p => p.PaymentErrorReason, errorReason)
            .Set(p => p.PaymentErrorReasonDescription, errorReasonDescription)
            .Set(p => p.PaymentErrorReasonCode, errorReasonCode);
        await _collection.UpdateOneAsync(p => p.Id == paymentId, update, cancellationToken: cancellationToken);
        return await GetPaymentAsync(paymentId, cancellationToken);
    }

    public async Task<PaymentDetail> UpdatePaymentReferenceAsync(Guid paymentId, string paymentReference, CancellationToken cancellationToken)
    {
        var update = Builders<PaymentDetail>.Update.Set(p => p.PaymentReference, paymentReference);
        await _collection.UpdateOneAsync(p => p.Id == paymentId, update, cancellationToken: cancellationToken);
        return await GetPaymentAsync(paymentId, cancellationToken);
    }

    public async Task<PaymentDetail> UpdatePaymentPaidAtAsync(Guid paymentId, DateTimeOffset paidAt, CancellationToken cancellationToken)
    {
        var update = Builders<PaymentDetail>.Update.Set(p => p.PaidAt, paidAt);
        await _collection.UpdateOneAsync(p => p.Id == paymentId, update, cancellationToken: cancellationToken);
        return await GetPaymentAsync(paymentId, cancellationToken);
    }

    public async Task<PaymentDetail> UpdatePaymentMoniesPaidAsync(Guid paymentId, decimal moniesPaid, CancellationToken cancellationToken)
    {
        var update = Builders<PaymentDetail>.Update.Set(p => p.MoniesPaid, moniesPaid);
        await _collection.UpdateOneAsync(p => p.Id == paymentId, update, cancellationToken: cancellationToken);
        return await GetPaymentAsync(paymentId, cancellationToken);
    }

    public async Task<PaymentDetail> UpdatePaymentMoniesPayableAsync(Guid paymentId, decimal moniesPayable, CancellationToken cancellationToken)
    {
        var update = Builders<PaymentDetail>.Update.Set(p => p.MoniesPayable, moniesPayable);
        await _collection.UpdateOneAsync(p => p.Id == paymentId, update, cancellationToken: cancellationToken);
        return await GetPaymentAsync(paymentId, cancellationToken);
    }

    public async Task<PaymentDetail> UpdatePaymentCurrencyAsync(Guid paymentId, string currency, CancellationToken cancellationToken)
    {
        var update = Builders<PaymentDetail>.Update.Set(p => p.Currency, currency);
        await _collection.UpdateOneAsync(p => p.Id == paymentId, update, cancellationToken: cancellationToken);
        return await GetPaymentAsync(paymentId, cancellationToken);
    }

    public async Task<PaymentDetail> UpdatePaymentMethodAsync(Guid paymentId, string paymentMethod, CancellationToken cancellationToken)
    {
        var update = Builders<PaymentDetail>.Update.Set(p => p.PaymentMethod, paymentMethod);
        await _collection.UpdateOneAsync(p => p.Id == paymentId, update, cancellationToken: cancellationToken);
        return await GetPaymentAsync(paymentId, cancellationToken);
    }
}