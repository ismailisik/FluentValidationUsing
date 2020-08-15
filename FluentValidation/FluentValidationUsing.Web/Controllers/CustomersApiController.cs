using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidationUsing.Web.DataAccess.EntityFramework.Context;
using FluentValidationUsing.Web.Entities;
using FluentValidation;

namespace FluentValidationUsing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly TestDbContext _context;
        private readonly IValidator<Customer> _customerValidator;
        public CustomersApiController(TestDbContext context, IValidator<Customer> customerValidator)
        {
            _context = context;
            _customerValidator = customerValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        //Not: Ben eğer bir bir apiden FluentValidation kullanarak custom bir hata mesajı döndürmek ister isem. Aşğıya bakınız... 

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {

            var result = _customerValidator.Validate(customer);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(x => new { property = x.PropertyName, error = x.ErrorMessage }));
            }

            //Bu şekilde custom hata mesajı verilmek istendiğinde çıktı örnek olarak aşağıdaki gibi olcaktır..

            //[
            //    {
            //        "property": "Name",
            //        "error": "Name alanı zorunludur."
            //    },
            //    {
            //        "property": "LastName",
            //        "error": "Last Name alanı zorunludur."
            //    },
            //    {
            //        "property": "BirthDate",
            //        "error": "Birth Date alanı zorunludur."
            //    },
            //    {
            //        "property": "BirthDate",
            //        "error": "Yaşınız 18 yada ondan büyük olmalıdır."
            //    }
            //]

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
