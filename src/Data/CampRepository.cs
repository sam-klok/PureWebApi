using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PureWebApi.Data.Entities;

namespace PureWebApiCore.Data
{
  public class CampRepository : ICampRepository
  {
    private readonly CampContext _ctx;
    private readonly ILogger<CampRepository> _logger;

    public CampRepository(CampContext context, ILogger<CampRepository> logger)
    {
      _ctx = context;
      _logger = logger;
    }

    public void Add<T>(T entity) where T : class 
    {
      _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
      _ctx.Add(entity);
    }

    public void Delete<T>(T entity) where T: class
    {
      _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
      _ctx.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
      _logger.LogInformation($"Attempitng to save the changes in the context");

      // Only return success if at least one row was changed
      return (await _ctx.SaveChangesAsync()) > 0;
    }

    public async Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false)
    {
      _logger.LogInformation($"Getting all Camps");

      IQueryable<Camp> query = _ctx.Camps
          .Include(c => c.Location);

      if (includeTalks)
      {
        query = query
          .Include(c => c.Talks)
          .ThenInclude(t => t.Speaker);
      }

      // Order It
      query = query.OrderByDescending(c => c.EventDate)
        .Where(c => c.EventDate.Date == dateTime.Date);

      return await query.ToArrayAsync();
    }

    public async Task<Camp[]> GetAllCampsAsync(bool includeTalks = false)
    {
      _logger.LogInformation($"Getting all Camps");

      IQueryable<Camp> query = _ctx.Camps
          .Include(c => c.Location);

      if (includeTalks)
      {
            query = query
                .Include(d => d.Talks)
                .ThenInclude(t => t.Speaker);
      }

      // Order It
      query = query.OrderByDescending(c => c.EventDate);

      return await query.ToArrayAsync();
    }

    public async Task<Camp> GetCampAsync(string moniker, bool includeTalks = false)
    {
      _logger.LogInformation($"Getting a Camp for {moniker}");

      IQueryable<Camp> query = _ctx.Camps
          .Include(c => c.Location);

      if (includeTalks)
      {
        query = query.Include(c => c.Talks)
          .ThenInclude(t => t.Speaker);
      }

      // Query It
      query = query.Where(c => c.Moniker == moniker);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Talk[]> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false)
    {
      _logger.LogInformation($"Getting all Talks for a Camp");

      IQueryable<Talk> query = _ctx.Talks;

      if (includeSpeakers)
      {
        query = query
          .Include(t => t.Speaker);
      }

      // Add Query
      query = query
        .Where(t => t.Camp.Moniker == moniker)
        .OrderByDescending(t => t.Title);

      return await query.ToArrayAsync();
    }

    public async Task<Talk> GetTalkByMonikerAsync(string moniker, int talkId, bool includeSpeakers = false)
    {
      _logger.LogInformation($"Getting all Talks for a Camp");

      IQueryable<Talk> query = _ctx.Talks;

      if (includeSpeakers)
      {
        query = query
          .Include(t => t.Speaker);
      }

      // Add Query
      query = query
        .Where(t => t.TalkId == talkId && t.Camp.Moniker == moniker);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Speaker[]> GetSpeakersByMonikerAsync(string moniker)
    {
      _logger.LogInformation($"Getting all Speakers for a Camp");

      IQueryable<Speaker> query = _ctx.Talks
        .Where(t => t.Camp.Moniker == moniker)
        .Select(t => t.Speaker)
        .Where(s => s != null)
        .OrderBy(s => s.LastName)
        .Distinct();

      return await query.ToArrayAsync();
    }

    public async Task<Speaker[]> GetAllSpeakersAsync()
    {
      _logger.LogInformation($"Getting Speaker");

      var query = _ctx.Speakers
        .OrderBy(t => t.LastName);

      return await query.ToArrayAsync();
    }


    public async Task<Speaker> GetSpeakerAsync(int speakerId)
    {
      _logger.LogInformation($"Getting Speaker");

      var query = _ctx.Speakers
        .Where(t => t.SpeakerId == speakerId);

      return await query.FirstOrDefaultAsync();
    }


        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public void AddOrder(Order newOrder)
        {
            // convert new products to lookup of product 
            // we don't want every order to automatically try and add 
            // the same product to the db multiple times.
            foreach (var item in newOrder.Items)
            {
                //the products visible to users should exist in the database anyway
                item.Product = _ctx.Products.Find(item.Product.Id);
            }

            AddEntity(newOrder);
        }

        /// <summary>
        /// Order refers to OrderItem refers to Order. 
        /// This would then result in self referencing errors. 
        /// To avoid this handle reference loop handling if you don't
        /// want to throw reference loop exceptions. 
        /// Let us return not just the order but also the related items
        /// and order item products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .ToList();
            }
            else
            {
                return _ctx.Orders
                .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string userName, bool includeItem)
        {
            if (includeItem)
            {
                return _ctx.Orders
                  .Where(o => o.User.UserName == userName)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .ToList();
            }
            else
            {
                return _ctx.Orders
                  .Where(o => o.User.UserName == userName)
                .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("GetAllProducts was called");
            return _ctx.Products.OrderBy(p => p.Title).ToList();
        }

        public Order GetOrderById(string userName, int id)
        {
            return _ctx.Orders
              .Include(o => o.Items)
              .ThenInclude(oi => oi.Product)
              .Where(o => o.Id == id && o.User.UserName == userName)
              .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products.Where(p => p.Category == category).ToList();
        }

        public bool SaveAll()
        {
            _ctx.SaveChanges();
            return true;
        }
    }
}
