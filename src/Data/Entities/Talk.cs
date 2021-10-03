using System.ComponentModel.DataAnnotations;

namespace PureWebApiCore.Data
{
  public class Talk
  {
    public int TalkId { get; set; }
    public Camp Camp { get; set; }
    public string Title { get; set; }

    //[Required]
    //[MinLength(50)]
    public string Abstract { get; set; }
    public int Level { get; set; }
    public Speaker Speaker { get; set; }
  }
}