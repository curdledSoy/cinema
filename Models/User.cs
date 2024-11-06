using System;
using System.Collections.Generic;

namespace cinema.Models;

public class User
{
    public int UserId { get; set; } // Primary key
    public string Username { get; set; } // Unique username
    public string Email { get; set; } // User's email address
    public string PasswordHash { get; set; } // Hashed password for security
    public DateTime DateJoined { get; set; } // Date the user registered

    // Optional user details
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    // Navigation properties
    public ICollection<Ticket> Tickets { get; set; } // Tickets purchased by the user
}
