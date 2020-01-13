using Genesis.Core.Models;
using Genesis.Data.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Genesis.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repository;
        public ContactsController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _repository.GetAllAsync());
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            try
            {
                return Ok(await _repository.GetAsync(id));
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Contact contact)
        {
            return Ok(await _repository.CreateAsync(contact));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] Contact contact)
        {
            if (contact.Id != id)
            {
                return BadRequest();
            }

            try
            {
                var contactChanges = await _repository.UpdateAsync(id, contact);
                return Ok(contactChanges);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return Ok();
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

    }
}