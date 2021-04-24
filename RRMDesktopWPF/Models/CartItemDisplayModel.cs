
using Caliburn.Micro;

namespace RRMDesktopWPF.Models
{
	public class CartItemDisplayModel : PropertyChangedBase
	{

		public ProductDisplayModel Product { get; set; }


		private int _quantityInCart;
		public int QuantityInCart
		{
			get => _quantityInCart;
			set
			{
				_quantityInCart = value;
				NotifyOfPropertyChange( () => QuantityInCart );
				NotifyOfPropertyChange( () => DisplayText );
			}
		}


		public string DisplayText { get => $"{Product.ProductName} ({QuantityInCart})"; }
	}
}
