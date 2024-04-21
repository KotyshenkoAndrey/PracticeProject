using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.Cars.Models;



public class CarBusinessModel
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public string SellerFullName { get; set; }
    public string Model { get; set; }
    public int? Year { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public DateTime DatePosted { get; set; }
    public virtual ICollection<string>? ViewingRequestsCar { get; set; }
}
