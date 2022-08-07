using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.OrderDTO
{
    public class OrderCreateDTO
    {

        [DataType(DataType.DateTime), Required(ErrorMessage = "تاريخ التسليم مطلوب"), ChecKDate]
        public DateTime DeliverDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }



    }
}
