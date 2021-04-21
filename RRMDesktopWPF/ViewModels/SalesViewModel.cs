using System.ComponentModel;

using Caliburn.Micro;

namespace RRMDesktopWPF.ViewModels
{
	public class SalesViewModel : Screen
	{
		private BindingList<string> _products;
		public BindingList<string> Products
		{
			get { return _products; }
			set
			{
				_products = value;
				NotifyOfPropertyChange( () => Products );
			}
		}

		private int _itemQuantity;
		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set
			{
				_itemQuantity = value;
				NotifyOfPropertyChange( () => ItemQuantity );
			}
		}

		private BindingList<string> _cart;
		public BindingList<string> Cart
		{
			get { return _cart; }
			set
			{
				_cart = value;
				NotifyOfPropertyChange( () => Cart );
			}
		}

		public string Subtotal
		{
			get
			{
				//replace with calculation
				return "0.00";
			}
		}

		public string Tax
		{
			get
			{
				//replace with calculation
				return "0.00";
			}
		}

		public string Total
		{
			get
			{
				//replace with calculation
				return "0.00";
			}
		}

		public bool CanAddToCart
		{
			get
			{
				// Make sure a product is selected
				// Make sure a quantity is given
				return true;
			}

		}

		public void AddToCart()
		{

		}

		public bool CanRemoveFromCart
		{
			get
			{
				// Make sure a product is selected
				// Make sure a quantity is given
				return true;
			}

		}

		public void RemoveFromCart()
		{

		}

		public bool CanCheckout
		{
			get
			{
				// Make sure something is in the cart
				return true;
			}

		}

		public void Checkout()
		{

		}
	}
}