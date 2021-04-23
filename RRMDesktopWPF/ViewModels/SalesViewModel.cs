﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Caliburn.Micro;

using RRMDesktopWPF.Library.Api;
using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.ViewModels
{
	public class SalesViewModel : Screen
	{
		private readonly IProductEndpoint _productEndpoint;


		private BindingList<ProductModel> _products;
		public BindingList<ProductModel> Products
		{
			get => _products;
			set
			{
				_products = value;
				NotifyOfPropertyChange( () => Products );
			}
		}


		private int _itemQuantity = 0;
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


		private BindingList<CartItemModel> _cart;
		public BindingList<CartItemModel> Cart
		{
			get => _cart;
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
				NotifyOfPropertyChange( () => CanAddToCart );
			}
		}



		public string Subtotal
		{
			get
			{
				decimal subTotal = 0;

				foreach ( CartItemModel item in Cart )
				{
					subTotal += item.Product.RetailPrice * item.QuantityInCart;
				}

				return subTotal.ToString( "C" );
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


		public SalesViewModel( IProductEndpoint productEndpoint )
		{
			_productEndpoint = productEndpoint;
			Products = new BindingList<ProductModel>();
			Cart = new BindingList<CartItemModel>();
			_itemQuantity = 0;
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


		/// <summary>
		/// You can only add something to the cart if the amount the customer wants is less than the amount in stock 
		/// </summary>
		public bool CanAddToCart => ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity;


		public void AddToCart()
		{
			CartItemModel existingModel = Cart.FirstOrDefault( c => c.Product == SelectedProduct );

			if ( existingModel != null )
			{
				existingModel.QuantityInCart += ItemQuantity;
				Cart.Remove( existingModel );
				Cart.Add( existingModel );
			}
			else
			{
				Cart.Add( new CartItemModel
				{
					Product = SelectedProduct ,
					QuantityInCart = ItemQuantity
				} );
			}

			ItemQuantity = 1;
			NotifyOfPropertyChange( () => Subtotal );
			NotifyOfPropertyChange( () => Cart );
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
			NotifyOfPropertyChange( () => Subtotal );

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