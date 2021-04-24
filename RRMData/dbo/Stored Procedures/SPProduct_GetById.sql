CREATE PROCEDURE [dbo].[SPProduct_GetById]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SET NOCOUNT ON;
	SELECT Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable
	FROM [dbo].[Product]
	WHERE Id = @Id
	ORDER BY ProductName

END
