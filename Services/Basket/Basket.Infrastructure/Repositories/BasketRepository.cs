using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository(IDistributedCache _cache, ILogger<BasketRepository> _logger) : IBasketRepository
    {
        private const int ExpirationInDays = 7;
        public async Task<ShoppingCart?> GetBasket(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    throw new ArgumentNullException("User Name Is Required", nameof(userName));
                var key = GetBasketKey(userName);
                var basket = await _cache.GetStringAsync(key);
                if (string.IsNullOrEmpty(basket))
                {
                    _logger.LogError("Basket Not Found For User : {userName}", userName);
                    return null;
                }
                _logger.LogInformation("Basket Retrieved For User : {userName}", userName);
                return JsonSerializer.Deserialize<ShoppingCart>(basket);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured While Retrieving Basket For User : {userName}", userName);
                throw;
            }
        }
        public async Task<ShoppingCart?> UpdateBasket(ShoppingCart shoppingCart)
        {
            try
            {
                if (shoppingCart == null) throw new ArgumentNullException(nameof(shoppingCart));
                if (string.IsNullOrEmpty(shoppingCart.UserName))
                    throw new ArgumentNullException("User Name IS Required", nameof(shoppingCart.UserName));
                var key = GetBasketKey(shoppingCart.UserName);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(ExpirationInDays)
                };
                var serializeBasket = JsonSerializer.Serialize(shoppingCart);
                await _cache.SetStringAsync(key, serializeBasket, options);
                _logger.LogInformation("Basket Updated For USer : {UserName}", shoppingCart.UserName);
                return await GetBasket(key);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured While Updating Basket For User : {userName}", shoppingCart.UserName);
                throw;
            }
        }
        public async Task<bool> DeleteBasket(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("User Name IS Required", nameof(userName));
                var key = GetBasketKey(userName);
                var basket = await _cache.GetStringAsync(key);
                if (string.IsNullOrEmpty(basket))
                {
                    _logger.LogError("No Baskets Found For User : {UserName}", userName);
                    return false;
                }
                await _cache.RemoveAsync(key);
                _logger.LogInformation("Basket deleted for user {userName}", userName);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured While Deleteing Basket For User : {userName}", userName);
                throw;
            }
        }
        private static string GetBasketKey(string userName)
        {
            return $"basket:{userName}";
        }
    }
}
