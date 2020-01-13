using Genesis.Core.Models;
using Genesis.Data.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Genesis.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _repository;

        public CompaniesController(ICompanyRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}", Name = "Get")]
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
        public async Task<ActionResult> PostAsync([FromBody] Company company)
        {
            return Ok(await _repository.CreateAsync(company));

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] Company company)
        {

            if (company.Id != id)
            {
                return BadRequest();
            }

            try
            {
                var companyChanges = await _repository.UpdateAsync(id, company);
                return Ok(companyChanges);
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

        [HttpGet("{id}/Contacts")]
        public async Task<ActionResult> GetContactsAsync(Guid id)
        {
            return Ok(await _repository.GetContactsAsync(id));
        }

        [HttpPost("{id}/Contacts/{contactId}")]
        public async Task<ActionResult> AddContactAsync(Guid id, Guid contactId)
        {
            try
            {
                return Ok(await _repository.AddContactAsync(id, contactId));
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/Contacts/{contactId}")]
        public async Task<ActionResult> RemoveContactAsync(Guid id, Guid contactId)
        {
            try
            {
                await _repository.RemoveContactAsync(id, contactId);
                return Ok();
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }
    }
}
