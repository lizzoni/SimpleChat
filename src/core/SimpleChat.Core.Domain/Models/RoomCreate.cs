using System.ComponentModel.DataAnnotations;

namespace SimpleChat.Core.Domain.Models;

public class RoomCreate
{
    [Required(ErrorMessage = "Field {0} is required")]
    public string Name { get; set; }
}
