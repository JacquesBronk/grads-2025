using Retro.Stock.Domain;

namespace Retro.Stock.Contracts.Request;

public record GetByConditionRequest(int PageNumber = 1, int PageSize = 10,StockCondition Condition = StockCondition.New);