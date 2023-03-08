using Ddd.Domain;
using Ddd.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sam.Domain;
using Sam.Infrastructure.Others;
using System.Collections.Concurrent;

namespace multi_threadings.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase {
    private readonly ILogger<ProductController> _logger;
    private readonly EcomShopContext _context;
    private readonly IConfiguration _config;

    private readonly IServiceProvider _sp;
    private readonly DbContextOptions<EcomShopContext> _options;
    //private readonly SaleContext _saleContext;

    // IEnumerable and IQueryable?
    // IEnumerable: iterable collection in-memory items
    // IQueryable: iterable collection of a queryable remote data source, like database
    //   created from LinQ and can be executed on that data source
    public ProductController(
        ILogger<ProductController> logger,
        EcomShopContext dbContext,
        IConfiguration config,
        IServiceProvider sp
        ) {
        _logger = logger;
        _context = dbContext;
        _config = config;
        _sp = sp;
        _options = new DbContextOptionsBuilder<EcomShopContext>()
                    .UseLazyLoadingProxies()
                    .UseSqlServer(_config.GetConnectionString("EcomShopDatabase")).Options;
    }

    //// GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
    //[HttpGet]
    //public IEnumerable<string> Get()
    //{
    //    return;
    //}



    







    [HttpGet("place_order")]
    public async Task<ActionResult<ICollection<Order>>> PlaceOrder() {
        var newOrderNumber = 10426;

        using var unitOfWork = new UnitOfWork(new EcomShopContext(_options));
        var product = new Product { ProductCode = "apl", ProductName = "Apple", BuyPrice = 1.99m };
        unitOfWork.ProductRepository.Insert(product);

        var order = new Order { OrderNumber = newOrderNumber, CustomerNumber = 103 };
        unitOfWork.OrderRepository.Insert(order);

        var orderDetail = new OrderDetail { OrderNumber = newOrderNumber, ProductCode = "apl" };
        unitOfWork.OrderDetailRepository.Insert(orderDetail);
        unitOfWork.Save();

        var res = await _context.Orders
            .Where(o => o.OrderNumber == newOrderNumber)
            .Include(o => o.OrderDetails)
            .ToListAsync();
        return Ok(res);
    }

    [HttpGet("place_order_cleanup")]
    public ActionResult PlaceOrderCleanUp() {
        _context.OrderDetails.Remove(_context.OrderDetails.Find(10426, "apl"));
        _context.Products.Remove(_context.Products.Find("apl"));
        _context.Orders.Remove(_context.Orders.Find(10426));
        _context.SaveChanges();
        return Ok();
    }









    //
    [HttpGet("do_lazy")]
    public async Task<ActionResult<ICollection<Order>>> DoLazyAsync() {
        // customer of that order is not loaded yet
        var res = await _context.Orders
            .Where(o => o.OrderNumber == 10100)
            .ToListAsync();

        // lazied data won't be loaded unless you access them
        Console.WriteLine(res[0].Customer.CustomerName);

        return Ok(res);
    }

    //
    [HttpGet("do_eager")]
    public async Task<ActionResult<ICollection<Order>>> DoEagerAsync() {
        // load related data together with the main entity
        // on one single trip
        var res = await _context.Orders
            .Where(o => o.OrderNumber == 10100)
            .Include(o => o.Customer)
            .ToListAsync();
        Console.WriteLine(res[0]?.Customer?.CustomerName);

        //
        return Ok(res);
    }


    //
    [HttpGet("do_explicit")]
    public async Task<ActionResult<ICollection<Customer>>> DoExplicitAsync() {
        // like eager loading but related data load at a later time...
        // customer loaded
        var customer = _context.Customers
            .Where(c => c.CustomerNumber == 103)
            .FirstOrDefault();

        // load the related data,
        // not waiting until serialization
        _context.Entry(customer)
            .Collection(c => c.Orders)
            .Load();

        // 
        return Ok(customer.Orders);
    }














    //
    [HttpGet("check concur")]
    public void UpdateConcurrency() {
        // set initial value to make sure concur error to always reproductible
        _context.Database.ExecuteSqlRaw("UPDATE Products SET BuyPrice = 49 WHERE ProductCode = 'S10_1678'");

        // try to update data
        var prod = _context.Products.FirstOrDefault(p => p.ProductCode == "S10_1678");
        prod!.BuyPrice = 50m;

        // simulate a concurrency conflict
        _context.Database.ExecuteSqlRaw("UPDATE Products SET BuyPrice = 51 WHERE ProductCode = 'S10_1678'");

        // throw ex
        _context.SaveChanges();
    }



    // This can happen if a second operation is started on this context instance before a previous operation completed. 
    [HttpGet("get custom")]
    public async Task<IEnumerable<Product>> GetCustom() {
        var summ = new ConcurrentBag<Product>();

        var cts = new CancellationTokenSource();
        CancellationToken ct = cts.Token;


        // Start a task to fetch data from source 1
        var task1 = Task.Run(async () => {
            EcomShopContext db = _context;

            
            var result = await db.Products.Take(2).ToListAsync();
            Console.WriteLine(result[0].ProductCode);
            Console.WriteLine(result[1].ProductCode);
            foreach (var item in result)
                summ.Add(item);
        },
        cts.Token);

        // Start a task to fetch data from source 2
        // Task: ko tao ra thread nhung lam cho .NET runtime tao ra thread moi
        var task2 = Task.Run(async () => {
            EcomShopContext db = _context;

            var result = await db.Products.Skip(2).Take(2).ToListAsync();
            Console.WriteLine(result[0].ProductCode);
            Console.WriteLine(result[1].ProductCode);
            foreach (var item in result)
                summ.Add(item);
        }, cts.Token);

        // cancelled task
        //var task3 = Task.Run(async () => {
        //    ct.ThrowIfCancellationRequested();
        //    bool moreToDo = true;
        //    while (moreToDo) {
        //        // Poll on this property if you have to do
        //        // other cleanup before throwing.
        //        if (ct.IsCancellationRequested) {
        //            // Clean up here, then...
        //            ct.ThrowIfCancellationRequested();
        //        }
        //    }

        //}, cts.Token);
        //cts.Cancel();


        // Wait for both tasks to complete
        await Task.WhenAll(task1
            , task2
            //, task3
            );

        return summ;
    }









    [ResponseCache(Duration = 60, VaryByHeader = "User-Agent")]
    [HttpGet]
    public IEnumerable<Product> Get() {
        return _context.Products.Take(10).ToList();
    }

    [HttpGet("{productCode}")]
    public async Task<ActionResult<Product>> Get(string productCode) {
        Product? res = await _context.Products.FindAsync(productCode);
        if (res == null)
            return NotFound();
        else
            return res;
    }

    [HttpPut]
    public async Task<ActionResult<Product>> Put(Product product) {
        Product? _findRes = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductCode == product.ProductCode);
        if (_findRes != null) {
            _findRes.ProductName = product.ProductName;
            _findRes.ProductScale = product.ProductScale;
            _findRes.ProductVendor = product.ProductVendor;
            _findRes.ProductDescription = product.ProductDescription;

            _context.Products.Update(_findRes);
            await _context.SaveChangesAsync();
            return Ok();
        }
        else {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Created("", product);
        }
    }



    [HttpDelete("{productCode}")]
    public async Task<ActionResult<Product>> Delete(string productCode) {
        Product? _findRes = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductCode == productCode);
        if (_findRes != null) {
            _context.Products.Remove(_findRes);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
        else {
            return NotFound("Not found");
        }
    }
}
