CREATE PROCEDURE [dbo].[SPSale_SaleReport]
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT *
	FROM dbo.Sale s
	INNER JOIN dbo.[User] u ON s.CashierId = u.Id
END