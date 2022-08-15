using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApplicationDevelopment.Models
{
    public class OrderDetail
    {
		public int Id { get; set; }
		[Required]
		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public Order Order { get; set; }
		[Required]
		[ForeignKey("Book")]
		public int BookId { get; set; }
		public Book Book { get; set; }
		public int Quantity { get; set; }
		public int Price { get; set; }
	}
}
