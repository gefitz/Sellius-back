namespace Sellius.API.Application.DTOs.TablesDTOs;

public class GroupCustomerTableReturn
{
    public long Id { get; set; }
    public string Name { get; set; }
    public short Active { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime AlteredDate { get; set; }
}
