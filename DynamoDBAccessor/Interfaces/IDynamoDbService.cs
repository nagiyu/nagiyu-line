using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DynamoDBAccessor.Models;

namespace DynamoDBAccessor.Interfaces
{
    public interface IDynamoDbService
    {
        Task<List<LineMessage>> GetLineMessageByUserIDAsync(string userId);

        Task AddLineMessageAsync(LineMessage lineMessage);
    }
}
