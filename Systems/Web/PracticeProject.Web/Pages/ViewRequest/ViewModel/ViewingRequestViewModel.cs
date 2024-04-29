namespace PracticeProject.Web.ViewRequest.Models;

public class ViewingRequestViewModel
{
    public Guid RequestId { get; set; }
    public int CarId { get; set; }
    public string Model {  get; set; }
    public int Year { get; set; }
    public int SenderId { get; set; }
    public string SellerFullName { get; set; }
    public StatusConfirm StateConfirmed { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? LastModifedDate { get; set; }
}