namespace Application.DTOs;

public class ReviewRequestDto
{
    public int RequestId { get; set; }
    public bool IsApproved { get; set; }
    public string? Reason { get; set; }
}