using BestDigiSellerApp.Core.Data;

namespace BestDigiSellerApp.Wallet.Entity
{
    public class Wallet : IEntity
    {
        public Wallet()
        {
            WalletDetails = new List<WalletDetail>();
        }
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public List<WalletDetail> WalletDetails { get; set; }
        public DateTime CreateDate => DateTime.UtcNow;

    }
}
