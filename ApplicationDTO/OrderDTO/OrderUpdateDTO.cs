using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.OrderDTO
{
    public class OrderUpdateDTO
    {
        [DataType(DataType.DateTime), Required(ErrorMessage = "تاريخ التسليم مطلوب"), ChecKDate]
        public DateTime DeliverDate { get; set; }

        [Required(ErrorMessage = "تاريخ الطلب مطلوب")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
    }
}
