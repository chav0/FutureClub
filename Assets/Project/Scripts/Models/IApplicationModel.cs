namespace Project.Scripts.Models
{
    public interface IApplicationModel
    {
        int Coins { get; set; }
        
        void Update();
    }
}