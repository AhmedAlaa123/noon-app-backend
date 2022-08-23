using noone.ApplicationDTO.ProductDTO;
using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.OrderDTO
{
    public class OrderCreateDTO
    {

        //[DataType(DataType.DateTime), Required(ErrorMessage = "تاريخ التسليم مطلوب"), ChecKDate]
        //public DateTime DeliverDate { get; set; }

        //[Required(ErrorMessage ="تاريخ الطلب مطلوب")]
        //[DataType(DataType.DateTime)]
        //public string OrderDate { get; set; }
        public List<ProductOrderCreateDTO> Products { get; set; } = new List<ProductOrderCreateDTO>();
    }
}
