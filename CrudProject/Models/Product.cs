using System.ComponentModel.DataAnnotations;
namespace CrudProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Productname { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual Category Category { get; set; }

    }
}
