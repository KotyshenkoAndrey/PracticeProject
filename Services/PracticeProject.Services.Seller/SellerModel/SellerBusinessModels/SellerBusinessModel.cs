using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.Sellers;



public class SellerBusinessModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string? PhoneNumber { get; set; }
}
