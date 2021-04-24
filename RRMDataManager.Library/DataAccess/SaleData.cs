using System.Collections.Generic;
using System.Linq;

using RRMDataManager.Library.Internal.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Library.DataAccess
{
	public class SaleData
	{
		public void SaveSale( SaleModel sale , string cashierId )
		{
			List<DbSaleDetailModel> saleDetailsList = new List<DbSaleDetailModel>();
			ProductData productData = new ProductData();
			SqlDataAccess sql = new SqlDataAccess();

			decimal taxRate = ConfigHelper.GetTaxRate();

			//Construct the Sale metadata
			foreach ( SaleDetailModel item in sale.SaleDetails )
			{
				DbSaleDetailModel detailToAdd = new DbSaleDetailModel
				{
					ProductId = item.ProductId ,
					Quantity = item.Quantity
				};

				ProductModel fetchedProduct = productData.GetProductById( detailToAdd.ProductId );

				if ( fetchedProduct == null )
				{
					throw new System.Exception( $"The product of ID {detailToAdd.ProductId} could not be found in the database" );
				}

				detailToAdd.PurchasePrice = fetchedProduct.RetailPrice * detailToAdd.Quantity;

				if ( fetchedProduct.IsTaxable )
				{
					detailToAdd.Tax = detailToAdd.PurchasePrice * taxRate;
				}

				saleDetailsList.Add( detailToAdd );
			}

			//Create model to save to the database and calculate the Subtotal, Tax and Total price.
			DbSaleModel saleToSave = new DbSaleModel
			{
				SubTotal = saleDetailsList.Sum( x => x.PurchasePrice ) ,
				Tax = saleDetailsList.Sum( x => x.Tax ) ,
				CashierId = cashierId
			};

			saleToSave.Total = saleToSave.SubTotal + saleToSave.Tax;

			//Save the sale to the database
			//First save the parent Sale Model
			sql.SaveData( "dbo.SPSale_Insert" , saleToSave , "RRMData" );

			//Next:
			//	- set the SaleID of each SaleDetails to the saved Sale
			//	- save the SaleDetail
			saleToSave.Id =  sql.LoadData<int, dynamic>( "SPSale_LookupID" , new { saleToSave.CashierId , saleToSave.SaleDate } , "RRMData" ).FirstOrDefault();

			saleDetailsList.ForEach( saleDetail =>
			 {
				 saleDetail.SaleId = saleToSave.Id;
				 sql.SaveData( "dbo.SPSaleDetail_Inset" , saleDetail , "RRMData" );
			 } );
		}
	}
}
