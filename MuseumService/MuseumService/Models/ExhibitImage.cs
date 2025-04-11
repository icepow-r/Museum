namespace MuseumService.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ExhibitImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImageId { get; set; }
    
    [ForeignKey("Exhibit")]
    public int ExhibitId { get; set; }
    
    public string ImagePath { get; set; }
    public string AltText { get; set; }
    public int DisplayOrder { get; set; }
    
    public Exhibit Exhibit { get; set; }
}