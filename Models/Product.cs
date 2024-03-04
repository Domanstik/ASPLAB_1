namespace ASPLAB_1.Models
{
    public class Product
    {
        static int Id;
        private int ID;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type {  get; set; } = string.Empty;
        public int Prise { get; set; }
        public int getID()
        {
            return ID;
        }

        public Product(string name, string description, string type, int prise) 
        {
            ID = ++Id;
            Name = name;
            Description = description;
            Type = type;
            Prise = prise;
        }
    }
}
