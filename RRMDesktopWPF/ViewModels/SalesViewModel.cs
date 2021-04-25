using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Caliburn.Micro;

using RRMDesktopWPF.Library.Api;
using RRMDesktopWPF.Library.Helpers;
using RRMDesktopWPF.Library.Models;
using RRMDesktopWPF.Models;

namespace RRMDesktopWPF.ViewModels
{
	public class SalesViewModel : Screen
	{
		private readonly IProductEndpoint _productEndpoint;
		private readonly ISaleEndpoint _saleEndpoint;
		private readonly IConfigHelper _configHelper;
		private readonly IMapper _mapper;
		private BindingList<ProductDisplayModel> _products;
		public BindingList<ProductDisplayModel> Products
		{
			get => _products;
			set
			{
				_products = value;
				NotifyOfPropertyChange( () => Products );
			}
		}


		private int _itemQuantity;
		public int ItemQuantity
		{
			get => _itemQuantity;
			set
			{
				_itemQuantity = value;
				NotifyOfPropertyChange( () => ItemQuantity );
				NotifyOfPropertyChange( () => CanAddToCart );
			}
		}


		private BindingList<CartItemDisplayModel> _cart;
		public BindingList<CartItemDisplayModel> Cart
		{
			get => _cart;
			set
			{
				_cart = value;
				NotifyOfPropertyChange( () => Cart );
			}
		}

		private ProductDisplayModel _selectedProduct;
		public ProductDisplayModel SelectedProduct
		{
			get => _selectedProduct;
			set
			{
				_selectedProduct = value;
				NotifyOfPropertyChange( () => SelectedProduct );
				NotifyOfPropertyChange( () => CanAddToCart );
			}
		}


		private CartItemDisplayModel _selectedCartItem;
		public CartItemDisplayModel SelectedCartItem
		{
			get { return _selectedCartItem; }
			set 
			{ 
				_selectedCartItem = value;
				NotifyOfPropertyChange( () => SelectedCartItem );
				NotifyOfPropertyChange( () => CanRemoveFromCart );
			}
		}


		public string Subtotal => CalculateSubTotal().ToString( "C" );
		public string Tax => CalculateTax().ToString( "C" );
		public string Total
		{
			get
			{
				decimal totalPrice = CalculateSubTotal() + CalculateTax();
				return totalPrice.ToString( "C" );
			}
		}


		//Constructor
		public SalesViewModel(
			IProductEndpoint productEndpoint ,
			ISaleEndpoint saleEndpoint ,
			IConfigHelper configHelper ,
			IMapper mapper )
		{
			_productEndpoint = productEndpoint;
			_saleEndpoint = saleEndpoint;
			_configHelper = configHelper;
			_mapper = mapper;

			Products = new BindingList<ProductDisplayModel>();
			Cart = new BindingList<CartItemDisplayModel>();
			_itemQuantity = 1;
		}

		protected override async void OnViewLoaded( object view )
		{
			base.OnViewLoaded( view );
			await InitializeProducts();
		}

		private async Task InitializeProducts()
		{
			List<ProductModel> productsList = await _productEndpoint.GetAllProductsAsync();
			List<ProductDisplayModel> productDisplayModelList = _mapper.Map<List<ProductDisplayModel>>( productsList );
			Products = new BindingList<ProductDisplayModel>( productDisplayModelList );
		}


		// You can only add something to the cart if the amount the customer wants is less than the amount in stock 
		public bool CanAddToCart => ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity;
		public void AddToCart()
		{
			CartItemDisplayModel existingModel = Cart.FirstOrDefault( c => c.Product == SelectedProduct );

			if ( existingModel != null )
			{
				existingModel.QuantityInCart += ItemQuantity;
			}
			else
			{
				Cart.Add( new CartItemDisplayModel
				{
					Product = SelectedProduct ,
					QuantityInCart = ItemQuantity
				} );
			}

			SelectedProduct.QuantityInStock -= ItemQuantity;
			ItemQuantity = 1;
			NotifyOfPropertyChange( () => Subtotal );
			NotifyOfPropertyChange( () => Tax );
			NotifyOfPropertyChange( () => Total );
			NotifyOfPropertyChange( () => Cart );
			NotifyOfPropertyChange( () => CanCheckout );
		}


		public bool CanRemoveFromCart => SelectedCartItem != null && SelectedCartItem?.QuantityInCart > 0;
		public void RemoveFromCart()
		{
			//remove the cart item from the cart
			//increment the Quantity In Stock of the removed product
			SelectedCartItem.Product.QuantityInStock += 1;

			if (SelectedCartItem.QuantityInCart > 1)
			{
				SelectedCartItem.QuantityInCart -= 1;
			}
			else
			{
				Cart.Remove( SelectedCartItem );
			}

			NotifyOfPropertyChange( () => Subtotal );
			NotifyOfPropertyChange( () => Tax );
			NotifyOfPropertyChange( () => Total );
			NotifyOfPropertyChange( () => CanCheckout );
			NotifyOfPropertyChange( () => CanAddToCart );

		}


		public bool CanCheckout => Cart.Count > 0;
		public async Task Checkout()
		{
			SaleModel saleModel = new SaleModel();

			foreach ( CartItemDisplayModel item in Cart )
			{
				saleModel.SaleDetails.Add( new SaleDetailModel
				{
					ProductId = item.Product.Id ,
					Quantity = item.QuantityInCart
				} );
			}

			await _saleEndpoint.PostSaleAsync( saleModel );
			await ResetSalesViewModel();
		}

		private async Task ResetSalesViewModel()
		{
			Cart = new BindingList<CartItemDisplayModel>();
			await InitializeProducts();

			NotifyOfPropertyChange( () => Subtotal );
			NotifyOfPropertyChange( () => Tax );
			NotifyOfPropertyChange( () => Total );
			NotifyOfPropertyChange( () => CanCheckout );
		}


		private decimal CalculateSubTotal()
		{
			decimal subTotal = 0;

			foreach ( CartItemDisplayModel item in Cart )
			{
				subTotal += item.Product.RetailPrice * item.QuantityInCart;
			}

			return subTotal;
		}

		private decimal CalculateTax()
		{
			decimal taxAmount = 0;
			decimal taxRate = _configHelper.GetTaxRate();

			taxAmount = Cart
						.Where( c => c.Product.IsTaxable )
						.Sum( c => c.Product.RetailPrice * c.QuantityInCart * taxRate );

			return taxAmount;
		}
	}
}