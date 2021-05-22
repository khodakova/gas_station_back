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

namespace gas_station.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListsController : ControllerBase
    {
        private readonly DBContext _context;

        public PriceListsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/PriceLists
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PriceList>>> GetPriceLists()
        //{
        //    return await _context.PriceLists.ToListAsync();
        //}

        // создаем объект на основе DeliveryItem для передачи клиенту
        public class pricelist : PriceList
        {
            public string FuelName { get; set; }

        }

        [HttpGet]
        public async Task<ActionResult<List<Object>>> GetPriceLists()
        {
            List<object> data = new();
            // прописываем соединение с базой
            NpgsqlConnection conn = new("Server=127.0.0.1;Port=5432;Database=gas_station;User Id=postgres;Password=123;");
            conn.Open();
            // создаем транзакцию, чтобы все команды были в одной сессии
            //using (NpgsqlTransaction tran = conn.BeginTransaction())
            NpgsqlTransaction tran = conn.BeginTransaction();
                
            //создаем новую команду для получения данных из базы (прописываем параметры и синтаксис)
            NpgsqlCommand command = new NpgsqlCommand("call public.get_pricelist(@p_cur)", conn);
            command.CommandType = CommandType.Text;
            // создаем именованные параметры (названия как в базе), прописываем их типы
            NpgsqlParameter p = new NpgsqlParameter();
            p.ParameterName = "@p_cur";
            p.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Refcursor;
            p.Direction = ParameterDirection.InputOutput;
            p.Value = "p_cur";
            command.Parameters.Add(p);
            // выполняем созданную команду и получаем курсор с данными
            command.ExecuteNonQuery();

            // парсим курсор с данными, полученный на предыдущем этапе
            command.CommandText = "fetch all in p_cur";
            command.CommandType = CommandType.Text;

            // для чтения данных создаем объект DataReader и сразу наполняем его данными
            NpgsqlDataReader dr = command.ExecuteReader();
            // заполняем список возвращаемых с сервера данных, идем по каждой записи курсора
            if (dr.HasRows)
            {
                while(dr.Read())
                {
                    Console.WriteLine(dr[0]);
                    pricelist obj = new pricelist();
                    obj.Id = Convert.ToInt32(dr.GetValue(0));
                    obj.BeginDate = Convert.ToDateTime(dr.GetValue(1));
                    if (!(dr.GetValue(2) is DBNull))
                    {
                        obj.EndDate = Convert.ToDateTime(dr.GetValue(2));
                    }
                    obj.FuelId = Convert.ToInt32(dr.GetValue(3));
                    obj.FuelName = Convert.ToString(dr.GetValue(4));
                    obj.Price = (float)dr.GetValue(5);
                    obj.Status = Convert.ToBoolean(dr.GetValue(6));
                    if (!(dr.GetValue(7) is DBNull))
                    {
                        obj.Note = Convert.ToString(dr.GetValue(7));
                    }
                        
                    data.Add(obj);
                }
            }
            dr.Close();
            tran.Commit();
            conn.Close();
            
            
            return data;
        }


        // GET: api/PriceLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceList>> GetPriceList(int id)
        {
            var priceList = await _context.PriceLists.FindAsync(id);

            if (priceList == null)
            {
                return NotFound();
            }

            return priceList;
        }

        // PUT: api/PriceLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriceList(int id, PriceList priceList)
        {
            if (id != priceList.Id)
            {
                return BadRequest();
            }

            _context.Entry(priceList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceListExists(id))
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

        // POST: api/PriceLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<PriceList>> PostPriceList(PriceList priceList)
        //{
        //    _context.PriceLists.Add(priceList);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPriceList", new { id = priceList.Id }, priceList);
        //}


        // создаем объект на основе DeliveryItem для передачи клиенту
        //public class pricelist
        //{
        //    public int FuelId { get; set; }
        //    public DateTime BeginDate { get; set; }
        //    public Double Price { get; set; }
        //    public string Note { get; set; }
        //}

        // от клиента получаем id топлива, дату начала действия новой цены, новую цену и комментарий
        // хранимка ищет в базе уже существующую запись по данному топливу (если она есть) и обновляет ее статус и дату окончания (дата начала действия новой цены минус день)
        // дальше вставляется новая запись по полученному виду топлива
        [HttpPost]
        public async Task<IActionResult> PostPriceList(PriceList item)
        {
            // прописываем соединение с базой
            NpgsqlConnection conn = new("Server=127.0.0.1;Port=5432;Database=gas_station;User Id=postgres;Password=123;");
            conn.Open();

            //создаем новую команду для получения данных из базы (прописываем параметры и синтаксис)
            NpgsqlCommand command = new NpgsqlCommand("call public.create_pricelist_item(@p_fuel_id, @p_begin_date, @p_price, @p_note)", conn);
            command.CommandType = CommandType.Text;
            // создаем именованные параметры (названия как в базе), прописываем их типы
            command.Parameters.Add(new NpgsqlParameter("@p_fuel_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = item.FuelId });
            command.Parameters.Add(new NpgsqlParameter("@p_begin_date", NpgsqlTypes.NpgsqlDbType.Date) { Value = item.BeginDate });
            command.Parameters.Add(new NpgsqlParameter("@p_price", NpgsqlTypes.NpgsqlDbType.Real) { Value = item.Price });
            command.Parameters.Add(new NpgsqlParameter("@p_note", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = "note" });
            // выполняем созданную команду и получаем курсор с данными
            command.ExecuteNonQuery();

            conn.Close();

            return NoContent();
        }

        // DELETE: api/PriceLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceList(int id)
        {
            var priceList = await _context.PriceLists.FindAsync(id);
            if (priceList == null)
            {
                return NotFound();
            }

            _context.PriceLists.Remove(priceList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceListExists(int id)
        {
            return _context.PriceLists.Any(e => e.Id == id);
        }
    }
}
