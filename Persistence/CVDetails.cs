namespace Persistence;

public class CVDetails
{
    public int? DetailsID{get; set;}
    public int CVID{get; set;}
    public string Title{get; set;}
    public string JobPosition{get; set;}
    public DateTime? FromDate{get; set;}
    public DateTime? ToDate{get; set;}
    public string? Association{get; set;}
    public string? Description{get; set;}

    public CVDetails()
    {
        this.Title = "";
        this.JobPosition = "";
    }

    public CVDetails(string _Title, string _JobPosition, DateTime? _FromDate, DateTime? _ToDate, string? _Association, string? _Description)
    {
        this.Title = _Title;
        this.JobPosition = _JobPosition;
        this.FromDate = _FromDate;
        this.ToDate = _ToDate;
        this.Association = _Association;
        this.Description = _Description;
    }
}