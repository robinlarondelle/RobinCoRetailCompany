using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using Caliburn.Micro;

using RRMDesktopWPF.Library.Api;
using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.ViewModels
{
	public class SalesViewModel : Screen
	{
		private BindingList<ProductModel> _products;
		public BindingList<ProductModel> Products
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


		private ProductModel _selectedProduct;
		public ProductModel SelectedProduct
		{
			get => _selectedProduct;
			set 
			{
				_selectedProduct = value;
				NotifyOfPropertyChange( () => SelectedProduct );
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

		private readonly IProductEndpoint _productEndpoint;


		public SalesViewModel( IProductEndpoint productEndpoint )
		{
			_productEndpoint = productEndpoint;

		}


		protected override async void OnViewLoaded( object view )
		{
			base.OnViewLoaded( view );
			await InitializeProducts();
		}

		private async Task InitializeProducts()
		{
			List<ProductModel> productsList = await _productEndpoint.GetAllProductsAsync();
			Products = new BindingList<ProductModel>( productsList );
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