using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gas_station.Models;
using Npgsql;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace gas_station.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly UserManager<User> _userManager;

        public OrdersController(DBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        public class order : Order
        {
            public string UserName { get; set; }
            public string StationId { get; set; }

        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(order model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            // прописываем соединение с базой
            NpgsqlConnection conn = new("Server=127.0.0.1;Port=5432;Database=gas_station;User Id=postgres;Password=123;");
            conn.Open();

            //создаем новую команду для получения данных из базы (прописываем параметры и синтаксис)
            NpgsqlCommand command = new NpgsqlCommand("call public.create_order(@p_user_id, @p_fuel_id, @p_value, @p_price, @p_station_id)", conn);
            command.CommandType = CommandType.Text;
            // создаем именованные параметры (названия как в базе), прописываем их типы
            command.Parameters.Add(new Npgsql.NpgsqlParameter("p_user_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = user.Id });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("p_fuel_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = model.FuelId });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("p_value", NpgsqlTypes.NpgsqlDbType.Real) { Value = model.Value });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("p_price", NpgsqlTypes.NpgsqlDbType.Real) { Value = model.Price });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("p_station_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = model.StationId });
            // выполняем созданную команду и получаем курсор с данными
            command.ExecuteNonQuery();

            conn.Close();

            return NoContent();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
