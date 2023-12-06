using Microsoft.AspNetCore.Mvc;
using Order.API.Services;

namespace Order.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        #region Variables
        private readonly IOrderService _orderService;
        #endregion
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="orderService">IOrderService instance.</param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// The method  that returns all orders from the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Models.Order>> Get()
        {
            try
            {
                var orders = await _orderService.GetOrderItems();

                if (orders.Count() == 0)
                    return NotFound("Orders not found.");

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// The method that returns the order by guidId from the database.
        /// </summary>
        /// <param name="orderGuid">GuidID parameter.</param>
        /// <returns></returns>
        [HttpGet("{orderGuid}")]
        public async Task<ActionResult<Models.Order>> Get(Guid orderGuid)
        {
            try
            {
                var order = await _orderService.GetById(orderGuid);

                if (order == null)
                    return NotFound($"Order with Id = '{orderGuid}' not found.");

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// The method that adds new order in the database.
        /// </summary>
        /// <param name="item">Order instance.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Models.Order>> Post([FromBody] Models.Order item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var order = await _orderService.Add(item);
                return CreatedAtAction("Get", new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// The method that updates the order in the database.
        /// </summary>
        /// <param name="item">Order instance.</param>
        /// <param name="orderGuid">GuidID parameter.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Models.Order>> Put([FromBody] Models.Order item, Guid orderGuid)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var orderUpdate = await _orderService.GetById(orderGuid);

                if (orderUpdate == null)
                    return NotFound($"Order with Id = '{orderGuid}' not found.");

                orderUpdate.Description = item.Description;
                orderUpdate.Price = item.Price;

                await _orderService.Update(orderUpdate);
                return Content($"Order with Id = '{orderGuid}' is updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// The method that deletes the order from the database.
        /// </summary>
        /// <param name="orderGuid">GuidID parameter.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<Models.Order>> Delete(Guid orderGuid)
        {
            try
            {
                var order = await _orderService.GetById(orderGuid);

                if (order == null)
                    return NotFound($"Order with Id = '{orderGuid}' not found.");

                _orderService.Delete(orderGuid);
                return Content($"Order with Id = '{orderGuid}' was deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
