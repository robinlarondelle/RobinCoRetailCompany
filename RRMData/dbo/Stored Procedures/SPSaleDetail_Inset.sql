﻿CREATE PROCEDURE [dbo].[SPSaleDetail_Inset]
	@SaleId int,
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@Tax money
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.SaleDetail(SaleId, ProductId, Quantity, PurchasePrice, Tax)
	VALUES (@SaleId, @ProductId, @Quantity, @PurchasePrice, @Tax)

END