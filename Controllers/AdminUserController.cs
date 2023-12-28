using System;
using Microsoft.AspNetCore.Mvc;
using TicketReservation.Models;
using TicketReservation.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketReservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly AdminUserService _adminUserService;

        public AdminController(AdminUserService adminService)
        {
            _adminUserService = adminService;
        }

        [HttpGet]
        public async Task<List<Admin>> Get()
        {
            // Retrieve a list of admins
            return await _adminUserService.GetUsersAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Admin admin)
        {
            // Create a new admin
            await _adminUserService.CreateAsync(admin);
            return CreatedAtAction(nameof(Get), new { id = admin.Id }, admin);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Admin admin)
        {
            // Login admin based on email and password
            var user1 = await _adminUserService.LoginAsync(admin.Email, admin.Password);
            if (user1 == null)
            {
                // Admin not found
                return NotFound();
            }
            return Ok(user1);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] Admin admin)
        {
            // Update admin information
            await _adminUserService.UpdateUserAsync(id, admin);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // Delete an admin by ID
            await _adminUserService.DeleteAsync(id);
            return NoContent();
        }
    }
}
