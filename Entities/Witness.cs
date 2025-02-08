using carrinho_api.Enums;

namespace carrinho_api.Entities
{
    public class Witness : BaseEntity
    {
        public int WitnessId { get; set; }
        public DateTime Date { get; set; }
        public string Publishers { get; set; }
        public WitnessType WitnessType { get; set; }
        public int LocalId { get; set; }
        public Local? Local { get; set; }

        public Witness()
        {
            
        }
    }
}
